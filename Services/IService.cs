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
        /// Deletes an entity by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        Task Delete(int id);

        /// <summary>
        /// Deletes an entity by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        Task Add(T obj);

        /// <summary>
        /// Retrieves an entity asynchronously by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>A Task that represents the asynchronous operation. The task result contains the entity.</returns>
        Task<List<T>> GetAsync();

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);
    }
}
