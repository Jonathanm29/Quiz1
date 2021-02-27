using System;
using System.Collections.Generic;
using System.Text;
using Solution.DAL.EF;
using Solution.DO.Interfaces;
using Solution.DAL.Reposity;
using data = Solution.DO.Objects;

namespace Solution.BS
{
    class Products : ICRUD<data.Products>
    {

        private SolutionDBContext context;
        public Products(SolutionDBContext _context)
        {
            context = _context;
        }
        public void Delete(data.Products t)
        {
            new DAL.Products(context).Delete(t);
        }

        public IEnumerable<data.Products> GetAll()
        {
            return new DAL.Products(context).GetAll();
        }

        public data.Products GetOneById(int id)
        {
            return new DAL.Products(context).GetOneById(id);
        }

        public void Insert(data.Products t)
        {
            new DAL.Products(context).Insert(t);
        }

        public void Update(data.Products t)
        {
            new DAL.Products(context).Update(t);
        }
    }
}
