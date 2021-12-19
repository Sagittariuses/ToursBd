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
    /// Логика взаимодействия для SeasonAdd.xaml
    /// </summary>
    public partial class SeasonAdd : Window
    {
        ToursEntities db = Helper.GetContext();
        public SeasonAdd()
        {
            InitializeComponent();
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 0x00000002;
            this.Left = (screenWidth - this.Width) / 0x00000002;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
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
                Add.Visibility = Visibility.Collapsed;
                MessageBox.Show("Такого тура не существует");
            }
            else
            {
                int MaxId = db.Seasons.Max(x => x.SeasonID) + 1;
                ToursEntities ConObj = new ToursEntities();
                Seasons seasons = new Seasons
                {
                    SeasonID = MaxId,
                    StartDate = Convert.ToDateTime(DateStart.Text),
                    FinishDate = Convert.ToDateTime(DateFinish.Text),
                    SeasonClosed = open_close,
                    NumberOfSeats = Convert.ToInt32(Seats.Text)
                };
                ConObj.Seasons.Add(seasons);
                ConObj.SaveChanges();
                this.Close();
            }
        }

        private void TourID_TextChanged(object sender, TextChangedEventArgs e)
        {
            Add.Visibility = Visibility.Visible;
        }
    }
}
