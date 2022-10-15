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
    /// Логика взаимодействия для ReturnFromRepair.xaml
    /// </summary>
    public partial class ReturnFromRepair : Window
    {
        private int UserID;
        private int ItemID;
        public ReturnFromRepair(int userID, int itemID)
        {
            UserID = userID;
            ItemID = itemID;
            InitializeComponent();
            
        }

        private bool CheckDataBeforeSave()
        {
            decimal repairCosts;
            if (!Decimal.TryParse(tbox_cost.Text, out repairCosts))
            {
                MessageBox.Show("Введены некорректные данные");
                return false;
            }
            if (repairCosts < 0)
            {
                MessageBox.Show("Сумма за ремонт не может быть отрицательной");
                return false;
            }

            return true;
        }

        // Вернуть с ремонта
        private void returnItem(object sender, RoutedEventArgs e)
        {
            if (!CheckDataBeforeSave()) return;

            decimal repairCosts;
            if (!Decimal.TryParse(tbox_cost.Text, out repairCosts)) return;

            using (ComShopContext context = new ComShopContext())
            {
                var item = context.Items.Find(ItemID);

                if (item.RepairCosts != null)
                {
                    item.RepairCosts += repairCosts;
                    item.UnderRepair = false;
                } else
                {
                    item.RepairCosts = repairCosts;
                    item.UnderRepair = false;
                }

                context.SaveChanges();
            }
            MessageBox.Show("Товар успешно принят с ремонта");

            AfterLogin after = new AfterLogin(UserID);
            after.Show();
            this.Close();            

        }
    }
}
