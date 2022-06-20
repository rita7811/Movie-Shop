using System;
namespace ApplicationCore.Contracts.Repository
{
	public interface IRepository<T> where T : class //generic repository: T will be replaced by our model entities
	{
		// basic methods:
		T GetById(int id); 

		IEnumerable<T> GetAll();

		T Add(T entity);

		T Update(T entity);

		T Delete(T entity);
	}
}

