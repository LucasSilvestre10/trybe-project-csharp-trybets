using TryBets.Users.Models;
using TryBets.Users.DTO;

namespace TryBets.Users.Repository;

public class UserRepository : IUserRepository
{
    protected readonly ITryBetsContext _context;
    public UserRepository(ITryBetsContext context)
    {
        _context = context;
    }

    public User Post(User user)
    {

        var addFound = _context.Users.Where(u => u.Email == user.Email);
        if (addFound.Count() > 0) throw new Exception("E-mail already used");
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }
    public User Login(AuthDTORequest login)
    {
        var userLogin = _context.Users.Where(user => user.Email == login.Email);
        if (userLogin.Count() == 0) throw new Exception("Authentication failed");
        if (userLogin.First().Password != login.Password) throw new Exception("Authentication failed");
        return userLogin.FirstOrDefault()!;
    }
}