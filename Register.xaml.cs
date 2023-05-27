using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
using Path = System.IO.Path;

namespace BookingApp
{
    /// <summary>
    /// Logika interakcji dla klasy Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }
        private void Register_btt_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string FileName = "Booking.mdf";
                string CurrentDirectory = Directory.GetCurrentDirectory();
                string ProjectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(CurrentDirectory).FullName).FullName).FullName;
                string FilePath = Path.Combine(ProjectDirectory, FileName);

                string conn = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={FilePath};Integrated Security=True;Connect Timeout=30;";
                SqlConnection con = new SqlConnection(conn);


                con.Open();
                string add_data = "INSERT into [dbo].[logins] values(@username, @password) ";
                SqlCommand cmd = new SqlCommand(add_data, con);


                cmd.Parameters.AddWithValue("@username", username.Text);
                cmd.Parameters.AddWithValue("@password", password.Password);
                cmd.ExecuteNonQuery();
                con.Close();
                username.Text = "";
                password.Password = "";

                MainWindow w1 = new MainWindow();
                this.Close();
                w1.Show();
            }
            catch
            {

            }
           
        }

       
    }
}
