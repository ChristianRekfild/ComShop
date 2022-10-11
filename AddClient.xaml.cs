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
            SetEmptyData();
            SetSettingByAcessLevel();
        }

        private void SetEmptyData()
        {
            tbox_ID.Text = string.Empty;
            tbox_familyName.Text = string.Empty;
            tbox_name.Text = string.Empty;
            tbox_patronymic.Text = string.Empty;
            tbox_dateOfBirth.Text = string.Empty;
            tbox_passport.Text = string.Empty;
        }

        private void SetSettingByAcessLevel()
        {
            using (ComShopContext comShop = new ComShopContext())
            {
                var user = comShop.staff.Find(UserID);
                
                // Скорее всего даже не понадобится, т.к. суда доступ для таких будет прикрыт
                if (user.AcessLevel < 4)
                {
                    btn_saveData.Visibility = Visibility.Collapsed;
                }
            }

        }

        // Проверка данных перед сохранением
        private bool CheckData()
        {
            if (String.IsNullOrEmpty(tbox_familyName.Text)) return false;
            if (String.IsNullOrEmpty(tbox_name.Text)) return false;
            // Отчество допусткает пустое значение
            if (String.IsNullOrEmpty(tbox_dateOfBirth.Text)) return false;
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
                    DateOfBirth = DateOnly.Parse(tbox_dateOfBirth.Text),
                    Passport = tbox_passport.Text
                };

                comShop.Clients.Add(client);
                comShop.SaveChanges();
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
