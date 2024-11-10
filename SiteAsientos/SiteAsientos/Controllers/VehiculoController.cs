using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteAsientos.Models;

namespace SiteAsientos.Controllers
{
    [Authorize]
    public class VehiculoController : Controller
    {
        private readonly CubreasientosContext _context;
        public VehiculoController(CubreasientosContext context)
        {
            _context = context;
        }
        // GET: Vehiculo 
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vehicles.ToListAsync());
        }

        // GET: Vehiculo/Details/5
        public async Task<IActionResult> Details(int EmployeeId, int id)
        {
            if (EmployeeId == 0 || EmployeeId == null) { 
                return BadRequest(); 
            }

            var vehiculo = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.Vehicle_Id == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
             
        }

        // GET: Vehiculo/Create
        public ActionResult Create(int EmployeeId)
        {
            if (EmployeeId == 0 || EmployeeId == null)
            {
                return BadRequest();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Vehicle_Id,Vehicle_Brand,Vehicle_Model,Vehicle_ModelYear")] Vehicle vehicle, int EmployeeId)
        { 
            if (EmployeeId == 0 || EmployeeId == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        //Verifica la existencia del nombre del material
         
        private bool Exists(Vehicle vehicle)
        {
           return _context.Vehicles.Where(x => x.Vehicle_Id == vehicle.Vehicle_Id).Any();
        }


        // POST: Vehiculo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, int EmployeeId)
        {
            if (EmployeeId == 0 || EmployeeId == null)
            {
                return BadRequest();
            }
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Vehiculo/Edit/5
        public async Task<IActionResult> Edit(int? id, int EmployeeId)
        {
            if (EmployeeId == 0 || EmployeeId == null)
            {
                return BadRequest();
            }
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }
        // POST: Vehiculo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Vehicle_Id,Vehicle_Brand,Vehicle_Model,Vehicle_ModelYear")] Vehicle vehicle)
        {
            if (id != vehicle.Vehicle_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (Exists(vehicle))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }


        // POST: Vehiculo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection, int EmployeeId)
        {
            if (EmployeeId == 0 || EmployeeId == null)
            {
                return BadRequest();
            }
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Vehicle_Id,Vehicle_Brand,Vehicle_Model,Vehicle_ModelYear")] Vehicle vehicle, int EmployeeId)
        {
            if (EmployeeId == 0 || EmployeeId == null)
            {
                return BadRequest();
            }
            if (id != vehicle.Vehicle_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Exists(vehicle.Vehicle_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicle/Delete/5
        public async Task<IActionResult> Delete(int? id, int EmployeeId)
        {
            if (EmployeeId == 0 || EmployeeId == null)
            {
                return BadRequest();
            }
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.Vehicle_Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }



        // POST: Vehicle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int EmployeeId)
        {
            if (EmployeeId == 0 || EmployeeId == null)
            {
                return BadRequest();
            }
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }       
    }
}
