namespace TimeTracking.Domain.DataAccess.Migrations
{
    using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using TimeTracking.Domain.Enums;
using TimeTracking.Domain.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TimeTrackingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TimeTrackingContext context)
        {
            context.Set<Employee>().AddOrUpdate(x => x.LastName, GetEmployees().ToArray());
            context.SaveChanges();

            context.Set<PlanWorkDay>().AddOrUpdate(x => x.EmployeeGroup, GetPlanWorkDays().ToArray());
            context.SaveChanges();

            context.Set<Holiday>().AddOrUpdate(x => x.Date, GetHolidays().ToArray());
            context.SaveChanges();
        }

        private const Int32 YEAR = 2015;

        private IEnumerable<Holiday> GetHolidays()
        {
            var holidays = new List<Holiday>();
            for (int i = 1; i <= 8; i++)
                holidays.Add(CreateHoliday(1, i));

            // holidays
            holidays.Add(CreateHoliday(2, 23));
            holidays.Add(CreateHoliday(3, 8));
            holidays.Add(CreateHoliday(5, 1));
            holidays.Add(CreateHoliday(5, 9));
            holidays.Add(CreateHoliday(6, 12));
            holidays.Add(CreateHoliday(11, 4));

            // preholidays
            holidays.Add(CreatePreholiday(4, 30));
            holidays.Add(CreatePreholiday(5, 8));
            holidays.Add(CreatePreholiday(6, 11));
            holidays.Add(CreatePreholiday(11, 3));
            holidays.Add(CreatePreholiday(12, 31));

            return holidays;
        }

        private Holiday CreatePreholiday(Int32 month, Int32 day)
        {
            return CreateHoliday(month, day, DayType.Preholiday);
        }

        private Holiday CreateHoliday(Int32 month, Int32 day)
        {
            return CreateHoliday(month, day, DayType.Holiday);
        }

        private Holiday CreateHoliday(Int32 month, Int32 day, DayType type)
        {
            return new Holiday { 
                Date = new DateTime(YEAR, month, day),
                Type = type
            };
        }

        private IEnumerable<PlanWorkDay> GetPlanWorkDays()
        {
            return new List<PlanWorkDay>
            {
                new PlanWorkDay
                {
                    Hours = 7.8,
                    EmployeeGroup = Enums.EmployeeGroup.JuniorStaff
                },
                new PlanWorkDay
                {
                    Hours = 7.7,
                    EmployeeGroup = Enums.EmployeeGroup.MiddleStaffAndDoctors
                }
            };
        }

        private IEnumerable<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee
                {
                    LastName = "Иванова", 
                    FirstName = "Мария",
                    MiddleName = "Ивановна",
                    Post = EmployeePost.JuniorNurse,
                    Group = Enums.EmployeeGroup.JuniorStaff
                },
                new Employee
                {
                    LastName = "Петрова",
                    FirstName = "Антонина",
                    MiddleName = "Петровна",
                    Post = EmployeePost.Nurse,
                    Group = Enums.EmployeeGroup.MiddleStaffAndDoctors
                },
                new Employee
                {
                    LastName = "Елизарова",
                    FirstName = "Елизавета",
                    MiddleName = "Михайловна",
                    Post = EmployeePost.Nurse,
                    Group = Enums.EmployeeGroup.MiddleStaffAndDoctors
                },
            };
        }
    }
}
