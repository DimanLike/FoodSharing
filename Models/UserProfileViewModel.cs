using FoodSharing.Models.Users;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace FoodSharing.Models
{
    public class UserProfileViewModel
    {
   
        public Guid Id { get; set; }

        [Display(Name = "Имя")]
        [ValidateNever]
        public string FirstName { get; set; }

        [ValidateNever]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Адрес")]
        [ValidateNever]
        public string Adress { get; set; }

        [ValidateNever]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [ValidateNever]
        public IFormFile Image { get; set; }
        [ValidateNever]
        public byte[] Avatar { get; set; }

        public UserProfileViewModel(){}

        public UserProfileViewModel(Guid id, string firstName, string lastName, string email, string adress, string phone, byte[] avatar)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Adress = adress;
            Phone = phone;
            Avatar = avatar;
        }
    }


}