using RestaurantManagement.Reservation.Api.Features.Reservations.Create;
using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Reservation.Api.Features.Reservations.Delete;

public record DeleteReservationCommand(Guid Id) : IRequestByServiceResult;

public class DeleteReservationHandler(AppDbContext context, ICacheService cacheService) : IRequestHandler<DeleteReservationCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
    {
        context.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

        var hasReservation = await context.Reservations.FindAsync([request.Id], cancellationToken);
        if (hasReservation == null) return ServiceResult.ErrorAsNotFound();

        hasReservation.IsAvailable = false;

        // 2. Masayı bul ve durumunu güncelle
        var hasTable = await context.Tables.FindAsync(hasReservation.TableId, cancellationToken);

        if (hasTable == null)
            return ServiceResult<CreateReservationCommand>.Error("Masa Bulunamadı",
                $"Seçilen masa (ID: {hasReservation.TableId}) sistemde kayıtlı değil.", HttpStatusCode.NotFound);
        
        // table status boş cevir
        hasTable.Status = Tables.TableStatus.Empty;

        context.Reservations.Update(hasReservation);
        context.Tables.Update(hasTable);

        await context.SaveChangesAsync(cancellationToken);

        cacheService.Remove("tables");
        cacheService.Remove("reservations");

        return ServiceResult.SuccessAsNoContent();
    }
}

public static class DeleteReservationEndpoint
    {
    public static RouteGroupBuilder DeleteReservationGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/{Id:guid}",
                async (IMediator mediator, Guid Id) =>
                    (await mediator.Send(new DeleteReservationCommand(Id))).ToGenericResult())
            .WithName("DeleteReservation")
            .MapToApiVersion(1, 0)
            .RequireAuthorization(policyNames: "InstructorPolicy");

        return group;
    }
}

