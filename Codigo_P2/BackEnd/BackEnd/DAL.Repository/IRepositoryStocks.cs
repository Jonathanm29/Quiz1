using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = DAL.DO.Objects;
namespace DAL.Repository
{
    interface IRepositoryStocks : IRepository<data.Stocks>
    {
        Task<IEnumerable<data.Stocks>> GetAllWithAsAsync();

        Task<data.Stocks> GetByOneWithAsAsync(int id);

    }
}

