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
    public class OrdersController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        //Declaracion del automapper para poder caster los objetos 
        private readonly IMapper _mapper;

        public OrdersController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataModels.Orders>>> GetOrders()
        {
            var aux = await new BS.Orders(_context).GetAllWithAsync();

            var mapaux = _mapper.Map<IEnumerable<data.Orders>, IEnumerable<datamodels.Orders>>(aux).ToList();
            return mapaux;

            //return await new Solution.BS.Foci(_context).GetAllWithAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataModels.Orders>> GetOrders(int id)
        {
            var orders = await new BS.Orders(_context).GetOneByIdWithAsync(id);
            var mapaux = _mapper.Map<data.Orders, datamodels.Orders>(orders);

            if (orders == null)
            {
                return NotFound();
            }

            return mapaux;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrders(int id, DataModels.Orders orders)
        {
            if (id != orders.OrderId)
            {
                return BadRequest();
            }

            try
            {
                var mapaux = _mapper.Map<datamodels.Orders, data.Orders>(orders);

                new BS.Orders(_context).Update(mapaux);
            }
            catch (Exception ee)
            {
                if (!OrdersExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DataModels.Orders>> PostOrders(DataModels.Orders orders)
        {
            var mapaux = _mapper.Map<datamodels.Orders, data.Orders>(orders);

            new BS.Orders(_context).Insert(mapaux);

            return CreatedAtAction("GetOrders", new { id = orders.OrderId }, orders);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataModels.Orders>> DeleteOrders(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }

            new BS.Orders(_context).Delete(orders);
            var mapaux = _mapper.Map<data.Orders, datamodels.Orders>(orders);

            return mapaux;
        }

        private bool OrdersExists(int id)
        {
            return (new BS.Orders(_context).GetOneById(id) != null);
        }
    }
}
