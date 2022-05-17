using Npgsql;

namespace FoodSharing.Tools.Database
{
    public class DbConnection
    {
        private string _connectionString;

        public DbConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<T> Get<T>(string expression, Func<NpgsqlDataReader, Task<T>> mapper, NpgsqlParameter[] parameters = default)
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            try
            {
                await conn.OpenAsync();

                await using var cmd = new NpgsqlCommand(expression, conn);

                foreach (NpgsqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }

                return await mapper(await cmd.ExecuteReaderAsync());
            }
            finally
            {
                await conn.CloseAsync();
                await conn.DisposeAsync();
            }
        }

        public async Task<List<T>> GetList<T>(string expression, Func<NpgsqlDataReader, Task<List<T>>> mapper, NpgsqlParameter[] parameters = default)
		{
            await using var conn = new NpgsqlConnection(_connectionString);
            try
            {
                await conn.OpenAsync();

                await using var cmd = new NpgsqlCommand(expression, conn);

                if (parameters is not null)
                {
                    foreach (NpgsqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }

                }

                return await mapper(await cmd.ExecuteReaderAsync());
            }
            finally
            {
                await conn.CloseAsync();
                await conn.DisposeAsync();
            }
        }

        public async Task Add(string expression, NpgsqlParameter[] parameters = default)
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            try
            {
                await conn.OpenAsync();

                await using var cmd = new NpgsqlCommand(expression, conn);

                foreach (NpgsqlParameter parameter in parameters)
                {
                    cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value ?? DBNull.Value);
                }
                await cmd.ExecuteNonQueryAsync();
                
            }
            finally
            {
                await conn.CloseAsync();
                await conn.DisposeAsync();
            }

        }

    }
}
