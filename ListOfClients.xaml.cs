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
        int ClientID;
        int CategoryID;
        public ListOfClients(int userID, int categoryID, int clientID)
        {
            UserID = userID;
            ClientID = clientID;
            CategoryID = categoryID;
            GetClientList();
            InitializeComponent();
            SetSettingsByAcessLevel(UserID);
        }

        public void GetClientList()
        {
            using (ComShopContext context = new ComShopContext())
            {
                this.DataContext = context.Clients.ToList();
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
            // Под чем я был, когда написл эту тупость?!

            //AddClient addClient = new AddClient(UserID);
            //addClient.Show();
            //this.Close();
        }

        // Выбираем клиента для покупки
        private void selectClient(object sender, RoutedEventArgs e)
        {
            //Item? item = listOfItemsblah.SelectedItem as Item;
            Client? client = clientList.SelectedItem as Client;

            if (client == null)
            {
                MessageBox.Show("Не выбран клиент");
                return;
            }

            BuyItem buyItem = new BuyItem(UserID, CategoryID, client.IdClient);
            buyItem.Show();
            this.Close();
        }
    }
}
