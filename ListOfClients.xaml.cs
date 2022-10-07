using ComShop.Model;
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

namespace ComShop
{
    /// <summary>
    /// Логика взаимодействия для ListOfClients.xaml
    /// </summary>
    public partial class ListOfClients : Window
    {
        private int UserID;
        public ListOfClients(int userID)
        {
            UserID = userID;
            GetClientList();
            InitializeComponent();
            SetSettingsByAcessLevel(UserID);
        }

        public void GetClientList()
        {
            using (ComShopContext context = new ComShopContext())
            {
                this.DataContext = context.staff.ToList();
            }
        }

        public void SetSettingsByAcessLevel(int userID)
        {
            using (ComShopContext comShop = new ComShopContext())
            {
                var user = comShop.staff.Find(UserID);

                if (user.AcessLevel < 1) {
                    btn_addClient.Visibility = Visibility.Collapsed;
                }
            }

        }

        // Главное меню
        private void goToMainMenu(object sender, RoutedEventArgs e)
        {
            AfterLogin afterLogin = new AfterLogin(UserID);
            afterLogin.Show();
            this.Close();
        }

        // Добавление клиента
        private void addClient(object sender, RoutedEventArgs e)
        {
            AddClient addClient = new AddClient(UserID);
            addClient.Show();
            this.Close();
        }
    }
}
