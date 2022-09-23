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

                tboxPurchasedCosts.Text = dbItem.PurchaseAmount.ToString();
                if (dbItem.RepairCosts != null)
                    tboxRepairCosts.Text = dbItem.RepairCosts.ToString();
                tboxPrice.Text = dbItem.Price.ToString();

                // Стажёрам не даем редактировать даже серийный номер
                if (user.AcessLevel < 1)
                    tboxSerialNo.IsReadOnly = true;


                

                
            }
            
        }

    }
}
