using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseManagementSystem.Services.Students.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.Students
{
    public interface IStudentsAppService : IApplicationService
    {
        Task<PagedResultDto<GetStudentForViewDto>> GetAll(GetAllStudentsInput input);

        Task<GetStudentForViewDto> GetStudentForView(int id);

        Task<GetStudentForEditOutput> GetStudentForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditStudentDto input);

        Task Delete(EntityDto input);

        Task DeleteAll(List<int> input);

        Task<List<StudentGradeLookupTableDto>> GetGradesForTableDropdown();

        Task<List<StudentSchoolLookupTableDto>> GetSchoolsForTableDropdown();

        Task<List<StudentCountryLookupTableDto>> GetCountriesForTableDropdown();

        Task<List<StudentCityLookupTableDto>> GetCitiesForTableDropdown(int? countryId);

        Task<List<StudentTownLookupTableDto>> GetTownsForTableDropdown(int? cityId);

        Task<List<StudentParentLookupTableDto>> GetParentsForTableDropdown();
    }
}
