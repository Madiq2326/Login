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

        public Window2(int roleVal)
        {
            int role = roleVal;

            InitializeComponent();
            if(role == 0)
            {
                Btn_MasterData.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Btn_MasterData.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void Click_Btn_MasterData(object sender, RoutedEventArgs e)
        {
            var newwindow = new Window1(0);
            newwindow.Show();
            Close();
        }
    }
}
