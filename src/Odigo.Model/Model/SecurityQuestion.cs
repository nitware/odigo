using System.ComponentModel.DataAnnotations;

namespace Odigo.Model.Model
{
    public class SecurityQuestion
    {
        [Display(Name = "Security Question")]
        public int Id { get; set; }

        [Display(Name = "Security Question")]
        public string Question { get; set; }
    }


}