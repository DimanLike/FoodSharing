using System.ComponentModel.DataAnnotations;

namespace FoodSharing.Models
{
    public class RegistrationView
    {
        [Required(ErrorMessage = "Email не может быть пустым")]
        [Display(Name ="Адрес электронной почты")]
        public string Email { get; set; }
            
        [Required(ErrorMessage = "Поле пароль является обязательным")]
        [Display(Name = "Пароль")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email не может быть пустым")]
        [Display(Name = "Адрес электронной почты")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле пароль является обязательным")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }


}
