using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Odigo.Model.Model
{
    public class RoleRight
    {
        public Role Role { get; set; }
        public Right Right { get; set; }
        public string Description { get; set; }
    }


}
