using JobMatchingAPI.Data;
using JobMatchingAPI.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//for entity framework
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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{

    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
          ValidateAudience = true,
          ValidateIssuer = true,
          ValidAudience = builder.Configuration["JWT:ValidAudience"],
          ValidIssuer = builder.Configuration["JWT:ValidAudience"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("qdTuh99KIPI7VSqgTwzj71Ba7ETRGNhP"))
    };
});

//todo later
//IRoleStore<>


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
