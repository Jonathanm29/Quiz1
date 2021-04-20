using DAL.EF;
using DAL.DO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using data = DAL.DO.Objects;


namespace BS
{
    public class Customers : ICRUD<data.Customers>
    {
        private SolutionDbContext context;
        public Customers(SolutionDbContext _context)
        {
            context = _context;
        }
        public void Delete(data.Customers t)
        {
            new DAL.Customers(context).Delete(t);
        }

        public IEnumerable<data.Customers> GetAll()
        {
            return new DAL.Customers(context).GetAll();
        }

        public data.Customers GetOneById(int id)
        {
            return new DAL.Customers(context).GetOneById(id);
        }

        public void Insert(data.Customers t)
        {
            new DAL.Customers(context).Insert(t);
        }

        public void Update(data.Customers t)
        {
            new DAL.Customers(context).Update(t);
        }
    }
}
