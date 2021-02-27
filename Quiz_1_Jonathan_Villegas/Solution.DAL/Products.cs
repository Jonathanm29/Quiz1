using System;
using System.Collections.Generic;
using Solution.DAL.EF;
using Solution.DAL.Reposity;
using Solution.DO.Interfaces;
using data = Solution.DO.Objects;

namespace Solution.DAL
{
    public class Products : ICRUD<data.Products>
    {
        private Repository<data.Products> _repo = null;
        public Products(SolutionDBContext solutionDbContext)
        {
            _repo = new Repository<data.Products>(solutionDbContext);
        }
        public void Delete(data.Products t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.Products> GetAll()
        {
            return _repo.GetAll();
        }

        public data.Products GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(data.Products t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.Products t)
        {
            _repo.Update(t);
            _repo.Commit();
        }
    }
}
