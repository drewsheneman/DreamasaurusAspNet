using System;

namespace Dreamasaurus.DAL
{
    public interface IUnitOfWork
    {
        DreamsRepository DreamsRepository { get; }
        void Save();
        void Dispose();
    }

    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly DreamsDbContext _context = new DreamsDbContext();
        private DreamsRepository _dreamsRepository;

        public DreamsRepository DreamsRepository => _dreamsRepository ?? (_dreamsRepository = new DreamsRepository(_context));

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}