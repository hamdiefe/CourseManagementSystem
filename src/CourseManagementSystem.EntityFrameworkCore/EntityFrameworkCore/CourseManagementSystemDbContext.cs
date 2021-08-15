using Abp.Zero.EntityFrameworkCore;
using CourseManagementSystem.Authorization.Roles;
using CourseManagementSystem.Authorization.Users;
using CourseManagementSystem.Models.Addresses;
using CourseManagementSystem.Models.Cities;
using CourseManagementSystem.Models.Countries;
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
using CourseManagementSystem.Models.Towns;
using CourseManagementSystem.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using System;

namespace CourseManagementSystem.EntityFrameworkCore
{
    public class CourseManagementSystemDbContext : AbpZeroDbContext<Tenant, Role, User, CourseManagementSystemDbContext>
    {
        public virtual DbSet<Document> Documents { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<TeacherSpecializedField> TeacherSpecializedFields { get; set; }

        public virtual DbSet<Teacher> Teachers { get; set; }

        public virtual DbSet<StudentParent> StudentParents { get; set; }

        public virtual DbSet<Parent> Parents { get; set; }

        public virtual DbSet<ParentType> ParentTypes { get; set; }

        public virtual DbSet<Job> Jobs { get; set; }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<Town> Towns { get; set; }

        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Phone> Phones { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<School> Schools { get; set; }

        public virtual DbSet<Grade> Grades { get; set; }


        public CourseManagementSystemDbContext(DbContextOptions<CourseManagementSystemDbContext> options)
            : base(options)
        {
        }

        public override bool Equals(object obj)
        {
            return obj is CourseManagementSystemDbContext context &&
                   base.Equals(obj) &&
                   System.Collections.Generic.EqualityComparer<DbSet<Teacher>>.Default.Equals(Teachers, context.Teachers);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Teachers);
        }
    }
}
