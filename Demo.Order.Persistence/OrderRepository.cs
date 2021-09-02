using System;
using System.Linq;
using System.Threading.Tasks;
using Demo.Order.Domain;
using Demo.Order.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Demo.Order.Persistence
{
    public sealed class OrderRepository : IOrderRepository
    {
        private readonly DemoContext demoContext;

        public OrderRepository(DemoContext demoContext)
        {
            this.demoContext = demoContext;
        }
        
        public async Task<T> GetByKey<T>(Guid key) where T : Domain.Entities.Order
        {
            return await demoContext
                .Orders
                .OfType<T>()
                .FirstOrDefaultAsync(e => e.Key == key);
        }
        
        public async Task SaveAs<T, TN>(TN entity) 
            where T : Domain.Entities.Order
            where TN: Domain.Entities.Order
        {
            var existing = await GetByKey<T>(entity.Key);
            this.demoContext.Remove(existing);

            this.demoContext.Orders.Add(entity);
            await this.demoContext.SaveChangesAsync();
        }

        public async Task Save<T>(T entity) where T : Domain.Entities.Order
        {
            this.demoContext.Orders.Add(entity);
            await this.demoContext.SaveChangesAsync();
        }
    }
}