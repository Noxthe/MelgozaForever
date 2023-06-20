using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MelgozaForever.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; } = new Credential();
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }



        public void OnGet()
        {

        }
        private void SetUpLabels()
        {

        }
        private bool CheckFields()
        {
            bool areValid = true;
            if (Credential.Username == null || Credential.Username == "")
            {
                areValid = false;
                ModelState.AddModelError("Credential.Username", "Username is required");
            }
            else if (Credential.Password == null || Credential.Password == "")
            {
                areValid = false;
                ModelState.AddModelError("Credential.Password", "Password is required");
            }
            return areValid;
        }
        
        private void Login()
        {
            if (CheckFields()) 
            {

            }
        }

    }
   
    public class Credential
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}