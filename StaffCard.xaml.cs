using ComShop.Model;
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
        int EmployeeIdToOpen;
        public StaffCard(int staffID, int staffIDToOpen)
        {
            UserID = staffID;
            EmployeeIdToOpen = staffIDToOpen;
            InitializeComponent();
            GetStaffInfo(UserID);
            SetSettingByAcessLevel(UserID);
        }

        private void GetStaffInfo(int UserID)
        {
            using (ComShopContext comShop = new ComShopContext())
            {
                // пользователь, который открыл карточку сотрудника
                var user = comShop.staff.Find(UserID);
                // Сотрудник, чью карточку открыли
                var staff = comShop.staff.Find(EmployeeIdToOpen);
                
                tbox_staffID.Text = staff.IdStaff.ToString();
                tbox_name.Text = staff.Name;
                tbox_familyName.Text = staff.FamilyName;
                tbox_patronymic.Text = staff.Patronymic;
                tbox_dateOfBirth.Text = staff.DateOfBirth.ToString();
                tbox_passport.Text = staff.Passport;
                tbox_login.Text = staff.Login;
                tbox_acessLevel.Text = staff.AcessLevel.ToString();
            }
        }

        private void SetSettingByAcessLevel(int UserID)
        {
            using (ComShopContext comShop = new ComShopContext())
            {
                // пользователь, который открыл карточку сотрудника
                var user = comShop.staff.Find(UserID);

                // Если сотрудник меньше управляющего магазином
                if (user.AcessLevel < 4)
                {
                    // Скрываем поля, которые не каждый должен видеть. Пусть даже не знаеь о них!
                    tbox_dateOfBirth.Visibility = Visibility.Collapsed;
                    tbox_passport.Visibility = Visibility.Collapsed;
                    lab_dateOfBirth.Visibility = Visibility.Collapsed;
                    lab_passport.Visibility = Visibility.Collapsed;
                    tbox_login.Visibility = Visibility.Collapsed;
                    lab_login.Visibility = Visibility.Hidden;
                    //btn_save.Visibility = Visibility.Hidden;
                }
            }

        }

        // Сохранить
        // Теперь у меня пропадал cproj файл, и я громко ругался, опять восстанавливая проект...
        private void btn_ClickToSave(object sender, RoutedEventArgs e)
        {
            using (ComShopContext comShop = new ComShopContext())
            {
                // Сотрудник, чью карточку открыли
                var staff = comShop.staff.Find(EmployeeIdToOpen);

                staff.Name = tbox_name.Text;
                staff.FamilyName = tbox_familyName.Text;
                staff.Patronymic = tbox_familyName.Text;
                staff.DateOfBirth = DateOnly.Parse(tbox_dateOfBirth.Text);
                staff.Passport = tbox_passport.Text;
                staff.Login = tbox_login.Text;
                staff.AcessLevel = int.Parse(tbox_acessLevel.Text);

                comShop.SaveChanges();
            }            

        }

    }
}
