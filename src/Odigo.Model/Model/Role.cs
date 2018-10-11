using System.Collections.Generic;

namespace Odigo.Model.Model
{
    public class Role : Setup
    {
        public enum EnumName
        {
            Admin = 1,
            Guest = 2,
            Teacher = 3,
            Parent = 4,
            Student = 5,
            Applicant = 6
        }

        public PersonRight UserRight { get; set; }
        public List<Right> Rights { get; set; }
        public bool HasUser { get; set; }
    }
}