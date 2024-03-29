﻿using System;
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
    /// Логика взаимодействия для UserAdd.xaml
    /// </summary>
    public partial class UserAdd : Window
    {
        ToursEntities db = Helper.GetContext();
        public UserAdd()
        {
            InitializeComponent();
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 0x00000002;
            this.Left = (screenWidth - this.Width) / 0x00000002;
            
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string aa = db.Users.ToList().FirstOrDefault(x => x.Login == Login.Text && x.UserID > -1).Login;
                if (Login.Text == db.Users.ToList().FirstOrDefault(x => x.Login == Login.Text && x.UserID > -1).Login)
                {
                    Add.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Такой логин уже занят");
                }
            }
            catch
            {
                int MaxId = db.Users.Max(x => x.UserID) + 1;
                ToursEntities ConObj = new ToursEntities();
                Users users = new Users
                {
                    UserID = MaxId,
                    Name = Name.Text,
                    Surname = Surname.Text,
                    Password = Password.Text,
                    Login = Login.Text,
                    RoleID = Convert.ToInt32(UserRole.SelectedIndex + 1)
                };

                ConObj.Users.Add(users);
                ConObj.SaveChanges();
                this.Close();

            }
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            Add.Visibility = Visibility.Visible;

        }
    }
}
