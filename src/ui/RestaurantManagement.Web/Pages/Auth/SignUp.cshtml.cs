using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantManagement.Web.Auth.SignUp;

namespace RestaurantManagement.Web.Auth
{
    public class SignUpModel : PageModel
    {
        [BindProperty] public required SignUpViewModel SignUpViewModel { get; set; } = SignUpViewModel.GetExampleModel;

        public void OnGet()
        {
        }
    }
}
