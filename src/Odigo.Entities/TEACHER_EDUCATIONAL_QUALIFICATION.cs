//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Odigo.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class TEACHER_EDUCATIONAL_QUALIFICATION
    {
        public long Teacher_Educational_Qualification_Id { get; set; }
        public long Person_Id { get; set; }
        public int School_Type_Id { get; set; }
        public string School { get; set; }
        public int Year_Of_Admission { get; set; }
        public int Year_Of_Graduation { get; set; }
        public int Qualification_Id { get; set; }
    
        public virtual QUALIFICATION QUALIFICATION { get; set; }
        public virtual SCHOOL_TYPE SCHOOL_TYPE { get; set; }
        public virtual TEACHER TEACHER { get; set; }
    }
}
