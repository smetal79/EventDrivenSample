using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Demo.Order.Api.Dtos
{
    public class CreateOrderRequest: IRequest<Guid>
    {
        [Required]
        public decimal Total { get; set; }
    }
}