using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MelgozaForever.Pages
{
    public class IndexModel : PageModel
    {
        public string? Username { get; set; }
        public void OnGet()
        {
            Username = HttpContext.Session.GetString("username");
            if(Username == null)
            {
                Response.Redirect("LogIn");
            }
        }

        public void OnGetLogout()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("token");
			Response.Redirect("LogIn");
        }
    }
}
