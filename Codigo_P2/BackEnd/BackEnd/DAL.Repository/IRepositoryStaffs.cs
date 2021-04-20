using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = DAL.DO.Objects;

namespace DAL.Repository
{
    interface IRepositoryStaffs : IRepository<data.Staffs>
    {
        Task<IEnumerable<data.Staffs>> GetAllWithAsAsync();

        Task<data.Staffs> GetByOneWithAsAsync(int id);

    }
}

