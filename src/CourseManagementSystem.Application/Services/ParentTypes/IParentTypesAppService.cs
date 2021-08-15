using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseManagementSystem.Services.ParentTypes.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.ParentTypes
{
    public interface IParentTypesAppService : IApplicationService
    {
        Task<PagedResultDto<GetParentTypeForViewDto>> GetAll(GetAllParentTypesInput input);

        Task<GetParentTypeForViewDto> GetParentTypeForView(int id);

        Task<GetParentTypeForEditOutput> GetParentTypeForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditParentTypeDto input);

        Task Delete(EntityDto input);

        Task DeleteAll(List<int> input);
    }
}
