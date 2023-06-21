using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LogInCredentials
    {
        public string username { get; set; }
        public string password { get; }

        public LogInCredentials(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
