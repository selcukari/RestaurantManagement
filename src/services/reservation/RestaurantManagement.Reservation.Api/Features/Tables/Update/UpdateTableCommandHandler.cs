using AutoMapper;
using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Reservation.Api.Features.Tables.Update;
    public class UpdateTableCommandHandler(AppDbContext context, IMapper mapper, ICacheService cacheService, IIdentityService identityService)
    : IRequestHandler<UpdateTableCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
        {
            var hasTable = await context.Tables.FindAsync([request.Id], cancellationToken);
            if (hasTable == null) return ServiceResult.ErrorAsNotFound();

            var newTable = mapper.Map<Table>(request);

            hasTable.TableNumber = request.TableNumber;
            hasTable.Capacity = request.Capacity;
            hasTable.Location = newTable.Location;
            hasTable.IsAvailable = request.IsAvailable;
            hasTable.UserFullName = identityService.UserName;

            context.Tables.Update(hasTable);


            await context.SaveChangesAsync(cancellationToken);

            cacheService.Remove("tables");

            return ServiceResult.SuccessAsNoContent();
        }
    }
