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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ComShop.Libs;


namespace ComShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = tboxLogin.Text;
            string password = passBoxPassword.Password.ToString();

            var list = new List<object>();

            string connString = "Host=localhost;Username=Chris;Password=Chris2113;Database=ComShop";
            NpgsqlConnection connect = new NpgsqlConnection(connString);
            connect.Open();

            CryptoHelper cryptHelp = new Libs.CryptoHelper();
            string hashedPass = cryptHelp.ComputeSha256Hash(password);
            string query = $"SELECT login, password, id_staff FROM \"Staff\" WHERE login = '{login}' AND password = '{hashedPass}'";              
            NpgsqlCommand cmd = new NpgsqlCommand(@query, connect);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            
            // запрещаем попытки схода при ошибке авторизации
            try { 
                    // Если криптохэш совпадает с БД
                if (reader[0].ToString() == login)
                {

                    this.Hide();
                    string strUserId = reader[2].ToString();
                    int intUserId = Convert.ToInt32(reader[2].ToString());
                    AfterLogin after = new AfterLogin(intUserId);
                    after.Show();               

                }
                this.Close();
            } catch
            {
                MessageBox.Show("Введён неверный пароль.\nВ доступе отказано");
            }

        }
    }
}
