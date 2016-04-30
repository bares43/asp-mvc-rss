using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using rssreader.Models.Entities;

namespace rssreader.Models.Repository
{
    public class Repository<T> : IDisposable where T : class, IEntity
    {
        protected RssContext context = null;
        private bool disposed = false;

        protected DbSet<T> DbSet
        {
            get; set;
        }

        public Repository(RssContext context)
        {
            this.context = context;
            DbSet = context.Set<T>();
        }

        public IList<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            context.Entry<T>(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public void DeleteAll(IList<int> items)
        {
            foreach (var item in items)
            {
                DbSet.Remove(DbSet.Find(item));
            }
        }

        public int Count()
        {
            return DbSet.Count();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            if (!disposed)
            {
                context.Dispose();
                disposed = true;
            }
        }
    }
}