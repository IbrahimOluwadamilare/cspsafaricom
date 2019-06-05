using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSBGlobal.Data.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillingProfileId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyAddress",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CspDefaultPassword",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CspDefaultUserName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CspId",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Domain",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Etag",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HasAgreement",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "Level",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterationDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAddress",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShopperId = table.Column<string>(nullable: true),
                    CustomerId = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    OwnerId = table.Column<string>(nullable: true),
                    DomainName = table.Column<string>(nullable: true),
                    OrderId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    IsAdded = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HelpRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(maxLength: 80, nullable: false),
                    Message = table.Column<string>(maxLength: 200, nullable: false),
                    DateCreated = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CspOrderId = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    CompanyAddress = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Total = table.Column<decimal>(nullable: false),
                    Payment = table.Column<bool>(nullable: false),
                    PaymentTransactionReference = table.Column<string>(nullable: true),
                    PaymentGateWay = table.Column<string>(nullable: true),
                    BillingCycle = table.Column<string>(nullable: true),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    Domain = table.Column<string>(nullable: true),
                    FulfillPayment = table.Column<bool>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    Refinfo = table.Column<string>(nullable: true),
                    PromoCode = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    AuthorizationPayment = table.Column<string>(nullable: true),
                    FulFillmentDate = table.Column<DateTime>(nullable: false),
                    LastPaymentDate = table.Column<DateTime>(nullable: false),
                    NextPaymentDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MinimumQuantity = table.Column<int>(nullable: true),
                    MaximumQuantity = table.Column<int>(nullable: true),
                    Rank = table.Column<int>(nullable: true),
                    Uri = table.Column<string>(nullable: true),
                    Locale = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    cspID = table.Column<string>(nullable: true),
                    IsAddOn = table.Column<string>(nullable: true),
                    Billing = table.Column<string>(nullable: true),
                    SalesGroupId = table.Column<string>(nullable: true),
                    LicenseAgreementType = table.Column<string>(nullable: true),
                    SecondaryLicenseType = table.Column<string>(nullable: true),
                    EndCustomerType = table.Column<string>(nullable: true),
                    WragbyPrice = table.Column<decimal>(nullable: false),
                    category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromoDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PromoCode = table.Column<string>(nullable: true),
                    PercentagePayment = table.Column<decimal>(nullable: false),
                    PercentageInvoice = table.Column<decimal>(nullable: false),
                    Authorization = table.Column<string>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubProducts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MeterCategory = table.Column<string>(nullable: true),
                    MeterSubCategory = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ResouceId = table.Column<string>(nullable: true),
                    WragbyPrice = table.Column<decimal>(nullable: false),
                    MinValue = table.Column<int>(nullable: false),
                    Units = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubProducts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SupportTicket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Department = table.Column<string>(nullable: true),
                    Priority = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(maxLength: 80, nullable: false),
                    Message = table.Column<string>(maxLength: 200, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateResolved = table.Column<DateTime>(nullable: false),
                    CaseOwner = table.Column<string>(nullable: true),
                    Attachment = table.Column<string>(nullable: true),
                    Response = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportTicket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    RecordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<string>(nullable: true),
                    CartId = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ProductId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_Carts_Products_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    ProductId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrerequisiteOffer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PrereqOffer = table.Column<string>(nullable: true),
                    ProductOfferingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrerequisiteOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrerequisiteOffer_Products_ProductOfferingId",
                        column: x => x.ProductOfferingId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupportedBillingCycle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SupBillingCycle = table.Column<string>(nullable: true),
                    ProductOfferingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportedBillingCycle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportedBillingCycle_Products_ProductOfferingId",
                        column: x => x.ProductOfferingId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UpgradeTargetOffer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpgTargetOffer = table.Column<string>(nullable: true),
                    ProductOfferingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpgradeTargetOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpgradeTargetOffer_Products_ProductOfferingId",
                        column: x => x.ProductOfferingId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VmCarts",
                columns: table => new
                {
                    RecordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<string>(nullable: true),
                    CartId = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Productlinkid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VmCarts", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_VmCarts_SubProducts_Productlinkid",
                        column: x => x.Productlinkid,
                        principalTable: "SubProducts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId1",
                table: "Carts",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId1",
                table: "OrderDetails",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_PrerequisiteOffer_ProductOfferingId",
                table: "PrerequisiteOffer",
                column: "ProductOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportedBillingCycle_ProductOfferingId",
                table: "SupportedBillingCycle",
                column: "ProductOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_UpgradeTargetOffer_ProductOfferingId",
                table: "UpgradeTargetOffer",
                column: "ProductOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_VmCarts_Productlinkid",
                table: "VmCarts",
                column: "Productlinkid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Domains");

            migrationBuilder.DropTable(
                name: "HelpRequests");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "PrerequisiteOffer");

            migrationBuilder.DropTable(
                name: "PromoDetails");

            migrationBuilder.DropTable(
                name: "SupportedBillingCycle");

            migrationBuilder.DropTable(
                name: "SupportTicket");

            migrationBuilder.DropTable(
                name: "UpgradeTargetOffer");

            migrationBuilder.DropTable(
                name: "VmCarts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SubProducts");

            migrationBuilder.DropColumn(
                name: "BillingProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanyAddress",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CspDefaultPassword",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CspDefaultUserName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CspId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Domain",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Etag",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HasAgreement",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RegisterationDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserAddress",
                table: "AspNetUsers");
        }
    }
}
