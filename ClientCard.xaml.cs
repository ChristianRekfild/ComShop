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
using ComShop.Model;

namespace ComShop
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class ClientCard : Window
    {
        private int UserID;
        private int ClientID;
        public ClientCard(int userID, int clientID)
        {
            UserID = userID;
            ClientID = clientID;
            InitializeComponent();
            GetData();
            SetSettingByAcessLevel();
        }

        private void GetData()
        {
            using (ComShopContext comShop = new ComShopContext())
            {
                var client = comShop.Clients.Find(ClientID);

                tbox_name.Text = client.Name;
                tbox_familyName.Text = client.FamilyName;
                tbox_patronymic.Text = client.Patronymic;
                DateTime date = DateTime.Parse(client.DateOfBirth.ToString());
                cld_dateOfBirth.SelectionMode = CalendarSelectionMode.SingleDate;
                cld_dateOfBirth.SelectedDate = date;
                cld_dateOfBirth.DisplayDate = date;
                tbox_passport.Text = client.Passport;

            }
        }

        private void SetSettingByAcessLevel()
        {
            using (ComShopContext comShop = new ComShopContext())
            {
                var user = comShop.staff.Find(UserID);

                if (user.AcessLevel < 1)
                    btn_saveData.Visibility = Visibility.Collapsed;
            }

        }

        // Проверка данных перед сохранением
        private bool CheckData()
        {
            if (String.IsNullOrWhiteSpace(tbox_familyName.Text)) return false;
            if (String.IsNullOrWhiteSpace(tbox_name.Text)) return false;

            if (cld_dateOfBirth.SelectedDate == null)
                return false;
            if (String.IsNullOrWhiteSpace(tbox_passport.Text)) return false;


            return true;
        }

        // Сохранение данных
        private void SaveDataOnDB()
        {
            if (!CheckData())
            {
                MessageBox.Show("Проверьте правильность заполнения данных!");
                return;
            }

            using (ComShopContext comShop = new ComShopContext())
            {
                var client = comShop.Clients.Find(ClientID);

                client.Name = tbox_name.Text;
                client.FamilyName = tbox_familyName.Text;
                client.Patronymic = tbox_patronymic.Text;
                client.Passport = tbox_passport.Text;
                DateOnly dateOnly = new DateOnly(
                            cld_dateOfBirth.SelectedDate.Value.Year,
                            cld_dateOfBirth.SelectedDate.Value.Month,
                            cld_dateOfBirth.SelectedDate.Value.Day
                            );
                client.DateOfBirth = dateOnly;

                comShop.SaveChanges();
                MessageBox.Show("Клиент успешно изменен");

                AfterLogin after = new AfterLogin(UserID);
                after.Show();
                this.Close();
            }
        }

        // В главное меню
        private void goToMainMenu(object sender, RoutedEventArgs e)
        {
            AfterLogin afterLogin = new AfterLogin(UserID);
            afterLogin.Show();
            this.Close();
        }

        private void saveData(object sender, RoutedEventArgs e)
        {
            SaveDataOnDB();
        }

    }
}
