using System.ComponentModel.DataAnnotations;

namespace FoodSharing.Models
{
    public class ProductsViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Категория")]
        public string Category { get; set; }

        [Display(Name = "Количество")]
        public string Quantity { get; set; }

        [Display(Name = "Изображение")]
        public byte[] Image { get; set; }
        public DateTime CreatedAt { get; set; }

        public IFormFile IFormFile { get; set; }

        public ProductsViewModel(){}


    }
}
