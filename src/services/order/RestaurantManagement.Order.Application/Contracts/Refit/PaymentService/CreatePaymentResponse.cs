using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Order.Application.Contracts.Refit.PaymentService
{
    public record CreatePaymentResponse(Guid? PaymentId, bool Status, string? ErrorMessage);
}
