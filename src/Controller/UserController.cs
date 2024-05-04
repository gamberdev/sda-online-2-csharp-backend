using ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecommerce.EntityFramework;
using ecommerce.EntityFramework.Table;
using ecommerce.service;
using ecommerce.utils;


namespace ecommerce.Controller;

[ApiController]
[Route("/users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(AppDbContext appDbContext)
    {
        _userService = new UserService(appDbContext);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var users = await _userService.GetUsers();
            if (users.Count() <= 0)
            {
                return ApiResponse.NotFound("There is no users");
            }
            return ApiResponse.Success(users, "All users inside E-commerce system");
        }
        catch (Exception)
        {
            return ApiResponse.ServerError("There is an error on getting the users");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        try
        {
            var found = await _userService.GetUserById(id);
            if (found == null)
            {
                return ApiResponse.BadRequest("The user not found");
            }
            return ApiResponse.Success(found, "User Detail");
        }
        catch (Exception)
        {
            return ApiResponse.ServerError("There is an error on getting the user");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(UserModel newUser)
    {
        try
        {
            await _userService.AddUser(newUser);
            return ApiResponse.Created(newUser, "The user is Added");
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is Npgsql.PostgresException postgresException)
        {
            if (postgresException.SqlState == "23505")
            {
                return ApiResponse.Conflict("Duplicate email. User with email already exists");
            }
            return ApiResponse.ServerError("Cannot add the user");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, UserModel updateData)
    {
        try
        {
            var found = await _userService.UpdateUser(id, updateData);
            if (found == null)
            {
                return ApiResponse.NotFound("The user not found");
            }
            return ApiResponse.Success(found, "User updated");
        }
        catch (Exception)
        {
            return ApiResponse.ServerError("There is an error on updating user");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            var deleted = await _userService.DeleteUser(id);
            if (!deleted)
            {
                return ApiResponse.NotFound("The user not found");
            }
            return ApiResponse.Success(id, "User Deleted");
        }
        catch (Exception)
        {
            return ApiResponse.ServerError("There is an error on deleting user");
        }
    }
}
