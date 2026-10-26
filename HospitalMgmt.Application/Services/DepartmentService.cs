using AutoMapper;
using HospitalMgmt.Application.Common;
using HospitalMgmt.Application.DTOs;
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
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DepartmentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            var departments = await _unitOfWork.Departments.GetAllAsync();
            return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }

        public async Task<DepartmentDto?> GetByIdAsync(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            return department == null ? null : _mapper.Map<DepartmentDto>(department);
        }

        public async Task<OperationResult> CreateAsync(CreateDepartmentDto dto)
        {
            var name = dto.Name.Trim();

            if (await _unitOfWork.Departments.NameExistsAsync(name))
                return OperationResult.Fail($"A department named '{name}' already exists.");

            var department = _mapper.Map<Department>(dto);
            department.Name = name;

            await _unitOfWork.Departments.AddAsync(department);
            await _unitOfWork.SaveChangesAsync();
            return OperationResult.Ok();
        }

        public async Task<OperationResult> UpdateAsync(UpdateDepartmentDto dto)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(dto.Id);
            if (department == null)
                return OperationResult.Fail("Department not found.");

            var name = dto.Name.Trim();

            if (await _unitOfWork.Departments.NameExistsAsync(name, dto.Id))
                return OperationResult.Fail($"A department named '{name}' already exists.");

            _mapper.Map(dto, department);   // copies DTO values ONTO the loaded entity
            department.Name = name;

            await _unitOfWork.SaveChangesAsync();
            return OperationResult.Ok();
        }

        // DeleteAsync stays exactly as it is
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

