using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;

namespace Login
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);

        public Window2(int roleVal, int Employeeval)
        {
            int role = roleVal;
            int temp_id = Employeeval;

            InitializeComponent();
            if(role == 1)
            {
                Btn_MasterData.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Btn_MasterData.Visibility = System.Windows.Visibility.Collapsed;
            }

            var name = con.QueryAsync<Employee>("SELECT Name FROM TB_M_Employee WHERE Id = @Id",
                  new { Id = temp_id }).Result.SingleOrDefault();
            var nik = con.QueryAsync<Employee>("SELECT NIK FROM TB_M_Employee WHERE Id = @Id",
               new { Id = temp_id }).Result.SingleOrDefault();
            var address = con.QueryAsync<Employee>("SELECT Address FROM TB_M_Employee WHERE Id = @Id",
                new { Id = temp_id }).Result.SingleOrDefault();
            var birthdate = con.QueryAsync<Employee>("SELECT BirthDate FROM TB_M_Employee WHERE Id = @Id",
               new { Id = temp_id }).Result.SingleOrDefault();
            var placebirth = con.QueryAsync<Employee>("SELECT PlaceBirth FROM TB_M_Employee WHERE Id = @Id",
                new { Id = temp_id }).Result.SingleOrDefault();
            var email = con.QueryAsync<Employee>("SELECT Email FROM TB_M_Employee WHERE Id = @Id",
                new { Id = temp_id }).Result.SingleOrDefault();
            var npwp = con.QueryAsync<Employee>("SELECT NPWP FROM TB_M_Employee WHERE Id = @Id",
                new { Id = temp_id }).Result.SingleOrDefault();
            var jobtitle = con.QueryAsync<view_employee>("SELECT J.Name_Jobtitle FROM TB_M_Employee E, TB_M_Jobtitle J WHERE E.Jobtitle_Id = J.Id AND E.Id = @Id ",
                new { Id = temp_id }).Result.SingleOrDefault();
            var department = con.QueryAsync<view_employee>("SELECT D.Name_Department FROM TB_M_Employee E, TB_M_Department D WHERE E.Dept_Id = D.Id AND E.Id = @Id ",
                new { Id = temp_id }).Result.SingleOrDefault();
            var majors = con.QueryAsync<view_employee>("SELECT M.Name_Majors FROM TB_M_Employee E, TB_M_Majors M WHERE E.Majors_Id = M.Id AND E.Id = @Id ",
                new { Id = temp_id }).Result.SingleOrDefault();

            lbname.Content = name.Name;
            lbNIK.Content = nik.NIK;
            lbAddress.Content = address.Address;
            lbBirthDate.Content = birthdate.BirthDate;
            lbPlaceBirth.Content = placebirth.PlaceBirth;
            lbEmail.Content = email.Email;
            lbNPWP.Content = npwp.NPWP;
            lbJobtitle.Content = jobtitle.Name_Jobtitle;
            lbDepartment.Content = department.Name_Department;
            lbMajors.Content = majors.Name_Majors;

        }

        private void Click_Btn_MasterData(object sender, RoutedEventArgs e)
        {
            var newwindow = new Window1(0);
            newwindow.Show();
            Close();
        }

        private void Btn_Logout_Click(object sender, RoutedEventArgs e)
        {
            var newwindow = new MainWindow();
            newwindow.Show();
            Close();
        }
    }
}
