using RestaurantManagement.Shared.Services;
using System;

namespace RestaurantManagement.Menu.Api.Features.Menus.Create;
    public class CreateMenuCommandHandler(AppDbContext context, ICacheService cacheService)
    : IRequestHandler<CreateMenuCommand, ServiceResult<CreateMenuResponse>>
{
    public async Task<ServiceResult<CreateMenuResponse>> Handle(CreateMenuCommand request,
        CancellationToken cancellationToken)
    {
        var existMenu =
            await context.Menus.AnyAsync(x => x.Name == request.Name, cancellationToken);


        if (existMenu) // menu isimle aynı data veritabanda var mı
            ServiceResult<CreateMenuResponse>.Error("Menu Name already exists",
                $"The menu name '{request.Name}' already exists", HttpStatusCode.BadRequest);


        var menu = new Menum
        {
            Name = request.Name,
            Id = NewId.NextSequentialGuid()
        };


        await context.AddAsync(menu, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        cacheService.Remove("menus");

        return ServiceResult<CreateMenuResponse>.SuccessAsCreated(new CreateMenuResponse(menu.Id),
            "<empty>");
    }
}
