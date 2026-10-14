using HospitalMgmt.Application.Common;
using HospitalMgmt.Application.Interfaces;
using HospitalMgmt.Core.Entities;
using HospitalMgmt.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HospitalMgmt.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(IUnitOfWork unitOfWork, ILogger<DepartmentService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _unitOfWork.Departments.GetAllAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Departments.GetByIdAsync(id);
        }

        // public async Task<OperationResult> CreateAsync(Department department)
        // {
        //     // Task 5: unique-name check goes here
        //     await _unitOfWork.Departments.AddAsync(department);
        //     await _unitOfWork.SaveChangesAsync();
        //     return OperationResult.Ok();
        // }

        // public async Task<OperationResult> UpdateAsync(Department department)
        // {
        //     // Task 5: unique-name check goes here (exclude the department itself)
        //     _unitOfWork.Departments.Update(department);
        //     await _unitOfWork.SaveChangesAsync();
        //     return OperationResult.Ok();
        // }
        public async Task<OperationResult> CreateAsync(Department department)
        {
            department.Name = department.Name.Trim();

            if (await _unitOfWork.Departments.NameExistsAsync(department.Name))
                return OperationResult.Fail($"A department named '{department.Name}' already exists.");

            await _unitOfWork.Departments.AddAsync(department);
            await _unitOfWork.SaveChangesAsync();
            return OperationResult.Ok();
        }

        public async Task<OperationResult> UpdateAsync(Department department)
        {
            department.Name = department.Name.Trim();

            if (await _unitOfWork.Departments.NameExistsAsync(department.Name, department.Id))
                return OperationResult.Fail($"A department named '{department.Name}' already exists.");

            _unitOfWork.Departments.Update(department);
            await _unitOfWork.SaveChangesAsync();
            return OperationResult.Ok();
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null)
                return OperationResult.Fail("Department not found.");

            // Business rule: prevent the known failure
            if (await _unitOfWork.Departments.HasDoctorsAsync(id))
                return OperationResult.Fail("Cannot delete this department because doctors are assigned to it.");

            try
            {
                _unitOfWork.Departments.Delete(department);
                await _unitOfWork.SaveChangesAsync();
                return OperationResult.Ok();
            }
            catch (DbUpdateException ex)
            {
                // Safety net: race conditions, timeouts
                _logger.LogError(ex, "Failed to delete department {DepartmentId}", id);
                return OperationResult.Fail("Could not delete the department. Please try again or contact support.");
            }
        }
    }
}