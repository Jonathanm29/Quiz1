using DAL.EF;
using DAL.Repository;
using DAL.DO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using data = DAL.DO.Objects;

namespace DAL
{
    public class Customers : ICRUD<data.Customers>
    {
        private Repository<data.Customers> _repo = null;
        public Customers(SolutionDbContext solutionDbContext)
        {
            _repo = new Repository<data.Customers>(solutionDbContext);
        }
        public void Delete(data.Customers t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.Customers> GetAll()
        {
            return _repo.GetAll();
        }

        public data.Customers GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(data.Customers t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.Customers t)
        {
            _repo.Update(t);
            _repo.Commit();
        }
    }
}
