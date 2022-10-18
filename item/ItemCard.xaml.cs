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
            SetSettingsForSoldItem();
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
                
                if (dbItem.UnderRepair)
                {
                    lab_repairMaster.Content = "Находится в ремонте у";
                    var master = comShop.RepairMasters.Find(dbItem.RepairMasterNo);
                    tbox_repairMaster.Text = $"{master.FamilyName} {master.Name} {master.Patronymic}";
                } else
                {
                    if (dbItem.RepairMasterNo != null)
                    {
                        var master = comShop.RepairMasters.Find(dbItem.RepairMasterNo);
                        tbox_repairMaster.Text = $"{master.FamilyName} {master.Name} {master.Patronymic}";
                    }
                    lab_repairMaster.Content = "Был в ремонте у";
                }

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

                if (user.AcessLevel < 4)
                    tboxRepairCosts.IsReadOnly = true;

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

                // Отправка на ремонт
                if (user.AcessLevel < 2)
                {
                    btn_repair.Visibility = Visibility.Collapsed;
                }

                // вернуть товар с ремонта может только менеджер магазина
                if (user.AcessLevel < 3)
                {
                    btn_returnFromRepair.Visibility = Visibility.Collapsed;
                }

                // Запрещаем редактировать стоимость покупки всем кроме заместителя директора
                if (user.AcessLevel < 4)
                    tboxPurchasedCosts.IsReadOnly = true;

                

                // Стажёрам не даем редактировать даже серийный номер
                if (user.AcessLevel < 1)
                {
                    btn_saveChanges.Visibility = Visibility.Collapsed;
                    tboxSerialNo.IsReadOnly = true;
                }
                    

                // Запрещаем редактировать серийный номер всем кроме зам директора для проданного товара
                if (dbItem.DateOfSale != null)
                {
                    if (user.AcessLevel < 4)
                        tboxSerialNo.IsReadOnly = true;
                }

                // в ремонте
                if (user.AcessLevel < 4)
                    chboxUnderRepair.IsEnabled = false;

                tboxDateOfSale.IsReadOnly = true;
                tboxDateOfPurchase.IsReadOnly = true;

                if (dbItem.UnderRepair)
                    btn_repair.IsEnabled = false;
                
                if (!dbItem.UnderRepair)
                    btn_returnFromRepair.IsEnabled = false;

                // Запрет на редактирование мастера по ремонту. Сдедано больше для отсутсивия путаницы у пользователя.
                tbox_repairMaster.IsReadOnly = true;
            }
        }

        private void SetSettingsForSoldItem()
        {
            using (ComShopContext context = new ComShopContext())
            {
                var item = context.Items.Find(ItemID);

                if (item.DateOfSale != null)
                {
                    btn_repair.Visibility = Visibility.Collapsed;
                    btn_returnFromRepair.Visibility = Visibility.Collapsed;
                    btn_sellItem.Visibility = Visibility.Collapsed;
                }
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
            ListOfRepairMaster listOfRepairMaster = new ListOfRepairMaster(UserID, ItemID);
            listOfRepairMaster.Show();
            this.Close();
        }

        // Вернуть с ремонта
        private void returnFromRepair(object sender, RoutedEventArgs e)
        {
            ReturnFromRepair repair = new ReturnFromRepair(UserID, ItemID);
            repair.Show();
            this.Close();
        }

        private bool CheckDataBeforeSave(){
            if ( String.IsNullOrWhiteSpace(tboxDesciption.Text) ) return false;
            if (String.IsNullOrWhiteSpace(tboxSerialNo.Text)) return false;
            decimal priceIn;
            decimal priceOut;
            if ( !Decimal.TryParse(tboxPurchasedCosts.Text, out priceIn) ) return false;
            if ( !Decimal.TryParse(tboxPrice.Text, out priceOut) ) return false;

            return true;
        }

        // Сохранить изменения
        private void saveItemChanges(object sender, RoutedEventArgs e)
        {
            if ( !CheckDataBeforeSave())
            {
                MessageBox.Show("Ошибка заполнения данных\nПроверьте правильность");
                return;
            }

            using (ComShopContext context = new ComShopContext())
            {
                var item = context.Items.Find(ItemID);
                item.Description = tboxDesciption.Text;
                item.SerialNumber = tboxSerialNo.Text;
                decimal priceIn;
                decimal priceOut;
                decimal repairCost;
                Decimal.TryParse(tboxPurchasedCosts.Text, out priceIn);
                Decimal.TryParse(tboxPrice.Text, out priceOut);
                Decimal.TryParse(tboxRepairCosts.Text, out repairCost);
                item.PurchaseAmount = priceIn;
                item.Price = priceOut;
                item.RepairCosts = repairCost;

                context.SaveChanges();
            }

        }
    }
}
