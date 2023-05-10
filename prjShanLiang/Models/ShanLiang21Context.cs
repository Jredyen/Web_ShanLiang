using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace prjShanLiang.Models;

public partial class ShanLiang21Context : DbContext
{
    public ShanLiang21Context()
    {
    }

    public ShanLiang21Context(DbContextOptions<ShanLiang21Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountStatus> AccountStatuses { get; set; }

    public virtual DbSet<Action> Actions { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<EatTypeMethod> EatTypeMethods { get; set; }

    public virtual DbSet<Identification> Identifications { get; set; }

    public virtual DbSet<MealMenu> MealMenus { get; set; }

    public virtual DbSet<MealOrder> MealOrders { get; set; }

    public virtual DbSet<MealOrderDetail> MealOrderDetails { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MemberAction> MemberActions { get; set; }

    public virtual DbSet<MemberCoupon> MemberCoupons { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<RestaurantType> RestaurantTypes { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<StoreAdImage> StoreAdImages { get; set; }

    public virtual DbSet<StoreDecorationImage> StoreDecorationImages { get; set; }

    public virtual DbSet<StoreEvaluate> StoreEvaluates { get; set; }

    public virtual DbSet<StoreMealImage> StoreMealImages { get; set; }

    public virtual DbSet<StoreReserved> StoreReserveds { get; set; }

    public virtual DbSet<StoreType> StoreTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=tcp:karamucho.asuscomm.com,1433;Initial Catalog=ShanLiang2.1;User ID=ispan_304_a;Password=aaaa1111bbbb2222;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("A_Member");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account", "dbo");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.AccountName).HasMaxLength(20);
            entity.Property(e => e.AccountPassword).HasMaxLength(50);

            entity.HasOne(d => d.IdentificationNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.Identification)
                .HasConstraintName("FK_Account_Identification1");
        });

        modelBuilder.Entity<AccountStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK_AccountStatus");

            entity.ToTable("Account_Status", "dbo");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StatusName).HasMaxLength(10);
        });

        modelBuilder.Entity<Action>(entity =>
        {
            entity.ToTable("Action", "dbo");

            entity.Property(e => e.ActionId).HasColumnName("ActionID");
            entity.Property(e => e.ActionName).HasMaxLength(8);
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Admin", "dbo");

            entity.Property(e => e.AdminId)
                .ValueGeneratedOnAdd()
                .HasColumnName("AdminID");
            entity.Property(e => e.AdminName).HasMaxLength(50);
            entity.Property(e => e.Passwoed).HasMaxLength(50);
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.ToTable("Blog");

            entity.Property(e => e.BlogHeader).HasMaxLength(50);
            entity.Property(e => e.BlogPic).HasMaxLength(50);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City", "dbo");

            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CityName).HasMaxLength(10);
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.ToTable("Coupon", "dbo");

            entity.Property(e => e.CouponId).HasColumnName("CouponID");
            entity.Property(e => e.CouponDeadline).HasColumnType("date");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.ToTable("District", "dbo");

            entity.Property(e => e.DistrictId).HasColumnName("DistrictID");
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.DistrictName).HasMaxLength(10);

            entity.HasOne(d => d.City).WithMany(p => p.Districts)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_District_City");
        });

        modelBuilder.Entity<EatTypeMethod>(entity =>
        {
            entity.HasKey(e => e.EatTypeMethodId).HasName("PK_EatTypeMethod");

            entity.ToTable("EatType_Method", "dbo");

            entity.Property(e => e.EatTypeMethodId)
                .ValueGeneratedNever()
                .HasColumnName("EatTypeMethodID");
            entity.Property(e => e.EatTypeMethodName).HasMaxLength(8);
        });

        modelBuilder.Entity<Identification>(entity =>
        {
            entity.ToTable("Identification", "dbo");

            entity.Property(e => e.IdentificationId).HasColumnName("IdentificationID");
            entity.Property(e => e.IdentificationName).HasMaxLength(10);
        });

        modelBuilder.Entity<MealMenu>(entity =>
        {
            entity.HasKey(e => e.MealId);

            entity.ToTable("Meal_Menu", "dbo");

            entity.Property(e => e.MealId).HasColumnName("MealID");
            entity.Property(e => e.MealImagePath).HasMaxLength(50);
            entity.Property(e => e.MealName).HasMaxLength(50);
            entity.Property(e => e.MealPrice).HasColumnType("money");
            entity.Property(e => e.Recommendation).HasColumnName("recommendation");
            entity.Property(e => e.StoreId).HasColumnName("StoreID");

            entity.HasOne(d => d.Store).WithMany(p => p.MealMenus)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_Meal_Menu_Store");
        });

        modelBuilder.Entity<MealOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId);

            entity.ToTable("Meal_Order", "dbo");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.OrderDate).HasColumnType("date");
            entity.Property(e => e.Remark).HasMaxLength(50);
            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.Total).HasColumnType("money");

            entity.HasOne(d => d.Member).WithMany(p => p.MealOrders)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_Meal_Order_Member");

            entity.HasOne(d => d.OrderStatusNavigation).WithMany(p => p.MealOrders)
                .HasForeignKey(d => d.OrderStatus)
                .HasConstraintName("FK_Meal_Order_Status");

            entity.HasOne(d => d.Store).WithMany(p => p.MealOrders)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_Meal_Order_Store");
        });

        modelBuilder.Entity<MealOrderDetail>(entity =>
        {
            entity.HasKey(e => e.Key);

            entity.ToTable("Meal_Order_Details", "dbo");

            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.MealId).HasColumnName("MealID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            entity.HasOne(d => d.Meal).WithMany(p => p.MealOrderDetails)
                .HasForeignKey(d => d.MealId)
                .HasConstraintName("FK_Meal_Order_Details_Meal_Menu");

            entity.HasOne(d => d.Order).WithMany(p => p.MealOrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Meal_Order_Details_Meal_Order");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.ToTable("Member", "dbo", tb => tb.HasTrigger("memberAccountInsert"));

            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.AccountName).HasMaxLength(20);
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.BrithDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(20);
            entity.Property(e => e.MemberName).HasMaxLength(10);
            entity.Property(e => e.Memberphone).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(50);

            entity.HasOne(d => d.AccountStatusNavigation).WithMany(p => p.Members)
                .HasForeignKey(d => d.AccountStatus)
                .HasConstraintName("FK_Member_AccountStatus");
        });

        modelBuilder.Entity<MemberAction>(entity =>
        {
            entity.HasKey(e => e.No);

            entity.ToTable("Member_Action", "dbo");

            entity.Property(e => e.No).HasColumnName("No.");
            entity.Property(e => e.ActionId).HasColumnName("ActionID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.StoreId).HasColumnName("StoreID");

            entity.HasOne(d => d.Action).WithMany(p => p.MemberActions)
                .HasForeignKey(d => d.ActionId)
                .HasConstraintName("FK_Member_Action_Action");

            entity.HasOne(d => d.Member).WithMany(p => p.MemberActions)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_Member_Action_Member");

            entity.HasOne(d => d.Store).WithMany(p => p.MemberActions)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_Member_Action_Store");
        });

        modelBuilder.Entity<MemberCoupon>(entity =>
        {
            entity.HasKey(e => e.No).HasName("PK_Customer_Coupon");

            entity.ToTable("Member_Coupon", "dbo");

            entity.Property(e => e.No).HasColumnName("No.");
            entity.Property(e => e.CouponId).HasColumnName("CouponID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");

            entity.HasOne(d => d.Coupon).WithMany(p => p.MemberCoupons)
                .HasForeignKey(d => d.CouponId)
                .HasConstraintName("FK_Customer_Coupon_Coupon");

            entity.HasOne(d => d.CouponStatusNavigation).WithMany(p => p.MemberCoupons)
                .HasForeignKey(d => d.CouponStatus)
                .HasConstraintName("FK_Member_Coupon_Status");

            entity.HasOne(d => d.Member).WithMany(p => p.MemberCoupons)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_Customer_Coupon_Member");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order", "dbo");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.CouponId).HasColumnName("CouponID");
            entity.Property(e => e.EatTypeMethodId).HasColumnName("EatTypeMethodID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");
            entity.Property(e => e.ReservationNumber).HasMaxLength(3);
            entity.Property(e => e.ReservationTime).HasColumnType("datetime");
            entity.Property(e => e.StoreId).HasColumnName("StoreID");

            entity.HasOne(d => d.Coupon).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CouponId)
                .HasConstraintName("FK_Order_Coupon");

            entity.HasOne(d => d.EatTypeMethod).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EatTypeMethodId)
                .HasConstraintName("FK_Order_EatTypeMethod");

            entity.HasOne(d => d.Member).WithMany(p => p.Orders)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_Order_Member");

            entity.HasOne(d => d.OrderStatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderStatus)
                .HasConstraintName("FK_Order_Status");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK_Order_PaymentMethod");

            entity.HasOne(d => d.Store).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_Order_Store");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.MealsId }).HasName("PK_Order_Details_1");

            entity.ToTable("Order_Details", "dbo");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.MealsId).HasColumnName("MealsID");
            entity.Property(e => e.MealsName).HasMaxLength(30);
            entity.Property(e => e.MealsPrice).HasColumnType("money");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Details_Order");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK_PaymentMethod");

            entity.ToTable("Payment_Method", "dbo");

            entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");
            entity.Property(e => e.PaymentName).HasMaxLength(8);
        });

        modelBuilder.Entity<RestaurantType>(entity =>
        {
            entity.HasKey(e => e.RestaurantTypeNum);

            entity.ToTable("Restaurant_Type", "dbo");

            entity.Property(e => e.TypeName).HasMaxLength(20);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status", "dbo");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StatusName).HasMaxLength(10);
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.ToTable("Store", "dbo", tb => tb.HasTrigger("storeAccountInsert"));

            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.AccountName).HasMaxLength(20);
            entity.Property(e => e.DistrictId).HasColumnName("DistrictID");
            entity.Property(e => e.Latitude).HasColumnType("decimal(10, 6)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(10, 6)");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.RestaurantAddress).HasMaxLength(40);
            entity.Property(e => e.RestaurantName).HasMaxLength(30);
            entity.Property(e => e.RestaurantPhone).HasMaxLength(20);
            entity.Property(e => e.StoreMail).HasMaxLength(40);
            entity.Property(e => e.TaxId)
                .HasMaxLength(8)
                .HasColumnName("TaxID");

            entity.HasOne(d => d.AccountStatusNavigation).WithMany(p => p.Stores)
                .HasForeignKey(d => d.AccountStatus)
                .HasConstraintName("FK_Store_AccountStatus");

            entity.HasOne(d => d.District).WithMany(p => p.Stores)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK_Store_District");
        });

        modelBuilder.Entity<StoreAdImage>(entity =>
        {
            entity.HasKey(e => e.ImageNo).HasName("PK_Meals_Menu");

            entity.ToTable("Store_AD_Image", "dbo");

            entity.Property(e => e.Adimage)
                .HasMaxLength(50)
                .HasColumnName("ADImage");
            entity.Property(e => e.Eximage)
                .HasMaxLength(50)
                .HasColumnName("EXImage");
            entity.Property(e => e.StoreId).HasColumnName("StoreID");

            entity.HasOne(d => d.Store).WithMany(p => p.StoreAdImages)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_Meals_Menu_Store");
        });

        modelBuilder.Entity<StoreDecorationImage>(entity =>
        {
            entity.HasKey(e => e.ImageId);

            entity.ToTable("Store_Decoration_Image", "dbo");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.ImagePath).HasMaxLength(50);
            entity.Property(e => e.StoreId).HasColumnName("StoreID");

            entity.HasOne(d => d.Store).WithMany(p => p.StoreDecorationImages)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_Store_Decoration_Image_Store");
        });

        modelBuilder.Entity<StoreEvaluate>(entity =>
        {
            entity.HasKey(e => e.EvaluateNo);

            entity.ToTable("Store_Evaluate", "dbo");

            entity.Property(e => e.EvaluateNo).HasColumnName("EvaluateNo.");
            entity.Property(e => e.EvaluateDate).HasColumnType("date");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.StoreId).HasColumnName("StoreID");

            entity.HasOne(d => d.Member).WithMany(p => p.StoreEvaluates)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_Store_Evaluate_Member");

            entity.HasOne(d => d.Store).WithMany(p => p.StoreEvaluates)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_Store_Evaluate_Store");
        });

        modelBuilder.Entity<StoreMealImage>(entity =>
        {
            entity.HasKey(e => e.ImageId);

            entity.ToTable("Store_Meal_Image", "dbo");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.ImagePath).HasMaxLength(50);
            entity.Property(e => e.StoreId).HasColumnName("StoreID");

            entity.HasOne(d => d.Store).WithMany(p => p.StoreMealImages)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_Store_Meal_Image_Store");
        });

        modelBuilder.Entity<StoreReserved>(entity =>
        {
            entity.HasKey(e => e.ReservationNo);

            entity.ToTable("Store_Reserved", "dbo");

            entity.Property(e => e.ReservationNo).HasColumnName("ReservationNo.");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.StoreId).HasColumnName("StoreID");

            entity.HasOne(d => d.Order).WithMany(p => p.StoreReserveds)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Store_Reserved_Order");

            entity.HasOne(d => d.Store).WithMany(p => p.StoreReserveds)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_Store_Reserved_Store");
        });

        modelBuilder.Entity<StoreType>(entity =>
        {
            entity.HasKey(e => e.No);

            entity.ToTable("Store_Type", "dbo");

            entity.Property(e => e.No).HasColumnName("No.");
            entity.Property(e => e.StoreId).HasColumnName("StoreID");

            entity.HasOne(d => d.RestaurantTypeNumNavigation).WithMany(p => p.StoreTypes)
                .HasForeignKey(d => d.RestaurantTypeNum)
                .HasConstraintName("FK_Store_Type_Restaurant_Type");

            entity.HasOne(d => d.Store).WithMany(p => p.StoreTypes)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_Store_Type_Store");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
