using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.Parents;
using CourseManagementSystem.Services.Parents.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.Parents
{
    public interface IParentsAppService : IApplicationService
    {
        Task<PagedResultDto<GetParentForViewDto>> GetAll(GetAllParentsInput input);

        Task<GetParentForViewDto> GetParentForView(int id);

        Task<GetParentForEditOutput> GetParentForEdit(EntityDto input);

        Task<GetParentForEditOutput> CreateOrEdit(CreateOrEditParentDto input);

        Task Delete(EntityDto input);

        Task DeleteAll(List<int> input);

        Task<List<ParentJobLookupTableDto>> GetJobsForTableDropdown();

        Task<List<ParentParentTypeLookupTableDto>> GetParentTypesForTableDropdown();

        Task<List<ParentCountryLookupTableDto>> GetCountriesForTableDropdown();

        Task<List<ParentCityLookupTableDto>> GetCitiesForTableDropdown(int? countryId);

        Task<List<ParentTownLookupTableDto>> GetTownsForTableDropdown(int? cityId);
    }
}
