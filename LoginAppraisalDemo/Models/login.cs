using System.ComponentModel.DataAnnotations;

namespace performance_appraisal_system.Models
{
    public class login
    {
        [Required(ErrorMessage ="Please Enter The Email Address")]
        [DataType(DataType.EmailAddress)]
        public string  email { get; set; }
        [Required(ErrorMessage ="Please Enter The Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public bool KeepLoggedIn { get; set; }
    }
}
