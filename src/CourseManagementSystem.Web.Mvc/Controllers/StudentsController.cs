using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Controllers;
using CourseManagementSystem.Services.Parents;
using CourseManagementSystem.Services.Students;
using CourseManagementSystem.Services.Students.Dtos;
using CourseManagementSystem.Web.Models.Students;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CourseManagementSystem.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Students)]
    public class StudentsController : CourseManagementSystemControllerBase
    {
        private readonly IStudentsAppService _studentsAppService;
        private readonly IParentsAppService _parentsAppService;


        public StudentsController(IStudentsAppService studentsAppService,
                                  IParentsAppService parentsAppService)
        {
            _studentsAppService = studentsAppService;
            _parentsAppService = parentsAppService;
        }

        public ActionResult Index()
        {
            var model = new StudentsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            GetStudentForEditOutput getStudentForEditOutput;

            if (id.HasValue)
            {
                getStudentForEditOutput = await _studentsAppService.GetStudentForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getStudentForEditOutput = new GetStudentForEditOutput
                {
                    Student = new CreateOrEditStudentDto()
                };
                getStudentForEditOutput.Student.IsActive = true;
            }

            var viewModel = new CreateOrEditStudentModalViewModel()
            {
                Student = getStudentForEditOutput.Student,
                GradeName = getStudentForEditOutput.GradeName,
                SchoolName = getStudentForEditOutput.SchoolName,
                StudentGradeList = await _studentsAppService.GetGradesForTableDropdown(),
                StudentSchoolList = await _studentsAppService.GetSchoolsForTableDropdown(),
                StudentCountryList = await _studentsAppService.GetCountriesForTableDropdown(),
                StudentParentList = await _studentsAppService.GetParentsForTableDropdown()
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewModal(int id)
        {
            var getStudentForViewDto = await _studentsAppService.GetStudentForView(id);

            var model = new StudentViewModel()
            {
                Student = getStudentForViewDto.Student,
                GradeName = getStudentForViewDto.GradeName,
                SchoolName = getStudentForViewDto.SchoolName
            };

            return PartialView("_ViewModal", model);
        }

        public async Task<PartialViewResult> AddressCityDropdown(int? countryId, int? selectedValue)
        {
            var viewModel = new StudentAddressCityDropdownViewModel();

            if (countryId != null)
            {
                viewModel.StudentCityList = await _studentsAppService.GetCitiesForTableDropdown(countryId);
                viewModel.SelectedValue = selectedValue;
            }

            return PartialView("_AddressCityDropdown", viewModel);
        }

        public async Task<PartialViewResult> AddressTownDropdown(int? cityId, int? selectedValue)
        {
            var viewModel = new StudentAddressTownDropdownViewModel();

            if (cityId != null)
            {
                viewModel.StudentTownList = await _studentsAppService.GetTownsForTableDropdown(cityId);
                viewModel.SelectedValue = selectedValue;
            }

            return PartialView("_AddressTownDropdown", viewModel);
        }

        public async Task<PartialViewResult> AddAddressField()
        {
            var viewModel = new AddStudentAddressFieldViewModel
            {
                StudentCountryList = await _studentsAppService.GetCountriesForTableDropdown()
            };
            return PartialView("_AddAddressField", viewModel);
        }

        public async Task<PartialViewResult> AddPhoneField()
        {
            var viewModel = new AddStudentPhoneFieldViewModel
            {
                StudentCountryList = await _studentsAppService.GetCountriesForTableDropdown()
            };
            return PartialView("_AddPhoneField", viewModel);
        }

        public async Task<PartialViewResult> AddParentField(int parentId)
        {
            var getParentForEditOutput = await _parentsAppService.GetParentForEdit(new EntityDto { Id = parentId });

            var viewModel = new AddStudentParentFieldViewModel
            {
                Parent = getParentForEditOutput.Parent,
                ParentTypeName = getParentForEditOutput.ParentTypeName
            };

            return PartialView("_AddParentField", viewModel);
        }
    }
}
