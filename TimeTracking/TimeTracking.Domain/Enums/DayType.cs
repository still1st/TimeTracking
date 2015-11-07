using System.ComponentModel;

namespace TimeTracking.Domain.Enums
{
    public enum DayType
    {
        [Description("Праздничный день")]
        Holiday = 1,
        [Description("Предпраздничный день")]
        Preholiday = 2
    }
}
