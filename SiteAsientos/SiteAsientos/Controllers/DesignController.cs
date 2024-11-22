﻿using System;
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
        public async Task<IActionResult> Index()
        {
            var cubreasientosContext = _context.Design.Include(d => d.CentralDesign).Include(d => d.Image).Include(d => d.LateralDesign).Include(d => d.VehicleSeat);
            return View(await cubreasientosContext.ToListAsync());
        }

        // GET: Design/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var design = await _context.Design
                .Include(d => d.CentralDesign)
                .Include(d => d.Image)
                .Include(d => d.LateralDesign)
                .Include(d => d.VehicleSeat)
                .FirstOrDefaultAsync(m => m.Design_Id == id);
            if (design == null)
            {
                return NotFound();
            }

            return View(design);
        }

        // GET: Design/Create
        public IActionResult Create()
        {
            ViewData["Design_CentralDesignId"] = new SelectList(_context.CentralDesign, "CentralDesign_Id", "CentralDesign_Id");
            ViewData["Design_ImageId"] = new SelectList(_context.Images, "Image_Id", "Image_Id");
            ViewData["Design_LateralDesignId"] = new SelectList(_context.LateralDesign, "LateralDesign_Id", "LateralDesign_Id");
            ViewData["Design_VehicleSeatId"] = new SelectList(_context.VehicleSeat, "VehicleSeat_Id", "VehicleSeat_Id");
            return View();
        }

        // POST: Design/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Design_Id,Design_VehicleSeatId,Design_CentralDesignId,Design_LateralDesignId,Design_ImageId,Design_Status")] Design design)
        {
            if (ModelState.IsValid)
            {
                _context.Add(design);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Design_CentralDesignId"] = new SelectList(_context.CentralDesign, "CentralDesign_Id", "CentralDesign_Id", design.Design_CentralDesignId);
            ViewData["Design_ImageId"] = new SelectList(_context.Images, "Image_Id", "Image_Id", design.Design_ImageId);
            ViewData["Design_LateralDesignId"] = new SelectList(_context.LateralDesign, "LateralDesign_Id", "LateralDesign_Id", design.Design_LateralDesignId);
            ViewData["Design_VehicleSeatId"] = new SelectList(_context.VehicleSeat, "VehicleSeat_Id", "VehicleSeat_Id", design.Design_VehicleSeatId);
            return View(design);
        }

        // GET: Design/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var design = await _context.Design.FindAsync(id);
            if (design == null)
            {
                return NotFound();
            }
            ViewData["Design_CentralDesignId"] = new SelectList(_context.CentralDesign, "CentralDesign_Id", "CentralDesign_Id", design.Design_CentralDesignId);
            ViewData["Design_ImageId"] = new SelectList(_context.Images, "Image_Id", "Image_Id", design.Design_ImageId);
            ViewData["Design_LateralDesignId"] = new SelectList(_context.LateralDesign, "LateralDesign_Id", "LateralDesign_Id", design.Design_LateralDesignId);
            ViewData["Design_VehicleSeatId"] = new SelectList(_context.VehicleSeat, "VehicleSeat_Id", "VehicleSeat_Id", design.Design_VehicleSeatId);
            return View(design);
        }

        // POST: Design/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Design_Id,Design_VehicleSeatId,Design_CentralDesignId,Design_LateralDesignId,Design_ImageId,Design_Status")] Design design)
        {
            if (id != design.Design_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(design);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesignExists(design.Design_Id))
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
            ViewData["Design_CentralDesignId"] = new SelectList(_context.CentralDesign, "CentralDesign_Id", "CentralDesign_Id", design.Design_CentralDesignId);
            ViewData["Design_ImageId"] = new SelectList(_context.Images, "Image_Id", "Image_Id", design.Design_ImageId);
            ViewData["Design_LateralDesignId"] = new SelectList(_context.LateralDesign, "LateralDesign_Id", "LateralDesign_Id", design.Design_LateralDesignId);
            ViewData["Design_VehicleSeatId"] = new SelectList(_context.VehicleSeat, "VehicleSeat_Id", "VehicleSeat_Id", design.Design_VehicleSeatId);
            return View(design);
        }

        // GET: Design/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var design = await _context.Design
                .Include(d => d.CentralDesign)
                .Include(d => d.Image)
                .Include(d => d.LateralDesign)
                .Include(d => d.VehicleSeat)
                .FirstOrDefaultAsync(m => m.Design_Id == id);
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
            var design = await _context.Design.FindAsync(id);
            if (design != null)
            {
                _context.Design.Remove(design);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DesignExists(int id)
        {
            return _context.Design.Any(e => e.Design_Id == id);
        }
    }
}
