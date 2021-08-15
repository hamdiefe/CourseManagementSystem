using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using CourseManagementSystem.Controllers;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Services.Jobs;
using System.Threading.Tasks;
using CourseManagementSystem.Web.Models.Jobs;
using CourseManagementSystem.Services.Jobs.Dtos;
using Abp.Application.Services.Dto;

namespace CourseManagementSystem.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Jobs)]
    public class JobsController : CourseManagementSystemControllerBase
    {
        private readonly IJobsAppService _jobsAppService;

        public JobsController(IJobsAppService jobsAppService)
        {
            _jobsAppService = jobsAppService;
        }

        public ActionResult Index()
        {
            var model = new JobsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            GetJobForEditOutput getJobForEditOutput;

            if (id.HasValue)
            {
                getJobForEditOutput = await _jobsAppService.GetJobForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getJobForEditOutput = new GetJobForEditOutput
                {
                    Job = new CreateOrEditJobDto()
                };
            }

            var viewModel = new CreateOrEditJobModalViewModel()
            {
                Job = getJobForEditOutput.Job,
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }   

        public async Task<PartialViewResult> ViewModal(int id)
        {
            var getJobForViewDto = await _jobsAppService.GetJobForView(id);

            var model = new JobViewModel()
            {
                Job = getJobForViewDto.Job
            };

            return PartialView("_ViewModal", model);
        }
    }
}
