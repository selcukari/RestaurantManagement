using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Order.Application.UseCases.Orders.CreateOrder;
using RestaurantManagement.Shared.Filters;
using RestaurantManagement.Shared.Extensions;

namespace RestaurantManagement.Order.Api.Endpoints.Orders
{
    public static class CreateOrderEndpoint
    {
        public static RouteGroupBuilder CreateOrderGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                    async ([FromBody] CreateOrderCommand command, [FromServices] IMediator mediator) =>
                    (await mediator.Send(command)).ToGenericResult())
                .WithName("CreateOrder")
                .MapToApiVersion(1, 0)
                .Produces<Guid>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status404NotFound)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .AddEndpointFilter<ValidationFilter<CreateOrderCommand>>();

            return group;
        }
    }
}
