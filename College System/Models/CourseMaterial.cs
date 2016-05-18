namespace College_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CourseMaterial
    {
        [Key]
        public Guid MaterialID { get; set; }

        public Guid CourseID { get; set; }

        public Guid MaterialTypeID { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(750)]
        public string FilePath { get; set; }

        public virtual Cours Cours { get; set; }

        public virtual MaterialType MaterialType { get; set; }
    }
}
