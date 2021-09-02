using System;
using System.Threading.Tasks;

namespace Demo.Order.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<T> GetByKey<T>(Guid key) where T : Entities.Order;

        Task Save<T>(T entity) where T : Entities.Order;

        Task SaveAs<T, TN>(TN entity)
            where T : Entities.Order
            where TN : Entities.Order;
    }
}