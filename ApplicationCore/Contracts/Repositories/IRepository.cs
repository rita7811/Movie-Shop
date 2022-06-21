using System;
namespace ApplicationCore.Contracts.Repository
{
	public interface IRepository<T> where T : class //generic repository: T will be replaced by our model entities
	{
		// basic methods:
		Task<T> GetById(int id);

        Task<IEnumerable<T>> GetAll();

        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task<T> Delete(T entity);
	}
}

