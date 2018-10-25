using System.ComponentModel.DataAnnotations;

namespace Auction.WebSite.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "!")]
        [Display(Name = "Email пользователя")]
        public string Email { get; set; }

        [Required(ErrorMessage = "!")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegistrationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email пользователя")]
        public string NewEmail { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("NewPassword", ErrorMessage = "Пароль и подтверждение пароля не совпадают.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }

    public class ProfileViewModel
    {
        [EmailAddress]
        [Display(Name = "Email пользователя")]
        public string Email { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Пользователь")]
        public string FullName => $"{FirstName} {LastName}";
    }
}