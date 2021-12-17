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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToursBd
{
    public partial class MainWindow : Window
    {
        int comp;
        ToursEntities db = Helper.GetContext();
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClientLV.ItemsSource = db.Tourists.ToList();
            ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
        }

        private void ClientLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh.Visibility = Visibility.Visible;
            ClientLV.ItemsSource = db.Tourists.ToList();
            comp = ClientLV.SelectedIndex;
            ClientInfoLV.ItemsSource = db.InfoAboutTourists.Where(a => a.ID_tourist == comp + 1).ToList();
            Helper.RemID = comp + 1;
        }
        private void ClientInfoLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh.Visibility = Visibility.Visible;
            ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
            comp = ClientInfoLV.SelectedIndex;
            ClientLV.ItemsSource = db.Tourists.Where(a => a.TouristID == comp + 1).ToList();
            Helper.RemID = comp + 1;
        }
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh.Visibility = Visibility.Hidden;
            ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
            ClientLV.ItemsSource = db.Tourists.ToList();
            TouristFilt.Text = null;
            PassSeries.Text = null;
            Phone.Text = null;
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditTourist editTourist = new EditTourist();
            editTourist.ShowDialog();
            ClientLV.ItemsSource = db.Tourists.ToList();
            ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
            Refresh.Visibility = Visibility.Hidden;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            ToursEntities ConObj = new ToursEntities();
            Tourists tourist = ConObj.Tourists.Where(o => o.TouristID == Helper.RemID).FirstOrDefault();
            InfoAboutTourists infoAboutTourists = ConObj.InfoAboutTourists.Where(o => o.ID_tourist == Helper.RemID).FirstOrDefault();
            ConObj.Tourists.Remove(tourist);
            ConObj.InfoAboutTourists.Remove(infoAboutTourists);
            ConObj.SaveChanges();
            ClientLV.ItemsSource = db.Tourists.ToList();
            ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
            Refresh.Visibility = Visibility.Hidden;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddTourist addTourist = new AddTourist();
            addTourist.ShowDialog();
            ClientLV.ItemsSource = db.Tourists.ToList();
            ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
        }

        private void TouristFilt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TouristFilt.Text == "")
            {
                ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
                ClientLV.ItemsSource = db.Tourists.ToList();
            }
            else
            {
                /*PassSeries.Text = null;
                Phone.Text = null;*/
                if (TouristFilt.Text.Contains(" "))
                {
                    string[] words = new string[3];
                    words = TouristFilt.Text.Split(' ');
                    if (words.Count() == 2)
                    {
                        int Id = Convert.ToInt32(db.Tourists.ToList().FindIndex(x => x.Surname.StartsWith(words[0]) && x.Name.StartsWith(words[1]))) + 1;
                        ClientLV.ItemsSource = db.Tourists.Where(x => x.TouristID == Id).ToList();
                        ClientInfoLV.ItemsSource = db.InfoAboutTourists.Where(x => x.ID_tourist == Id).ToList();
                    }
                    else if (words.Count() == 3)
                    {
                        int Id = Convert.ToInt32(db.Tourists.ToList().FindIndex(x => x.Surname.StartsWith(words[0]) && x.Name.StartsWith(words[1]) && x.Patronymic.StartsWith(words[2]))) + 1;
                        ClientLV.ItemsSource = db.Tourists.Where(x => x.TouristID == Id).ToList();
                        ClientInfoLV.ItemsSource = db.InfoAboutTourists.Where(x => x.ID_tourist == Id).ToList();
                    }
                    else
                    {
                        ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
                        ClientLV.ItemsSource = db.Tourists.ToList();
                    }
                } else
                {
                    int Id = Convert.ToInt32(db.Tourists.ToList().FindIndex(x => x.Surname.StartsWith(TouristFilt.Text))) + 1;
                    ClientLV.ItemsSource = db.Tourists.Where(x => x.TouristID == Id).ToList();
                    ClientInfoLV.ItemsSource = db.InfoAboutTourists.Where(x => x.ID_tourist == Id).ToList();
                }
            }
        }

        private void Phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Phone.Text == "")
            {
                ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
                ClientLV.ItemsSource = db.Tourists.ToList();
            }
            else
            {
                /*TouristFilt.Text = null;
                PassSeries.Text = null;*/
                int Id = Convert.ToInt32(db.InfoAboutTourists.ToList().FindIndex(x => x.Phone.StartsWith(Phone.Text))) + 1;
                ClientInfoLV.ItemsSource = db.InfoAboutTourists.Where(x => x.ID_tourist == Id).ToList();
                ClientLV.ItemsSource = db.Tourists.Where(x => x.TouristID == Id).ToList();
            }
        }

        private void PassSeries_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PassSeries.Text == "")
            {
                ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
                ClientLV.ItemsSource = db.Tourists.ToList();
            }
            else
            {
                /*TouristFilt.Text = null;
                Phone.Text = null;*/
                int mod = 1000;
                if (mod != 0)
                    for (int i = 0; i < PassSeries.Text.Length; i++)
                    {
                        var Ids = db.InfoAboutTourists.ToList().FindAll(x => x.PassportSeries / mod == Convert.ToInt32(PassSeries.Text));
                        ClientInfoLV.ItemsSource = Ids;
                        mod /= 10;
                    }
            }
            for (int i = 0; i < ClientInfoLV.Items.Count; i++)
            {

            }
        }
    }
    
}
