using FoodSharing.Models.Users;
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
        public int CategoryId { get; set; }

        [Display(Name = "Количество")]
        public string Quantity { get; set; }

        [Display(Name = "Изображение")]
        
        public DateTime CreatedAt { get; set; }
        public byte[] Image { get; set; }

        public IFormFile IFormFile { get; set; }
         
        public List<ProductCategories> Categories { get; set; }

        public ProductsViewModel(){}

        public ProductsViewModel(Guid id,
                                Guid userId,
                                string name,
                                string description,
                                int categoryId,
                                string quantity,
                                byte[] image,
                                DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Description = description;
            CategoryId = categoryId;
            Quantity = quantity;
            Image = image;
            CreatedAt = createdAt;
        }
    }
}
