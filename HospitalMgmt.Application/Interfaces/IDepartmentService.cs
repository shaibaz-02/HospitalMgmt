using HospitalMgmt.Application.Common;
using HospitalMgmt.Core.Entities;

namespace HospitalMgmt.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task<OperationResult> CreateAsync(Department department);
        Task<OperationResult> UpdateAsync(Department department);
        Task<OperationResult> DeleteAsync(int id);
    }
}