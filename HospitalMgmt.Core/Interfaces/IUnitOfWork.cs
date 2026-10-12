using System;
using System.Threading.Tasks;

namespace HospitalMgmt.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
         IDoctorRepository Doctors { get; }
        IDepartmentRepository Departments { get; }

        Task<int> SaveChangesAsync();
    }
}