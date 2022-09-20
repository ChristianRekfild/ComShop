using System;
using System.Collections.Generic;

namespace ComShop.Model
{
    /// <summary>
    /// Товары
    /// </summary>
    public partial class Item
    {
        public int IdItem { get; set; }
        /// <summary>
        /// Серийный номер
        /// </summary>
        public string? SerialNumber { get; set; }
        /// <summary>
        /// Дата покупки
        /// </summary>
        public DateOnly DateOfPurchase { get; set; }
        /// <summary>
        /// Сумма покупки
        /// </summary>
        public decimal PurchaseAmount { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// В ремонте?
        /// </summary>
        public bool UnderRepair { get; set; }
        /// <summary>
        /// Стоимость, затраченная на ремонт
        /// </summary>
        public decimal? RepairCosts { get; set; }
        /// <summary>
        /// Стоимость на витрине
        /// </summary>
        public decimal Price { get; set; }
        public int CaregoryNo { get; set; }
        public int? RepairMasterNo { get; set; }
        public int ClientNo { get; set; }
        /// <summary>
        /// Дата продажи
        /// </summary>
        public DateOnly? DateOfSale { get; set; }

        public virtual Category CaregoryNoNavigation { get; set; } = null!;
        public virtual Client ClientNoNavigation { get; set; } = null!;
        public virtual RepairMaster? RepairMasterNoNavigation { get; set; }
    }
}
