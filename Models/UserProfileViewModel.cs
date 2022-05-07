using System.ComponentModel.DataAnnotations;

namespace FoodSharing.Models
{
    public class UserProfileViewModel
    {


        public Guid Id { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Адрес")]
        public string Adress { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        public UserProfileViewModel()
        {

        }

        public UserProfileViewModel(Guid id, string firstName, string lastName, string email, string adress, string phone)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Adress = adress;
            Phone = phone;
        }
    }
}