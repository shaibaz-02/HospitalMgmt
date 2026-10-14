using HospitalMgmt.Core.Entities;

namespace HospitalMgmt.Core.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<IEnumerable<Department>> GetActiveDepartmentsAsync();
        Task<bool> HasDoctorsAsync(int departmentId);
        Task<bool> NameExistsAsync(string name, int? excludeId = null);
    }
}