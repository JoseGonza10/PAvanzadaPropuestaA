﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SiteAsientos.Models;

namespace SiteAsientos.Controllers
{
    public class VehicleController : Controller
    {
        private readonly CubreasientosContext _context;
        private static List<BrandModelData> _carData;

        public VehicleController(CubreasientosContext context)
        {
            _context = context;
            var json = System.IO.File.ReadAllText("wwwroot/data/brands.json");
            _carData = JsonConvert.DeserializeObject<List<BrandModelData>>(json);
        }

        // GET: Vehicle
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vehicle.ToListAsync());
        }

        // GET: Vehicle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Vehicle_Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicle/Create
        public IActionResult Create()
        {
            var vm = new CarSelectionViewModel
            {
                Brands = _carData.Select(x => x.Brand).ToList()
            }; 
            VehicleAndFeatures vehicleAndFeatures = new VehicleAndFeatures();
            vehicleAndFeatures.vehicle = new Vehicle();
            vehicleAndFeatures.vehicle.Vehicle_ModelYear = DateTime.Now.Year;
            vehicleAndFeatures.features = vm;

            return View(vehicleAndFeatures); 
        }

        // POST: Vehicle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleAndFeatures composed)
        {
            if (!TryValidateModel(composed.vehicle, "Vehicle"))
            {
                _context.Add(composed.vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(composed.vehicle);
        }

        // GET: Vehicle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VehicleAndFeatures composed)
        {
            //if (id != composed.vechicle.Vehicle_Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(composed.vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(composed.vehicle.Vehicle_Id))
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
            return View(composed);
        }

        // GET: Vehicle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Vehicle_Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicle.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicle.Any(e => e.Vehicle_Id == id);
        }
        // Esta acción devuelve modelos para una marca determinada a través de AJAX
        [HttpGet]
        public JsonResult GetModels(string brand)
        {
            var models = _carData.FirstOrDefault(x => x.Brand == brand)?.Models ?? new List<string>();
            return Json(models);
        }
    }
}
