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
using ToursBd.Windows;

namespace ToursBd
{
    public partial class MainWindow : Window
    {
        ToursEntities db = Helper.GetContext();
        public MainWindow()
        {
            InitializeComponent();
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 0x00000002;
            this.Left = (screenWidth - this.Width) / 0x00000002;
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            Registration reg = new Registration();
            reg.Show();
            this.Close();
        }

        private void WithNoRegBtn_Click(object sender, RoutedEventArgs e)
        {
            UserView userView = new UserView();
            this.Close();
            userView.Show();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text != "" && PasswordBox.Text != "")
            {
                WrongLP.Visibility = Visibility.Hidden;
                try
                {
                    string chekLog = db.Users.ToList().Find(x => x.Login.StartsWith(LoginBox.Text)).Login;
                    if (LoginBox.Text == chekLog)
                    {
                        if (PasswordBox.Text == db.Users.Where(x => x.Login == chekLog).ToList().Find(b => b.UserID > 0).Password)
                        {
                            if (Convert.ToInt32(db.Users.Where(a => a.Login == chekLog).ToList().Find(b => b.UserID > 0).RoleID) == 1)
                            {
                                AdminPanel ap = new AdminPanel();
                                ap.Show();
                                this.Close();
                            }
                            else
                            {
                                UserView us = new UserView();
                                us.Show();
                                this.Close();
                            }
                        }
                        else
                        {
                            WrongLP.Visibility = Visibility.Visible;
                        }
                    }
                }
                catch
                {
                    WrongLP.Visibility = Visibility.Visible;
                }

            }
            else
            {
                WrongLP.Visibility = Visibility.Visible;
            }
        }
        int i = 0;
        private void PasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            WrongLP.Visibility = Visibility.Hidden;
            while (i < PasswordBox.Text.Length)
            {
                i++;
                HidePassword.Text += '*';
            }
            if (i > PasswordBox.Text.Length)
            {
                i--;
                HidePassword.Text = HidePassword.Text.Substring(0, HidePassword.Text.Length - 1);
            }


        }

        private void LoginBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            WrongLP.Visibility = Visibility.Hidden;
        }

        private void ShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (ShowPassword.IsChecked == true)
            {
                PasswordBox.Foreground = Brushes.White;
                PasswordBox.Visibility = Visibility.Hidden;
                if (HidePassword.Text.Length > PasswordBox.Text.Length)
                {
                    HidePassword.Text = HidePassword.Text.Substring(0, PasswordBox.Text.Length);
                }
                else if (HidePassword.Text.Length < PasswordBox.Text.Length)
                {
                    for (int i = HidePassword.Text.Length; i != PasswordBox.Text.Length; i++)
                    {
                        HidePassword.Text += '*';
                    }
                }

            }
            else
            {
                PasswordBox.Foreground = Brushes.Black;
                PasswordBox.Visibility = Visibility.Visible;

            }

        }
    }
    
}