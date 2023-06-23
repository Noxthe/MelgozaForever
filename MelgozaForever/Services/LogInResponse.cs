using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LogInResponse
    {
        public string? token { get; set; }
        public Error error { get; set; }

        public LogInResponse()
        {
            error = new Error();
        }
    }
}
