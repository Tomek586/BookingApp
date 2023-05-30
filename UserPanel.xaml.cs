using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private int c;
        public UserPanel(int c)
        {
            InitializeComponent();

            LoadConcerts();
            this.c = c;

            welcome.Text = $"Witaj {c}  ";

        }
        private void LoadConcerts()
        {
            try
            {
                string FileName = "Booking.mdf";
                string CurrentDirectory = Directory.GetCurrentDirectory();
                string ProjectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(CurrentDirectory).FullName).FullName).FullName;
                string FilePath = Path.Combine(ProjectDirectory, FileName);

                string conn = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={FilePath};Integrated Security=True;Connect Timeout=30;";
                SqlConnection con = new SqlConnection(conn);
                {
                    con.Open();

                    string query = "SELECT c.concert_id, c.concert_date, a.artist_name, c.venue " +
                                   "FROM concerts c " +
                                   "JOIN artists a ON c.artist_id = a.artist_id";

                    SqlCommand command = new SqlCommand(query, con);
                    SqlDataReader reader = command.ExecuteReader();

                    List<Concert> concerts = new List<Concert>();

                    while (reader.Read())
                    {
                        int concertId = reader.GetInt32(0);
                        DateTime concertDate = reader.GetDateTime(1);
                        string artistName = reader.GetString(2);
                        string venue = reader.GetString(3);

                        Concert concert = new Concert(concertId, concertDate, artistName, venue);
                        concerts.Add(concert);
                    }

                    concertsDataGrid.ItemsSource = concerts;

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void BuyTicket_Click(object sender, RoutedEventArgs e)
        {
            Button buyTicketButton = (Button)sender;
            int concertId = (int)buyTicketButton.Tag;

            

            string FileName = "Booking.mdf";
            string CurrentDirectory = Directory.GetCurrentDirectory();
            string ProjectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(CurrentDirectory).FullName).FullName).FullName;
            string FilePath = Path.Combine(ProjectDirectory, FileName);

            string conn = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={FilePath};Integrated Security=True;Connect Timeout=30;";
            SqlConnection con = new SqlConnection(conn);
            con.Open();

            string query = "INSERT INTO bookings (login_id, concert_id, booking_date) " +
                          "VALUES (@UserId, @ConcertId, GETDATE())";

            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@UserId", c);
            command.Parameters.AddWithValue("@ConcertId", concertId);

            int rowsAffected = command.ExecuteNonQuery();

            MessageBox.Show("Ticket for concert with ID " + concertId + " purchased!");
        }
    }

    public class Concert
    {
        public int ConcertId { get; set; }
        public DateTime ConcertDate { get; set; }
        public string ArtistName { get; set; }
        public string Venue { get; set; }

        public Concert(int concertId, DateTime concertDate, string artistName, string venue)
        {
            ConcertId = concertId;
            ConcertDate = concertDate;
            ArtistName = artistName;
            Venue = venue;
        }
    }




}

