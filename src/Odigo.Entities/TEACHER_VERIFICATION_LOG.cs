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
    
    public partial class TEACHER_VERIFICATION_LOG
    {
        public long Teacher_Verification_Log_Id { get; set; }
        public long Person_Id { get; set; }
        public int Verification_Status_Id { get; set; }
        public long Verified_By { get; set; }
        public System.DateTime Date_Verified { get; set; }
    
        public virtual TEACHER TEACHER { get; set; }
    }
}
