using System;
using ApplicationCore.Contracts.Repository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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


        public async Task<T> Add(T entity)
        {
            // to save anything

            _dbContext.Set<T>().Add(entity); //Add() method: add var new User entity to user db set (add them to memory)
            await _dbContext.SaveChangesAsync(); //***SaveChangesAsync() method: actually to call the database and save them

            return entity;
        }

        public Task<T> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll()
        {

            // to get all the records from the table
            // we can use this method for getting all the list of Genres

            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(T entity)
        {
            throw new NotImplementedException();
        }







        //public T Add(T entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public T Delete(T entity)
        //{
        //    throw new NotImplementedException();
        //}

        //// make it virtual to allow us have option of overriding
        //public virtual IEnumerable<T> GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        //// make it virtual to allow us have option of overriding
        //public virtual T GetById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public T Update(T entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

