﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace College_System.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CollegeSystemEntities : DbContext
    {
        public CollegeSystemEntities()
            : base("name=CollegeSystemEntities")
        {
        }
    
    
        public virtual DbSet<AcademicStaff> AcademicStaffs { get; set; }
        public virtual DbSet<AcademicStaffRole> AcademicStaffRoles { get; set; }
        public virtual DbSet<AcademicYear> AcademicYears { get; set; }
        public virtual DbSet<AcademicYearSection> AcademicYearSections { get; set; }
        public virtual DbSet<CourseGrade> CourseGrades { get; set; }
        public virtual DbSet<CourseMaterial> CourseMaterials { get; set; }
        public virtual DbSet<Cours> Courses { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<MaterialType> MaterialTypes { get; set; }
        public virtual DbSet<Semester> Semesters { get; set; }
        public virtual DbSet<StudentCours> StudentCourses { get; set; }
        public virtual DbSet<StudentGrade> StudentGrades { get; set; }
        public virtual DbSet<Student> Students { get; set; }
    }
}