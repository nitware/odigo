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
    
    public partial class PAYMENT_WEB
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PAYMENT_WEB()
        {
            this.PAYMENT_INTERSWITCH_TRANSACTION = new HashSet<PAYMENT_INTERSWITCH_TRANSACTION>();
        }
    
        public long Payment_Id { get; set; }
        public int Payment_Channel_Id { get; set; }
        public string Transaction_Number { get; set; }
        public Nullable<System.DateTime> Transaction_Date { get; set; }
    
        public virtual PAYMENT PAYMENT { get; set; }
        public virtual PAYMENT_CHANNEL PAYMENT_CHANNEL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PAYMENT_INTERSWITCH_TRANSACTION> PAYMENT_INTERSWITCH_TRANSACTION { get; set; }
    }
}
