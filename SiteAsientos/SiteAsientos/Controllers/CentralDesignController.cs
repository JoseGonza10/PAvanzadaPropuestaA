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
    public class CentralDesignController : Controller
    {
        private readonly CubreasientosContext _context;

        public CentralDesignController(CubreasientosContext context)
        {
            _context = context;
        }

        // GET: CentralDesign
        public async Task<IActionResult> Index()
        {
            var cubreasientosContext = _context.CentralDesign.Include(c => c.Color).Include(c => c.Material);
            return View(await cubreasientosContext.ToListAsync());
        }

        // GET: CentralDesign/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centralDesign = await _context.CentralDesign
                .Include(c => c.Color)
                .Include(c => c.Material)
                .FirstOrDefaultAsync(m => m.CentralDesign_Id == id);
            if (centralDesign == null)
            {
                return NotFound();
            }

            return View(centralDesign);
        }

        // GET: CentralDesign/Create
        public IActionResult Create()
        {
            ViewData["CentralDesign_ColorId"] = new SelectList(_context.Color, "Color_Id", "Color_Name");
            ViewData["CentralDesign_MaterialId"] = new SelectList(_context.Material, "Material_Id", "Material_Name");
            return View();
        }

        // POST: CentralDesign/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CentralDesign_Id,CentralDesign_MaterialId,CentralDesign_ColorId")] CentralDesign centralDesign)
        {
            if (ModelState.IsValid)
            {
                _context.Add(centralDesign);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CentralDesign_ColorId"] = new SelectList(_context.Color, "Color_Id", "Color_Name", centralDesign.CentralDesign_ColorId);
            ViewData["CentralDesign_MaterialId"] = new SelectList(_context.Material, "Material_Id", "Material_Name", centralDesign.CentralDesign_MaterialId);
            return View(centralDesign);
        }

        // GET: CentralDesign/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centralDesign = await _context.CentralDesign.FindAsync(id);
            if (centralDesign == null)
            {
                return NotFound();
            }
            ViewData["CentralDesign_ColorId"] = new SelectList(_context.Color, "Color_Id", "Color_Name", centralDesign.CentralDesign_ColorId);
            ViewData["CentralDesign_MaterialId"] = new SelectList(_context.Material, "Material_Id", "Material_Name", centralDesign.CentralDesign_MaterialId);
            return View(centralDesign);
        }

        // POST: CentralDesign/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CentralDesign_Id,CentralDesign_MaterialId,CentralDesign_ColorId")] CentralDesign centralDesign)
        {
            if (id != centralDesign.CentralDesign_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(centralDesign);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CentralDesignExists(centralDesign.CentralDesign_Id))
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
            ViewData["CentralDesign_ColorId"] = new SelectList(_context.Color, "Color_Id", "Color_Name", centralDesign.CentralDesign_ColorId);
            ViewData["CentralDesign_MaterialId"] = new SelectList(_context.Material, "Material_Id", "Material_Name", centralDesign.CentralDesign_MaterialId);
            return View(centralDesign);
        }

        // GET: CentralDesign/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centralDesign = await _context.CentralDesign
                .Include(c => c.Color)
                .Include(c => c.Material)
                .FirstOrDefaultAsync(m => m.CentralDesign_Id == id);
            if (centralDesign == null)
            {
                return NotFound();
            }

            return View(centralDesign);
        }

        // POST: CentralDesign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var centralDesign = await _context.CentralDesign.FindAsync(id);
            if (centralDesign != null)
            {
                _context.CentralDesign.Remove(centralDesign);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CentralDesignExists(int id)
        {
            return _context.CentralDesign.Any(e => e.CentralDesign_Id == id);
        }
    }
}
