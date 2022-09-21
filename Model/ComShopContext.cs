using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ComShop.Model
{
    public partial class ComShopContext : DbContext
    {
        public ComShopContext()
        {
        }

        public ComShopContext(DbContextOptions<ComShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<RepairMaster> RepairMasters { get; set; } = null!;
        public virtual DbSet<staff> staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ComShop;Username=postgres;Password=Chris2113");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("Category_pkey");

                entity.ToTable("Category");

                entity.HasComment("Категория товаров");

                entity.Property(e => e.IdCategory)
                    .HasColumnName("id_category")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name")
                    .IsFixedLength()
                    .HasComment("Название категории");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient)
                    .HasName("Clients_pkey");

                entity.HasComment("Клиенты");

                entity.Property(e => e.IdClient)
                    .HasColumnName("id_client")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("dateOfBirth")
                    .HasComment("Дата рождения");

                entity.Property(e => e.FamilyName)
                    .HasMaxLength(100)
                    .HasColumnName("familyName")
                    .IsFixedLength()
                    .HasComment("Фамилия");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .IsFixedLength()
                    .HasComment("Имя");

                entity.Property(e => e.Passport)
                    .HasMaxLength(30)
                    .HasColumnName("passport")
                    .IsFixedLength()
                    .HasComment("Пасспорт");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(100)
                    .HasColumnName("patronymic")
                    .IsFixedLength()
                    .HasComment("Отчество");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.IdItem)
                    .HasName("Item_pkey");

                entity.ToTable("Item");

                entity.HasComment("Товары");

                entity.HasIndex(e => e.ClientNo, "fki_fk_client");

                entity.HasIndex(e => e.RepairMasterNo, "fki_fk_repairMaster");

                entity.Property(e => e.IdItem)
                    .HasColumnName("id_item")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CaregoryNo).HasColumnName("caregoryNo");

                entity.Property(e => e.ClientNo).HasColumnName("clientNo");

                entity.Property(e => e.DateOfPurchase)
                    .HasColumnName("dateOfPurchase")
                    .HasComment("Дата покупки");

                entity.Property(e => e.DateOfSale)
                    .HasColumnName("dateOfSale")
                    .HasComment("Дата продажи");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description")
                    .HasComment("Описание");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price")
                    .HasComment("Стоимость на витрине");

                entity.Property(e => e.PurchaseAmount)
                    .HasColumnType("money")
                    .HasColumnName("purchaseAmount")
                    .HasComment("Сумма покупки");

                entity.Property(e => e.RepairCosts)
                    .HasColumnType("money")
                    .HasColumnName("repairCosts")
                    .HasComment("Стоимость, затраченная на ремонт");

                entity.Property(e => e.RepairMasterNo).HasColumnName("repairMasterNo");

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(50)
                    .HasColumnName("serial_number")
                    .HasComment("Серийный номер");

                entity.Property(e => e.UnderRepair)
                    .HasColumnName("underRepair")
                    .HasComment("В ремонте?");

                entity.HasOne(d => d.CaregoryNoNavigation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.CaregoryNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("category");

                entity.HasOne(d => d.ClientNoNavigation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ClientNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_client");

                entity.HasOne(d => d.RepairMasterNoNavigation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.RepairMasterNo)
                    .HasConstraintName("fk_repairMaster");
            });

            modelBuilder.Entity<RepairMaster>(entity =>
            {
                entity.HasKey(e => e.IdRepairMatser)
                    .HasName("RepairMaster_pkey");

                entity.ToTable("RepairMaster");

                entity.HasComment("Мастера по ремонту");

                entity.Property(e => e.IdRepairMatser)
                    .HasColumnName("id_repairMatser")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("dateOfBirth")
                    .HasComment("Дата рождения");

                entity.Property(e => e.FamilyName)
                    .HasMaxLength(50)
                    .HasColumnName("familyName")
                    .IsFixedLength()
                    .HasComment("Фамилия");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .IsFixedLength()
                    .HasComment("Имя");

                entity.Property(e => e.Passport)
                    .HasMaxLength(10)
                    .HasColumnName("passport")
                    .IsFixedLength()
                    .HasComment("Серия и номер паспорта без пробелов");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(50)
                    .HasColumnName("patronymic")
                    .IsFixedLength()
                    .HasComment("Отчество");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.HasKey(e => e.IdStaff)
                    .HasName("Staff_pkey");

                entity.ToTable("Staff");

                entity.HasComment("Сотрудники");

                entity.Property(e => e.IdStaff)
                    .HasColumnName("id_staff")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AcessLevel)
                    .HasColumnName("acessLevel")
                    .HasComment("Уровень доступа");

                //entity.Property(e => e.BytePassword)
                //    .HasColumnName("bytePassword")
                //    .HasComment("Массив байт пароля");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("dateOfBirth")
                    .HasComment("Дата рождения");

                entity.Property(e => e.FamilyName)
                    .HasMaxLength(50)
                    .HasColumnName("familyName")
                    .IsFixedLength()
                    .HasComment("Фамилия");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .HasColumnName("login")
                    .HasComment("логин");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .IsFixedLength()
                    .HasComment("Имя");

                entity.Property(e => e.Passport)
                    .HasMaxLength(10)
                    .HasColumnName("passport")
                    .IsFixedLength()
                    .HasComment("Серия и номер паспорта");

                entity.Property(e => e.Password)
                    .HasMaxLength(120)
                    .HasColumnName("password")
                    .HasComment("Пароль");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(50)
                    .HasColumnName("patronymic")
                    .IsFixedLength()
                    .HasComment("Отчество");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
