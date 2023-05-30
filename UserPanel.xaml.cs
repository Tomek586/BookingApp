using System;
using System.Collections.Generic;
using System.Data;
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
    /// Logika interakcji dla klasy UserPanel.xaml
    /// </summary>
    public partial class UserPanel : Window
    {
        public UserPanel()
        {
            InitializeComponent();
            string FileName = "Booking.mdf";
            string CurrentDirectory = Directory.GetCurrentDirectory();
            string ProjectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(CurrentDirectory).FullName).FullName).FullName;
            string FilePath = Path.Combine(ProjectDirectory, FileName);

            string conn = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={FilePath};Integrated Security=True;Connect Timeout=30;";
            SqlConnection con = new SqlConnection(conn);

            
            SqlCommand cmdEvents = new SqlCommand("SELECT c.concert_date, a.artist_name, c.venue FROM concerts c JOIN artists a ON c.artist_id = a.artist_id", con);



            con.Open();
            DataTable dt = new DataTable();
            dt.Load(cmdEvents.ExecuteReader());
            con.Close();
            Events.DataContext = dt;





        }
        private void b1_click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
