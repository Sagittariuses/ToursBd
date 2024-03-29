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
    /// Логика взаимодействия для EditTourist.xaml
    /// </summary>
    public partial class EditTourist : Window
    {
        ToursEntities ConObj = new ToursEntities();
        public EditTourist()
        {
            InitializeComponent();
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 0x00000002;
            this.Left = (screenWidth - this.Width) / 0x00000002;
            TouristNumber.Text = TouristNumber.Text + Helper.TouristID;
            Tourists tourist = ConObj.Tourists.Where(x => x.TouristID == Helper.TouristID).FirstOrDefault();
            Surname.Text = tourist.Surname;
            Name.Text = tourist.Name;
            Patr.Text = tourist.Patronymic;

            InfoAboutTourists touristinfo = ConObj.InfoAboutTourists.Where(x => x.ID_tourist == Helper.TouristID).FirstOrDefault();
            PassSer.Text = touristinfo.PassportSeries.ToString();
            City.Text = touristinfo.City;
            Country.Text = touristinfo.Country;
            Phone.Text = touristinfo.Phone;
            Index.Text = touristinfo.Index.ToString();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Tourists tourist = ConObj.Tourists.Where(x => x.TouristID == Helper.TouristID).FirstOrDefault();
            tourist.Surname = Surname.Text;
            tourist.Name = Name.Text;
            tourist.Patronymic= Patr.Text;

            InfoAboutTourists touristinfo = ConObj.InfoAboutTourists.Where(x => x.ID_tourist == Helper.TouristID).FirstOrDefault();
            touristinfo.PassportSeries = Convert.ToInt32(PassSer.Text);
            touristinfo.City = City.Text;
            touristinfo.Country = Country.Text;
            touristinfo.Phone = Phone.Text;
            touristinfo.Index = Convert.ToInt32(Index.Text);

            ConObj.SaveChanges();
            this.Close();
        }
    }
}
