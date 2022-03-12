/*
 A repository is nothing but a class defined for an entity, with all the operations possible on that specific entity. 
For example, a repository for an entity Customer, will have basic CRUD operations and any other possible operations related to it. 
A Repository Pattern can be implemented in Following ways:

One repository per entity (non-generic) : This type of implementation involves the use of one repository class for each entity. 
For example, if you have two entities Order and Customer, each entity will have its own repository.

Generic repository: A generic repository is the one that can be used for all the entities, in other words it can be either used 
for Order or Customer or any other entity.

In this solution, we have been implementing the GENERIC REPOSITORY PATTERN
*/

using HotelListing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace HotelListing.IRepository
{
    // take a generic parameter where the parameter is a class
    // <T> = "I'm prepared to take a generic parameter"
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAll(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null
            );

        Task<IPagedList<T>> GetPagedList(
            RequestParams requestParams,
            List<string> includes = null
            );

        Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes);

        Task<T> GetByExpression(Expression<Func<T, bool>> expression);

        Task Insert(T entity);

        Task InsertRange(IEnumerable<T> entities);

        void Update(T entity);

        Task Delete(int id);

        void DeleteRange(IEnumerable<T> entities);
    }
}
