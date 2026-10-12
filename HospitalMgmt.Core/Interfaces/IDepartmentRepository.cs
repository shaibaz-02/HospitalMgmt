using HospitalMgmt.Core.Entities;

namespace HospitalMgmt.Core.Interfaces
{
   public interface IDepartmentRepository: IGenericRepository<Department>
    {
        Task<IEnumerable<Department>> GetActiveDepartmentsAsync();
        
    }
}