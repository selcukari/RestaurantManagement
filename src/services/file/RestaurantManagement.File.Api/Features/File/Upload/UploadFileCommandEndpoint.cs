using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Shared.Extensions;
using RestaurantManagement.Shared.Filters;

namespace RestaurantManagement.File.Api.Features.File.Upload
{
    public static class UploadFileCommandEndpoint
    {
        public static RouteGroupBuilder UploadFileGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                    async (IFormFile file, IMediator mediator) =>
                        (await mediator.Send(new UploadFileCommand(file))).ToGenericResult())
                .WithName("upload")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<UploadFileCommandValidator>>()
                .Produces<Guid>(StatusCodes.Status201Created)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError).DisableAntiforgery();

            return group;
        }
    }
}
