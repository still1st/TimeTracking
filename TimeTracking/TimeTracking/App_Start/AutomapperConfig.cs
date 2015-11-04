using AutoMapper;
using System;
using TimeTracking.Domain.Models;
using TimeTracking.Models;
using TimeTracking.Common.Extensions;

namespace TimeTracking.App_Start
{
    public static class AutomapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<Employee, EmployeeModel>()
                .ForMember(m => m.PostId, opt => opt.MapFrom(x => (Int32)x.Post))
                .ForMember(m => m.Post, opt => opt.MapFrom(x => x.Post.GetDescription()));

            Mapper.CreateMap<EmployeeModel, Employee>()
                .ForMember(m => m.Post, opt => opt.MapFrom(x => (EmployeePost)x.PostId));
        }
    }
}