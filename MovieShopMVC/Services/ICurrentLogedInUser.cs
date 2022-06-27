using System;
namespace MovieShopMVC.Services
{
	public interface ICurrentLogedInUser
	{
        // properties

        int UserId { get; }  //just reading

        string Email { get; }

        string FullName { get; }

        IEnumerable<string> Roles { get; }

        bool IsAuthenticated { get; }
    }
}

