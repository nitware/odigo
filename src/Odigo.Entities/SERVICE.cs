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
    
    public partial class SERVICE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SERVICE()
        {
            this.SERVICE_CHARGE = new HashSet<SERVICE_CHARGE>();
        }
    
        public int Service_Id { get; set; }
        public string Service_Name { get; set; }
        public string Service_Description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SERVICE_CHARGE> SERVICE_CHARGE { get; set; }
    }
}
