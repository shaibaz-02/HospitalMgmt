using System.Linq.Expressions;
using HospitalMgmt.Core.Entities;
using HospitalMgmt.Core.Interfaces;
using HospitalMgmt.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HospitalMgmt.Infrastructure.Repositories
{
    public class DepartmentRepository: GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(HospitalDbContext context):base(context)
        {
            
        }
        public async Task<IEnumerable<Department>> GetActiveDepartmentsAsync()
{
    return await _dbset
        .Where(d => d.IsActive)
        .ToListAsync();
}
    }
}