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
    public class VisitController : Controller
    {
        private readonly CubreasientosContext _context;

        public VisitController(CubreasientosContext context)
        {
            _context = context;
        }

        // GET: Visit
        public async Task<IActionResult> Index()
        {
            var cubreasientosContext = _context.Visit.Include(v => v.Employee).Include(v => v.Order);
            return View(await cubreasientosContext.ToListAsync());
        }

        // GET: Visit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _context.Visit
                .Include(v => v.Employee)
                .Include(v => v.Order)
                .FirstOrDefaultAsync(m => m.Visit_Id == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // GET: Visit/Create
        public IActionResult Create()
        {
            ViewData["Visit_EmployeeId"] = new SelectList(_context.Employee, "Employee_Id", "Employee_Email");
            ViewData["Visit_OrderId"] = new SelectList(_context.Orders, "Order_Id", "Order_Code");
            return View();
        }

        // POST: Visit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Visit_Id,Visit_Code,Visit_OrderId,Visit_EmployeeId,Visit_Type,Visit_Date,Visit_Status")] Visit visit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Visit_EmployeeId"] = new SelectList(_context.Employee, "Employee_Id", "Employee_Email", visit.Visit_EmployeeId);
            ViewData["Visit_OrderId"] = new SelectList(_context.Orders, "Order_Id", "Order_Code", visit.Visit_OrderId);
            return View(visit);
        }

        // GET: Visit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _context.Visit.FindAsync(id);
            if (visit == null)
            {
                return NotFound();
            }
            ViewData["Visit_EmployeeId"] = new SelectList(_context.Employee, "Employee_Id", "Employee_Email", visit.Visit_EmployeeId);
            ViewData["Visit_OrderId"] = new SelectList(_context.Orders, "Order_Id", "Order_Code", visit.Visit_OrderId);
            return View(visit);
        }

        // POST: Visit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Visit_Id,Visit_Code,Visit_OrderId,Visit_EmployeeId,Visit_Type,Visit_Date,Visit_Status")] Visit visit)
        {
            if (id != visit.Visit_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitExists(visit.Visit_Id))
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
            ViewData["Visit_EmployeeId"] = new SelectList(_context.Employee, "Employee_Id", "Employee_Email", visit.Visit_EmployeeId);
            ViewData["Visit_OrderId"] = new SelectList(_context.Orders, "Order_Id", "Order_Code", visit.Visit_OrderId);
            return View(visit);
        }

        // GET: Visit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _context.Visit
                .Include(v => v.Employee)
                .Include(v => v.Order)
                .FirstOrDefaultAsync(m => m.Visit_Id == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // POST: Visit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visit = await _context.Visit.FindAsync(id);
            if (visit != null)
            {
                _context.Visit.Remove(visit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitExists(int id)
        {
            return _context.Visit.Any(e => e.Visit_Id == id);
        }
    }
}
