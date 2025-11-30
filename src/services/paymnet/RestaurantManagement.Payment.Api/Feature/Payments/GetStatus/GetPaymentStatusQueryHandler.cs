using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Payment.Api.Repositories;
using RestaurantManagement.Shared;

namespace RestaurantManagement.Payment.Api.Feature.Payments.GetStatus
{
    public class GetPaymentStatusQueryHandler(AppDbContext context)
    : IRequestHandler<GetPaymentStatusRequest, ServiceResult<GetPaymentStatusResponse>>
    {
        public async Task<ServiceResult<GetPaymentStatusResponse>> Handle(GetPaymentStatusRequest request,
       CancellationToken cancellationToken)
        {
            var payment = await context.Payments.FirstOrDefaultAsync(x => x.OrderCode == request.orderCode,
                cancellationToken);

            if (payment is null)
                return ServiceResult<GetPaymentStatusResponse>.SuccessAsOk(new GetPaymentStatusResponse(null, false));

            return ServiceResult<GetPaymentStatusResponse>.SuccessAsOk(
                new GetPaymentStatusResponse(payment.Id, payment.Status == PaymentStatus.Success));
        }
    }
}
