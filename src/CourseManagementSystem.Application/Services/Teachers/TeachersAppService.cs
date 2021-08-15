using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Models.Addresses;
using CourseManagementSystem.Models.Cities;
using CourseManagementSystem.Models.Common;
using CourseManagementSystem.Models.Countries;
using CourseManagementSystem.Models.Parents;
using CourseManagementSystem.Models.ParentTypes;
using CourseManagementSystem.Models.Phones;
using CourseManagementSystem.Models.Schools;
using CourseManagementSystem.Models.Teachers;
using CourseManagementSystem.Models.TeacherSpecializedFields;
using CourseManagementSystem.Models.Towns;
using CourseManagementSystem.Services.Addresses.Dtos;
using CourseManagementSystem.Services.Phones.Dtos;
using CourseManagementSystem.Services.Teachers.Dtos;
using CourseManagementSystem.Services.TeacherSpecializedFields.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.Teachers
{
    [AbpAuthorize(PermissionNames.Pages_Teachers)]
    public class TeachersAppService : CourseManagementSystemAppServiceBase, ITeachersAppService
    {
        private readonly IRepository<Teacher> _teacherRepository;
        private readonly IRepository<School> _schoolRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Phone> _phoneRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Town> _townRepository;
        private readonly IRepository<TeacherSpecializedField> _teacherSpecializedField;

        public TeachersAppService(IRepository<Teacher> teacherRepository,
                                  IRepository<School> schoolRepository,
                                  IRepository<Country> countryRepository,
                                  IRepository<Phone> phoneRepository,
                                  IRepository<Address> addressRepository,
                                  IRepository<City> cityRepository,
                                  IRepository<Town> townRepository,
                                  IRepository<Parent> parentRepository,
                                  IRepository<ParentType> parentTypeRepository,
                                  IRepository<TeacherSpecializedField> teacherSpecializedField)
        {
            _teacherRepository = teacherRepository;
            _schoolRepository = schoolRepository;
            _countryRepository = countryRepository;
            _phoneRepository = phoneRepository;
            _addressRepository = addressRepository;
            _cityRepository = cityRepository;
            _townRepository = townRepository;
            _teacherSpecializedField = teacherSpecializedField;
        }

        public async Task<PagedResultDto<GetTeacherForViewDto>> GetAll(GetAllTeachersInput input)
        {

            var genderFilter = input.GenderFilter.HasValue
                       ? (Gender)input.GenderFilter
                       : default;

            var filteredTeachers = _teacherRepository.GetAll()
                        .Include(e => e.GraduationSchoolFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Surname.Contains(input.Filter))
                        .WhereIf(input.GenderFilter.HasValue && input.GenderFilter > -1, e => e.Gender == genderFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SchoolNameFilter), e => e.GraduationSchoolFk != null && e.GraduationSchoolFk.Name == input.SchoolNameFilter);

            var pagedAndFilteredTeachers = filteredTeachers
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var teachers = from o in pagedAndFilteredTeachers

                           join o1 in _schoolRepository.GetAll() on o.GraduationSchoolId equals o1.Id into j1
                           from s1 in j1.DefaultIfEmpty()

                           select new GetTeacherForViewDto()
                           {
                               Teacher = new TeacherDto
                               {
                                   Id = o.Id,
                                   IdentityNumber = o.IdentityNumber,
                                   Name = o.Name,
                                   Surname = o.Surname,
                                   Gender = o.Gender,
                                   BirthDate = o.BirthDate,
                                   BirthPlace = o.BirthPlace,
                                   Email = o.Email,
                                   GraduationSchoolId = o.GraduationSchoolId,
                                   EducationalStatus = o.EducationalStatus
                               },
                               GraduationSchoolName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                               Phones = (from p in _phoneRepository.GetAll().Where(x => x.TeacherId == o.Id)
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
                                            where p.TeacherId == o.Id

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




            var totalCount = await filteredTeachers.CountAsync();

            var teacherList = await teachers.ToListAsync();

            return new PagedResultDto<GetTeacherForViewDto>(
                totalCount,
                teacherList
            );
        }

        public async Task<GetTeacherForViewDto> GetTeacherForView(int id)
        {
            var teacher = await _teacherRepository.GetAsync(id);

            var output = new GetTeacherForViewDto { Teacher = ObjectMapper.Map<TeacherDto>(teacher) };

            if (output.Teacher.GraduationSchoolId != null)
            {
                var school = await _schoolRepository.FirstOrDefaultAsync((int)output.Teacher.GraduationSchoolId);
                output.GraduationSchoolName = school?.Name?.ToString();
            }

            return output;
        }

        public async Task<GetTeacherForEditOutput> GetTeacherForEdit(EntityDto input)
        {
            var teacher = await _teacherRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetTeacherForEditOutput { Teacher = ObjectMapper.Map<CreateOrEditTeacherDto>(teacher) };

            if (output.Teacher.GraduationSchoolId != null)
            {
                var school = await _schoolRepository.FirstOrDefaultAsync((int)output.Teacher.GraduationSchoolId);
                output.GraduationSchoolName = school?.Name?.ToString();
            }

            var phones = (from o in _phoneRepository.GetAll().Where(x => x.TeacherId == output.Teacher.Id)
                          select new CreateOrEditPhoneDto
                          {
                              Id = o.Id,
                              Code = o.Code,
                              Number = o.Number,
                              Type = o.Type
                          }).ToList();

            if (phones.Count != 0)
            {
                output.Teacher.Phones = phones;
            }


            var addresses = (from o in _addressRepository.GetAll().Where(x => x.TeacherId == output.Teacher.Id)
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
                output.Teacher.Addresses = addresses;
            }


            var teacherSpecializedFields = (from o in _teacherSpecializedField.GetAll().Where(x => x.TeacherId == output.Teacher.Id)
                                            select new CreateOrEditTeacherSpecializedFieldDto
                                            {
                                                Id = o.Id,
                                                SpecializedField = o.SpecializedField,
                                                TeacherId = o.TeacherId

                                            }).ToList();

            if (teacherSpecializedFields.Count != 0)
            {
                output.Teacher.TeacherSpecializedFields = teacherSpecializedFields;
            }


            return output;
        }

        public async Task CreateOrEdit(CreateOrEditTeacherDto input)
        {
            if (input.Id == null)
            {
                Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        protected virtual async Task Create(CreateOrEditTeacherDto input)
        {
            var teacher = ObjectMapper.Map<Teacher>(input);

            if (AbpSession.TenantId != null)
            {
                teacher.TenantId = (int)AbpSession.TenantId;
            }

            await _teacherRepository.InsertAsync(teacher);
        }

        protected virtual async Task Update(CreateOrEditTeacherDto input)
        {

            var teacher = await _teacherRepository.FirstOrDefaultAsync((int)input.Id);

            #region SetIdToTeacherSpecializedFieldFromView 
            //Selectden gelen branþlarýn idlerini bulman gerekiyordu böyle bir yol izledin.

            var teacherSpecializedFieldsFromDB = (from o in _teacherSpecializedField.GetAll().Where(x => x.TeacherId == teacher.Id)
                                                  select new CreateOrEditTeacherSpecializedFieldDto
                                                  {
                                                      Id = o.Id,
                                                      SpecializedField = o.SpecializedField
                                                  }).ToList();

            for (int i = 0; i < teacherSpecializedFieldsFromDB.Count; i++)
            {
                foreach (var item in input.TeacherSpecializedFields)
                {
                    if (item.SpecializedField == teacherSpecializedFieldsFromDB[i].SpecializedField)
                    {
                        item.Id = teacherSpecializedFieldsFromDB[i].Id;
                    }
                }
            }
            #endregion

            foreach (var item in input.Phones)
            {
                if (AbpSession.TenantId != null)
                {
                    item.TenantId = (int)AbpSession.TenantId;
                    item.CreatorUserId = teacher.CreatorUserId;
                }
            }

            foreach (var item in input.Addresses)
            {
                if (AbpSession.TenantId != null)
                {
                    item.TenantId = (int)AbpSession.TenantId;
                    item.CreatorUserId = teacher.CreatorUserId;
                }
            }

            foreach (var item in input.TeacherSpecializedFields)
            {
                if (AbpSession.TenantId != null)
                {
                    item.TenantId = (int)AbpSession.TenantId;
                    item.CreatorUserId = teacher.CreatorUserId;
                }
            }

            ObjectMapper.Map(input, teacher);

            #region Phones
            var phoneIdListFromDb = (from o in _phoneRepository.GetAll().Where(x => x.TeacherId == teacher.Id)
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
            var addressIdListFromDb = (from o in _addressRepository.GetAll().Where(x => x.TeacherId == teacher.Id)
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

            #region TeacherSpecializedFields
            var teacherSpecializedFieldIdListFromDb = (from o in _teacherSpecializedField.GetAll().Where(x => x.TeacherId == teacher.Id)
                                                       select o.SpecializedField).ToList();

            var teacherSpecializedFieldIdListFromView = (from p in input.TeacherSpecializedFields
                                                         select p.SpecializedField).ToList();

            var teacherSpecializedFieldsFirstNotSecond = teacherSpecializedFieldIdListFromDb.Except(teacherSpecializedFieldIdListFromView).ToList();

            foreach (var item in teacherSpecializedFieldsFirstNotSecond)
            {
                if (item != 0)
                {
                    await _teacherSpecializedField.DeleteAsync(x => x.SpecializedField == item && x.TeacherId == teacher.Id);
                }
            }
            #endregion
        }

        public async Task Delete(EntityDto input)
        {
            #region Phones
            var phoneIdList = (from o in _phoneRepository.GetAll().Where(x => x.TeacherId == input.Id)
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
            var addressIdList = (from o in _addressRepository.GetAll().Where(x => x.TeacherId == input.Id)
                                 select o.Id).ToList();

            if (addressIdList.Count != 0)
            {
                foreach (var addressId in addressIdList)
                {
                    await _addressRepository.DeleteAsync(addressId);
                }
            }
            #endregion

            #region TeacherSpecializeds
            var teacherSpecializedList = (from o in _teacherSpecializedField.GetAll().Where(x => x.TeacherId == input.Id)
                                          select o.Id).ToList();

            if (teacherSpecializedList.Count != 0)
            {
                foreach (var teacherSpecializedId in teacherSpecializedList)
                {
                    await _phoneRepository.DeleteAsync(teacherSpecializedId);
                }
            }
            #endregion

            await _teacherRepository.DeleteAsync(input.Id);
        }

        public async Task DeleteAll(List<int> input)
        {
            List<int> phoneIdList;
            List<int> addressIdList;
            List<int> teacherSpecializedList;

            foreach (var item in input)
            {
                #region Phones
                phoneIdList = (from o in _phoneRepository.GetAll().Where(x => x.TeacherId == item)
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
                addressIdList = (from o in _addressRepository.GetAll().Where(x => x.TeacherId == item)
                                 select o.Id).ToList();

                if (addressIdList.Count != 0)
                {
                    foreach (var addressId in addressIdList)
                    {
                        await _addressRepository.DeleteAsync(addressId);
                    }
                }
                #endregion

                #region TeacherSpecializeds
                teacherSpecializedList = (from o in _teacherSpecializedField.GetAll().Where(x => x.TeacherId == item)
                                          select o.Id).ToList();

                if (teacherSpecializedList.Count != 0)
                {
                    foreach (var teacherSpecializedId in teacherSpecializedList)
                    {
                        await _phoneRepository.DeleteAsync(teacherSpecializedId);
                    }
                }
                #endregion

                await _teacherRepository.DeleteAsync(item);
            }
        }

        public async Task<List<TeacherGraduationSchoolLookupTableDto>> GetGraduationSchoolsForTableDropdown()
        {
            return await _schoolRepository.GetAll().Where(x => x.Type == SchoolType.University)
               .Select(o => new TeacherGraduationSchoolLookupTableDto
               {
                   Id = o.Id,
                   DisplayName = o == null || o.Name == null ? "" : o.Name.ToString()
               }).ToListAsync();
        }

        public async Task<List<TeacherCountryLookupTableDto>> GetCountriesForTableDropdown()
        {
            return await _countryRepository.GetAll()
               .Select(o => new TeacherCountryLookupTableDto
               {
                   Id = o.Id,
                   DisplayName = o == null || o.Name == null ? "" : o.Name.ToString().ToUpper(),
                   PhoneCode = o == null || o.PhoneCode == null ? "" : o.PhoneCode.ToString(),
                   DualCode = o == null || o.DualCode == null ? "" : o.DualCode.ToString(),
               }).ToListAsync();
        }

        public async Task<List<TeacherCityLookupTableDto>> GetCitiesForTableDropdown(int? countryId)
        {
            return await _cityRepository.GetAll().WhereIf(countryId != null, x => x.CountryId == countryId)
               .Select(o => new TeacherCityLookupTableDto
               {
                   Id = o.Id,
                   DisplayName = o == null || o.Name == null ? "" : o.Name.ToString().ToUpper()
               }).ToListAsync();
        }

        public async Task<List<TeacherTownLookupTableDto>> GetTownsForTableDropdown(int? cityId)
        {
            return await _townRepository.GetAll().WhereIf(cityId != null, x => x.CityId == cityId)
               .Select(o => new TeacherTownLookupTableDto
               {
                   Id = o.Id,
                   DisplayName = o == null || o.Name == null ? "" : o.Name.ToString().ToUpper()
               }).ToListAsync();
        }
    }
}
