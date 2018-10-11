using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class Message
    {
        public int Id { get; set; }
        public MessageType Type { get; set; }
        public string Text { get; set; }
    }


}
