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
    public class StoresController : ControllerBase
    {
        private readonly SolutionDbContext _context;

        //Declaracion del automapper para poder caster los objetos 
        private readonly IMapper _mapper;

        public StoresController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/Stores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataModels.Stores>>> GetStores()
        {
            var aux = new BS.Stores(_context).GetAll();

            var mapaux = _mapper.Map<IEnumerable<data.Stores>, IEnumerable<datamodels.Stores>>(aux).ToList();
            return mapaux;
        }
        // GET: api/Stores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataModels.Stores>> GetStores(int id)
        {
            var stores = new BS.Stores(_context).GetOneById(id);
            var mapaux = _mapper.Map<data.Stores, datamodels.Stores>(stores);

            if (stores == null)
            {
                return NotFound();
            }

            return mapaux;
        }

        // PUT: api/Stores/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStores(int id, DataModels.Stores stores)
        {
            if (id != stores.StoreId)
            {
                return BadRequest();
            }
            try
            {
                var mapaux = _mapper.Map<datamodels.Stores, data.Stores>(stores);

                new BS.Stores(_context).Update(mapaux);
            }
            catch (Exception)
            {
                if (!StoresExists(id))
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

        // POST: api/Stores
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DataModels.Stores>> PostStores(DataModels.Stores stores)
        {
            var mapaux = _mapper.Map<datamodels.Stores, data.Stores>(stores);

            new BS.Stores(_context).Insert(mapaux);

            return CreatedAtAction("GetStores", new { id = stores.StoreId }, stores);
        }

        // DELETE: api/Stores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataModels.Stores>> DeleteStores(int id)
        {
            var stores = new BS.Stores(_context).GetOneById(id);
            if (stores == null)
            {
                return NotFound();
            }

            new BS.Stores(_context).Delete(stores);
            var mapaux = _mapper.Map<data.Stores, datamodels.Stores>(stores);
            return mapaux;
        }

        private bool StoresExists(int id)
        {
            return (new BS.Stores(_context).GetOneById(id) != null);
        }
    }
}
