using MapTask.Models;

namespace MapTask.Services.Interfaces
{
    public interface IUserService {
        void RegisterUser(User user);
        User? GetUserByEmail(string email);
        bool IsEmailTaken(string email);
    }
}
