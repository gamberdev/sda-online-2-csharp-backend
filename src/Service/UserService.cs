using ecommerce.EF;
using ecommerce.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

public class UserService
{
    private readonly AppDbContext _appDbContext;

    public UserService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        await Task.CompletedTask;
        var users = _appDbContext.Users.ToList();
        return users;
    }

    public async Task<User?> GetUserById(Guid id)
    {
        await Task.CompletedTask;
        var usersDb = _appDbContext.Users.ToList();
        var foundUser = usersDb.FirstOrDefault(user => user.UserId == id);
        return foundUser;
    }

    public async Task<UserModel> AddUser(UserModel newUser)
    {
        await Task.CompletedTask;
        User user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = newUser.FullName,
            Email = newUser.Email,
            Phone = newUser.Phone,
            Password = newUser.Password,
            CreatedAt = DateTime.UtcNow,
        };
        _appDbContext.Users.Add(user);
        _appDbContext.SaveChanges();
        return newUser;
    }

    public async Task<User?> UpdateUser(Guid id, UserModel updateUser)
    {
        await Task.CompletedTask;
        var usersDb = _appDbContext.Users.ToList();
        var foundUser = usersDb.FirstOrDefault(user => user.UserId == id);
        if (foundUser != null)
        {
            foundUser.FullName = updateUser.FullName ?? foundUser.FullName;
            foundUser.Email = updateUser.Email ?? foundUser.Email;
            foundUser.Phone = updateUser.Phone ?? foundUser.Phone;
            foundUser.Password = updateUser.Password ?? foundUser.Password;
        }
        _appDbContext.SaveChanges();
        return foundUser;
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        await Task.CompletedTask;
        var usersDb = _appDbContext.Users.ToList();
        var foundUser = usersDb.FirstOrDefault(user => user.UserId == id);
        if (foundUser != null)
        {
            _appDbContext.Users.Remove(foundUser);
            _appDbContext.SaveChanges();
            return true;
        }
        return false;
    }
}
