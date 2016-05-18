namespace College_System.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=DatabaseModel")
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcademicStaff>()
                .HasMany(e => e.Courses)
                .WithMany(e => e.AcademicStaffs)
                .Map(m => m.ToTable("AcademicStaffCourses").MapLeftKey("InstructorID").MapRightKey("CourseID"));

            modelBuilder.Entity<AcademicStaffRole>()
                .HasMany(e => e.AcademicStaffs)
                .WithRequired(e => e.AcademicStaffRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.AcademicYearSections)
                .WithRequired(e => e.AcademicYear)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.AcademicYear)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Departments)
                .WithRequired(e => e.AcademicYear)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Students)
                .WithRequired(e => e.AcademicYear)
                .HasForeignKey(e => e.CurrentAcademicYearID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AcademicYearSection>()
                .HasMany(e => e.Students)
                .WithRequired(e => e.AcademicYearSection)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseGrade>()
                .HasMany(e => e.StudentGrades)
                .WithRequired(e => e.CourseGrade)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cours>()
                .HasMany(e => e.CourseGrades)
                .WithRequired(e => e.Cours)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cours>()
                .HasMany(e => e.CourseMaterials)
                .WithRequired(e => e.Cours)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cours>()
                .HasMany(e => e.StudentCourses)
                .WithRequired(e => e.Cours)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.AcademicStaffs)
                .WithRequired(e => e.Department)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MaterialType>()
                .HasMany(e => e.CourseMaterials)
                .WithRequired(e => e.MaterialType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Semester>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.Semester)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StudentGrade>()
                .Property(e => e.Mark)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentCourses)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentGrades)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);
        }
    }
}
