using System.Text;
using FonApi.Database;
using FonApi.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

// Configuration özelliğini ekleyin
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AccountsDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("AccountSchemaConnection"),new MySqlServerVersion(new Version(8,3,0))));

builder.Services.AddDbContext<VenturesDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("VentureSchemaConnection"),new MySqlServerVersion(new Version(8,3,0))));

//Service Configurations
builder.Services.AddScoped<AccountDbService>();
builder.Services.AddScoped<VentureDbService>();
//

//Swagger Configurations
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "FonIste Database API", Version = "v1.0" }); });
builder.Services.AddCors();
//


var key = Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]);
            builder.Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = System.TimeSpan.FromMinutes(60),
                    RequireExpirationTime = true,
                };
            });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "MySql Db Operations API");});
}


app.UseCors();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
