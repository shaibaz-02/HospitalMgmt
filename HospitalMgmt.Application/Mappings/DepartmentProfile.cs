using AutoMapper;
using HospitalMgmt.Application.DTOs;
using HospitalMgmt.Core.Entities;

namespace HospitalMgmt.Application.Mappings
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>();

            CreateMap<CreateDepartmentDto, Department>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedOn, o => o.Ignore())
                .ForMember(d => d.Doctors, o => o.Ignore());

            CreateMap<UpdateDepartmentDto, Department>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedOn, o => o.Ignore())
                .ForMember(d => d.Doctors, o => o.Ignore());
        }
    }
}