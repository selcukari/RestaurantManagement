namespace RestaurantManagement.Menu.Api.Features.Products.Delete;

public record DeleteProductCommand(Guid Id) : IRequestByServiceResult;

public class DeleteCourseHandler(AppDbContext context) : IRequestHandler<DeleteProductCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var hasProduct = await context.Products.FindAsync([request.Id], cancellationToken);
        if (hasProduct == null) return ServiceResult.ErrorAsNotFound();

        context.Products.Remove(hasProduct);
        await context.SaveChangesAsync(cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}

public static class DeleteProductEndpoint
    {
    public static RouteGroupBuilder DeleteProductGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:guid}",
                async (IMediator mediator, Guid id) =>
                    (await mediator.Send(new DeleteProductCommand(id))).ToGenericResult())
            .WithName("DeleteProduct")
            .MapToApiVersion(1, 0);
            //.RequireAuthorization(policyNames: "InstructorPolicy");

        return group;
    }
}

