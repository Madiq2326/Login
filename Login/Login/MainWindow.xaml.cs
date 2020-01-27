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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using BCrypt.Net;

namespace Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_Login_Click(object sender, RoutedEventArgs e)
        {
            string hash_input = TB_Password.Password;

            //string hash = BCrypt.Net.BCrypt.HashPassword(hash_input);

            int temp_id;

            var check = con.Query<Check>("SELECT * FROM TB_M_User WHERE @Email = Email", 
                new { Email = TB_Email.Text}).SingleOrDefault();

            bool isValid = BCrypt.Net.BCrypt.Verify(hash_input, check.Password);

            if (isValid == true)
            {
                temp_id = check.Id;

                var role = con.QueryAsync<Check>("exec SP_Assign_Role @Id", 
                    new { Id = temp_id }).Result.SingleOrDefault();

                var temp_employee = con.QueryAsync<Check>("SELECT Employee_Id FROM TB_M_User WHERE Id=@Id",
                    new { Id = temp_id }).Result.SingleOrDefault();

                MessageBox.Show("Berhasil Login role : " + role.Role_Id);

                var newwindow = new Window2(role.Role_Id,temp_employee.Employee_Id);
                newwindow.Show();

                Close();
            }
            else
            {
                MessageBox.Show("Gagal Login");


                var newwindow = new Window2(1, 5);
                newwindow.Show();

                Close();
            }
        }

        private void Btn_Forgot_Password_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Password has been sended to " + TB_Email.Text);
        }
    }
}
