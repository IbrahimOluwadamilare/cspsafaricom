﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using cspv3.Data;

namespace cspv3.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("cspv3.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("IPAddress");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("cspv3.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("BillingProfileId");

                    b.Property<string>("City");

                    b.Property<string>("CompanyAddress")
                        .HasMaxLength(100);

                    b.Property<string>("CompanyName")
                        .HasMaxLength(100);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Country");

                    b.Property<string>("CspDefaultPassword");

                    b.Property<string>("CspDefaultUserName");

                    b.Property<string>("CspId")
                        .HasMaxLength(100);

                    b.Property<string>("Domain");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Etag");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("HasAgreement");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<byte>("Level");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PostCode")
                        .IsRequired();

                    b.Property<DateTime>("RegisterationDate");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("State");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("Url");

                    b.Property<string>("UserAddress")
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("cspv3.Models.BundleModels.Bundle", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BundleName");

                    b.HasKey("Id");

                    b.ToTable("Bundles");
                });

            modelBuilder.Entity("cspv3.Models.BundleModels.BundleCategory", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BundleId");

                    b.Property<string>("CategoryName");

                    b.Property<string>("CspId");

                    b.Property<string>("Price");

                    b.HasKey("Id");

                    b.HasIndex("BundleId");

                    b.ToTable("BundleCategories");
                });

            modelBuilder.Entity("cspv3.Models.BundleModels.Property", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("cspv3.Models.Cart", b =>
                {
                    b.Property<int>("RecordId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CartId");

                    b.Property<int>("Count");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("ProductId");

                    b.Property<int?>("ProductId1");

                    b.HasKey("RecordId");

                    b.HasIndex("ProductId1");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("cspv3.Models.ContactSupport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Phone");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.ToTable("HelpRequests");
                });

            modelBuilder.Entity("cspv3.Models.DomainOffer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomerId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("DomainName");

                    b.Property<bool>("IsAdded");

                    b.Property<int>("OrderId");

                    b.Property<string>("OwnerId");

                    b.Property<string>("Password");

                    b.Property<string>("ShopperId");

                    b.HasKey("Id");

                    b.ToTable("Domains");
                });

            modelBuilder.Entity("cspv3.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorizationPayment");

                    b.Property<string>("BillingCycle");

                    b.Property<string>("CompanyAddress");

                    b.Property<string>("CompanyName");

                    b.Property<string>("Country");

                    b.Property<string>("CspOrderId");

                    b.Property<string>("Domain");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<DateTime>("FulFillmentDate");

                    b.Property<bool>("FulfillPayment");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("LastPaymentDate");

                    b.Property<DateTime>("NextPaymentDate");

                    b.Property<DateTime>("OrderDate");

                    b.Property<string>("Path");

                    b.Property<bool>("Payment");

                    b.Property<DateTime>("PaymentDate");

                    b.Property<string>("PaymentGateWay");

                    b.Property<string>("PaymentTransactionReference");

                    b.Property<string>("Phone");

                    b.Property<string>("PromoCode");

                    b.Property<string>("Refinfo");

                    b.Property<decimal>("Total");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("cspv3.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrderId");

                    b.Property<string>("ProductId");

                    b.Property<int?>("ProductId1");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("UnitPrice");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId1");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("cspv3.Models.PrerequisiteOffer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PrereqOffer");

                    b.Property<int?>("ProductOfferingId");

                    b.HasKey("Id");

                    b.HasIndex("ProductOfferingId");

                    b.ToTable("PrerequisiteOffer");
                });

            modelBuilder.Entity("cspv3.Models.ProductOffering", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Billing");

                    b.Property<string>("Country");

                    b.Property<string>("Description");

                    b.Property<string>("EndCustomerType");

                    b.Property<string>("IsAddOn");

                    b.Property<string>("LicenseAgreementType");

                    b.Property<string>("Locale");

                    b.Property<int?>("MaximumQuantity");

                    b.Property<int?>("MinimumQuantity");

                    b.Property<string>("Name");

                    b.Property<int?>("Rank");

                    b.Property<string>("SalesGroupId");

                    b.Property<string>("SecondaryLicenseType");

                    b.Property<string>("Uri");

                    b.Property<decimal>("WragbyPrice");

                    b.Property<string>("category");

                    b.Property<string>("cspID");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("cspv3.Models.Promo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Authorization");

                    b.Property<DateTime>("ExpirationDate");

                    b.Property<decimal>("PercentageInvoice");

                    b.Property<decimal>("PercentagePayment");

                    b.Property<string>("PromoCode");

                    b.HasKey("Id");

                    b.ToTable("PromoDetails");
                });

            modelBuilder.Entity("cspv3.Models.SubProductOffering", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MeterCategory");

                    b.Property<string>("MeterSubCategory");

                    b.Property<int>("MinValue");

                    b.Property<string>("Name");

                    b.Property<string>("Region");

                    b.Property<string>("ResouceId");

                    b.Property<string>("Units");

                    b.Property<decimal>("WragbyPrice");

                    b.HasKey("id");

                    b.ToTable("SubProducts");
                });

            modelBuilder.Entity("cspv3.Models.Support", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Attachment");

                    b.Property<string>("CaseOwner");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateResolved");

                    b.Property<string>("Department");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Priority");

                    b.Property<string>("Response");

                    b.Property<int>("Status");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.ToTable("SupportTicket");
                });

            modelBuilder.Entity("cspv3.Models.SupportedBillingCycle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProductOfferingId");

                    b.Property<string>("SupBillingCycle");

                    b.HasKey("Id");

                    b.HasIndex("ProductOfferingId");

                    b.ToTable("SupportedBillingCycle");
                });

            modelBuilder.Entity("cspv3.Models.UpgradeTargetOffer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProductOfferingId");

                    b.Property<string>("UpgTargetOffer");

                    b.HasKey("Id");

                    b.HasIndex("ProductOfferingId");

                    b.ToTable("UpgradeTargetOffer");
                });

            modelBuilder.Entity("cspv3.Models.VmCart", b =>
                {
                    b.Property<int>("RecordId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CartId");

                    b.Property<int>("Count");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("ProductId");

                    b.Property<int?>("Productlinkid");

                    b.HasKey("RecordId");

                    b.HasIndex("Productlinkid");

                    b.ToTable("VmCarts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("cspv3.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("cspv3.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("cspv3.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("cspv3.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("cspv3.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("cspv3.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("cspv3.Models.BundleModels.BundleCategory", b =>
                {
                    b.HasOne("cspv3.Models.BundleModels.Bundle", "Bundle")
                        .WithMany()
                        .HasForeignKey("BundleId");
                });

            modelBuilder.Entity("cspv3.Models.Cart", b =>
                {
                    b.HasOne("cspv3.Models.ProductOffering", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId1");
                });

            modelBuilder.Entity("cspv3.Models.OrderDetail", b =>
                {
                    b.HasOne("cspv3.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("cspv3.Models.ProductOffering", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId1");
                });

            modelBuilder.Entity("cspv3.Models.PrerequisiteOffer", b =>
                {
                    b.HasOne("cspv3.Models.ProductOffering")
                        .WithMany("PrerequisiteOffers")
                        .HasForeignKey("ProductOfferingId");
                });

            modelBuilder.Entity("cspv3.Models.SupportedBillingCycle", b =>
                {
                    b.HasOne("cspv3.Models.ProductOffering")
                        .WithMany("SupportedBillingCycles")
                        .HasForeignKey("ProductOfferingId");
                });

            modelBuilder.Entity("cspv3.Models.UpgradeTargetOffer", b =>
                {
                    b.HasOne("cspv3.Models.ProductOffering")
                        .WithMany("UpgradeTargetOffers")
                        .HasForeignKey("ProductOfferingId");
                });

            modelBuilder.Entity("cspv3.Models.VmCart", b =>
                {
                    b.HasOne("cspv3.Models.SubProductOffering", "Productlink")
                        .WithMany()
                        .HasForeignKey("Productlinkid");
                });
#pragma warning restore 612, 618
        }
    }
}