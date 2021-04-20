using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using data = DAL.DO.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.EF;
using AutoMapper;
using datamodels = API.DataModels;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly SolutionDbContext _context;

        //Declaracion del automapper para poder caster los objetos 
        private readonly IMapper _mapper;

        public StaffsController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
            [HttpGet]
        public async Task<ActionResult<IEnumerable<DataModels.Staffs>>> GetStaffs()
        {
            var aux = new BS.Staffs(_context).GetAll();

            var mapaux = _mapper.Map<IEnumerable<data.Staffs>, IEnumerable<datamodels.Staffs>>(aux).ToList();
            return mapaux;
        }
        // GET: api/Staffs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataModels.Staffs>> GetStaffs(int id)
        {
            var staffs = new BS.Staffs(_context).GetOneById(id);
            var mapaux = _mapper.Map<data.Staffs, datamodels.Staffs>(staffs);

            if (staffs == null)
            {
                return NotFound();
            }

            return mapaux;
        }

        // PUT: api/Staffs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaffs(int id, DataModels.Staffs staffs)
        {
            if (id != staffs.StaffId)
            {
                return BadRequest();
            }

            try
            {
                var mapaux = _mapper.Map<datamodels.Staffs, data.Staffs>(staffs);

                new BS.Staffs(_context).Update(mapaux);
            }
            catch (Exception)
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
        public async Task<ActionResult<DataModels.Staffs>> PostStaffs(DataModels.Staffs staffs)
        {
            var mapaux = _mapper.Map<datamodels.Staffs, data.Staffs>(staffs);

            new BS.Staffs(_context).Insert(mapaux);

            return CreatedAtAction("GetStaffs", new { id = staffs.StaffId }, staffs);
        }

        // DELETE: api/Staffs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataModels.Staffs>> DeleteStaffs(int id)
        {
            var staffs = new BS.Staffs(_context).GetOneById(id);
            if (staffs == null)
            {
                return NotFound();
            }
            new BS.Staffs(_context).Delete(staffs);
            var mapaux = _mapper.Map<data.Staffs, datamodels.Staffs>(staffs);
            return mapaux;
        }

        private bool StaffsExists(int id)
        {
            return (new BS.Staffs(_context).GetOneById(id) != null);
        }
    }
}
