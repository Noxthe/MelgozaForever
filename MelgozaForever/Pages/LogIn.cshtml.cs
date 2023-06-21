using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace MelgozaForever.Pages
{
    public class LogInModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }

        private readonly ILogger<LogInModel> _logger;
        public ILogInProvider _login { get; set; }
        public ErrorCode? Error { get; set; }

        public LogInModel(ILogger<LogInModel> logger, ILogInProvider login)
        {
            _logger = logger;
            _login = login;
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

        public void OnPost()
        {
            Console.WriteLine(Credential.Username + " " + Credential.Password);
            if (_login != null && !String.IsNullOrEmpty(Credential.Username) && !String.IsNullOrEmpty(Credential.Password))
            {
                var logInCredential = new LogInCredentials(Credential.Username, Credential.Password);
                var logInResult = _login.LogIn(logInCredential);
                if (logInResult.error.Code == ErrorCode.Success)
                {
                    Request.HttpContext.Session.SetString("login", Credential.Username);
                    Request.HttpContext.Session.SetString("token", logInResult.token);
                }
                this.Error = logInResult.error.Code;
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