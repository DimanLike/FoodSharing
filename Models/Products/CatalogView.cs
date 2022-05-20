using System.ComponentModel.DataAnnotations;

namespace FoodSharing.Models
{
    public class CatalogView
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

		[Display(Name = "Название")]
		public string Name { get; set; }

		[Display(Name = "Описание")]
		public string Description { get; set; }

		[Display(Name = "Категория")]
		public string CategoryName { get; set; }
		public int CategoryId { get; set; }

		[Display(Name = "Количество")]
		public string Quantity { get; set; }

		[Display(Name = "Изображение")]
		public byte[] Image { get; set; }
		public DateTime CreatedAt { get; set; }
		public IFormFile IFormFile { get; set; }

		public string Email { get; set; }

		public CatalogView() { }

		public CatalogView(Guid id, Guid userId,string email, string name, string description, int categoryId, string categoryName, string quantity,
			byte[] image, DateTime createdAt)
		{
			Id = id;
			UserId = userId;
			Email = email;
			Name = name;
			Description = description;
			CategoryId = categoryId;
			CategoryName = categoryName;
			Quantity = quantity;
			Image = image;
			CreatedAt = createdAt;
		}
	}
}
