using DAL.EF;
//using dal.DO.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = DAL.DO.Objects;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class RepositoryStocks : Repository<data.Stocks>, IRepositoryStocks
    {
        public RepositoryStocks(SolutionDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<data.Stocks>> GetAllWithAsAsync()
        {
            return await _db.Stocks
                .Include(m => m.Store)
                .ToListAsync();
        }

        public async Task<data.Stocks> GetByOneWithAsAsync(int id)
        {
            return await _db.Stocks
              .Include(m => m.Store)
             .SingleOrDefaultAsync(m => m.StoreId == id);
        }

        //Metodo para obtener el context cargado del repository y asi utilizarlo en esta clase
        private SolutionDbContext _db
        {
            get { return dbContext as SolutionDbContext; }
        }
    }
}
