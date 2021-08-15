using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Controllers;
using CourseManagementSystem.Services.Parents;
using CourseManagementSystem.Services.Teachers;
using CourseManagementSystem.Services.Teachers.Dtos;
using CourseManagementSystem.Web.Models.Teachers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CourseManagementSystem.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Teachers)]
    public class TeachersController : CourseManagementSystemControllerBase
    {
        private readonly ITeachersAppService _teachersAppService;
        private readonly IParentsAppService _parentsAppService;


        public TeachersController(ITeachersAppService teachersAppService,
                                  IParentsAppService parentsAppService)
        {
            _teachersAppService = teachersAppService;
            _parentsAppService = parentsAppService;
        }

        public ActionResult Index()
        {
            var model = new TeachersViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            GetTeacherForEditOutput getTeacherForEditOutput;

            if (id.HasValue)
            {
                getTeacherForEditOutput = await _teachersAppService.GetTeacherForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getTeacherForEditOutput = new GetTeacherForEditOutput
                {
                    Teacher = new CreateOrEditTeacherDto()
                };
                getTeacherForEditOutput.Teacher.IsActive = true;
            }

            var viewModel = new CreateOrEditTeacherModalViewModel()
            {
                Teacher = getTeacherForEditOutput.Teacher,
                GraduationSchoolName = getTeacherForEditOutput.GraduationSchoolName,
                TeacherGraduationSchoolList = await _teachersAppService.GetGraduationSchoolsForTableDropdown(),
                TeacherCountryList = await _teachersAppService.GetCountriesForTableDropdown(),
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewModal(int id)
        {
            var getTeacherForViewDto = await _teachersAppService.GetTeacherForView(id);

            var model = new TeacherViewModel()
            {
                Teacher = getTeacherForViewDto.Teacher,
                GraduationSchoolName = getTeacherForViewDto.GraduationSchoolName
            };

            return PartialView("_ViewModal", model);
        }

        public async Task<PartialViewResult> AddressCityDropdown(int? countryId, int? selectedValue)
        {
            var viewModel = new TeacherAddressCityDropdownViewModel();

            if (countryId != null)
            {
                viewModel.TeacherCityList = await _teachersAppService.GetCitiesForTableDropdown(countryId);
                viewModel.SelectedValue = selectedValue;
            }

            return PartialView("_AddressCityDropdown", viewModel);
        }

        public async Task<PartialViewResult> AddressTownDropdown(int? cityId, int? selectedValue)
        {
            var viewModel = new TeacherAddressTownDropdownViewModel();

            if (cityId != null)
            {
                viewModel.TeacherTownList = await _teachersAppService.GetTownsForTableDropdown(cityId);
                viewModel.SelectedValue = selectedValue;
            }

            return PartialView("_AddressTownDropdown", viewModel);
        }

        public async Task<PartialViewResult> AddAddressField()
        {
            var viewModel = new AddTeacherAddressFieldViewModel
            {
                TeacherCountryList = await _teachersAppService.GetCountriesForTableDropdown()
            };
            return PartialView("_AddAddressField", viewModel);
        }

        public async Task<PartialViewResult> AddPhoneField()
        {
            var viewModel = new AddTeacherPhoneFieldViewModel
            {
                TeacherCountryList = await _teachersAppService.GetCountriesForTableDropdown()
            };
            return PartialView("_AddPhoneField", viewModel);
        }    
    }
}
