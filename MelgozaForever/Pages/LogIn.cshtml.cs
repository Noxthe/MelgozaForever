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
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public string Message { get; set; }

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
            if(HttpContext.Session != null)
            {
                var username = HttpContext.Session.GetString("username");
                if (username != null)
                {
                    Response.Redirect("Index");
                }
            }
        }
        private void SetUpLabels()
        {

        }
        private bool CheckFields()
        {
            bool areValid = true;
            if (Username == null || Username == "")
            {
                areValid = false;
                ModelState.AddModelError("Username", "Username is required");
            }
            else if (Password == null || Password == "")
            {
                areValid = false;
                ModelState.AddModelError("Password", "Password is required");
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
            if (_login != null && !String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Password))
            {
                var logInCredential = new LogInCredentials(Username, Password);
                var logInResult = _login.LogIn(logInCredential);
                if (logInResult.error.Code == ErrorCode.Success)
                {
                    Message = "Bienvenido";
                    Request.HttpContext.Session.SetString("username", Username);
                    Request.HttpContext.Session.SetString("token", logInResult.token);
                    Response.Redirect("ProductManagement");
                }
                else
                {
                    switch(logInResult.error.Code)
                    {
                        case ErrorCode.InvalidCredentials:
                            Message = "Credenciales invalidas";
                            break;

                        default:
                            Message = string.Format("Ocurrió un error ({0})", logInResult.error.Code);
                            break;
                    }
                }
                this.Error = logInResult.error.Code;
            }
            else
            {
                Message = "Campos invalidos";
            }
        }
    }
}