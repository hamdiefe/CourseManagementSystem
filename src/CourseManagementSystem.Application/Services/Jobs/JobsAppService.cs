using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Models.Jobs;
using CourseManagementSystem.Services.Jobs.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.Jobs
{
    [AbpAuthorize(PermissionNames.Pages_Jobs)]
    public class JobsAppService : CourseManagementSystemAppServiceBase, IJobsAppService
    {
        private readonly IRepository<Job> _jobRepository;

        public JobsAppService(IRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<PagedResultDto<GetJobForViewDto>> GetAll(GetAllJobsInput input)
        {
            var filteredJobs = _jobRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter));

            var pagedAndFilteredJobs = filteredJobs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var jobs = from o in pagedAndFilteredJobs
                         select new GetJobForViewDto()
                         {
                             Job = new JobDto
                             {
                                 Id = o.Id,
                                 Name = o.Name,
                                 Description = o.Description
                             }
                         };

            var totalCount = await filteredJobs.CountAsync();

            return new PagedResultDto<GetJobForViewDto>(
                totalCount,
                await jobs.ToListAsync()
            );
        }

        public async Task<GetJobForViewDto> GetJobForView(int id)
        {
            var job = await _jobRepository.GetAsync(id);

            var output = new GetJobForViewDto { Job = ObjectMapper.Map<JobDto>(job) };

            return output;
        }

        public async Task<GetJobForEditOutput> GetJobForEdit(EntityDto input)
        {
            var job = await _jobRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetJobForEditOutput { Job = ObjectMapper.Map<CreateOrEditJobDto>(job) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditJobDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        protected virtual async Task Create(CreateOrEditJobDto input)
        {
            var job = ObjectMapper.Map<Job>(input);


            if (AbpSession.TenantId != null)
            {
                job.TenantId = (int)AbpSession.TenantId;
            }


            await _jobRepository.InsertAsync(job);
        }

        protected virtual async Task Update(CreateOrEditJobDto input)
        {
            var job = await _jobRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, job);
        }

        public async Task Delete(EntityDto input)
        {
            await _jobRepository.DeleteAsync(input.Id);
        }

        public async Task DeleteAll(List<int> input)
        {
            foreach (var item in input)
            {
                await _jobRepository.DeleteAsync(item);
            }
        }
    }
}
