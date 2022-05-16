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
        
        public DateTime CreatedAt { get; set; }
        public byte[] Image { get; set; }

        public IFormFile IFormFile { get; set; }

        public ProductsViewModel(){}

        public ProductsViewModel(Guid id,
                                Guid userId,
                                string name,
                                string description,
                                string category,
                                string quantity,
                                byte[] image,
                                DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Description = description;
            Category = category;
            Quantity = quantity;
            Image = image;
            CreatedAt = createdAt;
        }
    }
}
