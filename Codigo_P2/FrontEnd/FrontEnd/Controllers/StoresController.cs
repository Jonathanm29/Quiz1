﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrontEnd.Models;

namespace FrontEnd.Controllers
{
    public class StoresController : Controller
    {
        private readonly BikeStoresContext _context;

        public StoresController(BikeStoresContext context)
        {
            _context = context;
        }

        // GET: Stores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stores.ToListAsync());
        }

        // GET: Stores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stores = await _context.Stores
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (stores == null)
            {
                return NotFound();
            }

            return View(stores);
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StoreId,StoreName,Phone,Email,Street,City,State,ZipCode")] Stores stores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stores);
        }

        // GET: Stores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stores = await _context.Stores.FindAsync(id);
            if (stores == null)
            {
                return NotFound();
            }
            return View(stores);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StoreId,StoreName,Phone,Email,Street,City,State,ZipCode")] Stores stores)
        {
            if (id != stores.StoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoresExists(stores.StoreId))
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
            return View(stores);
        }

        // GET: Stores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stores = await _context.Stores
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (stores == null)
            {
                return NotFound();
            }

            return View(stores);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stores = await _context.Stores.FindAsync(id);
            _context.Stores.Remove(stores);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoresExists(int id)
        {
            return _context.Stores.Any(e => e.StoreId == id);
        }
    }
}
