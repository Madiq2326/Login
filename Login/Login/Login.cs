using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    public class Check
    {
        public int Id { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public int Role_Id { get; set; }
        public int Employee_Id { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public String Email { get; set; }
        public String Name { get; set; }
        public String Name_Role { get; set; }
    }

    public class call_user
    {
        public int Id { get; set; }
        public int Employee_Id { get; set; }
        public int Role_Id { get; set; }
        public String Name { get; set; }
        public String Name_Role { get; set; }
    }
}
