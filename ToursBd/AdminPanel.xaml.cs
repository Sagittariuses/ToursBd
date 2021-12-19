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
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    /// dynamic selectedItem = TourLV.SelectedItem;
    /// Helper.TourRem = selectedItem.ID_tour;
    public partial class AdminPanel : Window
    {
        ToursEntities db = Helper.GetContext();
        ToursEntities ConObj = new ToursEntities();
        public AdminPanel()
        {
            InitializeComponent();
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 0x00000002;
            this.Left = (screenWidth - this.Width) / 0x00000002;
        }


        // --------------------------------------------------
        //          Динамическая подгрузка данных
        // --------------------------------------------------
        private void Tables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Users.IsSelected)
            {
                UsersLV.ItemsSource = db.Users.ToList();
                ClientLV.ItemsSource = null;
                ClientInfoLV.ItemsSource = null;
                VouchersLV.ItemsSource = null;
                ToursLV.ItemsSource = null;
                SeasonsLV.ItemsSource = null;
                PaymentLV.ItemsSource = null;
                LoginPlaceholder.Visibility = Visibility.Visible;
                Sur_NamePlaceholder.Visibility = Visibility.Visible;
                RolePlaceholder.Visibility = Visibility.Visible;
                Role.Text = "";
                Sur_Name.Text = "";
                Login.Text = "";
            } 
            else if (Tourists.IsSelected)
            {
                if (Helper.TouristID == 0)
                {
                    UsersLV.ItemsSource = null;
                    ClientLV.ItemsSource = db.Tourists.ToList();
                    ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
                    VouchersLV.ItemsSource = null;
                    ToursLV.ItemsSource = null;
                    SeasonsLV.ItemsSource = null;
                    PaymentLV.ItemsSource = null;
                    CityPlaceholder.Visibility = Visibility.Visible;
                    PassSeriesPlaceholder.Visibility = Visibility.Visible;
                    PhonePlaceholder.Visibility = Visibility.Visible;
                    Phone.Text = "";
                    PassSeries.Text = "";
                    City.Text = "";
                    SurnamePlaceholder.Visibility = Visibility.Visible;
                    NamePlaceholder.Visibility = Visibility.Visible;
                    PatrPlaceholder.Visibility = Visibility.Visible;
                    Name.Text = "";
                    Patr.Text = "";
                    Surname.Text = "";
                } else
                {
                    CityPlaceholder.Visibility = Visibility.Visible;
                    PassSeriesPlaceholder.Visibility = Visibility.Visible;
                    PhonePlaceholder.Visibility = Visibility.Visible;
                    Phone.Text = "";
                    PassSeries.Text = "";
                    City.Text = "";
                    SurnamePlaceholder.Visibility = Visibility.Visible;
                    NamePlaceholder.Visibility = Visibility.Visible;
                    PatrPlaceholder.Visibility = Visibility.Visible;
                    Name.Text = "";
                    Patr.Text = "";
                    Surname.Text = "";
                }
            } 
            else if (Seasons.IsSelected)
            {
                UsersLV.ItemsSource = null;
                ClientLV.ItemsSource = null;
                ClientInfoLV.ItemsSource = null;
                VouchersLV.ItemsSource = null;
                ToursLV.ItemsSource = null;
                SeasonsLV.ItemsSource = db.Seasons.ToList();
                PaymentLV.ItemsSource = null;
                DatePlaceholder.Visibility = Visibility.Visible;
                ClosedPlaceholder.Visibility = Visibility.Visible;
                TourSeasonPlaceholder.Visibility = Visibility.Visible;
                TourSeason.Text = "";
                Date.Text = "";
                Closed.Text = "";

            } 
            else if (Payment.IsSelected)
            {
                UsersLV.ItemsSource = null;
                ClientLV.ItemsSource = null;
                ClientInfoLV.ItemsSource = null;
                VouchersLV.ItemsSource = null;
                ToursLV.ItemsSource = null;
                SeasonsLV.ItemsSource = null;
                PaymentLV.ItemsSource = db.Payment.ToList();
                SumPayPlaceholder.Visibility = Visibility.Visible;
                DatePayPlaceholder.Visibility = Visibility.Visible;
                VouchPayPlaceholder.Visibility = Visibility.Visible;
                SumPay.Text = "";
                DatePay.Text = "";
                VouchPay.Text = "";
            }
            else if (Tours.IsSelected)
            {
                UsersLV.ItemsSource = null;
                ClientLV.ItemsSource = null;
                ClientInfoLV.ItemsSource = null;
                VouchersLV.ItemsSource = null;
                ToursLV.ItemsSource = db.Tour.ToList();
                SeasonsLV.ItemsSource = null;
                PaymentLV.ItemsSource = null;
                PricePlaceholder.Visibility = Visibility.Visible;
                TitlePlaceholder.Visibility = Visibility.Visible;
                TourIDPlaceholder.Visibility = Visibility.Visible;
                Title.Text = "";
                Price.Text = "";
                TourID.Text = "";
            } 
            else if (Vouchers.IsSelected)
            {
                UsersLV.ItemsSource = null;
                ClientLV.ItemsSource = null;
                ClientInfoLV.ItemsSource = null;
                VouchersLV.ItemsSource = db.Vouchers.ToList();
                ToursLV.ItemsSource = null;
                SeasonsLV.ItemsSource = null;
                PaymentLV.ItemsSource = null;
                SeasonPlaceholder.Visibility = Visibility.Visible;
                TouristPlaceholder.Visibility = Visibility.Visible;
                TouristID.Text = "";
                SeasonID.Text = "";
            }
        }

        // --------------------------------------------------
        //                      Tourists
        // --------------------------------------------------

        private void ClientLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshTourist.Visibility = Visibility.Visible;
            ClientLV.ItemsSource = db.Tourists.ToList();
            dynamic selectedItem = ClientLV.SelectedItem;
            Helper.TouristID = selectedItem.TouristID;
            ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList().FindAll(x => x.ID_tourist == Helper.TouristID);
        }

        private void ClientInfoLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshTourist.Visibility = Visibility.Visible;
            ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
            dynamic selectedItem = ClientInfoLV.SelectedItem;
            Helper.TouristID = selectedItem.TouristID;
            ClientLV.ItemsSource = db.Tourists.ToList().FindAll(x => x.TouristID == Helper.TouristID); ;
            
        }

        // CRUD
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshTourist.Visibility = Visibility.Hidden;
            ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
            ClientLV.ItemsSource = db.Tourists.ToList();
            PassSeries.Text = null;
            Phone.Text = null;
        }

        private void EditTourist_Click(object sender, RoutedEventArgs e)
        {
            EditTourist editTourist = new EditTourist();
            editTourist.ShowDialog();
            ClientLV.ItemsSource = db.Tourists.ToList();
            ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
            RefreshTourist.Visibility = Visibility.Hidden;
        }

        private void DeleteTourist_Click(object sender, RoutedEventArgs e)
        {
            Tourists tourist = ConObj.Tourists.Where(o => o.TouristID == Helper.TouristID).FirstOrDefault();
            InfoAboutTourists infoAboutTourists = ConObj.InfoAboutTourists.Where(o => o.ID_tourist == Helper.TouristID).FirstOrDefault();
            ConObj.Tourists.Remove(tourist);
            ConObj.InfoAboutTourists.Remove(infoAboutTourists);
            ConObj.SaveChanges();
            ClientLV.ItemsSource = db.Tourists.ToList();
            ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
            RefreshTourist.Visibility = Visibility.Hidden;
        }

        private void AddTourist_Click(object sender, RoutedEventArgs e)
        {
            AddTourist addTourist = new AddTourist();
            addTourist.ShowDialog();
            ClientLV.ItemsSource = db.Tourists.ToList();
            ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
        }

        // Фильтры
        private void Phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Phone.Text == "")
            {
                ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
            }
            else
            {
                ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList().FindAll(x => x.Phone.StartsWith(Phone.Text));
            }
        }

        private void PassSeries_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (PassSeries.Text == "")
                {
                    ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
                }
                else
                {
                    int mod = 1000;
                    if (mod != 0)
                        for (int i = 0; i < PassSeries.Text.Length; i++)
                        {

                            ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList().FindAll(x => x.PassportSeries / mod == Convert.ToInt32(PassSeries.Text));
                            mod /= 10;
                        }
                }
            }
            catch 
            {
                ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
            }
        }

        private void Surname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Surname.Text == "")
            {
                ClientLV.ItemsSource = db.Tourists.ToList();
            } 
            else
            {
                if (Surname.Text.Contains(" "))
                {
                    string[] words = new string[3];
                    words = Surname.Text.Split(' ');
                    if (words.Count() == 2)
                    {
                        ClientLV.ItemsSource = db.Tourists.ToList().FindAll(x => x.Surname.StartsWith(words[0]) && x.Name.StartsWith(words[1]));
                    }
                    else if (words.Count() == 3)
                    {
                        ClientLV.ItemsSource = db.Tourists.ToList().FindAll(x => x.Surname.StartsWith(words[0]) && x.Name.StartsWith(words[1]) && x.Patronymic.StartsWith(words[2]));

                    }
                    else
                    {
                        ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
                        ClientLV.ItemsSource = db.Tourists.ToList();
                    }
                }
                else
                {
                    ClientLV.ItemsSource = db.Tourists.ToList().FindAll(x => x.Surname.StartsWith(Surname.Text));
                }
            }
        }

        private void City_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (City.Text == "")
            {
                ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList();
            }
            else
            {
                ClientInfoLV.ItemsSource = db.InfoAboutTourists.ToList().FindAll(x => x.City.StartsWith(City.Text));
            }
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Name.Text == "")
            {
                ClientLV.ItemsSource = db.Tourists.ToList();
            }
            else
            {
                ClientLV.ItemsSource = db.Tourists.ToList().FindAll(x => x.Name.StartsWith(Name.Text));
            }
        }

        private void Patr_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Patr.Text == "")
            {
                ClientLV.ItemsSource = db.Tourists.ToList();
            }
            else
            {
                ClientLV.ItemsSource = db.Tourists.ToList().FindAll(x => x.Patronymic.StartsWith(Patr.Text));
            }
        }

        // Клик на tb
        private void Surname_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SurnamePlaceholder.Visibility = Visibility.Hidden;
            NamePlaceholder.Visibility = Visibility.Visible;
            PatrPlaceholder.Visibility = Visibility.Visible;
            Name.Text = "";
            Patr.Text = "";
        }

        private void Name_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SurnamePlaceholder.Visibility = Visibility.Visible;
            NamePlaceholder.Visibility = Visibility.Hidden;
            PatrPlaceholder.Visibility = Visibility.Visible;
            Patr.Text = "";
            Surname.Text = "";
        }

        private void Patr_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SurnamePlaceholder.Visibility = Visibility.Visible;
            NamePlaceholder.Visibility = Visibility.Visible;
            PatrPlaceholder.Visibility = Visibility.Hidden;
            Name.Text = "";
            Surname.Text = "";
        }

        private void Phone_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            CityPlaceholder.Visibility = Visibility.Visible;
            PassSeriesPlaceholder.Visibility = Visibility.Visible;
            PhonePlaceholder.Visibility = Visibility.Hidden;
            PassSeries.Text = "";
            City.Text = "";
        }

        private void PassSeries_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            CityPlaceholder.Visibility = Visibility.Visible;
            PassSeriesPlaceholder.Visibility = Visibility.Hidden;
            PhonePlaceholder.Visibility = Visibility.Visible;
            Phone.Text = "";
            City.Text = "";
        }

        private void City_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            CityPlaceholder.Visibility = Visibility.Hidden;
            PassSeriesPlaceholder.Visibility = Visibility.Visible;
            PhonePlaceholder.Visibility = Visibility.Visible;
            Phone.Text = "";
            PassSeries.Text = "";
        }

        // --------------------------------------------------
        //                      Users
        // --------------------------------------------------
        private void UsersLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic selectedItem = UsersLV.SelectedItem;
            Helper.UserID = selectedItem.UserID;
        }

        // CRUD
        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            Users user = new Users();
            user = ConObj.Users.Where(x => x.UserID == Helper.UserID).FirstOrDefault();
            ConObj.Users.Remove(user);
            ConObj.SaveChanges();
            UsersLV.ItemsSource = db.Users.ToList();
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            Helper.UserID = UsersLV.SelectedIndex + 1;
            UserEdit userEdit = new UserEdit();
            userEdit.ShowDialog();
            UsersLV.ItemsSource = db.Users.ToList();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            UserAdd addUser = new UserAdd();
            addUser.ShowDialog();
            UsersLV.ItemsSource = db.Users.ToList();
        }

        // Фильтры
        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            UsersLV.ItemsSource = db.Users.ToList().FindAll(x => x.Login.StartsWith(Login.Text));
        }

        private void Sur_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Sur_Name.Text.Contains(" "))
            {
                try
                {
                    string[] words = new string[2];
                    words = Sur_Name.Text.Split(' ');
                    UsersLV.ItemsSource = db.Users.ToList().FindAll(x => x.Surname.StartsWith(words[0]) && x.Name.StartsWith(words[1]));
                }
                catch
                {
                    string chek = Sur_Name.Text.Trim(' ');
                    UsersLV.ItemsSource = db.Users.ToList().FindAll(x => x.Surname.StartsWith(chek) || x.Name.StartsWith(chek));

                }
            }
            else
            {
                UsersLV.ItemsSource = db.Users.ToList().FindAll(x => x.Surname.StartsWith(Sur_Name.Text) || x.Name.StartsWith(Sur_Name.Text));
            }
        }

        private void Role_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                UsersLV.ItemsSource = db.Users.ToList().FindAll(x => x.RoleID == Convert.ToInt32(Role.Text));
            }
            catch
            {
                UsersLV.ItemsSource = db.Users.ToList();
            }
        }

        // Клик на tb
        private void Sur_Name_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Sur_NamePlaceholder.Visibility = Visibility.Hidden;
            RolePlaceholder.Visibility = Visibility.Visible;
            LoginPlaceholder.Visibility = Visibility.Visible;
            Role.Text = "";
            Login.Text = "";
        }

        private void Login_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            LoginPlaceholder.Visibility = Visibility.Hidden;
            Sur_NamePlaceholder.Visibility = Visibility.Visible;
            RolePlaceholder.Visibility = Visibility.Visible;
            Role.Text = "";
            Sur_Name.Text = "";
        }

        private void Role_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            RolePlaceholder.Visibility = Visibility.Hidden;
            LoginPlaceholder.Visibility = Visibility.Visible;
            Sur_NamePlaceholder.Visibility = Visibility.Visible;
            Login.Text = "";
            Sur_Name.Text = "";
        }

        // --------------------------------------------------
        //                      Vouchers
        // --------------------------------------------------

        // Фильтры
        private void TouristID_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                VouchersLV.ItemsSource = db.Vouchers.ToList().FindAll(x => x.TouristID == Convert.ToInt32(TouristID.Text));

            }
            catch
            {
                VouchersLV.ItemsSource = db.Vouchers.ToList();
            }

        }

        private void SeasonID_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                VouchersLV.ItemsSource = db.Vouchers.ToList().FindAll(x => x.SeasonID == Convert.ToInt32(SeasonID.Text));

            }
            catch
            {
                VouchersLV.ItemsSource = db.Vouchers.ToList();
            }
        }

        // Клик на tb
        private void SeasonID_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SeasonPlaceholder.Visibility = Visibility.Hidden;
            TouristPlaceholder.Visibility = Visibility.Visible;
            TouristID.Text = "";
        }

        private void TouristID_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SeasonPlaceholder.Visibility = Visibility.Visible;
            TouristPlaceholder.Visibility = Visibility.Hidden;
            SeasonID.Text = "";
        }

        // --------------------------------------------------
        //                      Tours
        // --------------------------------------------------
        private void ToursLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic selectedItem = ToursLV.SelectedItem;
            Helper.TourID = selectedItem.TourID;
        }

        // CRUD
        private void EditTour_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddTour_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteTour_Click(object sender, RoutedEventArgs e)
        {

        }


        // Фильтры
        private void TourID_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ToursLV.ItemsSource = db.Tour.ToList().FindAll(x => x.TourID == Convert.ToInt32(TourID.Text));

            }
            catch
            {
                ToursLV.ItemsSource = db.Tour.ToList();
            }

        }

        private void Title_TextChanged(object sender, TextChangedEventArgs e)
        {
            ToursLV.ItemsSource = db.Tour.ToList().FindAll(x => x.Title.StartsWith(Title.Text));
        }

        private void Price_TextChanged(object sender, TextChangedEventArgs e)
        {
            ToursLV.ItemsSource = db.Tour.ToList().FindAll(x => x.Price.StartsWith(Price.Text));

        }

        // Клик на tb
        private void Price_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PricePlaceholder.Visibility = Visibility.Hidden;
            TitlePlaceholder.Visibility = Visibility.Visible;
            TourIDPlaceholder.Visibility = Visibility.Visible;
            TourID.Text = "";
            Title.Text = "";
        }

        private void Title_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PricePlaceholder.Visibility = Visibility.Visible;
            TitlePlaceholder.Visibility = Visibility.Hidden;
            TourIDPlaceholder.Visibility = Visibility.Visible;
            TourID.Text = "";
            Price.Text = "";
        }

        private void TourID_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PricePlaceholder.Visibility = Visibility.Visible;
            TitlePlaceholder.Visibility = Visibility.Visible;
            TourIDPlaceholder.Visibility = Visibility.Hidden;
            Title.Text = "";
            Price.Text = "";
        }

        // --------------------------------------------------
        //                      Seasons
        // --------------------------------------------------
        private void SeasonsLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic selectedItem = SeasonsLV.SelectedItem;
            Helper.SeasonID = selectedItem.SeasonID;
        }

        // CRUD
        private void AddSeason_Click(object sender, RoutedEventArgs e)
        {

        }
        
        private void EditSeason_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteSeason_Click(object sender, RoutedEventArgs e)
        {

        }
       
        // Фильтры
        private void TourSeason_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                SeasonsLV.ItemsSource = db.Seasons.ToList().FindAll(x => x.TourID == Convert.ToInt32(TourSeason.Text));
            }
            catch
            {
                SeasonsLV.ItemsSource = db.Seasons.ToList();
            }

        }

        private void Closed_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                SeasonsLV.ItemsSource = db.Seasons.ToList().FindAll(x => x.SeasonClosed == Convert.ToBoolean(Closed.Text));
            }
            catch
            {
                SeasonsLV.ItemsSource = db.Seasons.ToList();
            }

        }

        private void Date_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                SeasonsLV.ItemsSource = db.Seasons.ToList().FindAll(x => x.StartDate.ToString().StartsWith(Date.Text) || x.FinishDate.ToString().StartsWith(Date.Text));
            }
            catch
            {
                SeasonsLV.ItemsSource = db.Seasons.ToList();
            }
        }

        // Клик на tb
        private void TourSeason_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DatePlaceholder.Visibility = Visibility.Visible;
            ClosedPlaceholder.Visibility = Visibility.Visible;
            TourSeasonPlaceholder.Visibility = Visibility.Hidden;
            Closed.Text = "";
            Date.Text = "";
        }

        private void Closed_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DatePlaceholder.Visibility = Visibility.Visible;
            ClosedPlaceholder.Visibility = Visibility.Hidden;
            TourSeasonPlaceholder.Visibility = Visibility.Visible;
            TourSeason.Text = "";
            Date.Text = "";
        }


        private void Date_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DatePlaceholder.Visibility = Visibility.Hidden;
            ClosedPlaceholder.Visibility = Visibility.Visible;
            TourSeasonPlaceholder.Visibility = Visibility.Visible;
            TourSeason.Text = "";
            Closed.Text = "";
        }

        // --------------------------------------------------
        //                      Payments
        // --------------------------------------------------
        
        // Фильтры
        private void SumPay_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                PaymentLV.ItemsSource = db.Payment.ToList().FindAll(x => x.Sum.ToString().StartsWith(SumPay.Text));

            }
            catch
            {
                PaymentLV.ItemsSource = db.Payment.ToList();
            }
        }

        private void VouchPay_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                PaymentLV.ItemsSource = db.Payment.ToList().FindAll(x => x.VoucherID == Convert.ToInt32(VouchPay.Text));

            }
            catch
            {
                PaymentLV.ItemsSource = db.Payment.ToList();
            }
        }

        private void DatePay_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                PaymentLV.ItemsSource = db.Payment.ToList().FindAll(x => x.Date.ToString().StartsWith(DatePay.Text));

            }
            catch
            {
                PaymentLV.ItemsSource = db.Payment.ToList();
            }
        }
        
        // Клик на tb
        private void DatePay_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SumPayPlaceholder.Visibility = Visibility.Visible;
            DatePayPlaceholder.Visibility = Visibility.Hidden;
            VouchPayPlaceholder.Visibility = Visibility.Visible;
            VouchPay.Text = "";
            SumPay.Text = "";

        }

        private void SumPay_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SumPayPlaceholder.Visibility = Visibility.Hidden;
            DatePayPlaceholder.Visibility = Visibility.Visible;
            VouchPayPlaceholder.Visibility = Visibility.Visible;
            VouchPay.Text = "";
            DatePay.Text = "";
        }

        private void VouchPay_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SumPayPlaceholder.Visibility = Visibility.Visible;
            DatePayPlaceholder.Visibility = Visibility.Visible;
            VouchPayPlaceholder.Visibility = Visibility.Hidden;
            SumPay.Text = "";
            DatePay.Text = "";
        }

        // ------------------------------
        //            Выход
        // ------------------------------
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        
    }
}
