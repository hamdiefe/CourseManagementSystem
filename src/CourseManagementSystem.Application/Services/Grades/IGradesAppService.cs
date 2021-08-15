using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseManagementSystem.Services.Grades.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.Grades
{
    public interface IGradesAppService : IApplicationService
    {
        Task<PagedResultDto<GetGradeForViewDto>> GetAll(GetAllGradesInput input);

        Task<GetGradeForViewDto> GetGradeForView(int id);

        Task<GetGradeForEditOutput> GetGradeForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditGradeDto input);

        Task Delete(EntityDto input);

        Task DeleteAll(List<int> input);
    }
}
