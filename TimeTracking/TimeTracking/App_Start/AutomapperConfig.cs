using AutoMapper;
using System;
using TimeTracking.Domain.Models;
using TimeTracking.Models;
using TimeTracking.Common.Extensions;
using TimeTracking.Domain.Enums;
using System.Globalization;

namespace TimeTracking.App_Start
{
    public static class AutomapperConfig
    {
        public static void Configure()
        {
            // Employee
            Mapper.CreateMap<Employee, EmployeeModel>()
                .ForMember(m => m.PostId, opt => opt.MapFrom(x => (Int32)x.Post))
                .ForMember(m => m.Post, opt => opt.MapFrom(x => x.Post.GetDescription()))
                .ForMember(m => m.Group, opt => opt.MapFrom(x => x.Group.GetDescription()))
                .ForMember(m => m.FullName, opt => opt.MapFrom(x => x.LastName + ' ' + x.FirstName + ' ' + x.MiddleName)) ;

            Mapper.CreateMap<EmployeeModel, Employee>()
                .ForMember(m => m.Post, opt => opt.MapFrom(x => (EmployeePost)x.PostId));

            // Holiday
            Mapper.CreateMap<Holiday, HolidayModel>()
                .ForMember(m => m.TypeId, opt => opt.MapFrom(x => (Int32)x.Type))
                .ForMember(m => m.Type, opt => opt.MapFrom(x => x.Type.GetDescription()));

            Mapper.CreateMap<HolidayModel, Holiday>()
                .ForMember(m => m.Type, opt => opt.MapFrom(x => (DayType)x.TypeId));

            // Plan workday
            Mapper.CreateMap<PlanWorkDay, PlanWorkDayModel>()
                .ForMember(m => m.GroupId, opt => opt.MapFrom(x => (Int32)x.EmployeeGroup))
                .ForMember(m => m.Group, opt => opt.MapFrom(x => x.EmployeeGroup.GetDescription()));

            Mapper.CreateMap<PlanWorkDayModel, PlanWorkDay>()
                .ForMember(m => m.EmployeeGroup, opt => opt.MapFrom(x => (EmployeeGroup)x.GroupId));

            // Table
            Mapper.CreateMap<TableModel, Table>();
            Mapper.CreateMap<Table, TableInfoModel>()
                .ForMember(m => m.Month, opt => opt.MapFrom(x => DateTimeFormatInfo.CurrentInfo.GetMonthName(x.Month)));

            // Table record
            Mapper.CreateMap<TableRecord, TableRecordModel>()
                .ForMember(m => m.IsDayoff, opt => opt.MapFrom(x => x.Type == TableRecordType.DayOff));

            Mapper.CreateMap<TableRecordModel, TableRecord>()
                .ForMember(m => m.Type, opt => opt.MapFrom(x => x.IsDayoff ? TableRecordType.DayOff : TableRecordType.Appearance));
        }
    }
}