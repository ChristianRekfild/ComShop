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
    /// Логика взаимодействия для RepairMasterCard.xaml
    /// </summary>
    public partial class RepairMasterCard : Window
    {
        private int UserID;
        private int RepairMasterID;
        public RepairMasterCard(int staffID, int repairMasterID)
        {
            UserID = staffID;
            RepairMasterID = repairMasterID;
            InitializeComponent();
            SetSettings();
        }

        // Тоько для чтения
        private void SetSettings()
        {
            tbox_id.IsReadOnly = true;
        }

        // Сохранить
        private void btn_ClickToSave(object sender, RoutedEventArgs e)
        {
            using (ComShopContext context = new ComShopContext())
            {
                // Редактируем старого
                if (RepairMasterID != 0) { 
                    var master = context.RepairMasters.Find(RepairMasterID);                
                    master.Name = tbox_name.Text;
                    master.FamilyName = tbox_familyName.Text;
                    master.Patronymic = tbox_patronymic.Text;
                    master.Passport = tbox_passport.Text;
                    master.DateOfBirth = new DateOnly(cld_dateOfDirth.SelectedDate.Value.Year,
                                                    cld_dateOfDirth.SelectedDate.Value.Month,
                                                    cld_dateOfDirth.SelectedDate.Value.Day);

                    context.SaveChanges();
                }
                //Добавляем нового
                if (RepairMasterID == 0)
                {
                    var master = new RepairMaster();
                    master.Name = tbox_name.Text;
                    master.FamilyName = tbox_familyName.Text;
                    master.Patronymic = tbox_patronymic.Text;
                    master.Passport = tbox_passport.Text;
                    master.DateOfBirth = new DateOnly(cld_dateOfDirth.SelectedDate.Value.Year,
                                                    cld_dateOfDirth.SelectedDate.Value.Month,
                                                    cld_dateOfDirth.SelectedDate.Value.Day);

                    context.Add(master);
                    context.SaveChanges();
                }
            }
        }

        private bool checkDataBeforeSave()
        {
            if (String.IsNullOrWhiteSpace(tbox_name.Text)) return false;
            if (String.IsNullOrWhiteSpace(tbox_familyName.Text)) return false;
            if (String.IsNullOrWhiteSpace(tbox_patronymic.Text)) return false;
            if (String.IsNullOrWhiteSpace(tbox_passport.Text)) return false;
            if (cld_dateOfDirth.SelectedDate == null) return false;


            return true;
        }

        // Главное меню
        private void btn_mainMenu(object sender, RoutedEventArgs e)
        {
            AfterLogin after = new AfterLogin(UserID);
            after.Show();
            this.Close();
        }
    }
}
