using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class Person
    {
        public enum Types
        {
            SupperAdmin = 0,
            Admin = 1,
            Teacher = 2,
            Parent = 3,
            School = 4
        }


        public long Id { get; set; }
        public PersonType Type { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string LastName { get; set; }

        [Display(Name = "Other Name")]
        public string OtherName { get; set; }
                
        [Required]
        [Display(Name = "Contact Address")]
        public string ContactAddress { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Mobile Phone")]
        public string MobilePhone { get; set; }
               
        public Country Country { get; set; }
        public DateTime DateEntered { get; set; }
        public State State { get; set; }
        public Lga Lga { get; set; }
        public bool IsBlacklisted { get; set; }

        //public LoginDetail LoginDetail { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + " " + FirstName + " " + OtherName; }
        }

        [Display(Name = "Name")]
        public string Name
        {
            get { return LastName + " " + FirstName; }
        }

        
        



       

    }



}
