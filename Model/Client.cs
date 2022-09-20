using System;
using System.Collections.Generic;

namespace ComShop.Model
{
    /// <summary>
    /// Клиенты
    /// </summary>
    public partial class Client
    {
        public Client()
        {
            Items = new HashSet<Item>();
        }

        public int IdClient { get; set; }
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
        public string? Patronymic { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateOnly DateOfBirth { get; set; }
        /// <summary>
        /// Пасспорт
        /// </summary>
        public string Passport { get; set; } = null!;

        public virtual ICollection<Item> Items { get; set; }
    }
}
