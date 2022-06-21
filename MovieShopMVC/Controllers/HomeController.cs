using System.Diagnostics;
using ApplicationCore.Contracts.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;

namespace MovieShopMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    //to refactor code "var movieService = new MovieService();"
    //by moving the instantiation from individual methods to much higher level class
    private readonly IMovieService _movieService;
    // depend on higher level abstraction (-> to create abstractions in our contracts -> using interface)

    public HomeController(ILogger<HomeController> logger, IMovieService movieService)
    {
        _logger = logger;
        //_movieService = new MovieService();
        // we want to have control over which implementation that we want to use
        _movieService = movieService;
        // var homeController = new HomeController();
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // display our home page
        // top 30 movies -> Movie Service

        // create instance of MovieService class (in order to call Movie Service to get information)
        // newing up -> avoid use it -> refactor this code
        //var movieService = new MovieService();
        //var movies = movieService.GetTopGrossingMovies();

        var movies = await _movieService.GetTopGrossingMovies();

        // method(int x, IMovieService service);

        // var movieService = new MovieService();
        // var movieService3 = new MovieTestService();

        // method(3, movieService3);

        // passing the data(movies) from Controller/action method to the View
        return View(movies);   // Not getting any compile time error here -> b/c the object is a base class for all our types in C# and dotnet
                               // we can pass anything if we have an object as a parameter.
    }

    [HttpGet]
    public IActionResult TopRatedMovies()
    {
        // this is going to call the movie service
        // movie service will call movie repository (delimited business project)
        // movie repository will call the database to get the data (delimited database project)
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

