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
    
    public partial class CourseMaterial
    {
        public System.Guid MaterialID { get; set; }
        public System.Guid CourseID { get; set; }
        public System.Guid MaterialTypeID { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
    
        public virtual Cours Cours { get; set; }
        public virtual MaterialType MaterialType { get; set; }
    }
}
