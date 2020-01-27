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

        public void refresh()
        {
            var list_user = con.QueryAsync<User>("exec SP_View_User").Result.ToList();
            DG_list_User.ItemsSource = list_user;
        }

        public void clear()
        {
            TB_Email.Text = "";
            TB_Password.Text = "";
            CB_Id.Text = "";
            CB_Role.Text = "";
            CB_Employee.Text = "";
        }

        public Window2(int roleVal, int Employeeval)
        {

            int role = roleVal;
            int temp_id = Employeeval;

            InitializeComponent();

            var list_id = con.QueryAsync<call_user>("SELECT Id FROM TB_M_User").Result.ToList();
            CB_Id.ItemsSource = list_id;
            var list_role_id = con.QueryAsync<call_user>("SELECT Name_Role FROM TB_M_Role").Result.ToList();
            CB_Role.ItemsSource = list_role_id;
            var list_employee = con.QueryAsync<call_user>("SELECT Name FROM TB_M_Employee").Result.ToList();
            CB_Employee.ItemsSource = list_employee;


            if (role == 1)
            {
                Tc_Controler.Visibility = System.Windows.Visibility.Visible;
                Btn_MasterData.Visibility = System.Windows.Visibility.Visible;
                Btn_Manage_User.Visibility = System.Windows.Visibility.Visible;
                Tc_User.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                Btn_MasterData.Visibility = System.Windows.Visibility.Collapsed;
                Btn_Manage_User.Visibility = System.Windows.Visibility.Collapsed;
            }

            var take = con.QueryAsync<Employee>("SELECT * FROM TB_M_Employee WHERE Id = @Id",
                  new { Id = temp_id }).Result.SingleOrDefault();
            var jobtitle = con.QueryAsync<view_employee>("SELECT J.Name_Jobtitle FROM TB_M_Employee E, TB_M_Jobtitle J WHERE E.Jobtitle_Id = J.Id AND E.Id = @Id ",
                new { Id = temp_id }).Result.SingleOrDefault();
            var department = con.QueryAsync<view_employee>("SELECT D.Name_Department FROM TB_M_Employee E, TB_M_Department D WHERE E.Dept_Id = D.Id AND E.Id = @Id ",
                new { Id = temp_id }).Result.SingleOrDefault();
            var majors = con.QueryAsync<view_employee>("SELECT M.Name_Majors FROM TB_M_Employee E, TB_M_Majors M WHERE E.Majors_Id = M.Id AND E.Id = @Id ",
                new { Id = temp_id }).Result.SingleOrDefault();

            lbname.Content = take.Name;
            lbNIK.Content = take.NIK;
            lbAddress.Content = take.Address;
            lbBirthDate.Content = take.BirthDate;
            lbPlaceBirth.Content = take.PlaceBirth;
            lbEmail.Content = take.Email;
            lbNPWP.Content = take.NPWP;
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

        private void Btn_Select_Click(object sender, RoutedEventArgs e)
        {
            var take = con.QueryAsync<User>("SELECT * FROM TB_M_Employee WHERE Id = @Id",
                    new { Id = CB_Id.Text }).Result.SingleOrDefault();

            TB_Email.Text = take.Email;
            TB_Password.Text = "";
            CB_Employee.Text = "Please fill in";
            CB_Role.Text = "Please fill in";
        }

        private void Btn_Isert_Click(object sender, RoutedEventArgs e)
        {
            string hash_input = TB_Password.Text;

            string mySalt = BCrypt.Net.BCrypt.GenerateSalt();

            string hash = BCrypt.Net.BCrypt.HashPassword(hash_input, mySalt);

            var list_employee = con.QueryAsync<call_user>("SELECT Id FROM TB_M_Employee WHERE Name = @Name", new { Name = CB_Employee.Text }).Result.SingleOrDefault();
            CB_Employee.Text = Convert.ToString(list_employee.Id);

            var list_role = con.QueryAsync<call_user>("SELECT Id FROM TB_M_Role WHERE Name_Role = @Name_Role", new { Name_Role = CB_Role.Text }).Result.SingleOrDefault();
            CB_Role.Text = Convert.ToString(list_role.Id);

            var insert_user = con.ExecuteAsync("exec SP_Insert_User @Email, " +
                "@Password, " +
                "@Employee_Id, " +
                "@Role_Id",
                new
                {
                    Email = TB_Email.Text,
                    Password = hash,
                    Employee_Id = CB_Employee.Text,
                    Role_Id = CB_Role.Text
                });
            clear();
            refresh();
        }

        private void Btn_Update_Click(object sender, RoutedEventArgs e)
        {
            string hash_input = TB_Password.Text;

            string mySalt = BCrypt.Net.BCrypt.GenerateSalt();

            string hash = BCrypt.Net.BCrypt.HashPassword(hash_input, mySalt);

            var list_employee = con.QueryAsync<call_user>("SELECT Id FROM TB_M_Employee WHERE Name = @Name", new { Name = CB_Employee.Text }).Result.SingleOrDefault();
            CB_Employee.Text = Convert.ToString(list_employee.Id);

            var list_role = con.QueryAsync<call_user>("SELECT Id FROM TB_M_Role WHERE Name_Role = @Name_Role", new { Name_Role = CB_Role.Text }).Result.SingleOrDefault();
            CB_Role.Text = Convert.ToString(list_role.Id);

            var insert_user = con.ExecuteAsync("exec SP_Update_User @Id " +
                "@Email, " +
                "@Password, " +
                "@Employee_Id, " +
                "@Role_Id",
                new
                {
                    Id = CB_Id.Text,
                    Email = TB_Email.Text,
                    Password = hash,
                    Employee_Id = CB_Employee.Text,
                    Role_Id = CB_Role.Text
                });
            clear();
            refresh();
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {

            var check = con.ExecuteAsync("exec SP_Delete_User @Id",
              new { Id = CB_Id.Text });
            MessageBox.Show("Data Has Been Deleted");
            clear();
            refresh();
        }

        private void DG_list_User_Loaded(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        private void Btn_Manage_User_Click(object sender, RoutedEventArgs e)
        {
            Tc_Controler.Visibility = System.Windows.Visibility.Collapsed;
            Tc_User.Visibility = System.Windows.Visibility.Visible;
            clear();
            refresh();
        }

        private void Btn_Profile_Click(object sender, RoutedEventArgs e)
        {
            Tc_Controler.Visibility = System.Windows.Visibility.Visible;
            Tc_User.Visibility = System.Windows.Visibility.Collapsed;
            clear();
            refresh();
        }
    }
}
