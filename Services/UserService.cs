using MapTask.Data;
using MapTask.Models;
using MapTask.Services.Interfaces;

public class UserService : IUserService { private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public void RegisterUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public User? GetUserByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }
    public bool IsEmailTaken(string email)
    { 
        return _context.Users.Any(u => u.Email == email);
    }

}
