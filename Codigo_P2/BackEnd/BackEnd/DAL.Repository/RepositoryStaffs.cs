using System.Threading.Tasks;
using data = DAL.DO.Objects;
using Microsoft.EntityFrameworkCore;
using DAL.EF;
using System.Collections.Generic;

namespace DAL.Repository
{
    public class RepositoryStaffs : Repository<data.Staffs>, IRepositoryStaffs
    {
        public RepositoryStaffs(SolutionDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<data.Staffs>> GetAllWithAsAsync()
        {
            return await _db.Staffs
                .Include(m => m.Store)
                .ToListAsync();
        }

        public async Task<data.Staffs> GetByOneWithAsAsync(int id)
        {
            return await _db.Staffs
              .Include(m => m.Store)
             .SingleOrDefaultAsync(m => m.StaffId == id);
        }

        //Metodo para obtener el context cargado del repository y asi utilizarlo en esta clase
        private SolutionDbContext _db
        {
            get { return dbContext as SolutionDbContext; }
        }
    }
}