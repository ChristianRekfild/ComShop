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
    /// Логика взаимодействия для ItemCard.xaml
    /// </summary>
    public partial class ItemCard : Window
    {
        private int UserID;
        private int ItemID;
        public ItemCard(int staffId, int itemId)
        {
            UserID = staffId;
            ItemID = itemId;
            InitializeComponent();
            Getitem(ItemID);
            SetSettingsByAcessLevel(UserID, ItemID);
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
                tboxDateOfPurchase.Text = dbItem.DateOfPurchase.ToString("dd-MM-yyyy");
                if (user.AcessLevel < 5)
                    tboxDateOfPurchase.IsReadOnly = true;

                if (dbItem.DateOfSale != null)
                {
                    // nullable type не захотел дружить, потому пришлось призывать посредников
                    DateOnly date = (DateOnly)dbItem.DateOfSale;
                    tboxDateOfSale.Text = date.ToString("dd-MM-yyyy");
                }
                else
                    tboxDateOfSale.Text = string.Empty;

                tboxPrice.Text = dbItem.Price.ToString();

                tboxPurchasedCosts.Text = dbItem.PurchaseAmount.ToString();

                // Запрещаем редактировать стоимость ремонта всем кроме зам директора
                if (dbItem.RepairCosts != null)
                    tboxRepairCosts.Text = dbItem.RepairCosts.ToString();
                else
                    tboxRepairCosts.Text = "0";

                if (!string.IsNullOrEmpty(dbItem.SerialNumber))
                    tboxSerialNo.Text = dbItem.SerialNumber;
                else
                    tboxSerialNo.Text = string.Empty;
              
                // В ремонте
                if (dbItem.UnderRepair)
                    chboxUnderRepair.IsChecked = true;
                else
                    chboxUnderRepair.IsChecked = false;
                


            }

        }

        private void SetSettingsByAcessLevel(int UserID, int ItemID)
        {
            using (ComShopContext comShop = new ComShopContext())
            {

                var user = comShop.staff.Find(UserID);
                var dbItem = comShop.Items.Find(ItemID);

                // Никто не достоин редактировать ID товара. Только высшие администраторы из легенд!
                tboxID.IsReadOnly = true;

                // запрещаем продажу и отправку на ремонт для стажёров
                if (user.AcessLevel < 1)
                {
                    btn_sellItem.Visibility = Visibility.Collapsed;
                    btn_repair.Visibility = Visibility.Collapsed;
                }

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
                if (user.AcessLevel < 4)
                    tboxPurchasedCosts.IsReadOnly = true;

                if (user.AcessLevel < 4)
                    tboxRepairCosts.IsReadOnly = true;

                // Стажёрам не даем редактировать даже серийный номер
                if (user.AcessLevel < 1)
                    tboxSerialNo.IsReadOnly = true;

                // Запрещаем редактировать серийный номер всем кроме зам директора для проданного товара
                if (dbItem.DateOfSale != null)
                {
                    if (user.AcessLevel < 4)
                        tboxSerialNo.IsReadOnly = true;
                }

                // в ремонте
                if (user.AcessLevel < 4)
                    chboxUnderRepair.IsEnabled = false;

                if (user.AcessLevel < 4)
                    tboxDateOfSale.IsReadOnly = true;
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

        // Главное меню
        private void Btn_MainMenu(object sender, RoutedEventArgs e)
        {
            AfterLogin afterLogin = new AfterLogin(UserID);
            afterLogin.Show();
            this.Close();
        }

        private void sellItem(object sender, RoutedEventArgs e)
        {
            using (ComShopContext comShop = new ComShopContext())
            {
                var item = comShop.Items.Find(ItemID);
                if (item.UnderRepair)
                {
                    MessageBox.Show("Нельзя продать товар, который находится в ремонте!");
                    return;
                }
            }

            SaleOfItem sale = new SaleOfItem(UserID, ItemID);
            sale.Show();
            this.Close();
        }

        // Отправить на ремонт
        private void sendForRepair(object sender, RoutedEventArgs e)
        {

        }
    }
}
