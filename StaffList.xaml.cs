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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ComShop.Model;

namespace ComShop
{
    /// <summary>
    /// Логика взаимодействия для StaffList.xaml
    /// </summary>
    public partial class StaffList : Window
    {
        int UserID;
        public StaffList(int staffID)
        {
            UserID = staffID;
            GetStaffList();
            InitializeComponent();
        }

        public void GetStaffList()
        {
            using (ComShopContext context = new ComShopContext())
            {
                this.DataContext = context.staff.ToList();
            }
        }

        private void btn_openStaffCard(object sender, RoutedEventArgs e)
        {

            staff? staff = staffList.SelectedItem as staff;
            if (staff != null)
            {
                StaffCard staffCard = new StaffCard(UserID, staff.IdStaff);
                staffCard.Show();
                this.Close();
            }

        }

        // В главное меню
        private void btn_MainMenu(object sender, RoutedEventArgs e)
        {
            AfterLogin afterLogin = new AfterLogin(UserID);
            afterLogin.Show();
            this.Close();
        }
    }
   
}
