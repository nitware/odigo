using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class ApplicationMessage
    {
        public ApplicationMessage() { }
        public ApplicationMessage(string message)
        {
            Description = message;
        }
        public ApplicationMessage(string message, int type)
        {
            Description = message;
            Type = type;
        }
        public ApplicationMessage(string title, string message, int type)
        {
            Title = title;
            Description = message;
            Type = type;
        }

        public enum Category
        {
            Warning = 1,
            Error = 2,
            Information = 3
        }

        public int Type { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Advice { get; set; }
        public string Action { get; set; }
    }
}
