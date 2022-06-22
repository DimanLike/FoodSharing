using FoodSharing.Models.Products.ProductCategories;
using System.ComponentModel.DataAnnotations;

namespace FoodSharing.Models
{
	public class ProductView
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }

		[Display(Name = "Название")]
		public string Name { get; set; }

		[Display(Name = "Описание")]
		public string Description { get; set; }

		[Display(Name = "Категория")]
		public string CategoryName { get; set; }

		public string CategoryIcon { get; set; }
		public int CategoryId { get; set; }

		[Display(Name = "Количество")]
		public string Quantity { get; set; }

		[Display(Name = "Изображение")]
		public byte[] Image { get; set; }
		public DateTime CreatedAt { get; set; }
		public IFormFile IFormFile { get; set; }
		public Boolean IsFavourite { get; set; }
		public List<ProductCategory> ProductCategories { get; set; }
		public string Email { get; set; }
		public string Time { get; set; }

		// здесь пустой конструктор нужен, потому что мы создали конструктор с параметрами, поэтому пустой конструктор по умолчанию не создастся
		public ProductView(){}

		public ProductView(Guid id, 
							Guid userId, 
							string email,  
							string name, 
							string description, 
							int categoryId, 
							string categoryName, 
							string categoryIcon,
							string quantity,
							byte[] image, 
							Boolean isFavourite,
							DateTime createdAt,
							string time)
		{
			Id = id;
			UserId = userId;
			Email = email;
			Name = name;
			Description = description;
			CategoryId = categoryId;
			CategoryName = categoryName;
			CategoryIcon = categoryIcon;
			Quantity = quantity;
			Image = image;
			IsFavourite = isFavourite;
			CreatedAt = createdAt;
			Time = time;
		}

        
    }
}
