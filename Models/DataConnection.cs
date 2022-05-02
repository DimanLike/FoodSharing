using Npgsql;

namespace FoodSharing.Models
{
    public class DataConnection
    {
        private readonly IConfiguration _config;

        public DataConnection(IConfiguration config)
        {
            _config = config;
        }

        public string ConnectionString()
        {
            return _config.GetConnectionString("DefaultConnection");
        }

        public async Task ExecuteNonQuery(string expression,NpgsqlParameter[] parameters = null)
        {
            var connectionString = ConnectionString();

            await using var db = new NpgsqlConnection(connectionString);
            try
            {
                await db.OpenAsync();

                Guid uuid1 = Guid.NewGuid();

                await using var cmd = new NpgsqlCommand(expression, db);
                if (parameters != null)
                    for (int i = 0; i < parameters.Length; i++)
                    {

                        cmd.Parameters.Add(parameters[i]);
                    }
                var result = cmd.ExecuteNonQuery();
                await cmd.ExecuteNonQueryAsync();
            }
            finally
            { 
                db.Dispose();
                GC.SuppressFinalize(db);
            }
                   
        }

    }
}
