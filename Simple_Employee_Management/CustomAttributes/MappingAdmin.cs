using AutoMapper;
using Simple_Employee_Management.Data.Entities;
using Simple_Employee_Management.DTO;

namespace Simple_Employee_Management.CustomAttributes
{
    public class MappingAdmin : Profile
    {
        public MappingAdmin()
        {
            CreateMap<Admin, AdminDTO>();
            CreateMap<AdminDTO, Admin>();
        }
    }
}
