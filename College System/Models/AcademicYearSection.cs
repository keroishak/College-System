namespace College_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AcademicYearSection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AcademicYearSection()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        public Guid SectionID { get; set; }

        public Guid AcademicYearID { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public Guid? DepartmentID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Department Department { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
