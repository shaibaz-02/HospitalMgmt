using AutoMapper;
using HospitalMgmt.Application.DTOs;
using HospitalMgmt.Web.ViewModels;

namespace HospitalMgmt.Web.Mappings
{
    public class DepartmentViewModelProfile : Profile
    {
        public DepartmentViewModelProfile()
        {
            CreateMap<DepartmentDto, DepartmentViewModel>();
            CreateMap<DepartmentDto, DepartmentFormViewModel>();   // Edit GET
            CreateMap<DepartmentFormViewModel, CreateDepartmentDto>();
            CreateMap<DepartmentFormViewModel, UpdateDepartmentDto>();
        }
    }
}