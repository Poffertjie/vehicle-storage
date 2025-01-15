using System;
using System.Collections.Generic;
using Database.DbContext.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Database.DbContext;

public partial class VehicleManagementContext : Microsoft.EntityFrameworkCore.DbContext
{
    public VehicleManagementContext(DbContextOptions<VehicleManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdditionalContactPerson> AdditionalContactPeople { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<BrandModel> BrandModels { get; set; }

    public virtual DbSet<BrandModelVariant> BrandModelVariants { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerStorageContract> CustomerStorageContracts { get; set; }

    public virtual DbSet<CustomerVehicle> CustomerVehicles { get; set; }

    public virtual DbSet<CustomerVehicleCheckIn> CustomerVehicleCheckIns { get; set; }

    public virtual DbSet<CustomerVehicleCheckOut> CustomerVehicleCheckOuts { get; set; }

    public virtual DbSet<CustomerVehicleDailyCheck> CustomerVehicleDailyChecks { get; set; }

    public virtual DbSet<CustomerVehicleStatus> CustomerVehicleStatuses { get; set; }

    public virtual DbSet<CustomerVehicleStorageCheckIn> CustomerVehicleStorageCheckIns { get; set; }

    public virtual DbSet<CustomerVehicleWeeklyCheck> CustomerVehicleWeeklyChecks { get; set; }

    public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    public virtual DbSet<FuelType> FuelTypes { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Medium> Media { get; set; }

    public virtual DbSet<PaymentOption> PaymentOptions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ServiceAdvisor> ServiceAdvisors { get; set; }

    public virtual DbSet<StorageBay> StorageBays { get; set; }

    public virtual DbSet<StorageBayStatus> StorageBayStatuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdditionalContactPerson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AdditionalContactPerson_Id");

            entity
                .ToTable("AdditionalContactPerson")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("AdditionalContactPersonHistory", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.ContactNumber).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.IdentificationNumber).HasMaxLength(30);
            entity.Property(e => e.Relationship).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.AdditionalContactPeople)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdditionalContactPerson_Customer");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Brand_Id");

            entity
                .ToTable("Brand")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("BrandHistory", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<BrandModel>(entity =>
        {
            entity
                .ToTable("BrandModel")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("BrandModelHistory", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.Name).HasMaxLength(20);

            entity.HasOne(d => d.Brand).WithMany(p => p.BrandModels)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BrandModel_Brand");
        });

        modelBuilder.Entity<BrandModelVariant>(entity =>
        {
            entity
                .ToTable("BrandModelVariant")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("BrandModelVariantHistory", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.Name).HasMaxLength(20);

            entity.HasOne(d => d.BrandModel).WithMany(p => p.BrandModelVariants)
                .HasForeignKey(d => d.BrandModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BrandModelVariant_BrandModel");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Company_Id");

            entity
                .ToTable("Company")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("CompanyHistory", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ContactNumber).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Customer_Id");

            entity
                .ToTable("Customer")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("CustomerHistory", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.ContactNumber).HasMaxLength(50);
            entity.Property(e => e.IdentificationNumber).HasMaxLength(30);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.Company).WithMany(p => p.Customers)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_Company");

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_User");
        });

        modelBuilder.Entity<CustomerStorageContract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CustomerStorageContract_Id");

            entity
                .ToTable("CustomerStorageContract")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("CustomerStorageHistory", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.PricePerMonth).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.CustomerStorageContracts)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerStorageContract_Company");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerStorageContracts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerStorageContract_Customer");

            entity.HasOne(d => d.CustomerVehicle).WithMany(p => p.CustomerStorageContracts)
                .HasForeignKey(d => d.CustomerVehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerStorageContract_CustomerVehicle");

            entity.HasOne(d => d.PaymentOption).WithMany(p => p.CustomerStorageContracts)
                .HasForeignKey(d => d.PaymentOptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerStorageContract_PaymentOptions");

            entity.HasOne(d => d.StorageBay).WithMany(p => p.CustomerStorageContracts)
                .HasForeignKey(d => d.StorageBayId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerStorageContract_StorageBay");
        });

        modelBuilder.Entity<CustomerVehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CustomerVehicle_Id");

            entity
                .ToTable("CustomerVehicle")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("CustomerVehicleHistory", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.AverageVehicleValuation).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChassisVin).HasMaxLength(50);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.LicensePlate).HasMaxLength(50);
            entity.Property(e => e.NextService).HasColumnType("datetime");
            entity.Property(e => e.RegistrationExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerVehicles)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicle_Customer");

            entity.HasOne(d => d.ServiceAdvisor).WithMany(p => p.CustomerVehicles)
                .HasForeignKey(d => d.ServiceAdvisorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicle_ServiceAdvisor");

            entity.HasOne(d => d.Status).WithMany(p => p.CustomerVehicles)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicle_Status");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.CustomerVehicles)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicle_Vehicle");
        });

        modelBuilder.Entity<CustomerVehicleCheckIn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CustomerVehicleCheckIn_Id");

            entity
                .ToTable("CustomerVehicleCheckIn")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("CustomerVehicleCheckInHistory", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.TyreMake).HasMaxLength(100);
            entity.Property(e => e.TyreTreadMeasure).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Company).WithMany(p => p.CustomerVehicleCheckIns)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicleCheckIn_Company");

            entity.HasOne(d => d.CustomerStorageContract).WithMany(p => p.CustomerVehicleCheckIns)
                .HasForeignKey(d => d.CustomerStorageContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicleCheckInCustomerStorageContract");

            entity.HasOne(d => d.CustomerVehicle).WithMany(p => p.CustomerVehicleCheckIns)
                .HasForeignKey(d => d.CustomerVehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicleCheckIn_CustomerVehicleId");
        });

        modelBuilder.Entity<CustomerVehicleCheckOut>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CustomerVehicleCheckOut_Id");

            entity
                .ToTable("CustomerVehicleCheckOut")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("CustomerVehicleCheckOutHistory", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.Address).HasMaxLength(450);
            entity.Property(e => e.CheckInDate).HasColumnType("datetime");
            entity.Property(e => e.CheckOutDate).HasColumnType("datetime");
            entity.Property(e => e.PlannedCheckInDate).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.CustomerVehicleCheckOuts)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicleCheckOut_Company");

            entity.HasOne(d => d.CustomerStorageContract).WithMany(p => p.CustomerVehicleCheckOuts)
                .HasForeignKey(d => d.CustomerStorageContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicleCheckOutCustomerStorageContract");

            entity.HasOne(d => d.CustomerVehicle).WithMany(p => p.CustomerVehicleCheckOuts)
                .HasForeignKey(d => d.CustomerVehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicleCheckOut_CustomerVehicle");

            entity.HasOne(d => d.DeliveryMethod).WithMany(p => p.CustomerVehicleCheckOuts)
                .HasForeignKey(d => d.DeliveryMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicleCheckOut_DeliveryMethod");
        });

        modelBuilder.Entity<CustomerVehicleDailyCheck>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CustomerVehicleDailyChecks_Id");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("CustomerVehicleDailyChecksHistory", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.BatteryChargeStatus).HasMaxLength(200);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.OilCheck).HasMaxLength(200);
            entity.Property(e => e.TyreCheck).HasMaxLength(200);
            entity.Property(e => e.WaterCheck).HasMaxLength(200);

            entity.HasOne(d => d.Company).WithMany(p => p.CustomerVehicleDailyChecks)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicleDailyChecks_Company");

            entity.HasOne(d => d.CustomerStorageContract).WithMany(p => p.CustomerVehicleDailyChecks)
                .HasForeignKey(d => d.CustomerStorageContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicleDailyChecks_CustomerStorageContract");

            entity.HasOne(d => d.CustomerVehicle).WithMany(p => p.CustomerVehicleDailyChecks)
                .HasForeignKey(d => d.CustomerVehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicleDailyChecks_CustomerVehicle");
        });

        modelBuilder.Entity<CustomerVehicleStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CustomerVehicleStatus_Id");

            entity.ToTable("CustomerVehicleStatus");

            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Company).WithMany(p => p.CustomerVehicleStatuses)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicleStatus_Company");
        });

        modelBuilder.Entity<CustomerVehicleStorageCheckIn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CustomerVehicleStorageCheckIn_Id");

            entity
                .ToTable("CustomerVehicleStorageCheckIn")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("CustomerVehicleStorageCheckInHistory", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.HasOne(d => d.Company).WithMany(p => p.CustomerVehicleStorageCheckIns)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicleStorageCheckInCompany");

            entity.HasOne(d => d.CustomerStorageContract).WithMany(p => p.CustomerVehicleStorageCheckIns)
                .HasForeignKey(d => d.CustomerStorageContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicleStorageCheckIn_CustomerStorageContract");

            entity.HasOne(d => d.CustomerVehicle).WithMany(p => p.CustomerVehicleStorageCheckIns)
                .HasForeignKey(d => d.CustomerVehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicleStorageCheckIn_CustomerVehicle");
        });

        modelBuilder.Entity<CustomerVehicleWeeklyCheck>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CustomerVehicleWeeklyChecks_Id");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("CustomerVehicleWeeklyChecksHistory", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.CustomerVehicleWeeklyChecks)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicleWeeklyChecks_Company");

            entity.HasOne(d => d.CustomerStorageContract).WithMany(p => p.CustomerVehicleWeeklyChecks)
                .HasForeignKey(d => d.CustomerStorageContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicleWeeklyChecks_CustomerStorageContract");

            entity.HasOne(d => d.CustomerVehicle).WithMany(p => p.CustomerVehicleWeeklyChecks)
                .HasForeignKey(d => d.CustomerVehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerVehicleWeeklyChecks_CustomerVehicle");
        });

        modelBuilder.Entity<DeliveryMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DeliveryMethod_Id");

            entity.ToTable("DeliveryMethod");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<FuelType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FuelType_Id");

            entity.ToTable("FuelType");

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.ToTable("Logs", "Observability");

            entity.Property(e => e.LogSource).HasMaxLength(50);
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<Medium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Media_Id");

            entity.HasOne(d => d.Company).WithMany(p => p.Media)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Media_Company");
        });

        modelBuilder.Entity<PaymentOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PaymentOption_Id");

            entity.ToTable("PaymentOption");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Company).WithMany(p => p.PaymentOptions)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentOption_Company");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Role_Id");

            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<ServiceAdvisor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ServiceAdvisor_Id");

            entity
                .ToTable("ServiceAdvisor")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("ServiceAdvisorHistory", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.ContactNumber).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<StorageBay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_StorageBay_Id");

            entity
                .ToTable("StorageBay")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("StorageBayHistory", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.HasOne(d => d.Company).WithMany(p => p.StorageBays)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StorageBay_Company");

            entity.HasOne(d => d.StorageBayStatus).WithMany(p => p.StorageBays)
                .HasForeignKey(d => d.StorageBayStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StorageBay_StorageBayStatus");
        });

        modelBuilder.Entity<StorageBayStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_StorageBayStatus_Id");

            entity.ToTable("StorageBayStatus");

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Company).WithMany(p => p.StorageBayStatuses)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StorageBayStatus_Company");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User_Id");

            entity
                .ToTable("User")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("UserHistory", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.ResetPasswordToken).HasMaxLength(450);
            entity.Property(e => e.SecurityStamp)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.Company).WithMany(p => p.Users)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_User_Company");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserRoles_Id");

            entity
                .ToTable("UserRole")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("UserRolesHistory", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.RoleId).HasMaxLength(450);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Role");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_User");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Vehicle_Id");

            entity
                .ToTable("Vehicle")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("VehicleHistory", "dbo");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.HasOne(d => d.Brand).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehicle_Brands");

            entity.HasOne(d => d.BrandModel).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.BrandModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehicle_BrandModel");

            entity.HasOne(d => d.BrandModelVariant).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.BrandModelVariantId)
                .HasConstraintName("FK_Vehicle_BrandModelVariant");

            entity.HasOne(d => d.Company).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehicle_Company");

            entity.HasOne(d => d.FuelType).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.FuelTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehicle_FuelType");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
