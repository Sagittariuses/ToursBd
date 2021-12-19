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
    /// Логика взаимодействия для UserEdit.xaml
    /// </summary>
    public partial class UserEdit : Window
    {
        ToursEntities db = Helper.GetContext();
        ToursEntities ConObj = new ToursEntities();
        public UserEdit()
        {
            InitializeComponent();
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 0x00000002;
            this.Left = (screenWidth - this.Width) / 0x00000002;
            UserNumb.Text += Helper.UserID;
            Users user = ConObj.Users.Where(x => x.UserID == Helper.UserID).FirstOrDefault();
            Surname.Text = user.Surname;
            Name.Text = user.Name;
            Login.Text = user.Login;
            Password.Text = user.Password;
            if (user.RoleID == 1)
            {
                UserRole.SelectedIndex = 0;
            }
            else
            {
                UserRole.SelectedIndex = 1;
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text == db.Users.ToList().Find(x => x.UserID != Helper.UserID && x.Login.StartsWith(Login.Text)).Login)
            {
                Edit.Visibility = Visibility.Collapsed;
            }
            else
            {
                Users user = ConObj.Users.Where(x => x.UserID == Helper.UserID).FirstOrDefault();
                user.Surname = Surname.Text;
                user.Name = Name.Text;
                user.Login = Login.Text;
                user.Password = Password.Text;
                user.RoleID = UserRole.SelectedIndex + 1;
                ConObj.SaveChanges();
                this.Close();
            }
                
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            Edit.Visibility = Visibility.Visible;
        }
    }
}
