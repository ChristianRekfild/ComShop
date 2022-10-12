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
    /// Логика взаимодействия для ListOfRepairMaster.xaml
    /// </summary>
    public partial class ListOfRepairMaster : Window
    {
        private int UserID;
        public ListOfRepairMaster(int staffID)
        {
            InitializeComponent();
            SetSettingsByAcessLevel(UserID);
        }

        private void SetSettingsByAcessLevel(int userID)
        {
            using (ComShopContext context = new ComShopContext())
            {
                var user = context.staff.Find(UserID);

                // скрываем кнопку добавления клиента
                if (user.AcessLevel < 4 )
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
                this.DataContext = context.Categories.ToList();
            }
        }
    }
}
