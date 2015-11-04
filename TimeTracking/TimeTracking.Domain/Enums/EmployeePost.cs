using System.ComponentModel;

namespace TimeTracking.Domain.Models
{
    public enum EmployeePost
    {
        [Description("Санитарка")]
        JuniorNurse = 1,

        [Description("Медсестра")]
        Nurse = 2,

        [Description("Старшая медсестра")]
        HeadNurse = 3,

        [Description("Главная медсестра")]
        ChiefNurse = 4,

        [Description("Врач-терепевт")]
        Doctor = 5
    }
}
