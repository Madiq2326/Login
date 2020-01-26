using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    public class Employee
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Address { get; set; }
        public String PlaceBirth { get; set; }
        public String BirthDate { get; set; }
        public String NIK { get; set; }
        public String Email { get; set; }
        public String NPWP { get; set; }
        public String University { get; set; }
        public String JoinDate { get; set; }
        public int Jobtitle_Id { get; set; }
        public int Dept_Id { get; set; }
        public int Majors_Id { get; set; }
        public int Religion_Id { get; set; }
        public int Degree_Id { get; set; }
        public String Name_Jobtitle { get; set; }
        public String Name_Department { get; set; }
        public String Name_Majors { get; set; }
        public String Name_Religion { get; set; }
        public String Name_Degree { get; set; }

    }

    public class view_employee
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Address { get; set; }
        public String BirthDate { get; set; }
        public String NIK { get; set; }
        public String Email { get; set; }
        public String NPWP { get; set; }
        public String University { get; set; }
        public String JoinDate { get; set; }
        public String Name_Jobtitle { get; set; }
        public String Name_Department { get; set; }
        public String Name_Majors { get; set; }
        public String Name_Religion { get; set; }
        public String Name_Degree { get; set; }
    }
}
