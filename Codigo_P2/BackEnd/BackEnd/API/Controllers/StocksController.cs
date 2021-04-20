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
    public class StocksController : ControllerBase
    {
        private readonly SolutionDbContext _context;

        //Declaracion del automapper para poder caster los objetos 
        private readonly IMapper _mapper;

        public StocksController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/Stocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataModels.Stocks>>> GetStocks()
        {
            var aux = new BS.Stocks(_context).GetAll();

            var mapaux = _mapper.Map<IEnumerable<data.Stocks>, IEnumerable<datamodels.Stocks>>(aux).ToList();
            return mapaux;
        }

        // GET: api/Stocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<datamodels.Stocks>> GetStocks(int id)
        {
            var stocks =  new BS.Stocks(_context).GetOneById(id);
            var mapaux = _mapper.Map<data.Stocks, datamodels.Stocks>(stocks);
            if (stocks == null)
            {
                return NotFound();
            }

            return mapaux;
        }

        // PUT: api/Stocks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStocks(int id, DataModels.Stocks stocks)
        {
            if (id != stocks.StoreId)
            {
                return BadRequest();
            }

            try
            {
                var mapaux = _mapper.Map<datamodels.Stocks, data.Stocks>(stocks);

                new BS.Stocks(_context).Update(mapaux);
            }
            catch (Exception)
            {
                if (!StocksExists(id))
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

        // POST: api/Stocks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DataModels.Stocks>> PostStocks(DataModels.Stocks stocks)
        {
            var mapaux = _mapper.Map<datamodels.Stocks, data.Stocks>(stocks);

            new BS.Stocks(_context).Insert(mapaux);

            return CreatedAtAction("GetStocks", new { id = stocks.StoreId }, stocks);
        }

        // DELETE: api/Stocks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataModels.Stocks>> DeleteStocks(int id)
        {
            var stocks = new BS.Stocks(_context).GetOneById(id);
            if (stocks == null)
            {
                return NotFound();
            }
            new BS.Stocks(_context).Delete(stocks);
            var mapaux = _mapper.Map<data.Stocks, datamodels.Stocks>(stocks);
            return mapaux;
        }

        private bool StocksExists(int id)
        {
            return (new BS.Stocks(_context).GetOneById(id) != null);
        }
    }
}

