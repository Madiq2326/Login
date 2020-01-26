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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);

        int Id_Temp;

        public void clear()
        {
            Id_Temp = 0;
            TB_Id.Text = "";
            TB_Name.Text = "";
            TB_PhoneNum.Text = "";
            TB_Address.Text = "";
            TB_PlaceBirth.Text = "";
            TB_BirthDate.Text = "";
            TB_NIK.Text = "";
            TB_Email.Text = "";
            TB_NPWP.Text = "";
            TB_University.Text = "";
            TB_JoinDate.Text = "";
            CB_Jobtitle.Text = "";
            CB_Department.Text = "";
            CB_Majors.Text = "";
            CB_Religion.Text = "";
            CB_Degree.Text = "";
        }

        public void refresh()
        {
            var view = con.QueryAsync<view_employee>("exec SP_View_Employee").Result.ToList();
            DaftarTable.ItemsSource = view;
        }

        public Window1(int IdVal)
        {
            InitializeComponent();

            Id_Temp = IdVal;
            string String_Id_Temp = Convert.ToString(Id_Temp);

            var list_jobtittle = con.QueryAsync<Call>("SELECT Name_Jobtitle FROM TB_M_Jobtitle").Result.ToList();
            CB_Jobtitle.ItemsSource = list_jobtittle;

            var list_dept = con.QueryAsync<Call>("SELECT Name_Department FROM TB_M_Department").Result.ToList();
            CB_Department.ItemsSource = list_dept;

            var list_Majors = con.QueryAsync<Call>("SELECT Name_Majors FROM TB_M_Majors").Result.ToList();
            CB_Majors.ItemsSource = list_Majors;

            var list_Religion = con.QueryAsync<Call>("SELECT Name_Religion FROM TB_M_Religion").Result.ToList();
            CB_Religion.ItemsSource = list_Religion;

            var list_Degree = con.QueryAsync<Call>("SELECT Name_Degree FROM TB_M_Degree").Result.ToList();
            CB_Degree.ItemsSource = list_Degree;

            if (Id_Temp == 0)
            {
                TB_Id.Text = "";
            }
            else
            {
                TB_Id.Text = String_Id_Temp;

                var name = con.QueryAsync<Employee>("SELECT Name FROM TB_M_Employee WHERE Id = @Id", 
                    new { Id = TB_Id.Text }).Result.SingleOrDefault();
                var phonenum = con.QueryAsync<Employee>("SELECT Phone FROM TB_M_Employee WHERE Id = @Id", 
                    new { Id = TB_Id.Text }).Result.SingleOrDefault();
                var address = con.QueryAsync<Employee>("SELECT Address FROM TB_M_Employee WHERE Id = @Id", 
                    new { Id = TB_Id.Text }).Result.SingleOrDefault();
                var placebirth = con.QueryAsync<Employee>("SELECT PlaceBirth FROM TB_M_Employee WHERE Id = @Id", 
                    new { Id = TB_Id.Text }).Result.SingleOrDefault();
                var birthdate = con.QueryAsync<Employee>("SELECT BirthDate FROM TB_M_Employee WHERE Id = @Id", 
                    new { Id = TB_Id.Text }).Result.SingleOrDefault();
                var nik = con.QueryAsync<Employee>("SELECT NIK FROM TB_M_Employee WHERE Id = @Id", 
                    new { Id = TB_Id.Text }).Result.SingleOrDefault();
                var email = con.QueryAsync<Employee>("SELECT Email FROM TB_M_Employee WHERE Id = @Id", 
                    new { Id = TB_Id.Text }).Result.SingleOrDefault();
                var npwp = con.QueryAsync<Employee>("SELECT NPWP FROM TB_M_Employee WHERE Id = @Id", 
                    new { Id = TB_Id.Text }).Result.SingleOrDefault();
                var university = con.QueryAsync<Employee>("SELECT University FROM TB_M_Employee WHERE Id = @Id", 
                    new { Id = TB_Id.Text }).Result.SingleOrDefault();
                var joindate = con.QueryAsync<Employee>("SELECT JoinDate FROM TB_M_Employee WHERE Id = @Id", 
                    new { Id = TB_Id.Text }).Result.SingleOrDefault();

                TB_Name.Text = name.Name;
                TB_PhoneNum.Text = phonenum.Phone;
                TB_Address.Text = address.Address;
                TB_PlaceBirth.Text = placebirth.PlaceBirth;
                TB_BirthDate.Text = birthdate.BirthDate;
                TB_NIK.Text = nik.NIK;
                TB_Email.Text = email.Email;
                TB_NPWP.Text = npwp.NPWP;
                TB_University.Text = university.University;
                TB_JoinDate.Text = joindate.JoinDate;
                CB_Jobtitle.Text = "Please fill in";
                CB_Department.Text = "Please fill in";
                CB_Majors.Text = "Please fill in";
                CB_Religion.Text = "Please fill in";
                CB_Degree.Text = "Please fill in";
            }

        }

        private void BTN_Insert_Click(object sender, RoutedEventArgs e)
        {
            var list_jobtittle = con.QueryAsync<Call>("SELECT Id FROM TB_M_Jobtitle WHERE Name_Jobtitle = @Name_Jobtitle", new { Name_Jobtitle = CB_Jobtitle.Text }).Result.SingleOrDefault();
            CB_Jobtitle.Text = Convert.ToString(list_jobtittle.Id);

            var list_dept = con.QueryAsync<Call>("SELECT Id FROM TB_M_Department WHERE Name_Department = @Name_Department", new { Name_Department = CB_Department.Text }).Result.SingleOrDefault();
            CB_Department.Text = Convert.ToString(list_dept.Id);

            var list_Majors = con.QueryAsync<Call>("SELECT Id FROM TB_M_Majors WHERE Name_Majors = @Name_Majors", new { Name_Majors = CB_Majors.Text }).Result.SingleOrDefault();
            CB_Majors.Text = Convert.ToString(list_Majors.Id);

            var list_Religion = con.QueryAsync<Call>("SELECT Id FROM TB_M_Religion WHERE Name_Religion = @Name_Religion", new { Name_Religion = CB_Religion.Text }).Result.SingleOrDefault();
            CB_Religion.Text = Convert.ToString(list_Religion.Id);

            var list_Degree = con.QueryAsync<Call>("SELECT Id FROM TB_M_Degree WHERE Name_Degree = @Name_Degree", new { Name_Degree = CB_Degree.Text }).Result.SingleOrDefault();
            CB_Degree.Text = Convert.ToString(list_Degree.Id);

            var insert = con.ExecuteAsync("exec SP_Insert_Employee @Name, " +
                "@PhoneNum, " +
                "@Address, " +
                "@PlaceBirth, " +
                "@BirthDate, " +
                "@NIK, " +
                "@Email, " +
                "@NPWP, " +
                "@University, " +
                "@JoinDate, " +
                "@Jobtitle_Id, " +
                "@Dept_Id, " +
                "@Majors_Id, " +
                "@Religion_Id, " +
                "@Degree_Id",
                new
                {
                    Name        = TB_Name.Text,
                    PhoneNum    = TB_PhoneNum.Text,
                    Address     = TB_Address.Text,
                    PlaceBirth  = TB_PlaceBirth.Text,
                    BirthDate   = TB_BirthDate.SelectedDate.Value.Date,
                    NIK         = TB_NIK.Text,
                    Email       = TB_Email.Text,
                    NPWP        = TB_NPWP.Text,
                    University  = TB_University.Text,
                    JoinDate    = TB_JoinDate.SelectedDate.Value.Date,
                    Jobtitle_Id = CB_Jobtitle.Text,
                    Dept_Id     = CB_Department.Text,
                    Majors_Id   = CB_Majors.Text,
                    Religion_Id = CB_Religion.Text,
                    Degree_Id   = CB_Degree.Text 
                });

            MessageBox.Show("Data Entered");
            refresh();
            clear();
        }

        private void BTN_Update_Click(object sender, RoutedEventArgs e)
        {
            if (Id_Temp == 0)
            {
                var newwindow_update = new select_update();
                newwindow_update.Show();
                Close();
            }

            else
            {
                var list_jobtittle = con.QueryAsync<Call>("SELECT Id FROM TB_M_Jobtitle WHERE Name_Jobtitle = @Name_Jobtitle", new { Name_Jobtitle = CB_Jobtitle.Text }).Result.SingleOrDefault();
                CB_Jobtitle.Text = Convert.ToString(list_jobtittle.Id);

                var list_dept = con.QueryAsync<Call>("SELECT Id FROM TB_M_Department WHERE Name_Department = @Name_Department", new { Name_Department = CB_Department.Text }).Result.SingleOrDefault();
                CB_Department.Text = Convert.ToString(list_dept.Id);

                var list_Majors = con.QueryAsync<Call>("SELECT Id FROM TB_M_Majors WHERE Name_Majors = @Name_Majors", new { Name_Majors = CB_Majors.Text }).Result.SingleOrDefault();
                CB_Majors.Text = Convert.ToString(list_Majors.Id);

                var list_Religion = con.QueryAsync<Call>("SELECT Id FROM TB_M_Religion WHERE Name_Religion = @Name_Religion", new { Name_Religion = CB_Religion.Text }).Result.SingleOrDefault();
                CB_Religion.Text = Convert.ToString(list_Religion.Id);

                var list_Degree = con.QueryAsync<Call>("SELECT Id FROM TB_M_Degree WHERE Name_Degree = @Name_Degree", new { Name_Degree = CB_Degree.Text }).Result.SingleOrDefault();
                CB_Degree.Text = Convert.ToString(list_Degree.Id);

                var update = con.ExecuteAsync("exec SP_Update_Employee @Id, " +
                 "@Name, " +
                 "@PhoneNum, " +
                 "@Address, " +
                 "@PlaceBirth, " +
                 "@BirthDate, " +
                 "@NIK, " +
                 "@Email, " +
                 "@NPWP, " +
                 "@University, " +
                 "@JoinDate, " +
                 "@Jobtitle_Id, " +
                 "@Dept_Id, " +
                 "@Majors_Id, " +
                 "@Religion_Id, " +
                 "@Degree_Id",
                 new
                 {
                     Id = TB_Id.Text,
                     Name = TB_Name.Text,
                     PhoneNum = TB_PhoneNum.Text,
                     Address = TB_Address.Text,
                     PlaceBirth = TB_PlaceBirth.Text,
                     BirthDate = TB_BirthDate.SelectedDate.Value.Date,
                     NIK = TB_NIK.Text,
                     Email = TB_Email.Text,
                     NPWP = TB_NPWP.Text,
                     University = TB_University.Text,
                     JoinDate = TB_JoinDate.SelectedDate.Value.Date,
                     Jobtitle_Id = CB_Jobtitle.Text,
                     Dept_Id = CB_Department.Text,
                     Majors_Id = CB_Majors.Text,
                     Religion_Id = CB_Religion.Text,
                     Degree_Id = CB_Degree.Text
                 });

                MessageBox.Show("Data Has Been Changed");
                refresh();
                clear();

            }
        }

        private void BTN_Delete_Click(object sender, RoutedEventArgs e)
        {

            var newwindow = new select_delete();
            newwindow.Show();
            refresh();
            clear();
            Close();
        }

        private void DaftarTable_Loaded(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        private void Btn_Logout_Click(object sender, RoutedEventArgs e)
        {
            var newwindow = new MainWindow();
            newwindow.Show();
            Close();
        }
    }
}
