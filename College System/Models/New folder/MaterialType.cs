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
    
    public partial class MaterialType
    {
        public MaterialType()
        {
            this.CourseMaterials = new HashSet<CourseMaterial>();
        }
    
        public System.Guid MaterialTypeID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<CourseMaterial> CourseMaterials { get; set; }
    }
}