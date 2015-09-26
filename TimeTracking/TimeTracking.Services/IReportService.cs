using ClosedXML.Excel;
using TimeTracking.Domain.Models;
using TimeTracking.Services.Reports;

namespace TimeTracking.Services
{
    public interface IReportService
    {
        Report BuildReport(Table table);

        XLWorkbook GetExcel(Report report);
    }
}
