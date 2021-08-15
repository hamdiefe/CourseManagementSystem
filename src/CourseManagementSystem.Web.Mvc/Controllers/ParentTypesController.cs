using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using CourseManagementSystem.Controllers;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Services.ParentTypes;
using System.Threading.Tasks;
using CourseManagementSystem.Web.Models.ParentTypes;
using CourseManagementSystem.Services.ParentTypes.Dtos;
using Abp.Application.Services.Dto;

namespace CourseManagementSystem.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_ParentTypes)]
    public class ParentTypesController : CourseManagementSystemControllerBase
    {
        private readonly IParentTypesAppService _parentTypesAppService;

        public ParentTypesController(IParentTypesAppService parentTypesAppService)
        {
            _parentTypesAppService = parentTypesAppService;
        }

        public ActionResult Index()
        {
            var model = new ParentTypesViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            GetParentTypeForEditOutput getParentTypeForEditOutput;

            if (id.HasValue)
            {
                getParentTypeForEditOutput = await _parentTypesAppService.GetParentTypeForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getParentTypeForEditOutput = new GetParentTypeForEditOutput
                {
                    ParentType = new CreateOrEditParentTypeDto()
                };
            }

            var viewModel = new CreateOrEditParentTypeModalViewModel()
            {
                ParentType = getParentTypeForEditOutput.ParentType,
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }   

        public async Task<PartialViewResult> ViewModal(int id)
        {
            var getParentTypeForViewDto = await _parentTypesAppService.GetParentTypeForView(id);

            var model = new ParentTypeViewModel()
            {
                ParentType = getParentTypeForViewDto.ParentType
            };

            return PartialView("_ViewModal", model);
        }
    }
}
