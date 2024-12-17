using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteAsientos.Models;

namespace SiteAsientos.Controllers
{
    public class MaterialController : Controller
    {
        private readonly CubreasientosContext _context;

        public MaterialController(CubreasientosContext context)
        {
            _context = context;
        }

        // GET: Materials
        public async Task<IActionResult> Index(string search, int page = 1, int pageSize = 10)
        {
            var query = _context.Material
                .Include(m => m.Supplier)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(m =>
                    (m.Material_Name != null && m.Material_Name.Contains(search)) ||
                    (m.Supplier != null && m.Supplier.Supplier_Name != null && m.Supplier.Supplier_Name.Contains(search))
                );
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var materials = await query
                .OrderBy(m => m.Material_Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentSearch = search;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalCount = totalCount;

            return View(materials);
        }
    

        // GET: Material/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = await _context.Material
                .Include(m => m.Supplier)
                .FirstOrDefaultAsync(m => m.Material_Id == id);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // GET: Material/Create
        public async Task<IActionResult> Create()
        {
            // Carga de proveedores activos (suponiendo que existan y tengan un campo Supplier_Status)
            ViewData["SupplierId"] = new SelectList(_context.Supplier.Where(x => x.Supplier_Status != false), "Supplier_Id", "Supplier_Name");
            return View();
        }

        // POST: Material/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< Updated upstream
        // POST: Material/Create 
        public async Task<IActionResult> Create(Material model)
=======
        public async Task<IActionResult> Create([Bind("Material_Id,Material_Name,Material_Status,Material_SupplierId")] Material material)
>>>>>>> Stashed changes
        {
            if (ModelState.IsValid)
            {
                _context.Add(material);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(material);
        }

        // GET: Materials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var material = await _context.Material.FindAsync(id);
            if (material == null) return NotFound();

            // Cargar proveedores
            ViewData["SupplierId"] = new SelectList(_context.Supplier.Where(x => x.Supplier_Status != false), "Supplier_Id", "Supplier_Name");
            return View(material);
        }

        // POST: Material/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Materials/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Material_Id,Material_Name,Material_Status,Material_SupplierId")] Material material)
        {
            if (id != material.Material_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(material);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialExists(material.Material_Id).Result)
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
            return View(material);
        }

        private async Task<bool> MaterialExists(int id)
        {
            return await _context.Material.AnyAsync(e => e.Material_Id == id);
        }
        // GET: Materials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var material = await _context.Material
                .Include(m => m.Supplier)
                .FirstOrDefaultAsync(m => m.Material_Id == id);

            if (material == null) return NotFound();

            // No hace falta cargar la lista de diseños aquí, sólo mostramos datos del material
            return View(material);
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var material = await _context.Material
                .Include(m => m.Designs)
                .FirstOrDefaultAsync(m => m.Material_Id == id);

            if (material == null) return NotFound();

            // Verificar si hay diseños asociados
            if (material.Designs != null && material.Designs.Any())
            {
                // No se puede borrar el material, ya que hay diseños asociados.
                TempData["ErrorMessage"] = "No se puede eliminar este material porque existen diseños asociados.";
                return RedirectToAction("Index");
            }

            _context.Material.Remove(material);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "El material se ha eliminado correctamente.";
            return RedirectToAction("Index");
        }

        //Verifica la existencia del nombre del material
        [AllowAnonymous]
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> MaterialExists(Material material)
        {
            var existingMaterial = _context.Material.Where(x => x.Material_Name == material.Material_Name && x.Material_Id != material.Material_Id);
            if (existingMaterial.Any())
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }





    }
}
