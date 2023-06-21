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
                                response.error.Code = ErrorCode.Success;
                            }
                            else
                            {
                                response = new LogInResponse();
                                response.error.Code = ErrorCode.ServerInternalError;
                            }
                            return response;
                        }
                        else
                        {
                            var response = new LogInResponse();
                            var errorResponse = logInResponseMessage.Content.ReadFromJsonAsync<Error>().Result;
                            if (errorResponse != null)
                            {
                                response.error = errorResponse;
                                if (logInResponseMessage.StatusCode == Unauthorized)
                                {
                                    response.error.Code = ErrorCode.InvalidData;
                                }
                                else
                                {
                                    response.error.Code = ErrorCode.ServerInternalError;
                                }
                            }
                            else
                            {
                                response.error.Code = ErrorCode.ServerInternalError;
                            }
                            return response;
                        }
                    }
                    else
                    {
                        var response = new LogInResponse();
                        response.error.Code = ErrorCode.NoResponse;
                        return response;
                    }
                }
            }
            catch (Exception e)
            {
                var response = new LogInResponse();
                response.error.Code = ErrorCode.ClientError;
                return response;
            }
        }
    }
}
