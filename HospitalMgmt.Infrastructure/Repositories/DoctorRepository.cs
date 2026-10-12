using HospitalMgmt.Core.Entities;
using HospitalMgmt.Core.Interfaces;
using HospitalMgmt.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HospitalMgmt.Infrastructure.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(HospitalDbContext context) : base(context) { }

        public async Task<IEnumerable<Doctor>> GetAllWithDepartmentAsync()
        {
            return await _context.Doctors
                .Include(d => d.Department)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Doctor?> GetByIdWithDepartmentAsync(int id)
        {
            return await _context.Doctors
                .Include(d => d.Department)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}