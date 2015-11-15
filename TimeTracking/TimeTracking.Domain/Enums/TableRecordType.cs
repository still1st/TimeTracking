using System.ComponentModel;

namespace TimeTracking.Domain.Models
{
    public enum TableRecordType
    {
        NotSpecified = 0,

        /// <summary>
        /// Явка
        /// </summary>
        [Description("я")]
        Appearance = 1,

        /// <summary>
        /// Выходной
        /// </summary>
        [Description("в")]
        DayOff = 2,

        /// <summary>
        /// Отпуск
        /// </summary>
        [Description("от")]
        Vacation = 3,

        /// <summary>
        /// Без содержания
        /// </summary>
        [Description("до")]
        WithoutContent = 4,

        /// <summary>
        /// Больничный
        /// </summary>
        [Description("бл")]
        Hospital = 5,
    }
}
