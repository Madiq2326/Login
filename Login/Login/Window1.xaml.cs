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

        public void clear()
        {
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

        public Window1()
        {
            InitializeComponent();
        }

        private void BTN_Insert_Click(object sender, RoutedEventArgs e)
        {
            var insert = con.QueryAsync<Employee>("exec SP_Insert_Employee @Id, @Name, @PhoneNum, @Address, @PlaceBirth, @BirthDate, @NIK, @Email, @NPWP, @Religion, @Bachelor, @University, @JoinDate",
                new { Id = TB_Id.Text, Name = TB_Name.Text, PhoneNum = TB_PhoneNum.Text, Address = TB_Address.Text, PlaceBirth = TB_PlaceBirth.Text, BirthDate = TB_BirthDate.Text, NIK = TB_NIK.Text, Email = TB_Email.Text, NPWP = TB_NPWP.Text, Religion = TB_Religion.Text, Bachelor = TB_Bachelor.Text, University = TB_University.Text, JoinDate = TB_JoinDate.Text });
            MessageBox.Show("Data Entered");
            clear();
        }

        private void BTN_Update_Click(object sender, RoutedEventArgs e)
        { 
            var update = con.QueryAsync<Employee>("exec SP_Update_Employee @Id, @Name, @PhoneNum, @Address, @PlaceBirth, @BirthDate, @NIK, @Email, @NPWP, @Religion, @Bachelor, @University, @JoinDate",
                new { Id = TB_Id.Text, Name = TB_Name.Text, PhoneNum = TB_PhoneNum.Text, Address = TB_Address.Text, PlaceBirth = TB_PlaceBirth.Text, BirthDate = TB_BirthDate.Text, NIK = TB_NIK.Text, Email = TB_Email.Text, NPWP = TB_NPWP.Text, Religion = TB_Religion.Text, Bachelor = TB_Bachelor.Text, University = TB_University.Text, JoinDate = TB_JoinDate.Text });
            MessageBox.Show("Data Has Been Changed");
            clear();
        }

        private void BTN_Delete_Click(object sender, RoutedEventArgs e)
        {
            var check = con.QueryAsync<Employee>("exec SP_Delete_Employee @Id",
                new { Id = TB_Id.Text });
            MessageBox.Show("Data Has Been Deleted");
            clear();
        }

        private void BTN_ShowData_Click(object sender, RoutedEventArgs e)
        {
            var view = con.Query<Employee>("SELECT * FROM Employee").ToList();
            DaftarTable.ItemsSource = view;
        }
    }
}
