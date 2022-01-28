using System.Threading.Tasks;

namespace GameLeaderboards.Application.Services.DomainService
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
