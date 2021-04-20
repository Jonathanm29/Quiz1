using DAL.EF;
using DAL.DO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using data = DAL.DO.Objects;

namespace BS
{
    public class Stores : ICRUD<data.Stores>
    {
        private SolutionDbContext context;
        public Stores(SolutionDbContext _context)
        {
            context = _context;
        }

        public void Delete(data.Stores t)
        {
            new DAL.Stores(context).Delete(t);
        }

        public IEnumerable<data.Stores> GetAll()
        {
            return new DAL.Stores(context).GetAll();
        }

        public data.Stores GetOneById(int id)
        {
            return new DAL.Stores(context).GetOneById(id);
        }

        public void Insert(data.Stores t)
        {
            new DAL.Stores(context).Insert(t);
        }

        public void Update(data.Stores t)
        {
            new DAL.Stores(context).Update(t);
        }
    }
}
