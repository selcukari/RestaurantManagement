using RestaurantManagement.Shared.Services;

namespace RestaurantManagement.Reservation.Api.Features.Tables.Create
{
    public class CreateProductCommandHandler(AppDbContext context,
    IMapper mapper,
    IIdentityService identityService, ICacheService cacheService) : IRequestHandler<CreateTableCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateTableCommand request, CancellationToken cancellationToken)
        {

            // daha once veri tabanda aynı isimle data var mı
            var hasTable = await context.Tables.AnyAsync(x => x.TableNumber == request.TableNumber, cancellationToken);

            if (hasTable)
                return ServiceResult<Guid>.Error("Table already exists.",
                    $"The Table with name({request.TableNumber}) already exists", HttpStatusCode.BadRequest);


            var newTable = mapper.Map<Table>(request);
            newTable.Created = DateTime.Now;
            newTable.UserId = identityService.UserId;
            newTable.Id = NewId.NextSequentialGuid(); // index performance

        

            context.Tables.Add(newTable);
            await context.SaveChangesAsync(cancellationToken);


            cacheService.Remove("tables");

            return ServiceResult<Guid>.SuccessAsCreated(newTable.Id, $"/api/tables/{newTable.Id}");
        }
    }
}
