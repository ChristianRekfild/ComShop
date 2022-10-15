using ComShop.item;
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
    /// Логика взаимодействия для CategoryList.xaml
    /// </summary>
    public partial class CategoryList : Window
    {
        int UserID;
        int ClientID;
        int CategoryID;
        public CategoryList(int userID, int categoryID, int clientID)
        {
            UserID = userID;
            ClientID = clientID;
            CategoryID = categoryID;
            GetCategoryList();
            InitializeComponent();
        }

        public void GetCategoryList()
        {
            using (ComShopContext context = new ComShopContext())
            {
                this.DataContext = context.Categories.ToList();
            }
        }

        // Главное меню
        private void goToMainMenu(object sender, RoutedEventArgs e)
        {
            AfterLogin afterLogin = new AfterLogin(UserID);
            afterLogin.Show();
            this.Close();
        }

        private void selectCategory(object sender, RoutedEventArgs e)
        {
            Category? category = categoryList.SelectedItem as Category;
            if (category == null)
            {
                MessageBox.Show("Не выбрана категория");
                return;
            }
            
            BuyItem buyItem = new BuyItem(UserID, category.IdCategory, ClientID);
            buyItem.Show();
            this.Close();
        }

        // Добавить категорию
        private void addCategory(object sender, RoutedEventArgs e)
        {
            AddCategory add = new AddCategory(UserID);
            add.Show();
            this.Close();
        }
    }
}
