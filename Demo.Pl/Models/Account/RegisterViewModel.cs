using System.ComponentModel.DataAnnotations;

namespace Demo.Pl.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Email is Required")]
        [EmailAddress(ErrorMessage ="invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is Required")]
        [MinLength(3,ErrorMessage ="Min length is 3 chars")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm Password is Required")]
        [Compare("Password")]

        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
