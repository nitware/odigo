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
    
    public partial class PAYMENT
    {
        public long Payment_Id { get; set; }
        public long Person_Id { get; set; }
        public int Payment_Mode_Id { get; set; }
        public int Service_Charge_Id { get; set; }
        public Nullable<long> Serial_Number { get; set; }
        public string Invoice_Number { get; set; }
        public System.DateTime Date_Entered { get; set; }
        public bool Paid { get; set; }
        public Nullable<System.DateTime> Date_Paid { get; set; }
    
        public virtual PAYMENT_WEB PAYMENT_WEB { get; set; }
        public virtual PAYMENT_MODE PAYMENT_MODE { get; set; }
        public virtual PERSON PERSON { get; set; }
        public virtual SERVICE_CHARGE SERVICE_CHARGE { get; set; }
    }
}