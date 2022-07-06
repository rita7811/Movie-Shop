using System.Text;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieShopAPI.Middlewares;
using MovieShopMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI is FIRST class citizen in .NET CORE
// older .NET Framework DI was not built-in, we had to rely on 3rd part libraries, autofac, ninject
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<ICastRepository, CastRepository>();
builder.Services.AddScoped<ICastService, CastService>();
builder.Services.AddScoped<IRepository<Genre>, Repository<Genre>>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICurrentLogedInUser, CurrentLogedInUser>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();


// Inject HttpContext for IHttpContextAccessor interface
builder.Services.AddHttpContextAccessor();

// Inject the connection string into DbContext options constructor
builder.Services.AddDbContext<MovieShopDbContext>(options =>
{
    // get the connection string from app settings
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieShopDbConnection"));
});


// API is gonna use JWT authentication, so that it can look at the incoming request and look for Token
// and if valid it will get the claims into HttpContext
// to actually check incoming Token using secret Key
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)   // install package "Microsoft.AspNetCore.Authentication.JwtBearer"
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["secretKey"]))
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
// **MiddleWare
// When you get a http request from client/brower
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddlewareClassTemplate();  // call Extension method inside MovieShopExceptionMiddleware.cs
app.UseHttpsRedirection();

// For Angular
app.UseCors(policy =>
{
    policy.WithOrigins(builder.Configuration["AngularURL"]).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
});

// make sure you add Authentication Middleware
// use for our Filter [Authorize]
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

