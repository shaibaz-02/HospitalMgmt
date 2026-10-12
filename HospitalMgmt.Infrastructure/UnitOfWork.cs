using HospitalMgmt.Core.Interfaces;
using HospitalMgmt.Infrastructure.Data;
using HospitalMgmt.Infrastructure.Repositories;

namespace HospitalMgmt.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HospitalDbContext _context;

        public IDoctorRepository Doctors { get; private set; }
        public IDepartmentRepository Departments { get; private set; }

        public UnitOfWork(HospitalDbContext context)
        {
            _context = context;
            Doctors = new DoctorRepository(_context);
            Departments = new DepartmentRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}