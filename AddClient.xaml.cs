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
    public partial class AddClient : Window
    {
        private int UserID;
        public AddClient(int userID)
        {
            UserID = userID;
            InitializeComponent();
            SetSettingByAcessLevel();
        }



        private void SetSettingByAcessLevel()
        {
            using (ComShopContext comShop = new ComShopContext())
            {
                var user = comShop.staff.Find(UserID);

                if (user.AcessLevel < 4)
                    btn_saveData.Visibility = Visibility.Collapsed;
            }

        }

        // Проверка данных перед сохранением
        private bool CheckData()
        {
            if (String.IsNullOrEmpty(tbox_familyName.Text)) return false;
            if (String.IsNullOrEmpty(tbox_name.Text)) return false;

            if (cld_dateOfDirth.SelectedDate == null)
                return false;
            if (String.IsNullOrEmpty(tbox_passport.Text)) return false;


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
                Client client = new Client
                {
                    FamilyName = tbox_familyName.Text,
                    Name = tbox_name.Text,
                    Patronymic = tbox_patronymic.Text,
                    //DateOfBirth = DateOnly.Parse(tbox_dateOfBirth.Text),
                    DateOfBirth = new DateOnly(cld_dateOfDirth.SelectedDate.Value.Year,
                                                cld_dateOfDirth.SelectedDate.Value.Month,
                                                cld_dateOfDirth.SelectedDate.Value.Day
                                                ),
                    Passport = tbox_passport.Text
                };

                comShop.Clients.Add(client);
                comShop.SaveChanges();

                MessageBox.Show("Клиент успешно добавлен");

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

        private void tbox_patronymic_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
