using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Web.PageModels;
using RestaurantManagement.Web.Services;
using RestaurantManagement.Web.ViewModel;

namespace RestaurantManagement.Web.Pages.Instructor
{
    [Authorize(Roles = "instructor")]
    public class ReportingModel(ReportingService reportingService) : BasePageModel
    {
        public ReportingViewModel ReportingViewModel { get; set; } = new ReportingViewModel();

        public async Task<IActionResult> OnGetAsync()
        {
            var result = await reportingService.GetAllReportingAsync();

            if (result.IsFail)
            {
                return ErrorPage(result);
            }

            ReportingViewModel = result.Data!;

            return Page();
        }
    }
}
