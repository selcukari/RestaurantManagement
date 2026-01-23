using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RestaurantManagement.Web.Pages.Order
{
    [Authorize(Roles = "customer,instructor")]
    public class ResultModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
