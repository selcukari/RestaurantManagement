using RestaurantManagement.Web.Pages.Order.Dto;
using RestaurantManagement.Web.Pages.Order.ViewModel;
using RestaurantManagement.Web.Services.Refit;
using System.Net;

namespace RestaurantManagement.Web.Services
{
    public class OrderService(IOrderRefitService orderService, ILogger<OrderService> logger)
    {
        public async Task<ServiceResult> CreateOrder(CreateOrderViewModel viewModel)
        {
            //createAddressDto
            var address = new AddressDto(viewModel.Address.Province, viewModel.Address.District,
                viewModel.Address.Street, viewModel.Address.ZipCode, viewModel.Address.Line);


            //paymentDto
            var payment = new PaymentDto(viewModel.Payment.CardNumber, viewModel.Payment.CardHolderName,
                viewModel.Payment.ExpiryDate, viewModel.Payment.Cvv, viewModel.TotalPrice);


            // orderItems
            var orderItems = viewModel.OrderItems.Select(x => new OrderItemDto(x.ProductId, x.ProductName, x.UnitPrice, x.Quantity))
                .ToList();


            var createOrderRequest = new CreateOrderRequest(viewModel.DiscountRate, address, payment, orderItems);


            var response = await orderService.CreateOrder(createOrderRequest);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                    return ServiceResult.FailFromProblemDetails(response.Error);

                logger.LogError(new EventId(), null, response.Error);
                return ServiceResult.Error("An error occurred while creating the order");
            }

            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<OrderHistoryViewModel>>> GetHistory()
        {
            var response = await orderService.GetOrders();

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError(new EventId(), null, response.Error);
                return ServiceResult<List<OrderHistoryViewModel>>.Error(
                    "An error occurred while getting the order history");
            }

            var orderHistoryList = new List<OrderHistoryViewModel>();


            foreach (var orderResponse in response.Content)
            {
                var newOrderHistory =
                    new OrderHistoryViewModel(orderResponse.Created.ToLongDateString(),
                        orderResponse.TotalPrice.ToString("C"));

                foreach (var orderItem in orderResponse.Items)
                    newOrderHistory.AddItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice, orderItem.Quantity);

                orderHistoryList.Add(newOrderHistory);
            }


            return ServiceResult<List<OrderHistoryViewModel>>.Success(orderHistoryList);
        }
    }
}
