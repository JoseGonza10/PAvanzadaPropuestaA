﻿using System;
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

        // GET: Material
        public async Task<IActionResult> Index()
        {
            var cubreasientosContext = _context.Material.Include(m => m.Supplier);
            return View(await cubreasientosContext.ToListAsync());
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
        public IActionResult Create()
        {
            ViewData["Material_SupplierId"] = new SelectList(_context.Supplier, "Supplier_Id", "Supplier_Address");
            return View();
        }

        // POST: Material/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Material_Id,Material_Name,Material_Status,Material_SupplierId")] Material material)
        {
            if (ModelState.IsValid)
            {
                _context.Add(material);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Material_SupplierId"] = new SelectList(_context.Supplier, "Supplier_Id", "Supplier_Address", material.Material_SupplierId);
            return View(material);
        }
        // GET: Material/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = await _context.Material.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            ViewData["Material_SupplierId"] = new SelectList(_context.Supplier, "Supplier_Id", "Supplier_Address", material.Material_SupplierId);
            return View(material);
        }

        // POST: Material/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    if (!MaterialExists(material.Material_Id))
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
            ViewData["Material_SupplierId"] = new SelectList(_context.Supplier, "Supplier_Id", "Supplier_Address", material.Material_SupplierId);
            return View(material);
        }

        // GET: Material/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Material/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var material = await _context.Material.FindAsync(id);
            if (material != null)
            {
                _context.Material.Remove(material);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialExists(int id)
        {
            return _context.Material.Any(e => e.Material_Id == id);
        }

        //Verifica la existencia del nombre del material
        [AllowAnonymous]
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> MaterialExists(Material material)
        {
            Debug.WriteLine("El id es" + material.Material_Id);
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
