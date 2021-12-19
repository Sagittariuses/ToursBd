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

namespace ToursBd
{
    /// <summary>
    /// Логика взаимодействия для SeasonEdit.xaml
    /// </summary>
    public partial class SeasonEdit : Window
    {
        ToursEntities db = Helper.GetContext();
        ToursEntities ConObj = new ToursEntities();
        public SeasonEdit()
        {
            InitializeComponent();
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 0x00000002;
            this.Left = (screenWidth - this.Width) / 0x00000002;
            SeasonNumb.Text += Helper.SeasonID;
            Seasons seasons = ConObj.Seasons.Where(x => x.SeasonID == Helper.SeasonID).FirstOrDefault();
            TourID.Text = seasons.TourID.ToString();
            DateStart.Text = seasons.StartDate.ToString();
            DateFinish.Text = seasons.FinishDate.ToString();
            Seats.Text = seasons.NumberOfSeats.ToString();
            if (seasons.SeasonClosed == true)
            {
                Open_close.SelectedIndex = 0;
            }
            else
            {
                Open_close.SelectedIndex = 1;
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            bool open_close;
            int tourid = Convert.ToInt32(TourID.Text);
            if (Open_close.SelectedIndex == 0)
            {
                open_close = true;
            }
            else
            {
                open_close = false;
            }
            if (tourid > db.Tour.Max(x => x.TourID))
            {
                Edit.Visibility = Visibility.Collapsed;
                MessageBox.Show("Такого тура не существует");
            }
            else
            {
                Seasons seasons = ConObj.Seasons.Where(x => x.SeasonID == Helper.SeasonID).FirstOrDefault();
                seasons.StartDate = Convert.ToDateTime(DateStart.Text);
                seasons.FinishDate = Convert.ToDateTime(DateFinish.Text);
                seasons.SeasonClosed = open_close;
                seasons.NumberOfSeats = Convert.ToInt32(Seats.Text);
                ConObj.Seasons.Add(seasons);
                ConObj.SaveChanges();
                this.Close();
            }
        }

        private void TourID_TextChanged(object sender, TextChangedEventArgs e)
        {
            Edit.Visibility = Visibility.Visible;

        }
    }
}
