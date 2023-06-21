using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using static System.Net.HttpStatusCode;

namespace Services
{
    public class LogInWSProvider : ILogInProvider
    {
        private static string ApiUrlBase
        {
            get
            {
                return "http://m4e.vadam.xyz/api";
            }
        }

        public LogInResponse LogIn(LogInCredentials credentials)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var logInUrl = string.Format("{0}/auth/customer", ApiUrlBase);
                    var logInEequestBody = new StringContent(JsonSerializer.Serialize(credentials), Encoding.UTF8, "application/json");
                    var logInResponseMessage = client.PostAsync(logInUrl, logInEequestBody).Result;

                    if (logInResponseMessage != null)
                    {
                        if (logInResponseMessage.IsSuccessStatusCode)
                        {
                            var response = logInResponseMessage.Content.ReadFromJsonAsync<LogInResponse>().Result;
                            if (response != null)
                            {
                                var userDataResponseMessage = client.GetAsync(logInUrl).Result;
                                if(userDataResponseMessage != null)
                                {
                                    if(userDataResponseMessage.IsSuccessStatusCode)
                                    {
                                        var userDataResponse = userDataResponseMessage.Content.ReadFromJsonAsync<UserData>().Result;
                                        response.userData = userDataResponse;
                                        response.error = new Error();
                                        response.error.Code = ErrorCode.Success;
                                        return response;
                                    }
                                    else if(userDataResponseMessage.StatusCode == BadRequest)
                                    {
                                        response.error = new Error();

                                    }
                                }
                            }
                            else
                            {
                                return Error.ServerError;
                            }
                        }
                        else if (responseMessage.StatusCode == Unauthorized || responseMessage.StatusCode == NotFound)
                        {
                            return Error.InvalidCredentials;
                        }
                        else if (responseMessage.StatusCode == InternalServerError)
                        {
                            return Error.ServerError;
                        }
                    }
                    return Error.ConnectionError;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e.Message);
                return Error.ClientError;
            }
        }
    }
}
