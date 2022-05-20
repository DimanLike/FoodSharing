using FoodSharing.Models;
using FoodSharing.Models.Products;
using FoodSharing.Models.Products.ProductCategories;
using FoodSharing.Services.Products.Converters;
using FoodSharing.Services.Products.Interfaces;
using FoodSharing.Tools.Database;
using Npgsql;

namespace FoodSharing.Services.Products.Repositories
{
    public class ProductRepository : IProductRepository
	{
		private DbConnection _dbConnection;

		public ProductRepository(IConfiguration config)
		{
			_dbConnection = new DbConnection(config.GetConnectionString("DefaultConnection"));
		}

		public Task AddProduct(ProductView model)
		{
			string expression = @"INSERT INTO products (id, userid, name, description, categoryid, quantity, image, createdat) 
								VALUES (@id, @userid, @name, @description, @categoryid, @quantity, @image, @createdat)";
			model.Id = Guid.NewGuid();
			model.CreatedAt = DateTime.Now;

			NpgsqlParameter[] parameters = new[]
			{
				new NpgsqlParameter(nameof(model.Id), model.Id),
				new NpgsqlParameter(nameof(model.UserId), model.UserId),
				new NpgsqlParameter(nameof(model.Name), model.Name),
				new NpgsqlParameter(nameof(model.Description), model.Description),
				new NpgsqlParameter(nameof(model.CategoryId), model.CategoryId),
				new NpgsqlParameter(nameof(model.Quantity), model.Quantity),
				new NpgsqlParameter(nameof(model.Image), model.Image),
				new NpgsqlParameter(nameof(model.CreatedAt), model.CreatedAt),
			};

			return _dbConnection.Add(expression, parameters);
		}

		public Task DeleteProduct(Guid id)
		{
			string expression = @"DELETE FROM products WHERE id = @id";

			NpgsqlParameter[] parameters = new[]
			{
				new NpgsqlParameter(nameof(id), id),
			};

			return _dbConnection.Add(expression, parameters);
		}

		public Task<List<Product>> GetProducts(Guid userid)
		{
			string expression = @"SELECT * FROM products WHERE userid = @userid";

			NpgsqlParameter[] parameters = new[]
			{
				new NpgsqlParameter(nameof(userid), userid),
			};

			return _dbConnection.GetList(expression, ProductConverter.MapToProducts, parameters);
		}

		public Task<List<Product>> GetCatalog(int categoryId)
        {
			string expression = @"SELECT * FROM products WHERE categoryId = @categoryId";

			NpgsqlParameter[] parameters = new[]
			{
				new NpgsqlParameter(nameof(categoryId), categoryId),
			};

			return _dbConnection.GetList(expression, ProductConverter.MapToProducts, parameters);
		}

		public Task<List<Product>> GetCatalog()
		{
			string expression = @"SELECT * FROM products";

			return _dbConnection.GetList(expression, ProductConverter.MapToProducts);
		}

		public Task<Product> GetProduct(Guid id)
        {
			string expression = @"SELECT * FROM products WHERE id = @id";

			NpgsqlParameter[] parameters = new[]
			{
				new NpgsqlParameter(nameof(id), id),
			};

			return _dbConnection.Get(expression, ProductConverter.MapToProduct, parameters);
		}

		public Task<List<ProductCategory>> GetProductCategories()
		{
			string expression = @"SELECT * FROM products_categories";

			return _dbConnection.GetList(expression, ProductConverter.MapToProductCategories);
		}

		public Task<List<ProductCategory>> GetProductCategories(int[] ids)
		{
			string expression = @"SELECT * FROM products_categories WHERE id = ANY(@ids)";

			NpgsqlParameter[] parameters = new[]
			{
				new NpgsqlParameter(nameof(ids), ids),
			};

			return _dbConnection.GetList(expression, ProductConverter.MapToProductCategories, parameters);
		}

		public Task<ProductCategory> GetProductCategory(int id)
        {
			string expression = @"SELECT * FROM products_categories WHERE id = @id";

			NpgsqlParameter[] parameters = new[]
			{
				new NpgsqlParameter(nameof(id), id),
			};

			return _dbConnection.Get(expression, ProductConverter.MapToProductCategory, parameters);
		}

		public Task EditProduct(ProductView model)
		{
            string expression = @"UPDATE products SET 
					name = @name,
					description = @description,
					categoryid = @categoryid,
					quantity = @quantity, 
					image = @image WHERE id = @id";

            NpgsqlParameter[] parameters = new[]
			{
				new NpgsqlParameter(nameof(model.Id), model.Id),
				new NpgsqlParameter(nameof(model.Name), model.Name),
				new NpgsqlParameter(nameof(model.Description), model.Description),
				new NpgsqlParameter(nameof(model.CategoryId), model.CategoryId),
				new NpgsqlParameter(nameof(model.Quantity), model.Quantity),
				new NpgsqlParameter(nameof(model.Image), model.Image),
			};

			return _dbConnection.Add(expression, parameters);
		}
	}
}
