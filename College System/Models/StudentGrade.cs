namespace College_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StudentGrade
    {
        [Key]
        [Column(Order = 0)]
        public Guid StudentID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid GradeID { get; set; }

        public decimal Mark { get; set; }

        public int Year { get; set; }

        public virtual CourseGrade CourseGrade { get; set; }

        public virtual Student Student { get; set; }
    }
}
