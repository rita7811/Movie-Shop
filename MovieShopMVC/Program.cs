using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MovieShopMVC.Middlewares;
using MovieShopMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// DI is FIRST class citizen in .NET CORE
// older .NET Framework DI was not built-in, we had to rely on 3rd part libraries, autofac, ninject
builder.Services.AddScoped<IMovieService, MovieService>();
//builder.Services.AddScoped<IMovieService, MovieTestService>();
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


// Inject HttpContext for IHttpContextAccessor interface
builder.Services.AddHttpContextAccessor();


// Inject the connection string into DbContext options constructor
builder.Services.AddDbContext<MovieShopDbContext>(options =>
{
    // get the connection string from app settings
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieShopDbConnection"));
});


// set Cookie based on Authentication information
// Cookie, JWT
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
        options =>
        {
            // specify settings for cookie
            options.Cookie.Name = "MovieShopAuthCookie";
            options.ExpireTimeSpan = TimeSpan.FromHours(2);
            options.LoginPath = "/account/login";  //if cookie is expired or cookie is invalidate, the user will be redirect to login page
        }
    );



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddlewareClassTemplate(); // call Extension method inside MovieShopExceptionMiddleware.cs
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication Middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

