using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Models.Addresses;
using CourseManagementSystem.Models.Cities;
using CourseManagementSystem.Models.Common;
using CourseManagementSystem.Models.Countries;
using CourseManagementSystem.Models.Jobs;
using CourseManagementSystem.Models.Phones;
using CourseManagementSystem.Models.ParentTypes;
using CourseManagementSystem.Models.Parents;
using CourseManagementSystem.Models.Towns;
using CourseManagementSystem.Services.Addresses.Dtos;
using CourseManagementSystem.Services.Phones.Dtos;
using CourseManagementSystem.Services.Parents.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.Parents
{
    [AbpAuthorize(PermissionNames.Pages_Parents)]
    public class ParentsAppService : CourseManagementSystemAppServiceBase, IParentsAppService
    {
        private readonly IRepository<Parent> _parentRepository;
        private readonly IRepository<Job> _jobRepository;
        private readonly IRepository<ParentType> _parentTypeRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Phone> _phoneRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Town> _townRepository;

        public ParentsAppService(IRepository<Parent> parentRepository,
                                  IRepository<Job> jobRepository,
                                  IRepository<ParentType> parentTypeRepository,
                                  IRepository<Country> countryRepository,
                                  IRepository<Phone> phoneRepository,
                                  IRepository<Address> addressRepository,
                                  IRepository<City> cityRepository,
                                  IRepository<Town> townRepository)
        {
            _parentRepository = parentRepository;
            _jobRepository = jobRepository;
            _parentTypeRepository = parentTypeRepository;
            _countryRepository = countryRepository;
            _phoneRepository = phoneRepository;
            _addressRepository = addressRepository;
            _cityRepository = cityRepository;
            _townRepository = townRepository;
        }

        public async Task<PagedResultDto<GetParentForViewDto>> GetAll(GetAllParentsInput input)
        {

            var genderFilter = input.GenderFilter.HasValue
                       ? (Gender)input.GenderFilter
                       : default;

            var filteredParents = _parentRepository.GetAll()
                        .Include(e => e.ParentTypeFk)
                        .Include(e => e.JobFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Surname.Contains(input.Filter))
                        .WhereIf(input.GenderFilter.HasValue && input.GenderFilter > -1, e => e.Gender == genderFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ParentTypeNameFilter), e => e.ParentTypeFk != null && e.ParentTypeFk.Name == input.ParentTypeNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.JobNameFilter), e => e.JobFk != null && e.JobFk.Name == input.JobNameFilter);

            var pagedAndFilteredParents = filteredParents
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var parents = from o in pagedAndFilteredParents

                          join o1 in _jobRepository.GetAll() on o.JobId equals o1.Id into j1
                          from s1 in j1.DefaultIfEmpty()

                          join o2 in _parentTypeRepository.GetAll() on o.ParentTypeId equals o2.Id into j2
                          from s2 in j2.DefaultIfEmpty()

                          select new GetParentForViewDto()
                          {
                              Parent = new ParentDto
                              {
                                  Id = o.Id,
                                  IdentityNumber = o.IdentityNumber,
                                  Name = o.Name,
                                  Surname = o.Surname,
                                  Gender = o.Gender,
                                  BirthDate = o.BirthDate,
                                  BirthPlace = o.BirthPlace,
                                  Email = o.Email,
                                  EducationalStatus = o.EducationalStatus,
                                  JobId = o.JobId,
                                  ParentTypeId = o.ParentTypeId
                              },
                              JobName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                              ParentTypeName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                              Phones = (from p in _phoneRepository.GetAll().Where(x => x.ParentId == o.Id)
                                        select new GetPhoneForViewDto
                                        {
                                            Phone = new PhoneDto
                                            {
                                                Id = p.Id,
                                                Code = p.Code,
                                                Number = p.Number,
                                                Type = p.Type
                                            }
                                        }).ToList(),
                              Addresses = (from p in _addressRepository.GetAll()
                                           join r in _countryRepository.GetAll() on p.CountryId equals r.Id
                                           join s in _cityRepository.GetAll() on p.CityId equals s.Id
                                           join t in _townRepository.GetAll() on p.TownId equals t.Id
                                           where p.ParentId == o.Id

                                           select new GetAddressForViewDto
                                           {
                                               Address = new AddressDto
                                               {
                                                   Id = p.Id,
                                                   Detail = p.Detail,
                                                   Type = p.Type,
                                                   District = p.District
                                               },
                                               CountryName = r == null || r.Name == null ? "" : r.Name.ToString(),
                                               CityName = s == null || s.Name == null ? "" : s.Name.ToString(),
                                               TownName = t == null || t.Name == null ? "" : t.Name.ToString()
                                           }).ToList()
                          };




            var totalCount = await filteredParents.CountAsync();

            var parentList = await parents.ToListAsync();

            return new PagedResultDto<GetParentForViewDto>(
                totalCount,
                parentList
            );
        }

        public async Task<GetParentForViewDto> GetParentForView(int id)
        {
            var parent = await _parentRepository.GetAsync(id);

            var output = new GetParentForViewDto { Parent = ObjectMapper.Map<ParentDto>(parent) };

            if (output.Parent.JobId != null)
            {
                var job = await _jobRepository.FirstOrDefaultAsync((int)output.Parent.JobId);
                output.JobName = job?.Name?.ToString();
            }

            if (output.Parent.ParentTypeId != null)
            {
                var parentType = await _parentTypeRepository.FirstOrDefaultAsync((int)output.Parent.ParentTypeId);
                output.ParentTypeName = parentType?.Name?.ToString();
            }

            return output;
        }

        public async Task<GetParentForEditOutput> GetParentForEdit(EntityDto input)
        {
            var parent = await _parentRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetParentForEditOutput { Parent = ObjectMapper.Map<CreateOrEditParentDto>(parent) };

            if (output.Parent.JobId != null)
            {
                var job = await _jobRepository.FirstOrDefaultAsync((int)output.Parent.JobId);
                output.JobName = job?.Name?.ToString();
            }

            if (output.Parent.ParentTypeId != null)
            {
                var parentType = await _parentTypeRepository.FirstOrDefaultAsync((int)output.Parent.ParentTypeId);
                output.ParentTypeName = parentType?.Name?.ToString();
            }

            var phones = (from o in _phoneRepository.GetAll().Where(x => x.ParentId == output.Parent.Id)
                          select new CreateOrEditPhoneDto
                          {
                              Id = o.Id,
                              Code = o.Code,
                              Number = o.Number,
                              Type = o.Type
                          }).ToList();

            if (phones.Count != 0)
            {
                output.Parent.Phones = phones;
            }


            var addresses = (from o in _addressRepository.GetAll().Where(x => x.ParentId == output.Parent.Id)
                             select new CreateOrEditAddressDto
                             {
                                 Id = o.Id,
                                 Detail = o.Detail,
                                 Type = o.Type,
                                 District = o.District,
                                 CountryId = o.CountryId,
                                 CityId = o.CityId,
                                 TownId = o.TownId
                             }).ToList();

            if (addresses.Count != 0)
            {
                output.Parent.Addresses = addresses;
            }

            return output;
        }

        public async Task<GetParentForEditOutput> CreateOrEdit(CreateOrEditParentDto input)
        {
            if (input.Id == null)
            {
                return await Create(input);
            }
            else
            {
                return await Update(input);
            }
        }

        protected virtual async Task<GetParentForEditOutput> Create(CreateOrEditParentDto input)
        {
            var parent = ObjectMapper.Map<Parent>(input);

            if (AbpSession.TenantId != null)
            {
                parent.TenantId = (int)AbpSession.TenantId;
            }

            var parentId = _parentRepository.InsertAndGetId(parent);

            //foreach (var item in input.Phones)
            //{
            //    item.ParentId = parentId;
            //    ObjectMapper.Map<Phone>(item);
            //}

            //foreach (var item in input.Addresses)
            //{
            //    item.ParentId = parentId;
            //    ObjectMapper.Map<Address>(item);
            //}

            input.Id = parentId;

            string parentTypeName = "";

            if (input.ParentTypeId != null)
            {
                var parentType = await _parentTypeRepository.FirstOrDefaultAsync((int)input.ParentTypeId);
                parentTypeName = parentType?.Name?.ToString();
            }

            var output = new GetParentForEditOutput
            {
                Parent = input,
                ParentTypeName = parentTypeName
            };

            return output;
        }

        protected virtual async Task<GetParentForEditOutput> Update(CreateOrEditParentDto input)
        {
            var parent = await _parentRepository.FirstOrDefaultAsync((int)input.Id);

            foreach (var item in input.Phones)
            {
                if (AbpSession.TenantId != null)
                {
                    item.TenantId = (int)AbpSession.TenantId;
                    item.CreatorUserId = parent.CreatorUserId;
                }
            }

            foreach (var item in input.Addresses)
            {
                if (AbpSession.TenantId != null)
                {
                    item.TenantId = (int)AbpSession.TenantId;
                    item.CreatorUserId = parent.CreatorUserId;
                }
            }


            ObjectMapper.Map(input, parent);

            #region Phones
            var phoneIdListFromDb = (from o in _phoneRepository.GetAll().Where(x => x.ParentId == parent.Id)
                                     select o.Id).ToList();

            var phoneIdListFromView = (from p in input.Phones
                                       select p.Id ?? 0).ToList();

            var phonesFirstNotSecond = phoneIdListFromDb.Except(phoneIdListFromView).ToList();

            foreach (var item in phonesFirstNotSecond)
            {
                if (item != 0)
                {
                    await _phoneRepository.DeleteAsync(item);
                }
            }
            #endregion

            #region Addresses
            var addressIdListFromDb = (from o in _addressRepository.GetAll().Where(x => x.ParentId == parent.Id)
                                       select o.Id).ToList();

            var addressIdListFromView = (from p in input.Addresses
                                         select p.Id ?? 0).ToList();

            var addressesFirstNotSecond = addressIdListFromDb.Except(addressIdListFromView).ToList();

            foreach (var item in addressesFirstNotSecond)
            {
                if (item != 0)
                {
                    await _addressRepository.DeleteAsync(item);
                }
            }
            #endregion

            string parentTypeName = "";

            if (input.ParentTypeId != null)
            {
                var parentType = await _parentTypeRepository.FirstOrDefaultAsync((int)input.ParentTypeId);
                parentTypeName = parentType?.Name?.ToString();
            }

            var output = new GetParentForEditOutput
            {
                Parent = input,
                ParentTypeName = parentTypeName
            };

            return output;
        }

        public async Task Delete(EntityDto input)
        {
            #region Phones
            var phoneIdList = (from o in _phoneRepository.GetAll().Where(x => x.ParentId == input.Id)
                               select o.Id).ToList();

            if (phoneIdList.Count != 0)
            {
                foreach (var phoneId in phoneIdList)
                {
                    await _phoneRepository.DeleteAsync(phoneId);
                }
            }
            #endregion

            #region Adddress
            var addressIdList = (from o in _addressRepository.GetAll().Where(x => x.ParentId == input.Id)
                                 select o.Id).ToList();

            if (addressIdList.Count != 0)
            {
                foreach (var addressId in addressIdList)
                {
                    await _addressRepository.DeleteAsync(addressId);
                }
            }
            #endregion

            await _parentRepository.DeleteAsync(input.Id);
        }

        public async Task DeleteAll(List<int> input)
        {
            List<int> phoneIdList;
            List<int> addressIdList;

            foreach (var item in input)
            {
                #region Phones
                phoneIdList = (from o in _phoneRepository.GetAll().Where(x => x.ParentId == item)
                               select o.Id).ToList();

                if (phoneIdList.Count != 0)
                {
                    foreach (var phoneId in phoneIdList)
                    {
                        await _phoneRepository.DeleteAsync(phoneId);
                    }
                }
                #endregion

                #region Addresses
                addressIdList = (from o in _addressRepository.GetAll().Where(x => x.ParentId == item)
                                 select o.Id).ToList();

                if (addressIdList.Count != 0)
                {
                    foreach (var addressId in addressIdList)
                    {
                        await _addressRepository.DeleteAsync(addressId);
                    }
                }
                #endregion

                await _parentRepository.DeleteAsync(item);
            }
        }

        public async Task<List<ParentJobLookupTableDto>> GetJobsForTableDropdown()
        {
            return await _jobRepository.GetAll()
               .Select(o => new ParentJobLookupTableDto
               {
                   Id = o.Id,
                   DisplayName = o == null || o.Name == null ? "" : o.Name.ToString()
               }).ToListAsync();
        }

        public async Task<List<ParentParentTypeLookupTableDto>> GetParentTypesForTableDropdown()
        {
            return await _parentTypeRepository.GetAll()
               .Select(o => new ParentParentTypeLookupTableDto
               {
                   Id = o.Id,
                   DisplayName = o == null || o.Name == null ? "" : o.Name.ToString()
               }).ToListAsync();
        }

        public async Task<List<ParentCountryLookupTableDto>> GetCountriesForTableDropdown()
        {
            return await _countryRepository.GetAll()
               .Select(o => new ParentCountryLookupTableDto
               {
                   Id = o.Id,
                   DisplayName = o == null || o.Name == null ? "" : o.Name.ToString().ToUpper(),
                   PhoneCode = o == null || o.PhoneCode == null ? "" : o.PhoneCode.ToString(),
                   DualCode = o == null || o.DualCode == null ? "" : o.DualCode.ToString(),
               }).ToListAsync();
        }

        public async Task<List<ParentCityLookupTableDto>> GetCitiesForTableDropdown(int? countryId)
        {
            return await _cityRepository.GetAll().WhereIf(countryId != null, x => x.CountryId == countryId)
               .Select(o => new ParentCityLookupTableDto
               {
                   Id = o.Id,
                   DisplayName = o == null || o.Name == null ? "" : o.Name.ToString().ToUpper()
               }).ToListAsync();
        }

        public async Task<List<ParentTownLookupTableDto>> GetTownsForTableDropdown(int? cityId)
        {
            return await _townRepository.GetAll().WhereIf(cityId != null, x => x.CityId == cityId)
               .Select(o => new ParentTownLookupTableDto
               {
                   Id = o.Id,
                   DisplayName = o == null || o.Name == null ? "" : o.Name.ToString().ToUpper()
               }).ToListAsync();
        }

    }
}
