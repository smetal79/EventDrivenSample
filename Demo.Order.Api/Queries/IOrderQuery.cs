using System;
using System.Threading.Tasks;
using Demo.Order.Api.Dtos;

namespace Demo.Order.Api.Queries
{
    public interface IOrderQuery
    {
        Task<OrderResponse> Get(Guid orderKey);
    }
}