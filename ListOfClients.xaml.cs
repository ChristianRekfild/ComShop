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
        private List<Client> clients;
        int NumberOfAPages;
        int CurrPage;
        public ListOfClients(int userID, int categoryID, int clientID)
        {
            UserID = userID;
            ClientID = clientID;
            CategoryID = categoryID;
            InitializeComponent();
            GetClientList();
            SetSettingsByAcessLevel(UserID);
        }

        public void GetClientList()
        {
            using (ComShopContext context = new ComShopContext())
            {
                clients = new List<Client>();
                clients = context.Clients.ToList();
                this.DataContext = clients.Skip(0).Take(15);

                CurrPage = 1;
                tbox_page.Text = CurrPage.ToString();
                NumberOfAPages = clients.Count / 15;
                if (clients.Count % 15 != 0)
                    NumberOfAPages++;
                tbox_totalPages.Text = NumberOfAPages.ToString();
            }
        }

        public void SetSettingsByAcessLevel(int userID)
        {
            tbox_page.IsReadOnly = true;
            tbox_totalPages.IsReadOnly = true;

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

            AddClient addClient = new AddClient(UserID);
            addClient.Show();
            this.Close();
        }

        // Выбираем клиента для покупки
        private void selectClient(object sender, RoutedEventArgs e)
        {
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

        private void getPrev(object sender, RoutedEventArgs e)
        {
            if (CurrPage > 1)
            {
                this.DataContext = clients.Skip((CurrPage - 1) * 15).Take(15).OrderBy(x => x.IdClient);
                CurrPage--;
                tbox_page.Text = CurrPage.ToString();
            }
        }

        private void getNext(object sender, RoutedEventArgs e)
        {
            if (CurrPage < NumberOfAPages)
            {
                this.DataContext = clients.Skip((CurrPage - 1) * 15).Take(15).OrderBy(x => x.IdClient);
                CurrPage++;
                tbox_page.Text = CurrPage.ToString();
            }
        }

        // Найти клиентов
        private void findClients(object sender, RoutedEventArgs e)
        {
            using (ComShopContext context = new ComShopContext())
            {
                // Защита от польщователя, который ввёл только пробелы, а потом удивляется, "А почему ничего не найдёно??.."
                if (String.IsNullOrWhiteSpace(tbox_familyName.Text))
                    tbox_familyName.Text = String.Empty;
                if (String.IsNullOrWhiteSpace(tbox_name.Text))
                    tbox_name.Text = String.Empty;
                if (String.IsNullOrWhiteSpace(tbox_patronomic.Text))
                    tbox_patronomic.Text = String.Empty;
                if (String.IsNullOrWhiteSpace(tbox_passport.Text))
                    tbox_passport.Text = String.Empty;

                clients = context.Clients.Where(x => x.FamilyName.Contains(tbox_familyName.Text)).Where(x => x.Name.Contains(tbox_name.Text)).Where(x => x.Patronymic.Contains(tbox_patronomic.Text)).Where(x => x.Passport.Contains(tbox_passport.Text)).OrderBy(x => x.IdClient).ToList();

                //this.DataContext = ItemList;

                this.DataContext = clients.Skip(0).Take(15);

                CurrPage = 1;
                tbox_page.Text = CurrPage.ToString();
                NumberOfAPages = clients.Count / 15;
                if (clients.Count % 15 != 0)
                    NumberOfAPages++;

                tbox_totalPages.Text = NumberOfAPages.ToString();
            }
        }

        // Открыть карточку клиента
        private void openClientCard(object sender, RoutedEventArgs e)
        {
            Client? client = clientList.SelectedItem as Client;

            if (client == null)
            {
                MessageBox.Show("Не выбран клиент");
                return;
            }

            ClientCard card = new ClientCard(UserID, client.IdClient);
            card.Show();
            this.Close();
        }
    }
}
