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
    /// Логика взаимодействия для BuyItem.xaml
    /// </summary>
    public partial class BuyItem : Window
    {
        int UserID;
        int ClientID;
        int CategoryID;
        public BuyItem(int userID, int categoryID, int clientID)
        {
            UserID = userID;
            ClientID = clientID;
            CategoryID = categoryID;
            InitializeComponent();
            SetSettingsByAcessLevel();
            GetDataFromDB();
        }



        private void goToMainMenu(object sender, RoutedEventArgs e)
        {
            AfterLogin afterLogin = new AfterLogin(UserID);
            afterLogin.Show();
            this.Close();
        }

        private void selectSeller(object sender, RoutedEventArgs e)
        {
            ListOfClients listOfClients = new ListOfClients(UserID, CategoryID, ClientID);
            listOfClients.Show();
            this.Close();
        }

        public void SetSettingsByAcessLevel()
        {
            using (ComShopContext comShop = new ComShopContext())
            {
                var user = comShop.staff.Find(UserID);

                // Категорию ручками выбирать не дадим!
                tbox_categoty.IsReadOnly = true;
                tbox_client.IsReadOnly = true;
            }
        }

        public void GetDataFromDB()
        {
            using (ComShopContext comShop = new ComShopContext())
            {
                if (CategoryID != 0)
                {
                    var category = comShop.Categories.Find(CategoryID);
                    tbox_categoty.Text = category.Name;
                }

                if (ClientID != 0)
                {
                    var client = comShop.Clients.Find(ClientID);
                    string clientName = $"{client.FamilyName}\n{client.Name}\n{client.Patronymic}\n{client.DateOfBirth}\n{client.Passport}";
                    tbox_client.Text = clientName;
                }
            }
                
        }

        // Выбор категории
        private void selectCategory(object sender, RoutedEventArgs e)
        {
            CategoryList category = new CategoryList(UserID, CategoryID, ClientID);
            category.Show();
            this.Close();
        }

        private void buyItem(object sender, RoutedEventArgs e)
        {

            // TODO добавить блок проверок 

            using (ComShopContext comshop = new ComShopContext())
            {
                var item = new Item();

                item.SerialNumber = tbox_serial.Text;
                item.Description = tbox_description.Text;
                item.PurchaseAmount = Convert.ToDecimal(tbox_purchPrice.Text);
                item.Price = Convert.ToDecimal(tbox_price.Text);
                item.DateOfPurchase = DateOnly.FromDateTime(DateTime.Now);
                item.UnderRepair = false;
                item.CaregoryNo = CategoryID;
                item.ClientNo = ClientID;


                comshop.Items.Add(item);
                comshop.SaveChanges();

                MessageBox.Show($"Товар {item.Description} куплен за {item.PurchaseAmount}");
            }

            

            AfterLogin after = new AfterLogin(UserID);
            after.Show();
            this.Close();
        }

    }
}
