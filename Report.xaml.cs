using ComShop.Model;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
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
            SetStartSetings();
        }

        // Уже не надо, пока костыль для ctrl + c, ctrl + v
        private void SetStartSetings()
        {
            lab_turnover.Visibility = Visibility.Collapsed;
            lab_clearProfit.Visibility = Visibility.Collapsed;
            lab_repairCosts.Visibility = Visibility.Collapsed;
            lab_purchItemCount.Visibility = Visibility.Collapsed;
            lab_soldItemCount.Visibility = Visibility.Collapsed;
            lab_potentialTurnover.Visibility = Visibility.Collapsed;
            lab_costsOnItemInRepair.Visibility = Visibility.Collapsed;
            lab_itemOnRepairCount.Visibility = Visibility.Collapsed;


            tbox_turnover.Visibility = Visibility.Collapsed;
            tbox_clearProfit.Visibility = Visibility.Collapsed;
            tbox_repairCosts.Visibility = Visibility.Collapsed;
            tbox_purchItemCount.Visibility = Visibility.Collapsed;
            tbox_soldItemCount.Visibility = Visibility.Collapsed;
            tbox_potentialTurnover.Visibility = Visibility.Collapsed;
            tbox_costsOnItemInRepair.Visibility = Visibility.Collapsed;
            tbox_itemOnRepairCount.Visibility = Visibility.Collapsed;
        }

        // Проверка на заполнение
        private bool CheckData()
        {
            if (cld_start.SelectedDate == null)
                return false;
            if (cld_end.SelectedDate == null)
                return false;

            return true;
        }

        // Запуск отчёта
        private void RunReport(object sender, RoutedEventArgs e)
        {
            if (!CheckData())
            {
                MessageBox.Show("Неверно указаны параметры");
                return;
            }

            DateOnly dateStart = new DateOnly(
                            cld_start.SelectedDate.Value.Year,
                            cld_start.SelectedDate.Value.Month,
                            cld_start.SelectedDate.Value.Day
                            );
            DateOnly dateEnd = new DateOnly(
                            cld_end.SelectedDate.Value.Year,
                            cld_end.SelectedDate.Value.Month,
                            cld_end.SelectedDate.Value.Day
                            );
            
            using (ComShopContext context = new ComShopContext())
            {
                List<Item> items = context.Items.ToList();

                List<Item> purchItems = items.Where(x => x.DateOfPurchase >= dateStart && x.DateOfPurchase <= dateEnd).ToList();
                List<Item> soldItems = items.Where(x => x.DateOfSale >= dateStart && x.DateOfSale <= dateEnd).ToList();
                List<Item> itemsInStock = items.Where(x => x.DateOfSale == null).ToList();
                List<Item> itemsOnRepair = items.Where(x => x.DateOfSale == null).Where(x => x.UnderRepair == true).ToList();


                decimal expenses = purchItems.Sum(x => x.PurchaseAmount);
                decimal profit = soldItems.Sum(x => x.Price);                               // Оборот
                decimal? repairExpenses = purchItems.Where(x => x.RepairCosts > 0).Sum(x => x.RepairCosts);
                
                if (repairExpenses == null)
                    repairExpenses = 0;

                decimal repairCost = (decimal) repairExpenses;                              // Затраты на ремонт

                
                decimal clearprofit = profit - expenses - repairCost;                       // Чистая прибыль
                int countOfPurchasedItems = purchItems.Count();                              // Кол-во купленных предметов
                int countOfSoldItems = soldItems.Count();                                   // Кол-во проданных предметов        
                decimal potentialTurnover = itemsInStock.Sum(x => x.Price);                 // Потенциальный оборот
                decimal expensesOnItemOnRepair = itemsOnRepair.Sum(x => x.PurchaseAmount);  // Сумма, затраченная на покупку товарав, которые в ремонте


                lab_turnover.Visibility = Visibility.Visible;
                lab_clearProfit.Visibility = Visibility.Visible;
                lab_repairCosts.Visibility = Visibility.Visible;
                lab_purchItemCount.Visibility = Visibility.Visible;
                lab_soldItemCount.Visibility = Visibility.Visible;
                lab_potentialTurnover.Visibility = Visibility.Visible;
                lab_costsOnItemInRepair.Visibility = Visibility.Visible;
                lab_itemOnRepairCount.Visibility = Visibility.Visible;
                lab_itemOnRepairCount.Visibility = Visibility.Visible;



                tbox_turnover.Text = profit.ToString();
                tbox_clearProfit.Text = clearprofit.ToString();
                tbox_repairCosts.Text = repairCost.ToString();
                tbox_purchItemCount.Text = countOfPurchasedItems.ToString();
                tbox_soldItemCount.Text = countOfSoldItems.ToString();
                tbox_potentialTurnover.Text = potentialTurnover.ToString();
                tbox_costsOnItemInRepair.Text = expensesOnItemOnRepair.ToString();
                tbox_itemOnRepairCount.Text = itemsOnRepair.Count().ToString();


                tbox_turnover.Visibility = Visibility.Visible;
                tbox_clearProfit.Visibility = Visibility.Visible;
                tbox_repairCosts.Visibility = Visibility.Visible;
                tbox_purchItemCount.Visibility = Visibility.Visible;
                tbox_soldItemCount.Visibility = Visibility.Visible;
                tbox_potentialTurnover.Visibility = Visibility.Visible;
                tbox_costsOnItemInRepair.Visibility = Visibility.Visible;
                tbox_itemOnRepairCount.Visibility = Visibility.Visible;


            }


        }

    }
}
