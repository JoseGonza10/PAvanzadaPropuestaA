using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteAsientos.Models;

namespace SiteAsientos.Controllers
{
    public class SupplierController : Controller
    {
        private readonly CubreasientosContext _context;

        public SupplierController(CubreasientosContext context)
        {
            _context = context;
        }

        // GET: Supplier
        public async Task<IActionResult> Index()
        {
            return View(await _context.Supplier.ToListAsync());
        }

        // GET: Supplier/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Supplier
                .FirstOrDefaultAsync(m => m.Supplier_Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Supplier/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Supplier_Id,Supplier_Name,Supplier_Address,Supplier_Phone,Supplier_Email,Supplier_DateAdded,Supplier_Status")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }
        // GET: Supplier/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Supplier.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Supplier_Id,Supplier_Name,Supplier_Address,Supplier_Phone,Supplier_Email,Supplier_DateAdded,Supplier_Status")] Supplier supplier)
        {
            if (id != supplier.Supplier_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.Supplier_Id))
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
            return View(supplier);
        }

        // GET: Supplier/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Supplier
                .FirstOrDefaultAsync(m => m.Supplier_Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await _context.Supplier.FindAsync(id);
            if (supplier != null)
            {
                _context.Supplier.Remove(supplier);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id)
        {
            return _context.Supplier.Any(e => e.Supplier_Id == id);
        }

        //Verifica si el correo existe
        [AllowAnonymous]
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> EmailExists(Supplier supplier)
        {
            var existingSupplier = _context.Supplier.Where(x => x.Supplier_Email == supplier.Supplier_Email && x.Supplier_Id != supplier.Supplier_Id);
            if (existingSupplier.Any())
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }
        //Verifica si el telefono existe
        [AllowAnonymous]
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> PhoneExists(Supplier supplier)
        {
            var existingSupplier = _context.Supplier.Where(x => x.Supplier_Phone == supplier.Supplier_Phone && x.Supplier_Id != supplier.Supplier_Id);
            if (existingSupplier.Any())
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
