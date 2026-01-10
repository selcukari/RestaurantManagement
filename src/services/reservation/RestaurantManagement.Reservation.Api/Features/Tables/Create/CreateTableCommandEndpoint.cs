using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Shared.Filters;

namespace RestaurantManagement.Reservation.Api.Features.Tables.Create
{
    public static class CreateTableCommandEndpoint
    {
        public static RouteGroupBuilder CreateTableGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                    async (CreateTableCommand command, IMediator mediator) =>
                    (await mediator.Send(command)).ToGenericResult())
                .WithName("CreateTable")
                .MapToApiVersion(1, 0)
                .Produces<Guid>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status404NotFound)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .AddEndpointFilter<ValidationFilter<CreateTableCommand>>() 
                .RequireAuthorization(policyNames: "InstructorPolicy");

            return group;
        }
    }
}
