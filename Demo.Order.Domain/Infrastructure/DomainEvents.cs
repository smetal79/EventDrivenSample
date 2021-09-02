using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Order.Domain.Infrastructure
{
    public interface IDomainEvent { }

    public interface IHandleDomainEvent<T> where T : IDomainEvent
    {
        Task Handle(T @event);
    }
    
    public static class DomainEvents
    {
        public static IServiceProvider Services { get; set; }
        
        public static void Raise<T>(T @event) where T : IDomainEvent
        {
            foreach (var handler in Services.GetServices<IHandleDomainEvent<T>>())
            {
                handler.Handle(@event);
            }
        }
    }
}