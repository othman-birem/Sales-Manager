using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_Manager.Services
{
    internal interface IService<T>
    {
        /// <summary>
        /// Deletes an entity asyncronously.
        /// </summary>
        /// <param name="obj">The object targeted by the process.</param>
        Task Delete(T obj);

        /// <summary>
        /// Inserts an object to the database asyncronously.
        /// </summary>
        /// <param name="obj">The object targeted by the process.</param>
        Task<T> Add(T obj);

        /// <summary>
        /// Retrieves all entities asynchronously from the database.
        /// </summary>
        /// <returns>A Task that represents the asynchronous operation. The task result contains the list entity.</returns>
        Task<List<T>> GetAsync();

        /// <summary>
        /// Retrieves an entity.
        /// </summary>
        /// <returns>The list containing all of the entities.</returns>
        List<T> Get();

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);
    }
}
