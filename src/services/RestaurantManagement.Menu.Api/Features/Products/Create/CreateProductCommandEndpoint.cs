using RestaurantManagement.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagement.Menu.Api.Features.Products.Create
{
    public static class CreateProductCommandEndpoint
    {
        public static RouteGroupBuilder CreateProductGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                    async ([FromForm] CreateProductCommand command, IMediator mediator) =>
                    (await mediator.Send(command)).ToGenericResult())
                .WithName("CreateProduct")
                .MapToApiVersion(1, 0)
                .Produces<Guid>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status404NotFound)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .AddEndpointFilter<ValidationFilter<CreateProductCommand>>().DisableAntiforgery()
                .RequireAuthorization(policyNames: "InstructorPolicy");

            return group;
        }
    }
}
