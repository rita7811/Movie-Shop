using System;
using System.Security.Claims;

namespace MovieShopMVC.Services
{
    public class CurrentLogedInUser : ICurrentLogedInUser
    {
        // to access HttpContext inside a regular C# class, we need to inject IHttpContextAccessor (through Dependency Injection)
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentLogedInUser(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }



        public int UserId => Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        public string Email => _contextAccessor?.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

        public string FullName => _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname).Value + " " +
            _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName).Value;

        public IEnumerable<string> Roles => throw new NotImplementedException();

        public bool IsAuthenticated => _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
    }
}

