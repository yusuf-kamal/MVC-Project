using System.ComponentModel.DataAnnotations;

namespace Demo.Pl.Models.Account
{
    public class ForgetPasswordViewModel
    {

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "invalid Email")]
        public string Email { get; set; } 
    }
}
