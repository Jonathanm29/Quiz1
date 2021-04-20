using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = DAL.DO.Objects;

namespace DAL.Repository
{
    public interface IReposityOrders : IRepository<data.Orders>
    {
        Task<IEnumerable<data.Orders>> GetAllWithAsAsync();

        Task<data.Orders> GetByOneWithAsAsync(int id);

    }
}
