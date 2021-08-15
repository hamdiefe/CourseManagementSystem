using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseManagementSystem.Services.Teachers.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.Teachers
{
    public interface ITeachersAppService : IApplicationService
    {
        Task<PagedResultDto<GetTeacherForViewDto>> GetAll(GetAllTeachersInput input);

        Task<GetTeacherForViewDto> GetTeacherForView(int id);

        Task<GetTeacherForEditOutput> GetTeacherForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditTeacherDto input);

        Task Delete(EntityDto input);

        Task DeleteAll(List<int> input);

        Task<List<TeacherGraduationSchoolLookupTableDto>> GetGraduationSchoolsForTableDropdown();

        Task<List<TeacherCountryLookupTableDto>> GetCountriesForTableDropdown();

        Task<List<TeacherCityLookupTableDto>> GetCitiesForTableDropdown(int? countryId);

        Task<List<TeacherTownLookupTableDto>> GetTownsForTableDropdown(int? cityId);
    }
}
