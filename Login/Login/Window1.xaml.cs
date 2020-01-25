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
            TB_Religion.Text = "";
            TB_Bachelor.Text = "";
            TB_University.Text = "";
            TB_JoinDate.Text = "";
        }

        public void refresh()
        {
            var view = con.QueryAsync<Employee>("exec SP_View_Employee").Result.ToList();
            DaftarTable.ItemsSource = view;
        }

        public Window1(int IdVal)
        {
            InitializeComponent();

            Id_Temp = IdVal;
            string String_Id_Temp = Convert.ToString(Id_Temp);

            var list = con.QueryAsync<Bachelor>("SELECT Name FROM Bachelor").Result.ToList();
            TB_Bachelor.ItemsSource = list;

            if (Id_Temp == 0)
            {
                TB_Id.Text = "";
            }
            else
            {
                TB_Id.Text = String_Id_Temp;

                var name = con.QueryAsync<Employee>("SELECT Name FROM TB_M_Employee WHERE Id = @Id", new { Id = TB_Id.Text }).Result.SingleOrDefault();
                var phonenum = con.QueryAsync<Employee>("SELECT PhoneNum FROM TB_M_Employee WHERE Id = @Id", new { Id = TB_Id.Text }).Result.SingleOrDefault();
                var address = con.QueryAsync<Employee>("SELECT Address FROM TB_M_Employee WHERE Id = @Id", new { Id = TB_Id.Text }).Result.SingleOrDefault();
                var placebirth = con.QueryAsync<Employee>("SELECT PlaceBirth FROM TB_M_Employee WHERE Id = @Id", new { Id = TB_Id.Text }).Result.SingleOrDefault();
                var birthdate = con.QueryAsync<Employee>("SELECT BirthDate FROM TB_M_Employee WHERE Id = @Id", new { Id = TB_Id.Text }).Result.SingleOrDefault();
                var nik = con.QueryAsync<Employee>("SELECT NIK FROM TB_M_Employee WHERE Id = @Id", new { Id = TB_Id.Text }).Result.SingleOrDefault();
                var email = con.QueryAsync<Employee>("SELECT Email FROM TB_M_Employee WHERE Id = @Id", new { Id = TB_Id.Text }).Result.SingleOrDefault();
                var npwp = con.QueryAsync<Employee>("SELECT NPWP FROM TB_M_Employee WHERE Id = @Id", new { Id = TB_Id.Text }).Result.SingleOrDefault();
                var religion = con.QueryAsync<Employee>("SELECT Religion FROM TB_M_Employee WHERE Id = @Id", new { Id = TB_Id.Text }).Result.SingleOrDefault();
                var bachelor = con.QueryAsync<Employee>("SELECT Bachelor FROM TB_M_Employee WHERE Id = @Id", new { Id = TB_Id.Text }).Result.SingleOrDefault();
                var university = con.QueryAsync<Employee>("SELECT University FROM TB_M_Employee WHERE Id = @Id", new { Id = TB_Id.Text }).Result.SingleOrDefault();
                var joindate = con.QueryAsync<Employee>("SELECT JoinDate FROM TB_M_Employee WHERE Id = @Id", new { Id = TB_Id.Text }).Result.SingleOrDefault();

                TB_Name.Text = name.Name;
                TB_PhoneNum.Text = phonenum.PhoneNum;
                TB_Address.Text = address.Address;
                TB_PlaceBirth.Text = placebirth.PlaceBirth;
                TB_BirthDate.Text = birthdate.BirthDate;
                TB_NIK.Text = nik.NIK;
                TB_Email.Text = email.Email;
                TB_NPWP.Text = npwp.NPWP;
                TB_Religion.Text = religion.Religion;
                TB_Bachelor.Text = bachelor.Bachelor;
                TB_University.Text = university.University;
                TB_JoinDate.Text = joindate.JoinDate;
            }

        }

        private void BTN_Insert_Click(object sender, RoutedEventArgs e)
        {
            var insert = con.ExecuteAsync("exec SP_Insert_Employee @Name, " +
                "@PhoneNum, " +
                "@Address, " +
                "@PlaceBirth, " +
                "@BirthDate, " +
                "@NIK, " +
                "@Email, " +
                "@NPWP, " +
                "@Religion, " +
                "@Bachelor, " +
                "@University, " +
                "@JoinDate",
                new
                {
                    Name = TB_Name.Text,
                    PhoneNum = TB_PhoneNum.Text,
                    Address = TB_Address.Text,
                    PlaceBirth = TB_PlaceBirth.Text,
                    BirthDate = TB_BirthDate.SelectedDate.Value.Date,
                    NIK = TB_NIK.Text,
                    Email = TB_Email.Text,
                    NPWP = TB_NPWP.Text,
                    Religion = TB_Religion.Text,
                    Bachelor = TB_Bachelor.Text,
                    University = TB_University.Text,
                    JoinDate = TB_JoinDate.SelectedDate.Value.Date
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
                var update = con.ExecuteAsync("exec SP_Update_Employee @Id, " +
                    "@Name, " +
                    "@PhoneNum, " +
                    "@Address, " +
                    "@PlaceBirth, " +
                    "@BirthDate, " +
                    "@NIK, " +
                    "@Email, " +
                    "@NPWP, " +
                    "@Religion, " +
                    "@Bachelor, " +
                    "@University, " +
                    "@JoinDate",
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
                        Religion = TB_Religion.Text,
                        Bachelor = TB_Bachelor.Text,
                        University = TB_University.Text,
                        JoinDate = TB_JoinDate.SelectedDate.Value.Date
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
    }
}
