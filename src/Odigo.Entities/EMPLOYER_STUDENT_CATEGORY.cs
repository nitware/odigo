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
    
    public partial class EMPLOYER_STUDENT_CATEGORY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMPLOYER_STUDENT_CATEGORY()
        {
            this.EMPLOYER_DESIRED_TIME = new HashSet<EMPLOYER_DESIRED_TIME>();
            this.REQUEST_FOR_EMPLOYMENT_COST_IMPLICATION = new HashSet<REQUEST_FOR_EMPLOYMENT_COST_IMPLICATION>();
        }
    
        public long Employer_Student_Category_Id { get; set; }
        public long Person_Id { get; set; }
        public int Student_Category_Id { get; set; }
        public int No_Of_Student { get; set; }
        public int Teacher_Type_Id { get; set; }
    
        public virtual EMPLOYER EMPLOYER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMPLOYER_DESIRED_TIME> EMPLOYER_DESIRED_TIME { get; set; }
        public virtual STUDENT_CATEGORY STUDENT_CATEGORY { get; set; }
        public virtual TEACHER_TYPE TEACHER_TYPE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REQUEST_FOR_EMPLOYMENT_COST_IMPLICATION> REQUEST_FOR_EMPLOYMENT_COST_IMPLICATION { get; set; }
    }
}
