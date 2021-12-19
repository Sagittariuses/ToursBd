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
    /// Логика взаимодействия для TourAdd.xaml
    /// </summary>
    public partial class TourAdd : Window
    {
        ToursEntities db = Helper.GetContext();
        ToursEntities ConObj = new ToursEntities();
        public TourAdd()
        {
            InitializeComponent();
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 0x00000002;
            this.Left = (screenWidth - this.Width) / 0x00000002;
            
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int MaxId = db.Tour.Max(x => x.TourID) + 1;
            ToursEntities ConObj = new ToursEntities();
            Tour tour = new Tour
            {
                TourID = MaxId,
                Title = Title.Text,
                Price = Price.Text,
                Information = Info.Text,
                Photo = Photo.Text
            };
            ConObj.Tour.Add(tour);
            ConObj.SaveChanges();
            this.Close();
        }
    }
    
}
