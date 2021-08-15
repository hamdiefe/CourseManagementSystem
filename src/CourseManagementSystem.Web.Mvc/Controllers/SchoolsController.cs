using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using CourseManagementSystem.Controllers;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Services.Schools;
using System.Threading.Tasks;
using CourseManagementSystem.Web.Models.Schools;
using CourseManagementSystem.Services.Schools.Dtos;
using Abp.Application.Services.Dto;

namespace CourseManagementSystem.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Schools)]
    public class SchoolsController : CourseManagementSystemControllerBase
    {
        private readonly ISchoolsAppService _schoolsAppService;

        public SchoolsController(ISchoolsAppService schoolsAppService)
        {
            _schoolsAppService = schoolsAppService;
        }

        public ActionResult Index()
        {
            var model = new SchoolsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            GetSchoolForEditOutput getSchoolForEditOutput;

            if (id.HasValue)
            {
                getSchoolForEditOutput = await _schoolsAppService.GetSchoolForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getSchoolForEditOutput = new GetSchoolForEditOutput
                {
                    School = new CreateOrEditSchoolDto()
                };
            }

            var viewModel = new CreateOrEditSchoolModalViewModel()
            {
                School = getSchoolForEditOutput.School,
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }   

        public async Task<PartialViewResult> ViewModal(int id)
        {
            var getSchoolForViewDto = await _schoolsAppService.GetSchoolForView(id);

            var model = new SchoolViewModel()
            {
                School = getSchoolForViewDto.School
            };

            return PartialView("_ViewModal", model);
        }
    }
}
