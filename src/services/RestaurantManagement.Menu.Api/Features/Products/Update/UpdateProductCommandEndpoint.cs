using RestaurantManagement.Shared.Filters;

namespace RestaurantManagement.Menu.Api.Features.Products.Update
{
    public static class UpdateProductCommandEndpoint
    {
        public static RouteGroupBuilder UpdateProductGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/",
                    async (UpdateProductCommand command, IMediator mediator) =>
                        (await mediator.Send(command)).ToGenericResult())
                .WithName("UpdateProduct")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<UpdateProductCommand>>();
                // .RequireAuthorization(policyNames: "InstructorPolicy");

            return group;
        }
    }
}
