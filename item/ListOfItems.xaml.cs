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
    /// Логика взаимодействия для ListOfItems.xaml
    /// </summary>
    public partial class ListOfItems : Window
    {
        private int UserID;
        private string DescriptionContains;
        private string SerialContains;
        private bool? InStock;
        private List<Item> ItemList;
        int NumberOfAPages;
        int CurrPage;
        public ListOfItems(int staffId, string descContains, string serialContains, bool? inStock)
        {
            UserID = staffId;
            DescriptionContains = descContains;
            SerialContains = serialContains;
            InStock = inStock;
            InitializeComponent();
            getListOfItems();
            tbox_page.IsReadOnly = true;
            tbox_page.Text = CurrPage.ToString();
            tbox_total.Text = NumberOfAPages.ToString();

        }

        public void getListOfItems()
        {

            using (ComShopContext context = new ComShopContext())
            {
                // listOfItems = context.Items.ToList();
                //this.DataContext = context.Items.ToList();

                ItemList = context.Items.Where(x => x.Description.Contains(DescriptionContains)).Where(x => x.SerialNumber.Contains(SerialContains)).OrderBy(x => x.IdItem).ToList();
                if (InStock == true)
                    ItemList = ItemList.Where(x => x.DateOfSale.Equals(null)).ToList();

                //this.DataContext = ItemList;

                this.DataContext = ItemList.Skip(0).Take(15);

                CurrPage = 1;
                NumberOfAPages = ItemList.Count / 15;
                if (ItemList.Count % 15 != 0)
                    NumberOfAPages++;

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
            }

        }

        private void listOfItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // В главное меню
        private void Btn_MainMenu(object sender, RoutedEventArgs e)
        {
            AfterLogin afterLogin = new AfterLogin(UserID);
            afterLogin.Show();
            this.Close();
        }


        private void getPrev(object sender, RoutedEventArgs e)
        {
            if (CurrPage > 1)
            {
                this.DataContext = ItemList.Skip( (CurrPage-1) * 15 ).Take(15).OrderBy(x => x.IdItem);
                CurrPage--;
                tbox_page.Text = CurrPage.ToString();
            }
        }

        private void getNext(object sender, RoutedEventArgs e)
        {
            if  (CurrPage < NumberOfAPages)
            {
                this.DataContext = ItemList.Skip((CurrPage - 1) * 15).Take(15).OrderBy(x => x.IdItem);
                CurrPage++;
                tbox_page.Text = CurrPage.ToString();
            }
        }
    }
}
