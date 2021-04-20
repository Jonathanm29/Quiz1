using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Repository;
using DAL.DO.Interfaces;
using data = DAL.DO.Objects;

namespace BS
{
    public class Orders : ICRUD<data.Orders>
    {
        private SolutionDbContext _repo = null;

        public Orders(SolutionDbContext solutionDbContext)
        {
            _repo = solutionDbContext;
        }

        public void Delete(data.Orders t)
        {
            new DAL.Orders(_repo).Delete(t);
        }

        public IEnumerable<data.Orders> GetAll()
        {
            return new DAL.Orders(_repo).GetAll();
        }

        public data.Orders GetOneById(int id)
        {
            return new DAL.Orders(_repo).GetOneById(id);
        }

        public void Insert(data.Orders t)
        {
            new DAL.Orders(_repo).Insert(t);
        }

        public void Update(data.Orders t)
        {
            new DAL.Orders(_repo).Update(t);
        }

        public async Task<IEnumerable<data.Orders>> GetAllWithAsync()
        {
            return await new DAL.Orders(_repo).GetAllWithAsync();
        }

        public async Task<data.Orders> GetOneByIdWithAsync(int id)
        {
            return await new DAL.Orders(_repo).GetOneByIdWithAsync(id);
        }
    }
}
