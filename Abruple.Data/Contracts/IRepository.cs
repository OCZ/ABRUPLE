namespace Abruple.Data.Contracts
{
    using System;
    using System.Linq;

    public interface IRepository<T> : IDisposable
        where T : class
    {
        // ALL
        IQueryable<T> All();

        // FIND
        T Find(object id);

        // ADD
        void Add(T entity);

        // UPDATE
        void Update(T entity);

        // DELETE
        void Delete(T entity);

        // DELETE BY ID
        void Delete(object id);

        // SAVE
        int SaveChanges();
    }
}
