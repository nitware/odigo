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
    
    public partial class SERVICE_CHARGE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SERVICE_CHARGE()
        {
            this.PAYMENT = new HashSet<PAYMENT>();
            this.REQUEST = new HashSet<REQUEST>();
        }
    
        public int Service_Charge_Id { get; set; }
        public int Service_Id { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime Date_Entered { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PAYMENT> PAYMENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REQUEST> REQUEST { get; set; }
        public virtual SERVICE SERVICE { get; set; }
    }
}
