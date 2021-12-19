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

namespace ToursBd.Windows
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        ToursEntities db = Helper.GetContext();
        int LastID;
        public Registration()
        {
            InitializeComponent();
            WrongLP.Visibility = Visibility.Hidden;
            //Центровка
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 0x00000002;
            this.Left = (screenWidth - this.Width) / 0x00000002;

            LastID = db.Users.Max(x => x.UserID);
        }
        private void EnterInButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            ToursEntities con = new ToursEntities();

            if (NameBox.Text != "" && LastnameBox.Text != "" && LoginBox.Text != "" && PasswordBox.Text != "" && RePasswordBox.Text != "")
            {
                WrongLP.Visibility = Visibility.Hidden;
                if (PasswordBox.Text != RePasswordBox.Text)
                {
                    WrongLP.Text = "\t\t\t\t\tПароли не совпадают";
                    WrongLP.Visibility = Visibility.Visible;
                }
                else
                {
                    try
                    {
                        if (LoginBox.Text == db.Users.ToList().Find(x => x.Login.StartsWith(LoginBox.Text)).Login)
                        {
                            WrongLP.Text = "\t\t\t\t\tЛогин уже занят";
                            WrongLP.Visibility = Visibility.Visible;
                        }
                    }
                    catch
                    {
                        Users users = new Users
                        {
                            UserID = LastID + 1,
                            Name = NameBox.Text,
                            Surname = LastnameBox.Text,
                            Login = LoginBox.Text,
                            Password = PasswordBox.Text,
                            RoleID = 2
                        };
                        try
                        {
                            con.Users.Add(users);
                            con.SaveChanges();
                        }
                        catch
                        { }
                        if (users.RoleID == 2)
                        {
                            Helper.TouristID = users.RoleID;
                            UserView us = new UserView();
                            us.Show();
                            this.Close();
                        }
                    }
                }
            }
            else
            {
                WrongLP.Text = "Все поля должны быть заполнены";
                WrongLP.Visibility = Visibility.Visible;
            }
        }
    }
}