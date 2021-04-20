using DAL.EF;
using DAL.DO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using data = DAL.DO.Objects;
using System.Threading.Tasks;

namespace BS
{
    public class Staffs : ICRUD<data.Staffs>
    {
        private SolutionDbContext _repo = null;

        public Staffs(SolutionDbContext solutionDbContext)
        {
            _repo = solutionDbContext;
        }
        public void Delete(data.Staffs t)
        {
            new DAL.Staffs(_repo).Delete(t);
        }

        public IEnumerable<data.Staffs> GetAll()
        {
            return new DAL.Staffs(_repo).GetAll();
        }

        public data.Staffs GetOneById(int id)
        {
            return new DAL.Staffs(_repo).GetOneById(id);
        }

        public void Insert(data.Staffs t)
        {
            new DAL.Staffs(_repo).Insert(t);
        }

        public void Update(data.Staffs t)
        {
            new DAL.Staffs(_repo).Update(t);
        }
        public async Task<IEnumerable<data.Staffs>> GetAllWithAsync()
        {
            return await new DAL.Staffs(_repo).GetAllWithAsync();
        }

        public async Task<data.Staffs> GetOneByIdWithAsync(int id)
        {
            return await new DAL.Staffs(_repo).GetOneByIdWithAsync(id);
        }
    }
}
