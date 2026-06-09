using AutoMapper;
using Simple_Employee_Management.Data.Entities;
using Simple_Employee_Management.DTO;

namespace Simple_Employee_Management.CustomAttributes
{
    public class MappingEmployee : Profile
    {
        public MappingEmployee()
        {
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO, Employee>();
        }
    }
}
