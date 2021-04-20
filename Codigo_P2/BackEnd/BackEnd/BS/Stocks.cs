using DAL.EF;
using DAL.DO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using data = DAL.DO.Objects;
using System.Threading.Tasks;

namespace BS
{
    public class Stocks : ICRUD<data.Stocks>
    {
        private SolutionDbContext _repo = null;

        public Stocks(SolutionDbContext solutionDbContext)
        {
            _repo = solutionDbContext;
        }

        public void Delete(data.Stocks t)
        {
            new DAL.Stocks(_repo).Delete(t);
        }

        public IEnumerable<data.Stocks> GetAll()
        {
            return new DAL.Stocks(_repo).GetAll();
        }

        public data.Stocks GetOneById(int id)
        {
            return new DAL.Stocks(_repo).GetOneById(id);
        }

        public void Insert(data.Stocks t)
        {
            new DAL.Stocks(_repo).Insert(t);
        }

        public void Update(data.Stocks t)
        {
            new DAL.Stocks(_repo).Update(t);
        }
        public async Task<IEnumerable<data.Stocks>> GetAllWithAsync()
        {
            return await new DAL.Stocks(_repo).GetAllWithAsync();
        }

        public async Task<data.Stocks> GetOneByIdWithAsync(int id)
        {
            return await new DAL.Stocks(_repo).GetOneByIdWithAsync(id);
        }
    }
}