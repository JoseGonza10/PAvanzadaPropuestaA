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
            ViewBag.Suppliers = await _context.Supplier
                .Where(s => s.Supplier_Status == true)
                .OrderBy(s => s.Supplier_Name)
                .ToListAsync();

            return View();
        }

        // POST: Material/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: Material/Create 
        public async Task<IActionResult> Create(Material model)
        {
            // Cargar la lista de proveedores en caso de error
            ViewBag.Suppliers = await _context.Supplier
                .Where(s => s.Supplier_Status == true)
                .OrderBy(s => s.Supplier_Name)
                .ToListAsync();

            // Validaciones manuales adicionales (si se requiere)
            if (model.Material_SupplierId == null)
            {
                ModelState.AddModelError("Material_SupplierId", "Debe seleccionar un proveedor.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Verificamos la unicidad del nombre del material por seguridad (además del [Remote])
            bool exists = await _context.Material.AnyAsync(m => m.Material_Name == model.Material_Name && m.Material_Id != model.Material_Id);
            if (exists)
            {
                ModelState.AddModelError("Material_Name", "Este material ya existe en el sistema.");
                return View(model);
            }

            _context.Material.Add(model);
            await _context.SaveChangesAsync();

            // Mensaje de éxito
            TempData["SuccessMessage"] = "El material se ha creado correctamente.";

            return RedirectToAction("Index");
        } 
        // GET: Materials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var material = await _context.Material.FindAsync(id);
            if (material == null) return NotFound();

            // Cargar proveedores
            ViewBag.Suppliers = await _context.Supplier
                .Where(s => s.Supplier_Status == true)
                .OrderBy(s => s.Supplier_Name)
                .ToListAsync();

            return View(material);
        }

        // POST: Material/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Materials/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Material model)
        {
            if (id != model.Material_Id) return NotFound();

            // Cargar proveedores en caso de error
            ViewBag.Suppliers = await _context.Supplier
                .Where(s => s.Supplier_Status == true)
                .OrderBy(s => s.Supplier_Name)
                .ToListAsync();

            if (model.Material_SupplierId == null)
            {
                ModelState.AddModelError("Material_SupplierId", "Debe seleccionar un proveedor.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Verificación de unicidad del nombre
            bool exists = await _context.Material.AnyAsync(m => m.Material_Name == model.Material_Name && m.Material_Id != model.Material_Id);
            if (exists)
            {
                ModelState.AddModelError("Material_Name", "Este material ya existe en el sistema.");
                return View(model);
            }

            var originalMaterial = await _context.Material.FindAsync(id);
            if (originalMaterial == null) return NotFound();

            // Actualizar campos
            originalMaterial.Material_Name = model.Material_Name;
            originalMaterial.Material_Status = model.Material_Status;
            originalMaterial.Material_SupplierId = model.Material_SupplierId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MaterialExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            TempData["SuccessMessage"] = "El material se ha actualizado correctamente.";
            return RedirectToAction("Index");
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

      

        

    }
}
