using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Shared.Filters;

namespace RestaurantManagement.Reservation.Api.Features.Reservations.Create
{
    public static class CreateReservationCommandEndpoint
    {
        public static RouteGroupBuilder CreateTableGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                    async (CreateReservationCommand command, IMediator mediator) =>
                    (await mediator.Send(command)).ToGenericResult())
                .WithName("CreateReservation")
                .MapToApiVersion(1, 0)
                .Produces<Guid>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status404NotFound)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .AddEndpointFilter<ValidationFilter<CreateReservationCommand>>() 
                .RequireAuthorization(policyNames: "InstructorPolicy");

            return group;
        }
    }
}
