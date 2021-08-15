using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using CourseManagementSystem.Controllers;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Services.Events;
using System.Threading.Tasks;
using CourseManagementSystem.Web.Models.Events;
using CourseManagementSystem.Services.Events.Dtos;
using Abp.Application.Services.Dto;
using System;

namespace CourseManagementSystem.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Events)]
    public class EventsController : CourseManagementSystemControllerBase
    {
        private readonly IEventsAppService _eventsAppService;

        public EventsController(IEventsAppService eventsAppService)
        {
            _eventsAppService = eventsAppService;
        }

        public ActionResult Index()
        {
            var model = new EventsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEventForEditOutput getEventForEditOutput;

            if (id.HasValue)
            {
                getEventForEditOutput = await _eventsAppService.GetEventForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getEventForEditOutput = new GetEventForEditOutput
                {
                    Event = new CreateOrEditEventDto(),
                };
            }

            var viewModel = new CreateOrEditEventModalViewModel()
            {
                Event = getEventForEditOutput.Event,
                EventStudentList = await _eventsAppService.GetStudentsForTableDropdown(),
                EventTeacherList = await _eventsAppService.GetTeachersForTableDropdown()
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewModal(int id)
        {
            var getEventForViewDto = await _eventsAppService.GetEventForView(id);

            var model = new EventViewModel()
            {
                Event = getEventForViewDto.Event
            };

            return PartialView("_ViewModal", model);
        }

        public PartialViewResult AddStartDateField()
        {
            var viewModel = new AddEventStartDateFieldViewModel
            {

            };
            return PartialView("_AddStartDateField", viewModel);
        }

        public PartialViewResult AddEndDateField(string startDate, string selectedEndDate)
        {
            var viewModel = new AddEventEndDateFieldViewModel
            {
                MinDate = Convert.ToDateTime(startDate).AddHours(1)
            };

            if (selectedEndDate != null)
            {
                viewModel.SelectedEndDate = Convert.ToDateTime(selectedEndDate);
            }

            return PartialView("_AddEndDateField", viewModel);
        }
    }
}
