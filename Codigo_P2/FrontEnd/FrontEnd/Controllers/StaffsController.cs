using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrontEnd.Models;

namespace FrontEnd.Controllers
{
    public class StaffsController : Controller
    {
        private readonly BikeStoresContext _context;

        public StaffsController(BikeStoresContext context)
        {
            _context = context;
        }

        // GET: Staffs
        public async Task<IActionResult> Index()
        {
            var bikeStoresContext = _context.Staffs.Include(s => s.Manager).Include(s => s.Store);
            return View(await bikeStoresContext.ToListAsync());
        }

        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs
                .Include(s => s.Manager)
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staffs == null)
            {
                return NotFound();
            }

            return View(staffs);
        }

        // GET: Staffs/Create
        public IActionResult Create()
        {
            ViewData["ManagerId"] = new SelectList(_context.Staffs, "StaffId", "Email");
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName");
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffId,FirstName,LastName,Email,Phone,Active,StoreId,ManagerId")] Staffs staffs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManagerId"] = new SelectList(_context.Staffs, "StaffId", "Email", staffs.ManagerId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName", staffs.StoreId);
            return View(staffs);
        }

        // GET: Staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs.FindAsync(id);
            if (staffs == null)
            {
                return NotFound();
            }
            ViewData["ManagerId"] = new SelectList(_context.Staffs, "StaffId", "Email", staffs.ManagerId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName", staffs.StoreId);
            return View(staffs);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffId,FirstName,LastName,Email,Phone,Active,StoreId,ManagerId")] Staffs staffs)
        {
            if (id != staffs.StaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffsExists(staffs.StaffId))
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
            ViewData["ManagerId"] = new SelectList(_context.Staffs, "StaffId", "Email", staffs.ManagerId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName", staffs.StoreId);
            return View(staffs);
        }

        // GET: Staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs
                .Include(s => s.Manager)
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staffs == null)
            {
                return NotFound();
            }

            return View(staffs);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staffs = await _context.Staffs.FindAsync(id);
            _context.Staffs.Remove(staffs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffsExists(int id)
        {
            return _context.Staffs.Any(e => e.StaffId == id);
        }
    }
}
