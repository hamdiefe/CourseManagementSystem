using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Models.Addresses;
using CourseManagementSystem.Models.Cities;
using CourseManagementSystem.Models.Common;
using CourseManagementSystem.Models.Countries;
using CourseManagementSystem.Models.Grades;
using CourseManagementSystem.Models.Parents;
using CourseManagementSystem.Models.ParentTypes;
using CourseManagementSystem.Models.Phones;
using CourseManagementSystem.Models.Schools;
using CourseManagementSystem.Models.StudentParents;
using CourseManagementSystem.Models.Students;
using CourseManagementSystem.Models.Towns;
using CourseManagementSystem.Services.Addresses.Dtos;
using CourseManagementSystem.Services.Parents.Dtos;
using CourseManagementSystem.Services.Phones.Dtos;
using CourseManagementSystem.Services.StudentParents.Dtos;
using CourseManagementSystem.Services.Students.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.Students
{
    [AbpAuthorize(PermissionNames.Pages_Students)]
    public class StudentsAppService : CourseManagementSystemAppServiceBase, IStudentsAppService
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Grade> _gradeRepository;
        private readonly IRepository<School> _schoolRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Phone> _phoneRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Town> _townRepository;
        private readonly IRepository<Parent> _parentRepository;
        private readonly IRepository<StudentParent> _studentParentRepository;
        private readonly IRepository<ParentType> _parentTypeRepository;

        public StudentsAppService(IRepository<Student> studentRepository,
                                  IRepository<Grade> gradeRepository,
                                  IRepository<School> schoolRepository,
                                  IRepository<Country> countryRepository,
                                  IRepository<Phone> phoneRepository,
                                  IRepository<Address> addressRepository,
                                  IRepository<City> cityRepository,
                                  IRepository<Town> townRepository,
                                  IRepository<Parent> parentRepository,
                                  IRepository<StudentParent> studentParentRepository,
                                  IRepository<ParentType> parentTypeRepository)
        {
            _studentRepository = studentRepository;
            _gradeRepository = gradeRepository;
            _schoolRepository = schoolRepository;
            _countryRepository = countryRepository;
            _phoneRepository = phoneRepository;
            _addressRepository = addressRepository;
            _cityRepository = cityRepository;
            _townRepository = townRepository;
            _parentRepository = parentRepository;
            _studentParentRepository = studentParentRepository;
            _parentTypeRepository = parentTypeRepository;
        }

        public async Task<PagedResultDto<GetStudentForViewDto>> GetAll(GetAllStudentsInput input)
        {

            var genderFilter = input.GenderFilter.HasValue
                       ? (Gender)input.GenderFilter
                       : default;

            var filteredStudents = _studentRepository.GetAll()
                        .Include(e => e.GradeFk)
                        .Include(e => e.SchoolFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Surname.Contains(input.Filter))
                        .WhereIf(input.GenderFilter.HasValue && input.GenderFilter > -1, e => e.Gender == genderFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.GradeNameFilter), e => e.GradeFk != null && e.GradeFk.Name == input.GradeNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SchoolNameFilter), e => e.SchoolFk != null && e.SchoolFk.Name == input.SchoolNameFilter);

            var pagedAndFilteredStudents = filteredStudents
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var students = from o in pagedAndFilteredStudents

                           join o1 in _gradeRepository.GetAll() on o.GradeId equals o1.Id into j1
                           from s1 in j1.DefaultIfEmpty()

                           join o2 in _schoolRepository.GetAll() on o.SchoolId equals o2.Id into j2
                           from s2 in j2.DefaultIfEmpty()

                           select new GetStudentForViewDto()
                           {
                               Student = new StudentDto
                               {
                                   Id = o.Id,
                                   IdentityNumber = o.IdentityNumber,
                                   Name = o.Name,
                                   Surname = o.Surname,
                                   Gender = o.Gender,
                                   BirthDate = o.BirthDate,
                                   Email = o.Email,
                                   BirthPlace = o.BirthPlace,
                                   StudentNumber = o.StudentNumber,
                                   GradeId = o.GradeId,
                                   SchoolId = o.SchoolId,
                                   HourlyPaymentPeriod = o.HourlyPaymentPeriod,
                                   HourlyPaymentAmount = o.HourlyPaymentAmount,
                                   Currency = o.Currency
                               },
                               GradeName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                               SchoolName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                               Phones = (from p in _phoneRepository.GetAll().Where(x => x.StudentId == o.Id)
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
                                            where p.StudentId == o.Id

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




            var totalCount = await filteredStudents.CountAsync();

            var studentList = await students.ToListAsync();

            return new PagedResultDto<GetStudentForViewDto>(
                totalCount,
                studentList
            );
        }

        public async Task<GetStudentForViewDto> GetStudentForView(int id)
        {
            var student = await _studentRepository.GetAsync(id);

            var output = new GetStudentForViewDto { Student = ObjectMapper.Map<StudentDto>(student) };

            if (output.Student.GradeId != null)
            {
                var grade = await _gradeRepository.FirstOrDefaultAsync((int)output.Student.GradeId);
                output.GradeName = grade?.Name?.ToString();
            }

            if (output.Student.SchoolId != null)
            {
                var school = await _schoolRepository.FirstOrDefaultAsync((int)output.Student.SchoolId);
                output.SchoolName = school?.Name?.ToString();
            }

            return output;
        }

        public async Task<GetStudentForEditOutput> GetStudentForEdit(EntityDto input)
        {
            var student = await _studentRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetStudentForEditOutput { Student = ObjectMapper.Map<CreateOrEditStudentDto>(student) };

            if (output.Student.GradeId != null)
            {
                var grade = await _gradeRepository.FirstOrDefaultAsync((int)output.Student.GradeId);
                output.GradeName = grade?.Name?.ToString();
            }

            if (output.Student.SchoolId != null)
            {
                var school = await _schoolRepository.FirstOrDefaultAsync((int)output.Student.SchoolId);
                output.SchoolName = school?.Name?.ToString();
            }

            var phones = (from o in _phoneRepository.GetAll().Where(x => x.StudentId == output.Student.Id)
                          select new CreateOrEditPhoneDto
                          {
                              Id = o.Id,
                              Code = o.Code,
                              Number = o.Number,
                              Type = o.Type
                          }).ToList();

            if (phones.Count != 0)
            {
                output.Student.Phones = phones;
            }


            var addresses = (from o in _addressRepository.GetAll().Where(x => x.StudentId == output.Student.Id)
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
                output.Student.Addresses = addresses;
            }

            var studentParents = (from o in _studentParentRepository.GetAll().Where(x => x.StudentId == output.Student.Id)
                                  join o1 in _parentRepository.GetAll() on o.ParentId equals o1.Id into j1
                                  from s1 in j1.DefaultIfEmpty()
                                  join o2 in _parentTypeRepository.GetAll() on s1.ParentTypeId equals o2.Id into j2
                                  from s2 in j2.DefaultIfEmpty()
                                  select new CreateOrEditStudentParentDto
                                  {
                                      Id = o.Id,
                                      Parent = new CreateOrEditParentDto
                                      {
                                          Id = s1.Id,
                                          Name = s1.Name,
                                          Surname = s1.Surname,
                                          Gender = s1.Gender
                                      },
                                      ParentTypeName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                                  }).ToList();

            if (studentParents.Count != 0)
            {
                output.Student.StudentParents = studentParents;
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditStudentDto input)
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

        protected virtual void Create(CreateOrEditStudentDto input)
        {
            var student = ObjectMapper.Map<Student>(input);

            if (AbpSession.TenantId != null)
            {
                student.TenantId = (int)AbpSession.TenantId;
            }

            _studentRepository.InsertAsync(student);

            //foreach (var item in input.Phones)
            //{
            //    item.StudentId = studentId;
            //    ObjectMapper.Map<Phone>(item);
            //}

            //foreach (var item in input.Addresses)
            //{
            //    item.StudentId = studentId;
            //    ObjectMapper.Map<Address>(item);
            //}

            //foreach (var item in input.StudentParents)
            //{
            //    item.StudentId = studentId;
            //    ObjectMapper.Map<StudentParent>(item);
            //}
        }

        protected virtual async Task Update(CreateOrEditStudentDto input)
        {
            var student = await _studentRepository.FirstOrDefaultAsync((int)input.Id);

            foreach (var item in input.Phones)
            {
                if (AbpSession.TenantId != null)
                {
                    item.TenantId = (int)AbpSession.TenantId;
                    item.CreatorUserId = student.CreatorUserId;
                }
            }

            foreach (var item in input.Addresses)
            {
                if (AbpSession.TenantId != null)
                {
                    item.TenantId = (int)AbpSession.TenantId;
                    item.CreatorUserId = student.CreatorUserId;
                }
            }

            foreach (var item in input.StudentParents)
            {
                if (AbpSession.TenantId != null)
                {
                    item.TenantId = (int)AbpSession.TenantId;
                    item.CreatorUserId = student.CreatorUserId;
                }
            }

            ObjectMapper.Map(input, student);

            #region Phones
            var phoneIdListFromDb = (from o in _phoneRepository.GetAll().Where(x => x.StudentId == student.Id)
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
            var addressIdListFromDb = (from o in _addressRepository.GetAll().Where(x => x.StudentId == student.Id)
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

            #region StudentParents
            var studentParentIdListFromDb = (from o in _studentParentRepository.GetAll().Where(x => x.StudentId == student.Id)
                                             select o.Id).ToList();

            var studentParentIdListFromView = (from p in input.StudentParents
                                               select p.Id ?? 0).ToList();

            var studentParentsFirstNotSecond = studentParentIdListFromDb.Except(studentParentIdListFromView).ToList();

            foreach (var item in studentParentsFirstNotSecond)
            {
                if (item != 0)
                {
                    await _studentParentRepository.DeleteAsync(item);
                }
            }
            #endregion
        }

        public async Task Delete(EntityDto input)
        {
            #region Phones
            var phoneIdList = (from o in _phoneRepository.GetAll().Where(x => x.StudentId == input.Id)
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
            var addressIdList = (from o in _addressRepository.GetAll().Where(x => x.StudentId == input.Id)
                                 select o.Id).ToList();

            if (addressIdList.Count != 0)
            {
                foreach (var addressId in addressIdList)
                {
                    await _addressRepository.DeleteAsync(addressId);
                }
            }
            #endregion

            #region StudentParents
            var studentParentIdList = (from o in _studentParentRepository.GetAll().Where(x => x.StudentId == input.Id)
                                       select o.Id).ToList();

            if (studentParentIdList.Count != 0)
            {
                foreach (var studentParentId in studentParentIdList)
                {
                    await _studentParentRepository.DeleteAsync(studentParentId);
                }
            }
            #endregion

            await _studentRepository.DeleteAsync(input.Id);
        }

        public async Task DeleteAll(List<int> input)
        {
            List<int> phoneIdList;
            List<int> addressIdList;

            foreach (var item in input)
            {
                #region Phones
                phoneIdList = (from o in _phoneRepository.GetAll().Where(x => x.StudentId == item)
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
                addressIdList = (from o in _addressRepository.GetAll().Where(x => x.StudentId == item)
                                 select o.Id).ToList();

                if (addressIdList.Count != 0)
                {
                    foreach (var addressId in addressIdList)
                    {
                        await _addressRepository.DeleteAsync(addressId);
                    }
                }
                #endregion

                #region StudentParents
                var studentParentIdList = (from o in _studentParentRepository.GetAll().Where(x => x.StudentId == item)
                                           select o.Id).ToList();

                if (studentParentIdList.Count != 0)
                {
                    foreach (var studentParentId in studentParentIdList)
                    {
                        await _studentParentRepository.DeleteAsync(studentParentId);
                    }
                }
                #endregion

                await _studentRepository.DeleteAsync(item);
            }
        }

        public async Task<List<StudentGradeLookupTableDto>> GetGradesForTableDropdown()
        {
            return await _gradeRepository.GetAll()
               .Select(o => new StudentGradeLookupTableDto
               {
                   Id = o.Id,
                   DisplayName = o == null || o.Name == null ? "" : o.Name.ToString()
               }).ToListAsync();
        }

        public async Task<List<StudentSchoolLookupTableDto>> GetSchoolsForTableDropdown()
        {
            return await _schoolRepository.GetAll()
               .Select(o => new StudentSchoolLookupTableDto
               {
                   Id = o.Id,
                   DisplayName = o == null || o.Name == null ? "" : o.Name.ToString()
               }).ToListAsync();
        }

        public async Task<List<StudentCountryLookupTableDto>> GetCountriesForTableDropdown()
        {
            return await _countryRepository.GetAll()
               .Select(o => new StudentCountryLookupTableDto
               {
                   Id = o.Id,
                   DisplayName = o == null || o.Name == null ? "" : o.Name.ToString().ToUpper(),
                   PhoneCode = o == null || o.PhoneCode == null ? "" : o.PhoneCode.ToString(),
                   DualCode = o == null || o.DualCode == null ? "" : o.DualCode.ToString(),
               }).ToListAsync();
        }

        public async Task<List<StudentCityLookupTableDto>> GetCitiesForTableDropdown(int? countryId)
        {
            return await _cityRepository.GetAll().WhereIf(countryId != null, x => x.CountryId == countryId)
               .Select(o => new StudentCityLookupTableDto
               {
                   Id = o.Id,
                   DisplayName = o == null || o.Name == null ? "" : o.Name.ToString().ToUpper()
               }).ToListAsync();
        }

        public async Task<List<StudentTownLookupTableDto>> GetTownsForTableDropdown(int? cityId)
        {
            return await _townRepository.GetAll().WhereIf(cityId != null, x => x.CityId == cityId)
               .Select(o => new StudentTownLookupTableDto
               {
                   Id = o.Id,
                   DisplayName = o == null || o.Name == null ? "" : o.Name.ToString().ToUpper()
               }).ToListAsync();
        }

        public async Task<List<StudentParentLookupTableDto>> GetParentsForTableDropdown()
        {
            return await _parentRepository.GetAll()
               .Select(o => new StudentParentLookupTableDto
               {
                   Id = o.Id,
                   DisplayName = o == null || o.Name == null ? "" : o.Name.ToString() + " " + o.Surname.ToString()
               }).ToListAsync();
        }
    }
}
