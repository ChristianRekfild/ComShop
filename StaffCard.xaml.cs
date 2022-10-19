using ComShop.Libs;
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
            if (EmployeeIdToOpen != 0)
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
                    //tbox_dateOfBirth.Text = staff.DateOfBirth.ToString("dd-MM-yyyy");
                    DateTime date = DateTime.Parse(staff.DateOfBirth.ToString());
                    cld_dateOfBirth.SelectionMode = CalendarSelectionMode.SingleDate;
                    cld_dateOfBirth.SelectedDate = date;
                    cld_dateOfBirth.DisplayDate = date;

                    tbox_passport.Text = staff.Passport;
                    tbox_login.Text = staff.Login;
                    tbox_acessLevel.Text = staff.AcessLevel.ToString();
                }
            }

        }

        private bool CheckDataBeforeSave()
        {
            if (String.IsNullOrWhiteSpace(tbox_login.Text))
                return false;
            if (String.IsNullOrWhiteSpace(tbox_acessLevel.Text))
                return false;
            if (String.IsNullOrWhiteSpace(tbox_familyName.Text))
                return false;
            if (String.IsNullOrWhiteSpace(tbox_name.Text))
                return false;
            if (String.IsNullOrWhiteSpace(tbox_patronymic.Text))
                return false;
            if (String.IsNullOrWhiteSpace(tbox_passport.Text))
                return false;
            if (String.IsNullOrWhiteSpace(tbox_acessLevel.Text))
                return false;

            return true;
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
                    //tbox_dateOfBirth.Visibility = Visibility.Collapsed;
                    cld_dateOfBirth.Visibility = Visibility.Collapsed;
                    tbox_passport.Visibility = Visibility.Collapsed;
                    lab_dateOfBirth.Visibility = Visibility.Collapsed;
                    lab_passport.Visibility = Visibility.Collapsed;
                    tbox_login.Visibility = Visibility.Collapsed;
                    lab_login.Visibility = Visibility.Hidden;
                    btn_save.Visibility = Visibility.Hidden;
                    lab_password.Visibility = Visibility.Hidden;

                    // На остальные поля нужно ставить органичение - только для чтения. Иначе наворотят дел
                    tbox_name.IsReadOnly = true;
                    tbox_familyName.IsReadOnly = true;
                    tbox_patronymic.IsReadOnly = true;
                    tbox_acessLevel.IsReadOnly = true;
                }

            }

        }

        // Сохранить
        // Теперь у меня пропадал cproj файл, и я громко ругался, опять восстанавливая проект...
        private void btn_ClickToSave(object sender, RoutedEventArgs e)
        {
            if (!CheckDataBeforeSave())
            {
                MessageBox.Show("Проверьте правильность заполнения данных!");
                return;
            }

            // Редактирование старого
            if (EmployeeIdToOpen != 0) {
                using (ComShopContext comShop = new ComShopContext())
                {
                    // Сотрудник, чью карточку открыли
                    var staff = comShop.staff.Find(EmployeeIdToOpen);

                    staff.Name = tbox_name.Text;
                    staff.FamilyName = tbox_familyName.Text;
                    staff.Patronymic = tbox_familyName.Text;
                    DateOnly dateOnly = new DateOnly(
                            cld_dateOfBirth.SelectedDate.Value.Year,
                            cld_dateOfBirth.SelectedDate.Value.Month,
                            cld_dateOfBirth.SelectedDate.Value.Day
                            );
                    staff.DateOfBirth = dateOnly;
                    staff.Passport = tbox_passport.Text;
                    staff.Login = tbox_login.Text;
                    staff.AcessLevel = int.Parse(tbox_acessLevel.Text);

                    if (!String.IsNullOrWhiteSpace(tbox_password.Text))
                    {
                        CryptoHelper helper = new CryptoHelper();
                        string crHash = helper.ComputeSha256Hash(tbox_password.Text);
                        staff.Password = crHash;
                    }

                    comShop.SaveChanges();

                    MessageBox.Show("Сотрудник успешно изменён");

                    AfterLogin after = new AfterLogin(UserID);
                    after.Show();
                    this.Close();
                }
            }

            // Добавление нового
            if (EmployeeIdToOpen == 0)
            {
                if (cld_dateOfBirth.SelectedDate == null)
                {
                    MessageBox.Show("Не выбрана дата");
                    return;
                }

                using (ComShopContext context = new ComShopContext())
                {
                    staff newStaff = new staff();
                    newStaff.Name = tbox_name.Text;
                    newStaff.FamilyName = tbox_familyName.Text;
                    newStaff.Patronymic = tbox_familyName.Text;
                    DateOnly dateOnly = new DateOnly(
                            cld_dateOfBirth.SelectedDate.Value.Year,
                            cld_dateOfBirth.SelectedDate.Value.Month,
                            cld_dateOfBirth.SelectedDate.Value.Day
                            );
                    newStaff.DateOfBirth = dateOnly;
                    newStaff.Passport = tbox_passport.Text;
                    newStaff.Login = tbox_login.Text;
                    newStaff.AcessLevel = int.Parse(tbox_acessLevel.Text);

                    CryptoHelper helper = new CryptoHelper();
                    newStaff.Password = helper.ComputeSha256Hash(tbox_password.Text);


                    context.staff.Add(newStaff);

                    context.SaveChanges();

                    MessageBox.Show("Сотрудник успешно добавлен");

                    AfterLogin after = new AfterLogin(UserID);
                    after.Show();
                    this.Close();
                }
            }
        }

        private void btn_mainMenu(object sender, RoutedEventArgs e)
        {
            AfterLogin afterLogin = new AfterLogin(UserID);
            afterLogin.Show();
            this.Close();
        }
    }
}
