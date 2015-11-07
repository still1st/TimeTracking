using System.ComponentModel;

namespace TimeTracking.Domain.Enums
{
    public enum EmployeeGroup
    {
        [Description("Младший медперсонал")]
        JuniorStaff = 1,
        [Description("Средний медперсонал и врачи")]
        MiddleStaffAndDoctors = 2
    }
}
