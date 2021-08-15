using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using CourseManagementSystem.Controllers;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Services.Parents;
using System.Threading.Tasks;
using CourseManagementSystem.Web.Models.Parents;
using CourseManagementSystem.Services.Parents.Dtos;
using Abp.Application.Services.Dto;

namespace CourseManagementSystem.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Parents)]
    public class ParentsController : CourseManagementSystemControllerBase
    {
        private readonly IParentsAppService _parentsAppService;

        public ParentsController(IParentsAppService parentsAppService)
        {
            _parentsAppService = parentsAppService;
        }

        public ActionResult Index()
        {
            var model = new ParentsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            GetParentForEditOutput getParentForEditOutput;

            if (id.HasValue)
            {
                getParentForEditOutput = await _parentsAppService.GetParentForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getParentForEditOutput = new GetParentForEditOutput
                {
                    Parent = new CreateOrEditParentDto()
                };
            }

            var viewModel = new CreateOrEditParentModalViewModel()
            {
                Parent = getParentForEditOutput.Parent,
                JobName = getParentForEditOutput.JobName,
                ParentTypeName = getParentForEditOutput.ParentTypeName,
                ParentJobList = await _parentsAppService.GetJobsForTableDropdown(),
                ParentParentTypeList = await _parentsAppService.GetParentTypesForTableDropdown(),
                ParentCountryList = await _parentsAppService.GetCountriesForTableDropdown(),
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewModal(int id)
        {
            var getParentForViewDto = await _parentsAppService.GetParentForView(id);

            var model = new ParentViewModel()
            {
                Parent = getParentForViewDto.Parent,
                JobName = getParentForViewDto.JobName,
                ParentTypeName = getParentForViewDto.ParentTypeName
            };

            return PartialView("_ViewModal", model);
        }

        public async Task<PartialViewResult> AddressCityDropdown(int? countryId, int? selectedValue)
        {
            var viewModel = new ParentAddressCityDropdownViewModel();

            if (countryId != null)
            {
                viewModel.ParentCityList = await _parentsAppService.GetCitiesForTableDropdown(countryId);
                viewModel.SelectedValue = selectedValue;
            }

            return PartialView("_AddressCityDropdown", viewModel);
        }

        public async Task<PartialViewResult> AddressTownDropdown(int? cityId, int? selectedValue)
        {
            var viewModel = new ParentAddressTownDropdownViewModel();

            if (cityId != null)
            {
                viewModel.ParentTownList = await _parentsAppService.GetTownsForTableDropdown(cityId);
                viewModel.SelectedValue = selectedValue;
            }

            return PartialView("_AddressTownDropdown", viewModel);
        }

        public async Task<PartialViewResult> AddAddressField()
        {
            var viewModel = new AddParentAddressFieldViewModel
            {
                ParentCountryList = await _parentsAppService.GetCountriesForTableDropdown()
            };
            return PartialView("_AddAddressField", viewModel);
        }

        public async Task<PartialViewResult> AddPhoneField()
        {
            var viewModel = new AddParentPhoneFieldViewModel
            {
                ParentCountryList = await _parentsAppService.GetCountriesForTableDropdown()
            };
            return PartialView("_AddPhoneField", viewModel);
        }
    }
}
