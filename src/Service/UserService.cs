using ecommerce.EntityFramework;
using ecommerce.EntityFramework.Table;
using ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.service;

public class UserService
{
    private readonly AppDbContext _appDbContext;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        var users = await _appDbContext.Users
        
            .Include(u => u.Reviews)
            .Include(u => u.Orders)
            .Include(u => u.OrderItems)
            .ToListAsync();
        return users;
    }

    public async Task<User?> GetUserById(Guid id)
    {
        var foundUser = await _appDbContext
            .Users.Include(u => u.Reviews)
            .Include(u => u.Orders)
            .Include(u => u.OrderItems)
            .FirstOrDefaultAsync(user => user.UserId == id);
        return foundUser;
    }

    public async Task<User?> SignIn(string email, string password)
    {
        var allUsers = await _appDbContext.Users.ToListAsync();
        var loginUser =
            allUsers.FirstOrDefault(user =>
                user.Email!.Equals(email, StringComparison.OrdinalIgnoreCase)
            ) ?? throw new Exception("User with this email not exist");

        var result = _passwordHasher.VerifyHashedPassword(loginUser, loginUser.Password!, password);
        if (result == PasswordVerificationResult.Success)
        {
            return loginUser;
        }
        throw new Exception("Unauthorize Access, incorrect Password for this email");
    }
    

    public async Task<UserModel> AddUser(UserModel newUser)
    {
        Role userRole = newUser.Role == Role.Customer ? Role.Customer : Role.Admin;
        User user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = newUser.FullName,
            Email = newUser.Email,
            Phone = newUser.Phone,
            Password = newUser.Password,
            CreatedAt = DateTime.UtcNow,
            Role = userRole
        };
        //hash the password
        user.Password = _passwordHasher.HashPassword(user, user.Password!);
        await _appDbContext.Users.AddAsync(user);
        await _appDbContext.SaveChangesAsync();
        return newUser;
    }

    public async Task<User?> UpdateUser(Guid id, UserModel updateUser)
    {
        var foundUser = await _appDbContext.Users.FirstOrDefaultAsync(user => user.UserId == id);
        if (foundUser != null)
        {
            foundUser.FullName = updateUser.FullName ?? foundUser.FullName;
            foundUser.Email = updateUser.Email ?? foundUser.Email;
            foundUser.Phone = updateUser.Phone ?? foundUser.Phone;
            foundUser.Password = foundUser.Password;
        }
        await _appDbContext.SaveChangesAsync();
        return foundUser;
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        await Task.CompletedTask;
        var foundUser = await _appDbContext.Users.FirstOrDefaultAsync(user => user.UserId == id);
        if (foundUser != null)
        {
            _appDbContext.Users.Remove(foundUser);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
