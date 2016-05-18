namespace College_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            StudentCourses = new HashSet<StudentCours>();
            StudentGrades = new HashSet<StudentGrade>();
        }

        public Guid StudentID { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        public int AdmissionYear { get; set; }

        public int GraduationYear { get; set; }

        public Guid CurrentAcademicYearID { get; set; }

        public Guid SectionID { get; set; }

        [StringLength(50)]
        public string SerialID { get; set; }

        public Guid? DepartmentID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual AcademicYearSection AcademicYearSection { get; set; }

        public virtual Department Department { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentCours> StudentCourses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentGrade> StudentGrades { get; set; }
    }
}
