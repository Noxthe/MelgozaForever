using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MelgozaForever.Pages
{
    public class UserManagementModel : PageModel
    {
        public string? Username { get; set; }
        public void OnGet()
        {
            Username = HttpContext.Session.GetString("username");
            if (Username == null)
            {
                Response.Redirect("LogIn");
            }
        }
    }
}
