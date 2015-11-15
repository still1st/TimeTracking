using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeTracking.Common.Extensions;
using TimeTracking.Domain.DataAccess.Base;
using TimeTracking.Domain.DataAccess.Repositories;
using TimeTracking.Domain.Enums;
using TimeTracking.Domain.Models;
using TimeTracking.Models;

namespace TimeTracking.Controllers
{
    [RoutePrefix("api/holiday")]
    public class HolidayController : ApiController
    {
        public HolidayController(IHolidayRepository holidayRepository,
            IUnitOfWork unitOfWork)
        {
            _holidayRepository = holidayRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IHttpActionResult Get(Int32 year)
        {
            var holidays = _holidayRepository.Query().Where(x => x.Date.Year == year).ToList();
            return Ok(Mapper.Map<IEnumerable<HolidayModel>>(holidays));
        }

        [HttpPost]
        public IHttpActionResult Post(HolidayModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (model.Date.Kind == DateTimeKind.Utc)
                model.Date = model.Date.ToLocalTime();

            var holiday = Mapper.Map<Holiday>(model);
            _holidayRepository.Add(holiday);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<HolidayModel>(holiday));
        }

        [HttpGet]
        [Route("Types")]
        public IHttpActionResult GetDayTypes()
        {
            return Ok(EnumExtensions.ToKeyValuePairs<DayType>());
        }

        #region private fields
        private IUnitOfWork _unitOfWork;
        private IHolidayRepository _holidayRepository; 
        #endregion
    }
}
