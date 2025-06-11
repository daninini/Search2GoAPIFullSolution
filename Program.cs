using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Search2Go.Application.Interfaces;
using Search2Go.Application.Services.Admin;

using Search2Go.Infrastructure.Auth;
using Search2Go.Infrastructure.Data;
using Search2Go.Infrastructure.Helpers;
using Search2Go.Infrastructure.Services;

using System.Text;

namespace Search2GoAPIFullSolution
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<JwtTokenGenerator>();

            // Authorization policies
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("ManagerOnly", policy => policy.RequireRole("Manager"));
            });
            // Add services
            builder.Services.AddHttpContextAccessor(); // ✅ Add this first
            builder.Services.AddScoped<IUserContextService, UserContextService>(); // ✅ Then this
            // === Auto-registered Services ===
            builder.Services.AddScoped<ICronService, CronService>();
            builder.Services.AddScoped<IDefaultService, DefaultService>();
            builder.Services.AddScoped<IMenuService, MenuService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IPackageService, PackageService>();
            builder.Services.AddScoped<IPlanService, PlanService>();
            builder.Services.AddScoped<IReportService, ReportService>();
            builder.Services.AddScoped<IResendEmailService, ResendEmailService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<ISettingService, SettingService>();
            builder.Services.AddScoped<IStatisticsService, StatisticsService>();
            builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<IUpgradeService, UpgradeService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();
            builder.Services.AddScoped<IDataService, DataService>();
            builder.Services.AddScoped<IInfoService, InfoService>();
            builder.Services.AddScoped<IEmptyService, EmptyService>();
            builder.Services.AddScoped<IEmpty2Service, Empty2Service>();
            builder.Services.AddScoped<ICompanyUserService, CompanyUserService>();
            builder.Services.AddScoped<IExportService, ExportService>();
            builder.Services.AddScoped<ILocationService, LocationService>();
            builder.Services.AddScoped<IMainService, MainService>();
            builder.Services.AddScoped<IMainLocalService, MainLocalService>();
            builder.Services.AddScoped<IAgencyManagerService, AgencyManagerService>();
            builder.Services.AddScoped<ICreditCardService, CreditCardService>();
            builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            builder.Services.AddSwaggerGen(c =>
            {
                c.OperationFilter<FileUploadOperation>(); // 👈 Register your custom filter
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles(); // Enables serving images from wwwroot/uploads
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            try
            {
                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
