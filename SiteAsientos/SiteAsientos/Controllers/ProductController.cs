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
    public class ProductController : Controller
    {
        private readonly CubreasientosContext _context;

        public ProductController(CubreasientosContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var cubreasientosContext = _context.Product.Include(p => p.Design).Include(p => p.Order).Include(p => p.Supplier);
            return View(await cubreasientosContext.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Design)
                .Include(p => p.Order)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Product_Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["Product_DesignId"] = new SelectList(_context.Design, "Design_Id", "Design_Id");
            ViewData["Product_OrderId"] = new SelectList(_context.Orders, "Order_Id", "Order_Code");
            ViewData["Product_SupplierId"] = new SelectList(_context.Supplier, "Supplier_Id", "Supplier_Address");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Product_Id,Product_OrderId,Product_DesignId,Product_SupplierId,Product_Embroidery")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Product_DesignId"] = new SelectList(_context.Design, "Design_Id", "Design_Id", product.Product_DesignId);
            ViewData["Product_OrderId"] = new SelectList(_context.Orders, "Order_Id", "Order_Code", product.Product_OrderId);
            ViewData["Product_SupplierId"] = new SelectList(_context.Supplier, "Supplier_Id", "Supplier_Address", product.Product_SupplierId);
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["Product_DesignId"] = new SelectList(_context.Design, "Design_Id", "Design_Id", product.Product_DesignId);
            ViewData["Product_OrderId"] = new SelectList(_context.Orders, "Order_Id", "Order_Code", product.Product_OrderId);
            ViewData["Product_SupplierId"] = new SelectList(_context.Supplier, "Supplier_Id", "Supplier_Address", product.Product_SupplierId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Product_Id,Product_OrderId,Product_DesignId,Product_SupplierId,Product_Embroidery")] Product product)
        {
            if (id != product.Product_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Product_Id))
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
            ViewData["Product_DesignId"] = new SelectList(_context.Design, "Design_Id", "Design_Id", product.Product_DesignId);
            ViewData["Product_OrderId"] = new SelectList(_context.Orders, "Order_Id", "Order_Code", product.Product_OrderId);
            ViewData["Product_SupplierId"] = new SelectList(_context.Supplier, "Supplier_Id", "Supplier_Address", product.Product_SupplierId);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Design)
                .Include(p => p.Order)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Product_Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Product_Id == id);
        }
    }
}
