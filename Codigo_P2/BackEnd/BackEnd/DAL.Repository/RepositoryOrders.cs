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
    public class RepositoryOrders : Repository<data.Orders>, IReposityOrders
    {
        public RepositoryOrders(SolutionDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<data.Orders>> GetAllWithAsAsync()
        {
            return await _db.Orders
                .Include(m => m.Customer)
                .Include(m => m.Staff)
                .Include(m => m.Store)
                .ToListAsync();
        }

        public async Task<data.Orders> GetByOneWithAsAsync(int id)
        {
            return await _db.Orders
             .Include(m => m.Customer)
              .Include(m => m.Staff)
              .Include(m => m.Store)
             .SingleOrDefaultAsync(m => m.OrderId == id);
        }

        //Metodo para obtener el context cargado del repository y asi utilizarlo en esta clase
        private SolutionDbContext _db
        {
            get { return dbContext as SolutionDbContext; }
        }
    }
}
