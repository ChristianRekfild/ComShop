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
    /// Логика взаимодействия для SaleOfItem.xaml
    /// </summary>
    public partial class SaleOfItem : Window
    {
        int UserID;
        int ItemID;
        public SaleOfItem(int userID, int itemID)
        {
            UserID = userID;
            ItemID = itemID;
            InitializeComponent();
            SetEDefaultValue();
        }

        // устанавливаем пустые значения
        public void SetEDefaultValue()
        {
            using (ComShopContext comShop = new ComShopContext())
            {
                var item = comShop.Items.Find(ItemID);
                tbox_price.Text = item.Price.ToString();
            }
        }

        // продать товар
        private void sellItem(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tbox_price.Text))
            {
                MessageBox.Show("Не указана цена продажи");
                return;
            }
            if (!Decimal.TryParse(tbox_price.Text, out decimal price))
            {
                MessageBox.Show("Неверно заполнена цена продажи.\nПроверьте правильность ввода!");
                return;
            }
                        
            using (ComShopContext comShop = new ComShopContext())
            {
                var user = comShop.staff.Find(UserID);
                var itemForSale = comShop.Items.Find(ItemID);

                // от руководителя магазина сотрудник может продавать товар за какую угодно сумму
                if (user.AcessLevel < 4)
                {
                    if ( price < (Decimal.Multiply(itemForSale.Price, new decimal(0.9) )))
                    {
                        MessageBox.Show($"Вы не можете продать товар менее чем за {Decimal.Multiply(itemForSale.Price, new decimal(0.9))}");
                        return;
                    }

                }

                itemForSale.DateOfSale = DateOnly.FromDateTime(DateTime.Now);
                itemForSale.Price = price;
                comShop.SaveChanges();

                MessageBox.Show($"Товар успешно продан за {itemForSale.Price}");

                AfterLogin afterLogin = new AfterLogin(UserID);
                afterLogin.Show();
                this.Close();

            }
        }
    }
}
