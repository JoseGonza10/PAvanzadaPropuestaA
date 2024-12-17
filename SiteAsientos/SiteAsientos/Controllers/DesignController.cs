using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteAsientos.Models;

namespace SiteAsientos.Controllers
{
    public class DesignController : Controller
    {
        private readonly CubreasientosContext _context;

        public DesignController(CubreasientosContext context)
        {
            _context = context;
        }

        // GET: Design
        //public async Task<IActionResult> Index()
        //{
        //    var cubreasientosContext = _context.Design.Include(d => d.Material);
        //    return View(await cubreasientosContext.ToListAsync());
        //}

        // GET: Design/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var design = await _context.Design
                .Include(d => d.Material)
                .FirstOrDefaultAsync(m => m.Design_Id == id);
            if (design == null)
            {
                return NotFound();
            }

            return View(design);
        }

         
        // GET: Designs/Create
        public async Task<IActionResult> Create()
        {
            // Cargamos la lista de materiales activos para el dropdown
            ViewData["MaterialId"] = new SelectList(_context.Material.Where(x => x.Material_Status != false), "Material_Id", "Material_Name");
            return View();
        }

        // POST: Design/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
          
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Design model, IFormFile UploadedImage)
        {
            // Validamos el modelo
            if (!ModelState.IsValid)
            {
                // Volvemos a cargar los materiales en caso de error
                ViewData["MaterialId"] = new SelectList(_context.Material.Where(x => x.Material_Status != false), "Material_Id", "Material_Name");
                return View(model);
            }

            // Validación de la imagen
            if (UploadedImage != null && UploadedImage.Length > 0)
            {
                // Verificar el tamaño (máx 1MB)
                if (UploadedImage.Length > 1024 * 1024)
                {
                    ModelState.AddModelError("UploadedImage", "La imagen no puede pesar más de 1MB.");
                }

                // Verificar el formato
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(UploadedImage.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("UploadedImage", "El formato de la imagen debe ser JPG, JPEG o PNG.");
                }

                if (!ModelState.IsValid)
                {
                    ViewData["MaterialId"] = new SelectList(_context.Material.Where(x => x.Material_Status != false), "Material_Id", "Material_Name");
                }
            }

            //  creamos el diseño
            _context.Design.Add(model);
            await _context.SaveChangesAsync();

            // Si hay imagen, la guardamos
            if (UploadedImage != null && UploadedImage.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await UploadedImage.CopyToAsync(ms);
                    var imageData = ms.ToArray();

                    var imageEntity = new Image
                    {
                        Image_DesignId = model.Design_Id,
                        Image_Content = imageData
                    };
                    _context.Images.Add(imageEntity);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }
         
        // GET: Designs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var design = await _context.Design
                .Include(d => d.Images)
                .Include(d => d.Material)
                .FirstOrDefaultAsync(d => d.Design_Id == id);

            if (design == null) return NotFound();

            // Cargar materiales activos
            ViewData["MaterialId"] = new SelectList(_context.Material.Where(x => x.Material_Status != false), "Material_Id", "Material_Name");

            return View(design);
        }
        


        // POST: Design/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Designs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Design model, IFormFile UploadedImage)
        {
            if (id != model.Design_Id) return NotFound();

            // Obtenemos el diseño original incluyendo las imágenes
            var originalDesign = await _context.Design
                .Include(d => d.Images)
                .FirstOrDefaultAsync(d => d.Design_Id == id);

            if (originalDesign == null) return NotFound();

            // Validación adicional de la imagen
            if (UploadedImage != null && UploadedImage.Length > 0)
            {
                // Verificar el tamaño (máx 1MB)
                if (UploadedImage.Length > 1024 * 1024)
                {
                    ModelState.AddModelError("UploadedImage", "La imagen no puede pesar más de 1MB.");
                }

                // Verificar el formato
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(UploadedImage.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("UploadedImage", "El formato de la imagen debe ser JPG, JPEG o PNG.");
                }
            }

            if (!ModelState.IsValid)
            {
                // Volvemos a cargar los materiales en caso de error
                ViewData["MaterialId"] = new SelectList(_context.Material.Where(x => x.Material_Status != false), "Material_Id", "Material_Name");

                return View(model);
            }

            // Actualizamos las propiedades del diseño original con los nuevos valores
            originalDesign.Design_Description = model.Design_Description;
            originalDesign.Design_MaterialId = model.Design_MaterialId;
            originalDesign.Design_Color = model.Design_Color;
            originalDesign.Design_Price = model.Design_Price;
            originalDesign.Design_Taxable = model.Design_Taxable;
            originalDesign.Design_Status = model.Design_Status;
            originalDesign.Design_Service = model.Design_Service;

            // Si se subió una nueva imagen, la reemplazamos
            if (UploadedImage != null && UploadedImage.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await UploadedImage.CopyToAsync(ms);
                    var imageData = ms.ToArray();

                    // Si ya existe una imagen, la actualizamos
                    if (originalDesign.Images != null && originalDesign.Images.Any())
                    {
                        var existingImage = originalDesign.Images.First();
                        existingImage.Image_Content = imageData;
                        _context.Images.Update(existingImage);
                    }
                    else
                    {
                        // Si no hay imagen, creamos una nueva
                        var imageEntity = new Image
                        {
                            Image_DesignId = originalDesign.Design_Id,
                            Image_Content = imageData
                        };
                        _context.Images.Add(imageEntity);
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DesignExists(originalDesign.Design_Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Index");
        }

        private bool DesignExists(int id)
        {
            return _context.Design.Any(e => e.Design_Id == id);
        }
     

        // GET: Design/Delete/5 

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var design = await _context.Design
                .Include(d => d.Material)
                .FirstOrDefaultAsync(d => d.Design_Id == id);

            if (design == null) return NotFound();

            if (design == null)
            {
                return NotFound();
            }
            return View(design);
        }
        // POST: Design/Delete/5
        [HttpPost, ActionName("Delete")] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var design = await _context.Design
                .Include(d => d.Images)
                .FirstOrDefaultAsync(d => d.Design_Id == id);

            if (design == null) return NotFound();

            // Eliminar las imágenes asociadas (si existen)
            if (design.Images != null && design.Images.Any())
            {
                _context.Images.RemoveRange(design.Images);
            }

            _context.Design.Remove(design);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        // GET: Designs
        public async Task<IActionResult> Index(string search, int page = 1, int pageSize = 10)
        {
            // Construimos la consulta base
            var query = _context.Design
                .Include(d => d.Material)
                .AsQueryable();

            // Filtro por búsqueda
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(d =>
                    (d.Design_Description != null && d.Design_Description.Contains(search)) ||
                    (d.Material != null && d.Material.Material_Name != null && d.Material.Material_Name.Contains(search))
                );
            }

            // Contamos la cantidad total para la paginación
            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // Paginación
            var designs = await query
                .OrderBy(d => d.Design_Id) // ordenar por ID 
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentSearch = search;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalCount = totalCount;

            return View(designs);
        }
    }
}
