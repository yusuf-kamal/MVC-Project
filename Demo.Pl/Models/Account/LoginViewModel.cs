using System.ComponentModel.DataAnnotations;

namespace Demo.Pl.Models.Account
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [MinLength(3, ErrorMessage = "Min length is 3 chars")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
}
}
