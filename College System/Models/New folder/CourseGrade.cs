//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class CourseGrade
    {
        public CourseGrade()
        {
            this.StudentGrades = new HashSet<StudentGrade>();
        }
    
        public System.Guid GradeID { get; set; }
        public System.Guid CourseID { get; set; }
        public string Name { get; set; }
        public int Total { get; set; }
    
        public virtual Cours Cours { get; set; }
        public virtual ICollection<StudentGrade> StudentGrades { get; set; }
    }
}