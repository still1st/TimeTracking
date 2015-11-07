using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeTracking.Common.Extensions;
using TimeTracking.Domain.DataAccess.Base;
using TimeTracking.Domain.DataAccess.Repositories;
using TimeTracking.Domain.Models;
using TimeTracking.Models;

namespace TimeTracking.Controllers
{
    [RoutePrefix("api/planworktime")]
    public class PlanWorktimeController : ApiController
    {
        public PlanWorktimeController(IPlanWorkDayRepository planWorkDayRepository,
            IHolidayRepository holidayRepository,
            IUnitOfWork unitOfWork)
        {
            _planWorkDayRepository = planWorkDayRepository;
            _holidayRepository = holidayRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(Mapper.Map<IEnumerable<PlanWorkDayModel>>(_planWorkDayRepository.Query().ToList()));
        }

        [HttpPost]
        public IHttpActionResult Post(PlanWorkDayModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var standartWorkday = Mapper.Map<PlanWorkDay>(model);
            _planWorkDayRepository.Add(standartWorkday);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<PlanWorkDayModel>(standartWorkday));
        }

        [HttpGet]
        [Route("calc")]
        public IHttpActionResult Calc(Int32 year)
        {
            var planWorkDays = _planWorkDayRepository.Query().ToList();
            var holidays = _holidayRepository.Query().Where(x => x.Date.Year == year).ToList();

            var models = new List<PlanWorkMonthModel>();
            for (int i = 1; i <= 12; i++)
            {
                var model = new PlanWorkMonthModel 
                {
                    Month = DateTimeFormatInfo.CurrentInfo.GetMonthName(i),
                };
                
                foreach (var planWorkDay in planWorkDays)
                {
                    var group = new GroupModel
                    {
                        Group = EnumExtensions.GetDescription(planWorkDay.EmployeeGroup),
                        Hours = GetBusinessDays(year, i) * planWorkDay.Hours
                    };

                    model.Groups.Add(group);
                }

                models.Add(model);
            }

            return Ok(models);
        }

        public static Double GetBusinessDays(Int32 year, Int32 month)
        {
            var start = GetFirstDay(year, month);
            var end = GetLastDay(year, month);

            double calcBusinessDays = 1 + ((end-start).TotalDays * 5 - (start.DayOfWeek-end.DayOfWeek) * 2) / 7;
            if ((int)end.DayOfWeek == 6) 
                calcBusinessDays--;
            if ((int)start.DayOfWeek == 0) 
                calcBusinessDays--;

            return calcBusinessDays;
        }

        public static DateTime GetFirstDay(Int32 year, Int32 month)
        {
            return new DateTime(year, month, 1);
        }

        public static DateTime GetLastDay(Int32 year, Int32 month)
        {
            return GetFirstDay(year, month).AddMonths(1).AddDays(-1);
        }

        #region private fields
        private IPlanWorkDayRepository _planWorkDayRepository;
        private IUnitOfWork _unitOfWork;
        private IHolidayRepository _holidayRepository;
        #endregion
    }
}
