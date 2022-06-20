using System;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories
{
	public interface ICastRepository : IRepository<Cast>
	{
		// some common methods such as GetById(int id)...
	}
}

