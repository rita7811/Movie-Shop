using System;
using ApplicationCore.Contracts.Repository;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        // make it protected to allow this dbContext to be available in all our derived classes(derived from Repository<T>)
        // dbContext is a class that we can use to access our database
        protected readonly MovieShopDbContext _dbContext;

        public Repository(MovieShopDbContext dbContext)  //ctor ->constructor
        {
            _dbContext = dbContext;
        }


        public T Add(T entity)
        {
            throw new NotImplementedException();
        }

        public T Delete(T entity)
        {
            throw new NotImplementedException();
        }

        // make it virtual to allow us have option of overriding
        public virtual IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        // make it virtual to allow us have option of overriding
        public virtual T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public T Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

