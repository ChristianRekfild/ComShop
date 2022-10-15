using ComShop.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
    /// Логика взаимодействия для ListOfRepairMaster.xaml
    /// </summary>
    public partial class ListOfRepairMaster : Window
    {
        private int UserID;
        private int ItemID;
        public ListOfRepairMaster(int staffID, int itemID)
        {
            UserID = staffID;
            ItemID = itemID;
            InitializeComponent();
            SetSettingsByAcessLevel(UserID);
            GetRepairMasterList();
        }

        private void SetSettingsByAcessLevel(int userID)
        {
            using (ComShopContext context = new ComShopContext())
            {
                var user = context.staff.Find(UserID);

                // скрываем кнопку добавления клиента
                if (user.AcessLevel < 4)
                {
                    btn_addRepairMaster.Visibility = Visibility.Collapsed;
                }
            }

        }


        // Главное меню
        private void goToMainMenu(object sender, RoutedEventArgs e)
        {
            AfterLogin after = new AfterLogin(UserID);
            after.Show();
            this.Close();
        }

        private void GetRepairMasterList()
        {
            using (ComShopContext context = new ComShopContext())
            {
                this.DataContext = context.RepairMasters.ToList();
            }
        }

        // Выбрать мастера
        private void selectRepairMasterCard(object sender, RoutedEventArgs e)
        {
            RepairMaster? master = repairMasterList.SelectedItem as RepairMaster;
            if (master == null)
            {
                MessageBox.Show("Не выбран мастер по ремонту");
                return;
            }

            using (ComShopContext context = new ComShopContext())
            {
                var item = context.Items.Find(ItemID);
                item.UnderRepair = true;
                item.RepairMasterNo = master.IdRepairMatser;

                context.SaveChanges();
            }

            MessageBox.Show("Товар успешно отправлен на ремонт");

            AfterLogin after = new AfterLogin(UserID);
            after.Show();
            this.Close();

        }

        // Открыть карточку мастера
        private void openRepairMasterCars(object sender, RoutedEventArgs e)
        {
            RepairMaster? master = repairMasterList.SelectedItem as RepairMaster;
            if (master == null)
            {
                MessageBox.Show("Не выбран мастер по ремонту");
                return;
            }

            RepairMasterCard masterCard = new RepairMasterCard(UserID, master.IdRepairMatser);
            masterCard.Show();
            this.Close();
        }

        // Добавить мастера по ремонту
        private void addRepairMaster(object sender, RoutedEventArgs e)
        {
            RepairMasterCard masterCard = new RepairMasterCard(UserID, 0);
            masterCard.Show();
            this.Close();
        }
    }
}
