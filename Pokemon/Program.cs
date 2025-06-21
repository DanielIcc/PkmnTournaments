using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Pokemon.Data;
using Pokemon.Data.Models;
using Pokemon.Services;
using System.Security.Claims;
soria
namespace Pokemon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=torneo.db"));

            builder.Services.AddAuthentication("Cookies")
                .AddCookie("Cookies", options =>
                {
                    options.LoginPath = "/Login";
                    options.LogoutPath = "/Logout";

                    options.Events = new CookieAuthenticationEvents
                    {
                        OnValidatePrincipal = async context =>
                        {
                            var sessionToken = context?.Principal?.FindFirst("SessionToken")?.Value;
                            var userId = context?.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                            var db = context.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();
                            var session = await db.UserSessions
                                .FirstOrDefaultAsync(s => s.SessionToken == sessionToken && s.UserId.ToString().Equals(userId) && !s.IsRevoked);

                            if (session == null)
                            {
                                context.RejectPrincipal();
                                await context.HttpContext.SignOutAsync("Cookies");
                            }
                        }
                    };
                });

            builder.Services.AddAuthorization();

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddSession();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<AuthService>();

            builder.Services.AddHostedService<SessionCleanupService>();
            builder.Services.Configure<SessionSettings>(builder.Configuration.GetSection("SessionSettings"));
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                //db.Database.EnsureDeleted();
                db.Database.Migrate();
                Seed.SeedAdmin(db);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.Use(async (context, next) =>
            {

                if (!context.Response.HasStarted)
                {
                    context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
                    context.Response.Headers["Pragma"] = "no-cache";
                    context.Response.Headers["Expires"] = "0";
                }
                await next();

            });
             //hola
            app.UseSession();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapRazorPages();

            //rama deleted rama1

            app.Run();
            //SIN ERROR
        }
    }
}
