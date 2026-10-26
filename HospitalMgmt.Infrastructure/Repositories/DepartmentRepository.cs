using System.Linq.Expressions;
using HospitalMgmt.Core.Entities;
using HospitalMgmt.Core.Interfaces;
using HospitalMgmt.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HospitalMgmt.Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(HospitalDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Department>> GetActiveDepartmentsAsync()
        {
            return await _dbset
                .Where(d => d.IsActive)
                .ToListAsync();
        }
        public async Task<bool> HasDoctorsAsync(int departmentId)
        {
            return await _context.Doctors.AnyAsync(d => d.DepartmentId == departmentId);
        }
        public async Task<bool> NameExistsAsync(string name, int? excludeId = null)
        {
            var normalized = name.Trim().ToLower();

            return await _context.Departments.AnyAsync(d =>
                d.Name.ToLower() == normalized &&
                (excludeId == null || d.Id != excludeId));
        }
    }
}