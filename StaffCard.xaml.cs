using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
    /// Логика взаимодействия для StaffCard.xaml
    /// </summary>
    public partial class StaffCard : Window
    {
        int UserID;
        public StaffCard(int staffID)
        {
            UserID = staffID;
            InitializeComponent();
        }

        private void GetStaffInfo()
        {

        }

        private void SetSettingByAcessLevel()
        {

        }
    }
}
