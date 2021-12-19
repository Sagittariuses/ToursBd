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
    /// Логика взаимодействия для TourEdit.xaml
    /// </summary>
    public partial class TourEdit : Window
    {
        ToursEntities ConObj = new ToursEntities();
        public TourEdit()
        {
            InitializeComponent();
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 0x00000002;
            this.Left = (screenWidth - this.Width) / 0x00000002;
            TourNumb.Text += Helper.UserID;
            Tour tour = ConObj.Tour.Where(x => x.TourID == Helper.TourID).FirstOrDefault();
            Title.Text = tour.Title;
            Price.Text = tour.Price;
            Info.Text = tour.Information;
            Photo.Text = tour.Photo;
        }


        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Tour tour = ConObj.Tour.Where(x => x.TourID == Helper.TourID).FirstOrDefault();
            tour.Title = Title.Text;
            tour.Price = Price.Text;
            tour.Information = Info.Text;
            tour.Photo = Photo.Text;
            ConObj.SaveChanges();
            this.Close();
        }
    }
}
