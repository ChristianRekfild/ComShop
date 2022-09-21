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
using System.Linq;
using ComShop.Model;

namespace ComShop
{
    /// <summary>
    /// Логика взаимодействия для ListOfItems.xaml
    /// </summary>
    public partial class ListOfItems : Window
    {
        int UserID;
        //List<Item> listOfItems;
        public ListOfItems(int staffId)
        {
            UserID = staffId;
            getListOfItems();
            InitializeComponent();

            // Данные читает, уже неплохо
            //foreach (var item in listOfItems)
            //{
            //    MessageBox.Show(item.Description);
            //}
        }

        public void getListOfItems()
        {
            using (ComShopContext context = new ComShopContext())
            {
                // listOfItems = context.Items.ToList();
                this.DataContext = context.Items.ToList();
            }
        }

        // Открыть товар
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //Item sellectedItem = listOfItems.SelectedItem as Item;
            //MessageBox.Show(sellectedItem.Description);

            Item? item = listOfItemsblah.SelectedItem as Item;            
            if (item != null)
            {
                ItemCard itemCard = new ItemCard(UserID, item.IdItem);
                itemCard.Show();
                this.Close();

                //ListOfItems listOfItems = new ListOfItems(UserID);
                //listOfItems.Show();
                //this.Close();
            }

        }

        private void listOfItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
