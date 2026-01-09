using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Reservation.Api.Features.Tables.Update;
    public class UpdateTableCommandHandler(AppDbContext context, ICacheService cacheService, IIdentityService identityService, IPublishEndpoint publishEndpoint)
    : IRequestHandler<UpdateTableCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
        {
            var hasTable = await context.Tables.FindAsync([request.Id], cancellationToken);
            if (hasTable == null) return ServiceResult.ErrorAsNotFound();

            hasTable.TableNumber = request.TableNumber;
            hasTable.Capacity = request.Capacity;
            hasTable.Location = request.Location;
            hasTable.IsAvailable = request.IsAvailable;
            hasTable.Status = request.Status;
            hasTable.UserFullName = identityService.UserName;

            context.Tables.Update(hasTable);


            await context.SaveChangesAsync(cancellationToken);

            cacheService.Remove("tables");

            return ServiceResult.SuccessAsNoContent();
        }
    }
