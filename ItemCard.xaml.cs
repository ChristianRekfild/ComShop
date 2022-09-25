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
    /// Логика взаимодействия для ItemCard.xaml
    /// </summary>
    public partial class ItemCard : Window
    {
        int UserID;
        int ItemID;
        public ItemCard(int staffId, int itemId)
        {
            UserID = staffId;
            ItemID = itemId;
            InitializeComponent();
            Getitem(ItemID);
        }

        public void Getitem(int itemID)
        {
            using (ComShopContext comShop = new ComShopContext())
            {
                var dbItem = comShop.Items.Find(ItemID);
                var user = comShop.staff.Find(UserID);

                tboxID.Text = dbItem.IdItem.ToString();
                if (dbItem.Description != null)
                    tboxDesciption.Text = dbItem.Description;
                // Даты покупки и продажи
                tboxDateOfPurchase.Text = dbItem.DateOfPurchase.ToString();
                if (user.AcessLevel < 5)
                    tboxDateOfPurchase.IsReadOnly = true;

                if (dbItem.DateOfSale != null)
                    tboxDateOfSale.Text = dbItem.DateOfSale.ToString();

                tboxPrice.Text = dbItem.Price.ToString();
                // Только директор может редактировть ценник уже проданного товара
                if (user.AcessLevel < 5)
                {
                    if (dbItem.DateOfSale == null)
                    {
                        // Запрещаем редактировать ценник стажерам
                        if (user.AcessLevel < 1)
                            tboxPrice.IsReadOnly = true;
                    }
                    else
                    {
                        // Если товар уже продан - никому кроме директора нельзя редактировать цену товара
                        tboxPrice.IsReadOnly = true;
                    }
                }

                // Запрещаем редактировать стоимость покупки всем кроме заместителя директора
                tboxPurchasedCosts.Text = dbItem.PurchaseAmount.ToString();
                if (user.AcessLevel < 4)
                    tboxPurchasedCosts.IsReadOnly = true;

                // Запрещаем редактировать стоимость ремонта всем кроме зам директора
                if (dbItem.RepairCosts != null)                
                    tboxRepairCosts.Text = dbItem.RepairCosts.ToString();
                if (user.AcessLevel < 4)
                    tboxRepairCosts.IsReadOnly = true;

                    

                // Стажёрам не даем редактировать даже серийный номер
                if (user.AcessLevel < 1)
                    tboxSerialNo.IsReadOnly = true;

                // Запрещаем редактировать серийный номер всем кроме зам директора
                if (user.AcessLevel < 4)
                    tboxSerialNo.IsReadOnly = true;

                // В ремонте
                if (dbItem.UnderRepair)
                    chboxUnderRepair.Content = true;
                else
                    chboxUnderRepair.Content = false;
                if (user.AcessLevel < 4)
                    chboxUnderRepair.Focusable = false;


            }

        }

        private void chboxUnderRepair_Checked(object sender, RoutedEventArgs e)
        {
            using (ComShopContext comShop = new ComShopContext())
            {
                var user = comShop.staff.Find(UserID);
                if (user.AcessLevel >= 4)
                {
                    if (chboxUnderRepair.IsChecked.Value == true)
                    {
                        chboxUnderRepair.Content = false;
                    } else
                    {
                        chboxUnderRepair.Content = true;
                    }
                }
            }
        }

    }
}
