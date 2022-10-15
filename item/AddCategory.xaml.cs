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

namespace ComShop.item
{
    /// <summary>
    /// Логика взаимодействия для AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        int UserID;
        public AddCategory(int userID)
        {
            UserID = userID;
            InitializeComponent();
        }

        private bool CheckDataBeforeAdd()
        {
            if (String.IsNullOrWhiteSpace(tbox_newCategory.Text)) return false;
            if (tbox_newCategory.Text.Length > 150) return false;

            return true;
        }

        // Добавление категории
        private void add(object sender, RoutedEventArgs e)
        {
            if (!CheckDataBeforeAdd())
            {
                MessageBox.Show("Некорректно заполнены данные");
                return;
            }

            using (ComShopContext context = new ComShopContext())
            {
                var category = new Category();
                category.Name = tbox_newCategory.Text;
                context.Categories.Add(category);

                context.SaveChanges();
            }

            AfterLogin after = new AfterLogin(UserID);
            after.Show();
            this.Close();

        }
    }
}
