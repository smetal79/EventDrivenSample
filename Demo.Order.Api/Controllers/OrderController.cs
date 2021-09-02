using System;
using System.Threading.Tasks;
using Demo.Order.Api.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;

namespace Demo.Order.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class OrdersController : Controller
    {
        private readonly IMessageSession messageSession;
        private readonly IMediator mediator;

        public OrdersController(IMessageSession messageSession, IMediator mediator)
        {
            this.messageSession = messageSession;
            this.mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody]CreateOrderRequest request)
        {
            var orderKey = await this.mediator.Send(request);
            return OrderAccepted(orderKey);
        }
        
        [HttpGet("{key:guid}")]
        public async Task<IActionResult> GetOrder(Guid key)
        {
            var result = await this.mediator.Send(new GetOrder(key));
            return result == default ? NotFound() : Ok(result);
        }
        
        [HttpPost("{key:guid}/submit")]
        public async Task<IActionResult> Submit(Guid key)
        {
            var result = await this.mediator.Send(new SubmitOrder(key));

            return result switch
            {
                SubmitOrderStatus.NotFound => NotFound(),
                SubmitOrderStatus.InvalidState => BadRequest(new {error = "Order is not in draft state"}),
                SubmitOrderStatus.Success => OrderAccepted(key),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private IActionResult OrderAccepted(Guid key)
        {
            var url = new UriBuilder()
            {
                Path = Url.Action(nameof(GetOrder), "Orders", new {key = key})
            };
            
            return Accepted(url.ToString());
        }
    }
}