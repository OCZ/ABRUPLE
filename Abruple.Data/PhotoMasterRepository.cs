namespace Abruple.Data
{
    using System.Data.Entity;
    using System.Linq;

    using Contracts;

    public class PhotoMasterRepository<T> : IRepository<T>
        where T : class 
    {
        private readonly DbContext context;
        private readonly IDbSet<T> set;

        public PhotoMasterRepository()
            : this(new PhotoMasterDbContext())
        {
        }

        public PhotoMasterRepository(DbContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        }

        // DISPOSE
        public void Dispose()
        {
            this.context.Dispose();
        }

        // ALL
        public IQueryable<T> All()
        {
            return this.set;
        }

        // FIND
        public T Find(object id)
        {
            return this.set.Find(id);
        }

        // ADD
        public void Add(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Added);
        }

        // UPDATE
        public void Update(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Modified);
        }

        // DELETE
        public void Delete(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Deleted);
        }

        // DELETE ID
        public void Delete(object id)
        {
            this.Delete(this.Find(id));
        }

        // SAVE
        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        // CHANGE ENTITY STATE
        private void ChangeEntityState(T entity, EntityState state)
        {
            var entry = this.context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }

            entry.State = state;
        }
    }
}
