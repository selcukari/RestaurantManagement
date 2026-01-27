using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagement.Discount.Api.Features.Discounts.GetDiscountByCode
{
    public static class GetDiscountByCodeEndpoint
    {
        public static RouteGroupBuilder GetDiscountByCodeGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{coupon:length(10)}", // validation yapıldı bu sekilde de olabilir
                    async (IMediator mediator, string coupon) =>
                        (await mediator.Send(new GetDiscountByCodeQuery(coupon))).ToGenericResult())
                .WithName("GetDiscountByCode")
                .MapToApiVersion(1, 0)
                .Produces<GetDiscountByCodeQueryResponse>()
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);

            return group;
        }
    }
}
