using System;
using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
	public class CastController : Controller
	{

        // call CastService -> inject
        private readonly ICastService _castService;

        public CastController(ICastService castService)
        {
            _castService = castService;
        }



        // method: showing details of the cast

        public async Task<IActionResult> Details(int id)
        {
            var cast = await _castService.GetCastDetails(id);
            return View(cast);
        }


    }
}

