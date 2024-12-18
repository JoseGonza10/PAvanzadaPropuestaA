using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteAsientos.Models;

namespace SiteAsientos.Controllers
{
    public class OrdersController : Controller
    {
        private readonly CubreasientosContext _context;

        public OrdersController(CubreasientosContext context)
        {
            _context = context;
        }

        // GET: Orders/Create?designId=XX
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var design = await _context.Design
                .Include(d => d.Material)
                .FirstOrDefaultAsync(d => d.Design_Id == id);

            if (design == null) return NotFound();

            // Cargar materiales para el dropdown
            ViewBag.Materials = await _context.Material
                .Where(m => m.Material_Status)
                .OrderBy(m => m.Material_Name)
                .ToListAsync();

            // Datos fijos (pueden venir de una fuente estática o configuraciones)
            ViewBag.CarBrands = new List<string> { "Toyota", "Honda", "Ford", "Chevrolet", "Nissan" };
            // Nota: en un caso real, podrías hacer que el modelo se filtre según la marca seleccionada vía AJAX
            //ViewBag.CarModels = new Dictionary<string, List<string>>
            //{
            //    { "Toyota", new List<string>{"Yaris", "Corolla", "Hilux"} },
            //    { "Honda", new List<string>{"Civic", "Accord", "CR-V"} },
            //    { "Ford", new List<string>{"Focus", "Fiesta", "Ranger"} },
            //    { "Chevrolet", new List<string>{"Cruze", "Spark", "Silverado"} },
            //    { "Nissan", new List<string>{"Versa", "Sentra", "Frontier"} }
            //};

            ViewBag.CarModels = new List<string> { "Yaris", "Corolla", "Spark", "Ranger", "Frontier" };

            // Creamos una nueva orden en memoria
            var order = new Order
            {
                Order_DesignId = id,
                Order_Code = Guid.NewGuid().ToString("N").Substring(0, 32), // GUID de 32 dígitos sin guiones
                Order_Date = DateTime.Now,
                Order_Status = true
            };

            // Pasar el precio base a la vista a través de ViewBag
            ViewBag.BasePrice = design.Design_Price;

            return View(order);
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order model, float MontoAbonado, string CarBrand, string CarModel, string ServiceType, DateTime AppointmentDate, TimeSpan AppointmentTime)
        {
            // Volvemos a cargar materiales y listas fijas
            ViewBag.Materials = await _context.Material
                .Where(m => m.Material_Status)
                .OrderBy(m => m.Material_Name)
                .ToListAsync();

            ViewBag.CarBrands = new List<string> { "Toyota", "Honda", "Ford", "Chevrolet", "Nissan" };
            ViewBag.CarModels = new List<string> { "Yaris", "Corolla", "Spark", "Ranger", "Frontier" };

            // Obtener el diseño para calcular el precio
            var design = await _context.Design.FindAsync(model.Order_DesignId);
            if (design == null)
            {
                ModelState.AddModelError("", "El diseño seleccionado no existe.");
                return View(model);
            }

            // Asignar marca, modelo y año al pedido
            model.Order_CarBrand = CarBrand;
            model.Order_CarModel = CarModel;

            // Tipo de servicio y cálculo de precio
            float basePrice = design.Design_Price;
            float finalPrice = basePrice;
            switch (ServiceType)
            {
                case "Solo asientos delanteros":
                    finalPrice = basePrice * 0.65f;
                    break;
                case "Solo asientos traseros":
                    finalPrice = basePrice * 0.45f;
                    break;
                case "Full extras":
                    finalPrice = basePrice * 1.35f;
                    break;
                    // "Completo" se queda con el precio base
            }

            // Asignar la fecha y hora de cita a la orden
            var appointmentDateTime = AppointmentDate.Date + AppointmentTime;

            // Verificar si existe otra orden con la misma fecha y hora
            bool sameDateTimeExists = await _context.Orders
                .AnyAsync(o => o.Order_Date.Date == appointmentDateTime.Date
                            && o.Order_Date.Hour == appointmentDateTime.Hour
                            && o.Order_Date.Minute == appointmentDateTime.Minute);

            // Nota: Aquí asumo que la cita se almacena en Order_Date o en otro campo adicional 
            // (Se podría crear Order_AppointmentDateTime en el modelo si es requerido)
            // Para este ejemplo, utilizo Order_Date como la fecha/hora de la cita.
            // Ajusta a tus necesidades (puedes agregar un campo Order_AppointmentDateTime en el modelo).
            model.Order_Date = appointmentDateTime;

            if (sameDateTimeExists)
            {
                ModelState.AddModelError("Order_Date", "Ya existe una orden en esta fecha y hora. Por favor seleccione otra.");
            }

            // Validación del monto abonado (≥50% o igual total)
            if (MontoAbonado < finalPrice * 0.5f && MontoAbonado != finalPrice)
            {
                ModelState.AddModelError("MontoAbonado", $"El monto abonado debe ser al menos el 50% del total ({finalPrice * 0.5f:C}) o el monto total ({finalPrice:C}).");
            }

            //if (!ModelState.IsValid)
            //{
            //    // Si hay errores, devolver la vista
            //    ViewBag.BasePrice = basePrice;
            //    return View(model);
            //}

            // Datos de la orden
            model.Order_Code = string.IsNullOrWhiteSpace(model.Order_Code) ? Guid.NewGuid().ToString("N").Substring(0, 32) : model.Order_Code;
            model.Order_Status = true;

            // Crear la factura (Invoice)
            var invoice = new Invoice
            {
                Invoice_Code = Guid.NewGuid().ToString("N").Substring(0, 32),
                Invoice_OrderId = model.Order_Id, // Aún no se ha guardado la orden, se hará después
                Invoice_Total = finalPrice,
                Invoice_AmoundPaid = MontoAbonado,
                Invoice_Date = DateTime.Now,
                Invoice_CustomerName = model.Order_CustomerName,
                Invoice_CustomerPhone = model.Order_CustomerPhone,
                Invoice_CustomerEmail = model.Order_CustomerEmail
            };

            // Guardar la orden y la factura
            // Primero guardamos la orden para obtener su ID
            _context.Orders.Add(model);
            await _context.SaveChangesAsync();

            // Ahora que la orden tiene ID, asignamos Invoice_OrderId
            invoice.Invoice_OrderId = model.Order_Id;
            _context.Invoice.Add(invoice);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "La orden ha sido creada correctamente.";
            return RedirectToAction("Index", "Home");
        }
    }
}