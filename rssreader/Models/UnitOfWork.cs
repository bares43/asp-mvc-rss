using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using rssreader.Models.Repository;

namespace rssreader.Models
{
    public class UnitOfWork
    {
        private readonly RssContext _context = new RssContext();
        private RepositoryFeed _repositoryFeed;
        private RepositoryItem _repositoryItem;

        public RepositoryFeed RepositoryFeed => _repositoryFeed ?? (_repositoryFeed = new RepositoryFeed(_context));
        public RepositoryItem RepositoryItem => _repositoryItem ?? (_repositoryItem = new RepositoryItem(_context));

        public int Save()
        {
            return _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}