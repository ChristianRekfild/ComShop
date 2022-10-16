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
    /// Логика взаимодействия для Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        private int UserID;
        public Report(int userID)
        {
            UserID = userID;
            InitializeComponent();
            
        }

        private bool CheckData()
        {


            return true;
        }

        // Запуск отчёта
        private void RunReport(object sender, RoutedEventArgs e)
        {
            if (!CheckData())
            {
                MessageBox.Show("Неверно укказаны параметры");
                return;
            }


        }
    }
}
