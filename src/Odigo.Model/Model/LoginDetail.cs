using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class LoginDetail
    {
        public Person Person { get; set; }
        //public string Username { get; set; }
        public byte[] Password { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string RawPassword { get; set; }

        [Compare("RawPassword")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public SecurityQuestion SecurityQuestion { get; set; }

        [Required]
        [Display(Name = "Security Answer")]
        public string SecurityAnswer { get; set; }

        public Role Role { get; set; }
        public bool IsActivated { get; set; }
        public bool IsLocked { get; set; }
        public bool IsFirstLogon { get; set; }
        public DateTime? FirstLogonDate { get; set; }
        public DateTime? LastLogonDate { get; set; }

    }
}
