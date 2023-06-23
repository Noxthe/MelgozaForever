using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserData
    {
        public enum Type
        {
            Employee = 1,
            Customer
        }

        public enum Gender
        {
            Men,
            Women,
            Other
        }

        public string name { get; set; }
        public string paternalSurname { get; set; }
        public string maternalSurname { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public Type role { get; set; }
        public Gender gender { get; set; }

        public UserData() { }

    }
}
