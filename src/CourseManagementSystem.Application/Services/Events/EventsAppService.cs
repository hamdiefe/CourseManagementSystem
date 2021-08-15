using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Models.Events;
using CourseManagementSystem.Models.Students;
using CourseManagementSystem.Models.Teachers;
using CourseManagementSystem.Services.Events.Dtos;
using CourseManagementSystem.Services.Students.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;


namespace CourseManagementSystem.Services.Events
{
    [AbpAuthorize(PermissionNames.Pages_Events)]
    public class EventsAppService : CourseManagementSystemAppServiceBase, IEventsAppService
    {
        private readonly IRepository<Event> _eventRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Teacher> _teacherRepository;


        public EventsAppService(IRepository<Event> eventRepository,
                                IRepository<Student> studentRepository,
                                IRepository<Teacher> teacherRepository)
        {
            _eventRepository = eventRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<PagedResultDto<GetEventForViewDto>> GetAll(GetAllEventsInput input)
        {
            var filteredEvents = _eventRepository.GetAll()
                        .Include(x => x.StudentFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || (e.StudentFk.Name + " " + e.StudentFk.Surname).Contains(input.Filter));

            var pagedAndFilteredEvents = filteredEvents
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var events = from o in pagedAndFilteredEvents
                         join o1 in _studentRepository.GetAll() on o.StudentId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         join o2 in _teacherRepository.GetAll() on o.TeacherId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         select new GetEventForViewDto()
                         {
                             Event = new EventDto
                             {
                                 Id = o.Id,
                                 Title = o.Title,
                                 Description = o.Description,
                                 Type = o.Type,
                                 StartDate = o.StartDate,
                                 EndDate = o.EndDate,
                                 TotalHours = o.TotalHours,
                                 StudentId = o.StudentId,
                                 TeacherId = o.TeacherId,
                                 Status = o.Status
                             },
                             StudentName = s1 == null || s1.Name == null ? "" : s1.Name + " " + s1.Surname,
                             TeacherName = s2 == null || s2.Name == null ? "" : s2.Name + " " + s2.Surname
                         };

            var totalCount = await filteredEvents.CountAsync();

            return new PagedResultDto<GetEventForViewDto>(
                totalCount,
                await events.ToListAsync()
            );
        }

        public async Task<GetEventForViewDto> GetEventForView(int id)
        {
            var appEvent = await _eventRepository.GetAsync(id);

            var output = new GetEventForViewDto { Event = ObjectMapper.Map<EventDto>(appEvent) };

            return output;
        }

        public async Task<GetEventForEditOutput> GetEventForEdit(EntityDto input)
        {
            var appEvent = await _eventRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEventForEditOutput { Event = ObjectMapper.Map<CreateOrEditEventDto>(appEvent) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditEventDto input)
        {
            #region CalculateTotalHours
            var result = (input.EndDate ?? DateTime.Now) - (input.StartDate ?? DateTime.Now);
            input.TotalHours = Convert.ToInt32(result.TotalHours);
            #endregion

            if (input.Id == null)
            {
                if (input.WeekRepeat > 0)
                {
                    CreateWithRepeatWeeks(input);
                }
                else
                {
                    await Create(input);
                }
            }
            else
            {
                await Update(input);
            }
        }

        protected virtual async Task Create(CreateOrEditEventDto input)
        {
            var appEvent = ObjectMapper.Map<Event>(input);

            if (AbpSession.TenantId != null)
            {
                appEvent.TenantId = (int)AbpSession.TenantId;
            }

            await _eventRepository.InsertAsync(appEvent);
        }

        protected virtual void CreateWithRepeatWeeks(CreateOrEditEventDto input)
        {
            var appEvent = new Event
            {
                Title = input.Title,
                Description = input.Description,
                Type = input.Type,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                TotalHours = input.TotalHours,
                StudentId = input.StudentId,
                TeacherId = input.TeacherId
            };

            if (_eventRepository.InsertAndGetId(appEvent) > 0)
            {
                var startDate = appEvent.StartDate ?? DateTime.MinValue;
                var endDate = appEvent.EndDate ?? DateTime.MinValue;


                for (int i = 1; i <= input.WeekRepeat; i++)
                {
                    startDate = startDate.AddDays(7);
                    endDate = endDate.AddDays(7);

                    var repeatAppEvent = new Event
                    {
                        Title = input.Title,
                        Description = input.Description,
                        Type = input.Type,
                        StartDate = startDate,
                        EndDate = endDate,
                        TotalHours = input.TotalHours,
                        StudentId = input.StudentId,
                        TeacherId = input.TeacherId
                    };
                    _eventRepository.Insert(repeatAppEvent);
                }
            }
        }

        protected virtual async Task Update(CreateOrEditEventDto input)
        {
            var appEvent = await _eventRepository.FirstOrDefaultAsync((int)input.Id);

            ObjectMapper.Map(input, appEvent);
        }

        public async Task Delete(EntityDto input)
        {
            await _eventRepository.DeleteAsync(input.Id);
        }

        public async Task DeleteAll(List<int> input)
        {
            foreach (var item in input)
            {
                await _eventRepository.DeleteAsync(item);
            }
        }

        public async Task<List<EventStudentLookupTableDto>> GetStudentsForTableDropdown()
        {
            return await _studentRepository.GetAll().Where(x => x.IsActive == true)
               .Select(o => new EventStudentLookupTableDto
               {
                   Id = o.Id,
                   DisplayName = o == null || o.Name == null ? "" : o.Name.ToString() + " " + o.Surname.ToString()
               }).ToListAsync();
        }

        public async Task<List<EventTeacherLookupTableDto>> GetTeachersForTableDropdown()
        {
            return await _teacherRepository.GetAll().Where(x => x.IsActive == true)
               .Select(o => new EventTeacherLookupTableDto
               {
                   Id = o.Id,
                   DisplayName = o == null || o.Name == null ? "" : o.Name.ToString() + " " + o.Surname.ToString()
               }).ToListAsync();
        }

        public List<CalendarEventDataOutput> GetCalendarData()
        {
            var events = (from o in _eventRepository.GetAll()
                          select new CalendarEventDataOutput
                          {
                              Id = o.Id,
                              Title = o.Title,
                              Start = string.Format("{0:s}", o.StartDate),
                              End = string.Format("{0:s}", o.EndDate),
                              ClassName = o.Status == EventStatus.Completed ? "fullcalendar-custom-event-holidays"
                                        : o.Status == EventStatus.NotCompleted ? "fullcalendar-custom-event-tasks"
                                        : o.Status == EventStatus.Nonselected ? "fullcalendar-custom-event-hs-team"
                                        : "",
                              Description = o.Description,
                              Students = (from p in _studentRepository.GetAll().Where(x => x.Id == o.StudentId)
                                          select new CalendarEventStudentDto
                                          {
                                              Id = p.Id,
                                              Value = p.Name + " " + p.Surname,
                                              Src = "",
                                              Gender = p.Gender
                                          }).ToList(),
                              Teachers = (from p in _teacherRepository.GetAll().Where(x => x.Id == o.TeacherId)
                                          select new CalendarEventTeacherDto
                                          {
                                              Id = o.Id,
                                              Value = p.Name + " " + p.Surname,
                                              Src = "",
                                              Gender = p.Gender
                                          }).ToList(),
                              Image = o.Type == EventType.Payment ? "./front-admin/assets/svg/brands/mastercard.svg" : "./front-admin/assets/svg/brands/figma.svg",
                              TotalHours = o.TotalHours,
                              Type = o.Type
                          }).ToList();


            return events;
        }

        public void RefreshPayments(int? studentId)
        {
            var paymentEventIdList = new List<int>();
            if (studentId == null)
            {
                paymentEventIdList = _eventRepository.GetAll().Where(x => x.Type == EventType.Payment).Select(x => x.Id).ToList();
            }
            else
            {
                paymentEventIdList = _eventRepository.GetAll().Where(x => x.Type == EventType.Payment && x.StudentId == studentId).Select(x => x.Id).ToList();
            }


            foreach (var item in paymentEventIdList)
            {
                _eventRepository.Delete(item);
            }

            CreatePaymentEvent(studentId);
        }

        public void CreatePaymentEvent(int? studentId)
        {

            if (studentId == null)
            {

                var events = (from o in _eventRepository.GetAll().Where(x => x.Status != EventStatus.NotCompleted)
                              select new CreateOrEditEventDto
                              {
                                  Id = o.Id,
                                  StartDate = o.StartDate,
                                  EndDate = o.EndDate,
                                  TotalHours = o.TotalHours,
                                  StudentId = o.StudentId
                              }).OrderBy(x => x.StartDate).ToList();

                var students = (from o in _studentRepository.GetAll()
                                select new StudentDto
                                {
                                    Id = o.Id,
                                    HourlyPaymentPeriod = o.HourlyPaymentPeriod,
                                    Name = o.Name,
                                    Surname = o.Surname,
                                }).ToList();

                var studentEvents = new List<CreateOrEditEventDto>();
                foreach (var student in students)
                {
                    int counter = 0;

                    studentEvents = events.Where(x => x.StudentId == student.Id).ToList();
                    foreach (var item in studentEvents)
                    {
                        counter += item.TotalHours;
                        if (counter >= student.HourlyPaymentPeriod)
                        {
                            #region CreatePaymentEvent
                            var paymentEvent = new Event
                            {
                                Type = EventType.Payment,
                                Title = student.Name + " " + student.Surname + "-" + L("Payment"),
                                Description = "",
                                StudentId = student.Id,
                                StartDate = item.StartDate
                            };

                            _eventRepository.Insert(paymentEvent);
                            #endregion

                            counter -= student.HourlyPaymentPeriod;
                        }
                    }
                }

            }
            else
            {

                var events = (from o in _eventRepository.GetAll().Where(x => x.StudentId == studentId && x.Status != EventStatus.NotCompleted)
                              select new CreateOrEditEventDto
                              {
                                  Id = o.Id,
                                  StartDate = o.StartDate,
                                  EndDate = o.EndDate,
                                  TotalHours = o.TotalHours,
                              }).OrderBy(x => x.StartDate).ToList();

                var student = (from o in _studentRepository.GetAll().Where(x => x.Id == studentId)
                               select new StudentDto
                               {
                                   Id = o.Id,
                                   HourlyPaymentPeriod = o.HourlyPaymentPeriod,
                                   Name = o.Name,
                                   Surname = o.Surname,
                               }).FirstOrDefault();
                int counter = 0;

                foreach (var item in events)
                {
                    counter += item.TotalHours;
                    if (counter >= student.HourlyPaymentPeriod)
                    {
                        #region CreatePaymentEvent
                        var paymentEvent = new Event
                        {
                            Type = EventType.Payment,
                            Title = student.Name + " " + student.Surname + "-" + L("Payment"),
                            Description = "",
                            StudentId = studentId,
                            StartDate = item.StartDate
                        };

                        _eventRepository.Insert(paymentEvent);
                        #endregion

                        counter -= student.HourlyPaymentPeriod;
                    }
                }
            }
        }
    }
}
