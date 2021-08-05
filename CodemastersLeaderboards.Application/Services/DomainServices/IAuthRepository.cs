using CodemastersLeaderboards.Domain.Models;
using System.Threading.Tasks;

namespace CodemastersLeaderboards.Application.Services.DomainService
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
