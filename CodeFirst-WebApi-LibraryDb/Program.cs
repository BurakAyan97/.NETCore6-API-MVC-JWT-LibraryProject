using CodeFirst_WebApi_LibraryDb.Entities;
using CodeFirst_WebApi_LibraryDb.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace CodeFirst_WebApi_LibraryDb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson(option => option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    //Uygulamada izin verilen sitenin denetlenip denetlenmeyeceğini söyler.
                    ValidateAudience = true,
                    //hangi sitenin bunları denetleyip denetlemeyeceğine izin verir
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    //token bize ait mi değil mi onu kontrol eder.
                    ValidateIssuerSigningKey = true,
                    ValidIssuers = new string[] { builder.Configuration["JWT:Issuer"] },
                    ValidAudiences = new string[] { builder.Configuration["JWT:Audience"] },
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                    ClockSkew = TimeSpan.FromMinutes(30),
                };
            });

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddEndpointsApiExplorer();
            //Swagger API kontrolü için yapıldı.Authorize kullanarak girmemize izin veriyor artık.
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Insert JWT Token",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey

                });
                opt.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            builder.Services.AddDbContext<LibraryDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));

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


            app.MapControllers();

            app.Run();
        }
    }
}