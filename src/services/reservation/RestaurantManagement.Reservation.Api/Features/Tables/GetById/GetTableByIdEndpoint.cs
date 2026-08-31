
using RestaurantManagement.Reservation.Api.Features.Tables.Dtos;

namespace RestaurantManagement.Reservation.Api.Features.Tables.GetById
{
    public record GetTableByIdQuery(Guid Id) : IRequestByServiceResult<TableDto>;

    public class GetProductByIdQueryHandler(AppDbContext context, IMapper mapper)
    : IRequestHandler<GetTableByIdQuery, ServiceResult<TableDto>>
    {
        public async Task<ServiceResult<TableDto>> Handle(GetTableByIdQuery request,
            CancellationToken cancellationToken)
        {
            var hasTable = await context.Tables.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (hasTable is null)
                return ServiceResult<TableDto>.Error("Table not found",
                    $"The Table with id({request.Id}) was not found", HttpStatusCode.NotFound);

            var tableAsDto = mapper.Map<TableDto>(hasTable);
            return ServiceResult<TableDto>.SuccessAsOk(tableAsDto);
        }
    }

    public static class GetTableByIdEndpoint
    {
        public static RouteGroupBuilder GetByIdTableGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}",
                    async (IMediator mediator, Guid id) =>
                        (await mediator.Send(new GetTableByIdQuery(id))).ToGenericResult())
                .WithName("GetByIdTable")
                .MapToApiVersion(1, 0);

            return group;
        }
    }
}
