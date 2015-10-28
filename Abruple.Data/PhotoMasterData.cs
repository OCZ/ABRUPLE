namespace Abruple.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using Contracts;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class PhotoMasterData : IPhotoMasterData
    {
        private readonly DbContext context;

        private readonly IDictionary<Type, object> repositories;

        public PhotoMasterData()
            : this(new PhotoMasterDbContext())
        {
        }

        public PhotoMasterData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        // USERS
        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        // USER ROLES
        public IRepository<IdentityRole> UserRoles
        {
            get
            {
                return this.GetRepository<IdentityRole>();
            }
        }

        // SAVE
        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        // GET REPOSITORY
        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(PhotoMasterRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
