using DAL.EF;
using DAL.Repository;
using DAL.DO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using data = DAL.DO.Objects;
using System.Threading.Tasks;

namespace DAL
{
    public class Stores : ICRUD<data.Stores>
    {
        private Repository<data.Stores> _repo = null;
        public Stores(SolutionDbContext solutionDbContext)
        {
            _repo = new Repository<data.Stores>(solutionDbContext);
        }
        public void Delete(data.Stores t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.Stores> GetAll()
        {
            return _repo.GetAll();
        }

        public data.Stores GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(data.Stores t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.Stores t)
        {
            _repo.Update(t);
            _repo.Commit();
        }
    }
}
