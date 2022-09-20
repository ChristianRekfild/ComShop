using System;
using System.Collections.Generic;

namespace ComShop.Model
{
    /// <summary>
    /// Мастера по ремонту
    /// </summary>
    public partial class RepairMaster
    {
        public RepairMaster()
        {
            Items = new HashSet<Item>();
        }

        public int IdRepairMatser { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string FamilyName { get; set; } = null!;
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; } = null!;
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateOnly DateOfBirth { get; set; }
        /// <summary>
        /// Серия и номер паспорта без пробелов
        /// </summary>
        public string Passport { get; set; } = null!;

        public virtual ICollection<Item> Items { get; set; }
    }
}
