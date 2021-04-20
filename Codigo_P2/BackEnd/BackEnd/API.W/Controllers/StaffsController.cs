using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.W.Models;

namespace API.W.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly BikeStoresContext _context;

        public StaffsController(BikeStoresContext context)
        {
            _context = context;
        }

        // GET: api/Staffs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staffs>>> GetStaffs()
        {
            return await _context.Staffs.ToListAsync();
        }

        // GET: api/Staffs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Staffs>> GetStaffs(int id)
        {
            var staffs = await _context.Staffs.FindAsync(id);

            if (staffs == null)
            {
                return NotFound();
            }

            return staffs;
        }

        // PUT: api/Staffs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaffs(int id, Staffs staffs)
        {
            if (id != staffs.StaffId)
            {
                return BadRequest();
            }

            _context.Entry(staffs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Staffs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Staffs>> PostStaffs(Staffs staffs)
        {
            _context.Staffs.Add(staffs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStaffs", new { id = staffs.StaffId }, staffs);
        }

        // DELETE: api/Staffs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Staffs>> DeleteStaffs(int id)
        {
            var staffs = await _context.Staffs.FindAsync(id);
            if (staffs == null)
            {
                return NotFound();
            }

            _context.Staffs.Remove(staffs);
            await _context.SaveChangesAsync();

            return staffs;
        }

        private bool StaffsExists(int id)
        {
            return _context.Staffs.Any(e => e.StaffId == id);
        }
    }
}
