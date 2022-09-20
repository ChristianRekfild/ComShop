using System;
using System.Collections.Generic;

namespace ComShop.Model
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    public partial class staff
    {
        public int IdStaff { get; set; }
        /// <summary>
        /// логин
        /// </summary>
        public string Login { get; set; } = null!;
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = null!;
        /// <summary>
        /// Уровень доступа
        /// </summary>
        public int AcessLevel { get; set; }
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
        /// Серия и номер паспорта
        /// </summary>
        public string Passport { get; set; } = null!;
        /// <summary>
        /// Массив байт пароля
        /// </summary>
        public byte[]? BytePassword { get; set; }
    }
}
