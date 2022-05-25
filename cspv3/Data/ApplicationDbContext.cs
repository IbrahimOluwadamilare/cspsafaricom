using System;
using System.Collections.Generic;
using System.Text;
using cspv3.Models;
using cspv3.Models.BundleModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace cspv3.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



        }

        public DbSet<ProductOffering> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<VmCart> VmCarts { get; set; }
        public DbSet<DomainOffer> Domains { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Promo> PromoDetails { get; set; }
        public DbSet<SubProductOffering> SubProducts { get; set; }
        public DbSet<Support> SupportTicket { get; set; }
        public DbSet<ContactSupport> HelpRequests { get; set; }
        public DbSet<Bundle> Bundles { get; set; }
        public DbSet<BundleCategory> BundleCategories { get; set; }
        public DbSet<Property> Properties { get; set; }
    }
}
