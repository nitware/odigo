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
    
    public partial class REQUEST
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public REQUEST()
        {
            this.REQUEST_FOR_EMPLOYMENT_COST_IMPLICATION = new HashSet<REQUEST_FOR_EMPLOYMENT_COST_IMPLICATION>();
        }
    
        public long Request_Id { get; set; }
        public long From_Person_Id { get; set; }
        public long To_Person_Id { get; set; }
        public int Request_Message_Id { get; set; }
        public Nullable<int> Reply_Message_Id { get; set; }
        public int Service_Charge_Id { get; set; }
        public int Request_Status_Id { get; set; }
        public System.DateTime Request_Date { get; set; }
        public Nullable<System.DateTime> Reply_Date { get; set; }
    
        public virtual MESSAGE MESSAGE { get; set; }
        public virtual MESSAGE MESSAGE1 { get; set; }
        public virtual PERSON PERSON { get; set; }
        public virtual PERSON PERSON1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REQUEST_FOR_EMPLOYMENT_COST_IMPLICATION> REQUEST_FOR_EMPLOYMENT_COST_IMPLICATION { get; set; }
        public virtual REQUEST_STATUS REQUEST_STATUS { get; set; }
        public virtual SERVICE_CHARGE SERVICE_CHARGE { get; set; }
    }
}
