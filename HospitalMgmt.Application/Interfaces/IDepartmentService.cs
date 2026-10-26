using HospitalMgmt.Application.Common;
using HospitalMgmt.Application.DTOs;

namespace HospitalMgmt.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllAsync();
        Task<DepartmentDto?> GetByIdAsync(int id);
        Task<OperationResult> CreateAsync(CreateDepartmentDto dto);
        Task<OperationResult> UpdateAsync(UpdateDepartmentDto dto);
        Task<OperationResult> DeleteAsync(int id);
    }
}