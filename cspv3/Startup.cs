using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using cspv3.Data;
using cspv3.Models;
using cspv3.Services;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;
using System;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Hangfire;
using cspv3.Helpers;
using NToastNotify;

namespace cspv3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the fusruntime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddHangfire(
      x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<SendGridSettings>(Configuration.GetSection("SendGridSettings"));
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddApplicationInsightsTelemetry();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

            });
            services.AddDistributedMemoryCache();
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;


                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;

            })
             .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {

                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

            });


            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });
            services.AddAntiforgery(o => o.SuppressXFrameOptionsHeader = true);



            services.AddTransient<IDomainService, DomainService>();
            services.AddTransient<IEmailSender, SendGridEmailService>();
            services.AddTransient<IAdministratorDashboard, AdministratorDashboardService>();
            services.AddTransient<ICustomerDashboard, CustomerDashboardServices>();
            services.AddTransient<IProductOffering, ProductOfferingService>();
            services.AddTransient<IShoppingCart, ShoppingCartService>();
            services.AddTransient<ICSPapi, CspApiService>();
            services.AddTransient<IAzureApi, AzureApiService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IExcelToProductService, ExcelToProductService>();
            services.AddTransient<IFirstCheckout, FirstCheckoutService>();


            services.AddMvc().AddNToastNotifyNoty(
                  new NotyOptions
                  {
                      ProgressBar = false,
                      Timeout = 3000,
                      Theme = "metroui",
                      VisibilityControl = true,
                      
                  });

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
            {
                //app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }





            // app.UseRewriter(new RewriteOptions().AddRedirectToHttpsPermanent());
            app.UseStaticFiles();
            app.UseSession();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });


            var options = new RewriteOptions()
            .AddRedirectToHttps();

            app.UseRewriter(options);

            app.UseAuthentication();

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthFilter() }
            }); app.UseHangfireServer();

            //app.Use(async (context, next) =>
            //{
            //    context.Response.Headers.Add("X-Frame-Options", "DENY");
            //    await next();
            //});

            app.UseXfo(o => o.Deny());
            app.UseNToastNotify();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //   RotativaConfiguration.Setup(env);
           CreateUserRoles(provider).Wait();
          BackgroundChecker(provider).Wait();

        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();



            IdentityResult roleResult;
            //Adding Admin Role 
            var AdminroleCheck = await RoleManager.RoleExistsAsync("Admin");
            var UserroleCheck = await RoleManager.RoleExistsAsync("User");

            if (!AdminroleCheck)
            {
                roleResult = await RoleManager.CreateAsync(new ApplicationRole("Admin"));
            }
            if (!UserroleCheck)
            {
                roleResult = await RoleManager.CreateAsync(new ApplicationRole("User"));
            }


        }

        private async Task BackgroundChecker(IServiceProvider serviceProvider)
        {
            var usermanager =   serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var azureservice = serviceProvider.GetRequiredService<IAzureApi>();
            var _dbcontext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var _emailservice = serviceProvider.GetRequiredService<IEmailSender>();

            var checker = new BackgroundSubChecker(_dbcontext, azureservice, usermanager, _emailservice);

            RecurringJob.AddOrUpdate(() => checker.BackgroundSubTask(), Cron.Daily);
        }
    }
}
