using Npgsql;
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
    /// Логика взаимодействия для AfterLogin.xaml
    /// </summary>
    public partial class AfterLogin : Window
    {
        int UserID;
        public AfterLogin(int staffId)
        {
            InitializeComponent();
            UserID = staffId;
        }

        // Найти товар
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        // Скупка товара
        private void buyItem(object sender, RoutedEventArgs e)
        {
            BuyItem buyItem = new BuyItem(UserID, 0, 0);
            buyItem.Show();
            this.Close();
        }

        // Добавление клиента
        private void btn_addClient(object sender, RoutedEventArgs e)
        {
            //ListOfClients listOfClients = new ListOfClients(UserID);
            //listOfClients.Show();
            //this.Close();
        }

        // Приём из ремонта
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
        // Добавление нового мастера
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }
        // Сотрудники - изменение уровня доступа
        private void btn_OpenStaffList(object sender, RoutedEventArgs e)
        {
            StaffList staffList = new StaffList(UserID);
            staffList.Show();
            this.Close();
        }
        // Найти клиента
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {

        }
        // Открыть список товаров
        private void Button_OpenListItems(object sender, RoutedEventArgs e)
        {
            ListOfItems listOfItems = new ListOfItems(UserID, tbox_description.Text, tbox_serial.Text, chbox_inStock.IsChecked);
            listOfItems.Show();            
            this.Close();
        }

        private void btn_addRepairMaster(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
