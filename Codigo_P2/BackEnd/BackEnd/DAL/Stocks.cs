using System.Threading.Tasks;
using DAL.EF;
using DAL.Repository;
using DAL.DO.Interfaces;
using data = DAL.DO.Objects;
using System.Collections.Generic;

namespace DAL
{
    public class Stocks : ICRUD<data.Stocks>
    {
        private RepositoryStocks _repo = null;
        public Stocks(SolutionDbContext solutionDbContext)
        {
            _repo = new RepositoryStocks(solutionDbContext);
        }

        public void Delete(data.Stocks t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.Stocks> GetAll()
        {
            return _repo.GetAll();
        }

        public data.Stocks GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(data.Stocks t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.Stocks t)
        {
            _repo.Update(t);
            _repo.Commit();
        }
        public async Task<IEnumerable<data.Stocks>> GetAllWithAsync()
        {
            return await _repo.GetAllWithAsAsync();
        }

        public async Task<data.Stocks> GetOneByIdWithAsync(int id)
        {
            return await _repo.GetByOneWithAsAsync(id);
        }
    }
}