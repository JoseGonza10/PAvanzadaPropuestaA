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
    public class LateralDesignController : Controller
    {
        private readonly CubreasientosContext _context;

        public LateralDesignController(CubreasientosContext context)
        {
            _context = context;
        }

        // GET: LateralDesign
        public async Task<IActionResult> Index()
        {
            var cubreasientosContext = _context.LateralDesign.Include(l => l.Color).Include(l => l.Material);
            return View(await cubreasientosContext.ToListAsync());
        }

        // GET: LateralDesign/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lateralDesign = await _context.LateralDesign
                .Include(l => l.Color)
                .Include(l => l.Material)
                .FirstOrDefaultAsync(m => m.LateralDesign_Id == id);
            if (lateralDesign == null)
            {
                return NotFound();
            }

            return View(lateralDesign);
        }

        // GET: LateralDesign/Create
        public IActionResult Create()
        {
            ViewData["LateralDesign_ColorId"] = new SelectList(_context.Color, "Color_Id", "Color_Name");
            ViewData["LateralDesign_MaterialId"] = new SelectList(_context.Material, "Material_Id", "Material_Name");
            return View();
        }

        // POST: LateralDesign/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LateralDesign_Id,LateralDesign_MaterialId,LateralDesign_ColorId")] LateralDesign lateralDesign)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lateralDesign);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LateralDesign_ColorId"] = new SelectList(_context.Color, "Color_Id", "Color_Name", lateralDesign.LateralDesign_ColorId);
            ViewData["LateralDesign_MaterialId"] = new SelectList(_context.Material, "Material_Id", "Material_Name", lateralDesign.LateralDesign_MaterialId);
            return View(lateralDesign);
        }

        // GET: LateralDesign/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lateralDesign = await _context.LateralDesign.FindAsync(id);
            if (lateralDesign == null)
            {
                return NotFound();
            }
            ViewData["LateralDesign_ColorId"] = new SelectList(_context.Color, "Color_Id", "Color_Name", lateralDesign.LateralDesign_ColorId);
            ViewData["LateralDesign_MaterialId"] = new SelectList(_context.Material, "Material_Id", "Material_Name", lateralDesign.LateralDesign_MaterialId);
            return View(lateralDesign);
        }

        // POST: LateralDesign/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LateralDesign_Id,LateralDesign_MaterialId,LateralDesign_ColorId")] LateralDesign lateralDesign)
        {
            if (id != lateralDesign.LateralDesign_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lateralDesign);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LateralDesignExists(lateralDesign.LateralDesign_Id))
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
            ViewData["LateralDesign_ColorId"] = new SelectList(_context.Color, "Color_Id", "Color_Name", lateralDesign.LateralDesign_ColorId);
            ViewData["LateralDesign_MaterialId"] = new SelectList(_context.Material, "Material_Id", "Material_Name", lateralDesign.LateralDesign_MaterialId);
            return View(lateralDesign);
        }

        // GET: LateralDesign/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lateralDesign = await _context.LateralDesign
                .Include(l => l.Color)
                .Include(l => l.Material)
                .FirstOrDefaultAsync(m => m.LateralDesign_Id == id);
            if (lateralDesign == null)
            {
                return NotFound();
            }

            return View(lateralDesign);
        }

        // POST: LateralDesign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lateralDesign = await _context.LateralDesign.FindAsync(id);
            if (lateralDesign != null)
            {
                _context.LateralDesign.Remove(lateralDesign);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LateralDesignExists(int id)
        {
            return _context.LateralDesign.Any(e => e.LateralDesign_Id == id);
        }
    }
}
