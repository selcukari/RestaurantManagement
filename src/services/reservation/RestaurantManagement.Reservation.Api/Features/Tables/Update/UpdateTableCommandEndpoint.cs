using RestaurantManagement.Shared.Filters;

namespace RestaurantManagement.Reservation.Api.Features.Tables.Update;
    public static class UpdateTableCommandEndpoint
    {
        public static RouteGroupBuilder UpdateTableGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/",
                    async (UpdateTableCommand command, IMediator mediator) =>
                        (await mediator.Send(command)).ToGenericResult())
                .WithName("UpdateTable")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<UpdateTableCommand>>()
                .RequireAuthorization(policyNames: "InstructorPolicy");

            return group;
        }
    }
