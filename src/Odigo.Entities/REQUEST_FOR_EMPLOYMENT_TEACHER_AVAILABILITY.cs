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
    
    public partial class REQUEST_FOR_EMPLOYMENT_TEACHER_AVAILABILITY
    {
        public long Request_For_Employment_Teacher_Availability_Id { get; set; }
        public long Request_For_Employment_Cost_Implication_Id { get; set; }
        public long Teacher_Availability_Id { get; set; }
        public int Teaching_Cost_Id { get; set; }
    
        public virtual REQUEST_FOR_EMPLOYMENT_COST_IMPLICATION REQUEST_FOR_EMPLOYMENT_COST_IMPLICATION { get; set; }
        public virtual TEACHER_AVAILABILITY TEACHER_AVAILABILITY { get; set; }
        public virtual TEACHING_COST TEACHING_COST { get; set; }
    }
}
