using System;
using System.Collections.Generic;

namespace ComShop.Model
{
    /// <summary>
    /// Категория товаров
    /// </summary>
    public partial class Category
    {
        public Category()
        {
            Items = new HashSet<Item>();
        }

        public int IdCategory { get; set; }
        /// <summary>
        /// Название категории
        /// </summary>
        public string Name { get; set; } = null!;

        public virtual ICollection<Item> Items { get; set; }
    }
}
