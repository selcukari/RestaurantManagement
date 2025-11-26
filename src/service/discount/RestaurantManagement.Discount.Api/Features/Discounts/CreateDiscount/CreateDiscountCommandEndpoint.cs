using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Shared.Filters;

namespace RestaurantManagement.Discount.Api.Features.Discounts.CreateDiscount
{
    public static class GetDiscountByCodeEndpoint
    {
        public static RouteGroupBuilder CreateDiscountGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                    async (CreateDiscountCommand command, IMediator mediator) =>
                        (await mediator.Send(command)).ToGenericResult())
                .WithName("CreateDiscount")
                .MapToApiVersion(1, 0)
                .Produces<Guid>(StatusCodes.Status201Created)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .AddEndpointFilter<ValidationFilter<CreateDiscountCommand>>().AllowAnonymous();

            return group;
        }
    }
}
