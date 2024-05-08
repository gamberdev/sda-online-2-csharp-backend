using ecommerce.EntityFramework;
using ecommerce.Models;
using ecommerce.service;
using ecommerce.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Controller;

[ApiController]
[Route("/users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly AuthService _authService;

    public UserController(AppDbContext appDbContext, AuthService authService)
    {
        _userService = new UserService(appDbContext);
        _authService = authService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
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
        catch (UnauthorizedAccessException)
        {
            return ApiResponse.Forbidden("Only admin can visit this route");
        }
        catch (Exception)
        {
            return ApiResponse.ServerError("There is an error on getting the users");
        }
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin")]
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

    [HttpPost("signIn")]
    public async Task<IActionResult> SignIn(string email, string password)
    {
        try
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return ApiResponse.BadRequest("User email and password required");
            }

            var userSignIn = await _userService.SignIn(email, password);
            var token = _authService.GenerateJwt(userSignIn!);
            Console.WriteLine($"{token}");

            return ApiResponse.Success(new { token, userSignIn }, "User is SignIn successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.UnAuthorized(ex.Message);
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
                return ApiResponse.Conflict(
                    "Duplicate email/phone. User with email/phone already exists"
                );
            }
            return ApiResponse.ServerError("Cannot add the user");
        }
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    [Authorize(Policy = "RequiredNotBanned")]
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

    [HttpDelete("{id:guid}")]
    [Authorize]
    [Authorize(Policy = "RequiredNotBanned")]
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
