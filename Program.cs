
using Microsoft.EntityFrameworkCore;
using ScheduleGym.Repositories;
using ScheduleGym.Repositories.Interfaces;
using ScheduleGym.Models.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ScheduleGym.Controllers;
using ScheduleGym.Utility;
using Microsoft.AspNetCore.Cors;



var builder = WebApplication.CreateBuilder(args);


//app.MapGet("/", () => "Hello World!");

// builder.Services.AddControllers();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


   // builder.Services.AddDbContext<ApplicationDbContext>(
   //     options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IPlaceRepository ,PlaceRepository>();
builder.Services.AddScoped<IUserRepository ,UserRepository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<ScheduleGymContext>(options =>
{
options.UseSqlServer("Server=localhost;Database=ScheduleGym;User Id=sa;Password=Bidat123;TrustServerCertificate=True");
   // options.UseNpgsql("Host=localhost;port=5432;Database=some-postgres;username=postgres;password=mysecretpassword;");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "CorsPolicy",
        builder => builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
        ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });

var app = builder.Build();

app.UseMiddleware<CustomMiddleware>();

app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});
 app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});



//app.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


// app.UseRouting();

// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapControllers();
// });
app.UseCors("CorsPolicy");

app.MapControllers();
 app.UseRouting();
 app.UseAuthentication();
app.UseAuthorization();

app.Run();
