using FoodSharing.Models;
using FoodSharing.Models.Products;
using FoodSharing.Models.Products.ProductCategories;
using Npgsql;

namespace FoodSharing.Services.Products.Converters
{
    public static class ProductConverter
	{
		public static async Task<Product> MapToProduct(NpgsqlDataReader reader)
		{
			Product product = new Product();

			if (reader.HasRows)
			{
				while (await reader.ReadAsync())
				{
					product.Id = (Guid)reader["Id"];
					product.UserId = (Guid)reader["UserId"];
					product.Name = (string)reader["Name"];
					product.Description = (string)reader["Description"];
					product.CategoryId = (int)reader["CategoryId"];
					product.Quantity = (string)reader["Quantity"];
					product.Image = (byte[])reader["Image"];
					product.CreatedAt = (DateTime)reader["CreatedAt"];
				}
			}
			else
			{
				return null;
			}

			return product;
		}

		public static async Task<List<Product>> MapToProducts(NpgsqlDataReader reader)
		{
			List<Product> products = new List<Product>();

			if (reader.HasRows)
			{
				while (await reader.ReadAsync())
				{
					Product product = new Product();

					product.Id = (Guid)reader["Id"];
					product.UserId = (Guid)reader["UserId"];
					product.Name = (string)reader["Name"];
					product.Description = (string)reader["Description"];
					product.CategoryId = (int)reader["CategoryId"];
					product.Quantity = (string)reader["Quantity"];
					product.Image = (byte[])reader["Image"];
					product.CreatedAt = (DateTime)reader["CreatedAt"];

					products.Add(product);
				}
			}
			else
			{
				return new List<Product>();
			}

			return products;
		}

		public static async Task<List<ProductCategory>> MapToProductCategories(NpgsqlDataReader reader)
		{
			List<ProductCategory> productCategories = new List<ProductCategory>();

			if (reader.HasRows)
			{
				while (await reader.ReadAsync())
				{
					ProductCategory productCategory = new ProductCategory();

					productCategory.Id = (int)reader["Id"];
					productCategory.Name = (string)reader["Name"];
					productCategory.CreatedAt = (DateTime)reader["CreatedAt"];

					productCategories.Add(productCategory);
				}
			}
			else
			{
				return new List<ProductCategory>();
			}

			return productCategories;
		}

		public static async Task<ProductCategory> MapToProductCategory(NpgsqlDataReader reader)
		{
			ProductCategory productCategories = new ProductCategory();

			if (reader.HasRows)
			{
				while (await reader.ReadAsync())
				{
					productCategories.Id = (int)reader["Id"];
					productCategories.Name = (string)reader["Name"];
					productCategories.CreatedAt = (DateTime)reader["CreatedAt"];
				}
			}
			else
			{
				return null;
			}

			return productCategories;
		}

		public static ProductView MapToUserProductsView(Product product)
		{
			return new ProductView(product.Id, product.UserId, product.Name, product.Description, product.CategoryId, null,
				product.Quantity, product.Image, product.CreatedAt);
		}
	}
}
