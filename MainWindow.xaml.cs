﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace BookingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string FileName = "Booking.mdf";
            string CurrentDirectory = Directory.GetCurrentDirectory();
            string ProjectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(CurrentDirectory).FullName).FullName).FullName;
            string FilePath = Path.Combine(ProjectDirectory, FileName);

            string conn = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={FilePath};Integrated Security=True;Connect Timeout=30;";
            SqlConnection con = new SqlConnection(conn);
        }
        private void Login_btt_click()
        {
            try
            {
                string FileName = "ConcertsBookings.mdf";
                string CurrentDirectory = Directory.GetCurrentDirectory();
                string ProjectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(CurrentDirectory).FullName).FullName).FullName;
                string FilePath = Path.Combine(ProjectDirectory, FileName);

                string conn = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={FilePath};Integrated Security=True;Connect Timeout=30;";
                SqlConnection con = new SqlConnection(conn);


                con.Open();
                string add_data = "Select * from [dbo].[logins] where username=@username and password=@password ";
                SqlCommand cmd = new SqlCommand(add_data, con);


                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Password);
                cmd.ExecuteNonQuery();
                int Count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                txtUsername.Text = "";
                txtPassword.Password = "";
                if (Count > 0)
                {
                    UserPanel u1 = new UserPanel();
                    this.Close();
                    u1.Show();

                }
                else
                {
                    MessageBox.Show("chujoza");
                }


            }
            catch
            {

            }
        }

        private void Button_click(object sender, RoutedEventArgs e)
        {
            Register r1 = new Register();
            this.Close();
            r1.Show();
        }
    }
}
