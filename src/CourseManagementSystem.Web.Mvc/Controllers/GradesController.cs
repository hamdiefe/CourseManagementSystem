using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using CourseManagementSystem.Controllers;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Services.Grades;
using System.Threading.Tasks;
using CourseManagementSystem.Web.Models.Grades;
using CourseManagementSystem.Services.Grades.Dtos;
using Abp.Application.Services.Dto;

namespace CourseManagementSystem.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Grades)]
    public class GradesController : CourseManagementSystemControllerBase
    {
        private readonly IGradesAppService _gradesAppService;

        public GradesController(IGradesAppService gradesAppService)
        {
            _gradesAppService = gradesAppService;
        }

        public ActionResult Index()
        {
            var model = new GradesViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            GetGradeForEditOutput getGradeForEditOutput;

            if (id.HasValue)
            {
                getGradeForEditOutput = await _gradesAppService.GetGradeForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getGradeForEditOutput = new GetGradeForEditOutput
                {
                    Grade = new CreateOrEditGradeDto()
                };
            }

            var viewModel = new CreateOrEditGradeModalViewModel()
            {
                Grade = getGradeForEditOutput.Grade,
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }   

        public async Task<PartialViewResult> ViewModal(int id)
        {
            var getGradeForViewDto = await _gradesAppService.GetGradeForView(id);

            var model = new GradeViewModel()
            {
                Grade = getGradeForViewDto.Grade
            };

            return PartialView("_ViewModal", model);
        }
    }
}
