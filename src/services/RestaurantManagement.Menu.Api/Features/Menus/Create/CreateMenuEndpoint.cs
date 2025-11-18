using RestaurantManagement.Shared.Filters;

namespace RestaurantManagement.Menu.Api.Features.Menus.Create
{
    public static class CreateMenuEndpoint
    {
        public static RouteGroupBuilder CreateMenuGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                    async (CreateMenuCommand command, IMediator mediator) =>
                        (await mediator.Send(command)).ToGenericResult())
                .WithName("CreateMenu")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<CreateMenuCommand>>();


            return group;
        }
    }
}
