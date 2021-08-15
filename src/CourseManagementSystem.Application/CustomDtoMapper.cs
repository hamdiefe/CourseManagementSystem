using Abp.Auditing;
using AutoMapper;
using CourseManagementSystem.Models.Addresses;
using CourseManagementSystem.Models.Documents;
using CourseManagementSystem.Models.Events;
using CourseManagementSystem.Models.Grades;
using CourseManagementSystem.Models.Jobs;
using CourseManagementSystem.Models.Parents;
using CourseManagementSystem.Models.ParentTypes;
using CourseManagementSystem.Models.Phones;
using CourseManagementSystem.Models.Schools;
using CourseManagementSystem.Models.StudentParents;
using CourseManagementSystem.Models.Students;
using CourseManagementSystem.Models.Teachers;
using CourseManagementSystem.Models.TeacherSpecializedFields;
using CourseManagementSystem.Services.Addresses.Dtos;
using CourseManagementSystem.Services.AuditLogs.Dtos;
using CourseManagementSystem.Services.Documents.Dtos;
using CourseManagementSystem.Services.Events.Dtos;
using CourseManagementSystem.Services.Grades.Dtos;
using CourseManagementSystem.Services.Jobs.Dtos;
using CourseManagementSystem.Services.Parents.Dtos;
using CourseManagementSystem.Services.ParentTypes.Dtos;
using CourseManagementSystem.Services.Phones.Dtos;
using CourseManagementSystem.Services.Schools.Dtos;
using CourseManagementSystem.Services.StudentParents.Dtos;
using CourseManagementSystem.Services.Students.Dtos;
using CourseManagementSystem.Services.Teachers.Dtos;
using CourseManagementSystem.Services.TeacherSpecializedFields.Dtos;

namespace CourseManagementSystem
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //Events
            configuration.CreateMap<CreateOrEditDocumentDto, Document>().ReverseMap();
            configuration.CreateMap<DocumentDto, Document>().ReverseMap();

            //AuditLogs
            configuration.CreateMap<AuditLogDto, AuditLog>().ReverseMap();

            //Events
            configuration.CreateMap<CreateOrEditEventDto, Event>().ReverseMap();
            configuration.CreateMap<EventDto, Event>().ReverseMap();

            //TeacherSpecializedFields
            configuration.CreateMap<CreateOrEditTeacherSpecializedFieldDto, TeacherSpecializedField>().ReverseMap();

            //Teachers
            configuration.CreateMap<CreateOrEditTeacherDto, Teacher>().ReverseMap();
            configuration.CreateMap<TeacherDto, Teacher>().ReverseMap();

            //StudentParents
            configuration.CreateMap<CreateOrEditStudentParentDto, StudentParent>().ReverseMap();

            //Parents
            configuration.CreateMap<CreateOrEditParentDto, Parent>().ReverseMap();
            configuration.CreateMap<ParentDto, Parent>().ReverseMap();

            //ParentTypes
            configuration.CreateMap<CreateOrEditParentTypeDto, ParentType>().ReverseMap();
            configuration.CreateMap<ParentTypeDto, ParentType>().ReverseMap();

            //Jobs
            configuration.CreateMap<CreateOrEditJobDto, Job>().ReverseMap();
            configuration.CreateMap<JobDto, Job>().ReverseMap();

            //Addresses
            configuration.CreateMap<CreateOrEditAddressDto, Address>().ReverseMap();

            //Phones
            configuration.CreateMap<CreateOrEditPhoneDto, Phone>().ReverseMap();

            //Grades
            configuration.CreateMap<CreateOrEditGradeDto, Grade>().ReverseMap();
            configuration.CreateMap<GradeDto, Grade>().ReverseMap();

            //Schools
            configuration.CreateMap<CreateOrEditSchoolDto, School>().ReverseMap();
            configuration.CreateMap<SchoolDto, School>().ReverseMap();

            //Students
            configuration.CreateMap<CreateOrEditStudentDto, Student>().ReverseMap();
            configuration.CreateMap<StudentDto, Student>().ReverseMap();
        }
    }
}