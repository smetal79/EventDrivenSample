using System;
using System.Threading.Tasks;
using Dapper;
using Demo.Order.Api.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Demo.Order.Api.Queries
{
    public sealed class OrderQuery : IOrderQuery
    {
        private readonly string connectionString;

        public OrderQuery(IConfiguration configuration)
            => this.connectionString = configuration.GetConnectionString("Db");

        public async Task<OrderResponse> Get(Guid orderKey)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();
                
            var result = await conn
                .QueryFirstOrDefaultAsync<OrderResponse>("SELECT Key, Total, Discriminator FROM Orders Where Key=@Key",
                    new {Key = orderKey});
            return result;
        }
    }
}