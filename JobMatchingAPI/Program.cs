using JobMatchingAPI.Data;
using JobMatchingAPI.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//for entity framework
var configuration = builder.Configuration;
builder.Services.AddDbContext<JobContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// for identity
builder.Services
        .AddIdentity<User, IdentityRole>()
        .AddDefaultTokenProviders()
        .AddEntityFrameworkStores<JobContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// for authentication
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
  .AddJwtBearer(option =>
  {
      option.SaveToken = true;
      option.RequireHttpsMetadata = false;
      option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
      {
          ValidateAudience = true,
          ValidateIssuer = true,
          ValidAudience = builder.Configuration["JWT:ValidAudience"],
          ValidIssuer = builder.Configuration["JWT:ValidAudience"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
      };
  });



var app = builder.Build();

// Configure the HTTP request pipeline. n
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
