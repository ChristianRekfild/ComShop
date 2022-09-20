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

            //string connString = "Host=localhost;Username=Chris;Password=Chris2113;Database=ComShop";
            //NpgsqlConnection connect = new NpgsqlConnection(connString);
            ////try
            ////{
            ////Открываем соединение.
            //connect.Open();

            //string query = $"SELECT login, password, id_staff FROM \"Staff\" WHERE login = '{login}' AND password = '{hashedPass}'";
            //NpgsqlCommand cmd = new NpgsqlCommand(@query, connect);
            //NpgsqlDataReader reader = cmd.ExecuteReader();
            //reader.Read();
        }
        // Найти товар
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        // Скупка товара
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
        // Добавление клиента
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

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
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }
        // Найти клиента
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {

        }
        // Открыть список товаров
        private void Button_OpenListItems(object sender, RoutedEventArgs e)
        {
            ListOfItems listOfItems = new ListOfItems(UserID);
            listOfItems.Show();            
            this.Close();
        }
    }
}
