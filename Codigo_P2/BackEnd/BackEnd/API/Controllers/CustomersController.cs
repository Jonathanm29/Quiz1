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
    public class CustomersController : ControllerBase
    {
        private readonly SolutionDbContext _context;

        //Declaracion del automapper para poder caster los objetos 
        private readonly IMapper _mapper;

        public CustomersController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataModels.Customers>>> GetCustomers()
        {
            var aux = new BS.Customers(_context).GetAll();

            var mapaux = _mapper.Map<IEnumerable<data.Customers>, IEnumerable<datamodels.Customers>>(aux).ToList();
            return mapaux;
            //return await new Solution.BS.Foci(_context).GetAllWithAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataModels.Customers>> GetCustomers(int id)
        {
            var customers = new BS.Customers(_context).GetOneById(id);
            var mapaux = _mapper.Map<data.Customers, datamodels.Customers>(customers);

            if (customers == null)
            {
                return NotFound();
            }

            return mapaux;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomers(int id, DataModels.Customers customers)
        {
            if (id != customers.CustomerId)
            {
                return BadRequest();
            }

            try
            {
                var mapaux = _mapper.Map<datamodels.Customers, data.Customers>(customers);

                new BS.Customers(_context).Update(mapaux);
            }
            catch (Exception ee)
            {
                if (!CustomersExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DataModels.Customers>> PostCustomers(DataModels.Customers customers)
        {
            var mapaux = _mapper.Map<datamodels.Customers, data.Customers>(customers);

            new BS.Customers(_context).Insert(mapaux);


            return CreatedAtAction("GetCustomers", new { id = customers.CustomerId }, customers);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataModels.Customers>> DeleteCustomers(int id)
        {
            var customers = new BS.Customers(_context).GetOneById(id);
            if (customers == null)
            {
                return NotFound();
            }

            new BS.Customers(_context).Delete(customers);
            var mapaux = _mapper.Map<data.Customers, datamodels.Customers>(customers);

            return mapaux;
        }

        private bool CustomersExists(int id)
        {
            return (new BS.Customers(_context).GetOneById(id) != null);
        }
    }
}
