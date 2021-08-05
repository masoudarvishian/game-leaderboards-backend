using CodemastersLeaderboards.Application.Services.DomainService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodemastersLeaderboards.Infrastructure.Services.DomainService
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private Dictionary<string, object> Repositories { get; set; }

        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            Repositories = new Dictionary<string, object>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            var typeName = typeof(T).Name;
            if (Repositories.ContainsKey(typeName))
            {
                return Repositories[typeName] as IGenericRepository<T>;
            }

            IGenericRepository<T> repo = new GenericRepository<T>(_context);
            Repositories.Add(typeName, repo);
            return repo;
        }

        public int SaveChanges() => _context.SaveChanges();

        public async Task<int> SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}