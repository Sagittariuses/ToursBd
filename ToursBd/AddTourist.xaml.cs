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
    /// Логика взаимодействия для AddTourist.xaml
    /// </summary>
    public partial class AddTourist : Window
    {
        ToursEntities db = Helper.GetContext();
        public AddTourist()
        {
            InitializeComponent();
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 0x00000002;
            this.Left = (screenWidth - this.Width) / 0x00000002;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int MaxId = db.Tourists.Max(x => x.TouristID) + 1;
            ToursEntities ConObj = new ToursEntities();
            Tourists tourist = new Tourists()
            {
                TouristID = MaxId,
                Name = Name.Text,
                Surname = Surname.Text,
                Patronymic = Patr.Text
            };
            InfoAboutTourists touristinfo = new InfoAboutTourists()
            {
                ID_tourist = MaxId,
                PassportSeries = Convert.ToInt32(PassSer.Text),
                City = City.Text,
                Country = Country.Text,
                Phone = Phone.Text,
                Index = Convert.ToInt32(Index.Text)
            };
            ConObj.Tourists.Add(tourist);
            ConObj.InfoAboutTourists.Add(touristinfo);
            ConObj.SaveChanges();
            this.Close();
        }
    }
}
