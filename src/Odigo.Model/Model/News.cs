using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Model.Model
{
    public class News
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        //[Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        public string ImageFileUrl { get; set; }
        public string ImageTitle { get; set; }
        //public Staff Staff { get; set; }
        public string Venue { get; set; }
        public byte? Hour { get; set; }
        public byte? Minute { get; set; }

    }




}
