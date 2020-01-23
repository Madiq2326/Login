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
            var check = con.QueryAsync<Check>("exec SP_Retrieve_Login @Username, @Password",
                new { Username = TB_Username.Text, Password = TB_Password.Password }).Result.SingleOrDefault();
            if(check != null)
            {
                MessageBox.Show("Berhasil Login");
                var newwindow = new Window2();
                newwindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Gagal Login");
            }
        }
    }
}
