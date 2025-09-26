using FreeSpinsGame.Data;
using FreeSpinsGame.Data.Models;
using FreeSpinsGame.Mapping;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FreeSpinsGame.Services.Interfaces;
using FreeSpinsGame.Services;

namespace FreeSpinsGame.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<FreeSpinsGameDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddAutoMapper(conf =>
            {
                conf.AddProfile<FreeSpinsGameProfile>();
            });

            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddIdentity<Player, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = builder
                   .Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");

                options.Password.RequireUppercase = builder
                    .Configuration.GetValue<bool>("Identity:Password:RequireUppercase");

                options.Password.RequireLowercase = builder
                    .Configuration.GetValue<bool>("Identity:Password:RequireLowercase");

                options.Password.RequireNonAlphanumeric = builder
                    .Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");

                options.Password.RequireDigit = builder
                    .Configuration.GetValue<bool>("Identity:Password:RequireDigit");

                options.Password.RequiredLength = builder
                    .Configuration.GetValue<int>("Identity:Password:RequiredLength");

            }).AddEntityFrameworkStores<FreeSpinsGameDbContext>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]!))
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
