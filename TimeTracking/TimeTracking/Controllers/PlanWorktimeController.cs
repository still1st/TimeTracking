using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using TimeTracking.Common.Extensions;
using TimeTracking.Domain.DataAccess.Base;
using TimeTracking.Domain.DataAccess.Repositories;
using TimeTracking.Domain.Enums;
using TimeTracking.Domain.Models;
using TimeTracking.Models;
using TimeTracking.Services;

namespace TimeTracking.Controllers
{
    [RoutePrefix("api/planworktime")]
    public class PlanWorktimeController : ApiController
    {
        public PlanWorktimeController(IPlanWorkDayRepository planWorkDayRepository,
            ITableService tableService,
            IUnitOfWork unitOfWork)
        {
            _planWorkDayRepository = planWorkDayRepository;
            _tableService = tableService;
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

            if (_planWorkDayRepository.Query().Any(x => x.EmployeeGroup == (EmployeeGroup)model.GroupId))
                return BadRequest("Plan work time for the employee group already exists");

            var standartWorkday = Mapper.Map<PlanWorkDay>(model);
            _planWorkDayRepository.Add(standartWorkday);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<PlanWorkDayModel>(standartWorkday));
        }

        [HttpDelete]
        public IHttpActionResult Delete(Int64 id)
        {
            var planworktime = _planWorkDayRepository.GetById(id);
            if (planworktime == null)
                return NotFound();

            _planWorkDayRepository.Delete(planworktime);
            _unitOfWork.Commit();

            return Ok();
        }

        [HttpGet]
        [Route("calc")]
        public IHttpActionResult Calc(Int32 year)
        {
            var planWorkDays = _planWorkDayRepository.Query().ToList();

            var models = new List<PlanWorkMonthModel>();
            for (int i = 1; i <= 12; i++)
            {
                var model = new PlanWorkMonthModel  
                {
                    Month = DateTimeFormatInfo.CurrentInfo.GetMonthName(i),
                };
                
                foreach (var planWorkDay in planWorkDays)
                    model.Groups.Add(CreateGroupModel(year, i, planWorkDay));

                models.Add(model);
            }

            return Ok(models);
        }

        private GroupModel CreateGroupModel(Int32 year, Int32 month, PlanWorkDay planWorkDay)
        {
            return new GroupModel
            {
                Group = EnumExtensions.GetDescription(planWorkDay.EmployeeGroup),
                Hours = _tableService.CalcMonth(year, month, planWorkDay.Hours)
            };
        }

        #region private fields
        private IPlanWorkDayRepository _planWorkDayRepository;
        private IUnitOfWork _unitOfWork;
        private ITableService _tableService;
        #endregion
    }
}
