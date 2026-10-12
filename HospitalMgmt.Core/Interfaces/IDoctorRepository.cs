using HospitalMgmt.Core.Entities;

namespace HospitalMgmt.Core.Interfaces
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        Task<IEnumerable<Doctor>> GetAllWithDepartmentAsync();
        Task<Doctor?> GetByIdWithDepartmentAsync(int id);
    }
}