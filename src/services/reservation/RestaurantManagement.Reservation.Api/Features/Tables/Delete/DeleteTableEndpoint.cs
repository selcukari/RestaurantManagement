using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Reservation.Api.Features.Table.Delete;

public record DeleteTableCommand(Guid Id) : IRequestByServiceResult;

public class DeleteTableHandler(AppDbContext context, ICacheService cacheService) : IRequestHandler<DeleteTableCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
    {
        var hasTable = await context.Tables.FindAsync([request.Id], cancellationToken);
        if (hasTable == null) return ServiceResult.ErrorAsNotFound();

        context.Tables.Remove(hasTable);
        await context.SaveChangesAsync(cancellationToken);

        cacheService.Remove("tables");

        return ServiceResult.SuccessAsNoContent();
    }
}

public static class DeleteTableEndpoint
    {
    public static RouteGroupBuilder DeleteTableGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:guid}",
                async (IMediator mediator, Guid id) =>
                    (await mediator.Send(new DeleteTableCommand(id))).ToGenericResult())
            .WithName("DeleteTable")
            .MapToApiVersion(1, 0)
            .RequireAuthorization(policyNames: "InstructorPolicy");

        return group;
    }
}

