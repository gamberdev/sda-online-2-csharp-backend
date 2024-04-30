using ecommerce.EF;
using ecommerce.Models;
using ecommerce.utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
                return NotFound(new ErrorResponse { Message = "There is no users" });
            }
            return Ok(
                new SuccessResponse<IEnumerable<User>>
                {
                    Message = "All users inside E-commerce system",
                    Data = users
                }
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ErrorResponse { Message = "There is an error on getting the users" }
            );
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
                return BadRequest(new ErrorResponse { Message = "The user not found" });
            }
            return Ok(new SuccessResponse<User> { Message = "User Detail", Data = found });
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ErrorResponse { Message = "There is an error on getting the user" }
            );
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(UserModel newUser)
    {
        try
        {
            await _userService.AddUser(newUser);
            // return CreatedAtAction(nameof(GetUserById), new { id = newUser.UserId }, newUser);
            return Ok(new SuccessResponse<UserModel> { Message = "The user is Added" });
        }
        catch (Exception)
        {
            return StatusCode(500, new ErrorResponse { Message = "Cannot add the user" });
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
                return NotFound(new ErrorResponse { Message = "The user not found" });
            }
            return Ok(new SuccessResponse<User> { Message = "User updated", Data = found });
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ErrorResponse { Message = "There is an error on updating user" }
            );
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
                return NotFound(new ErrorResponse { Message = "The user not found" });
            }
            return Ok(new SuccessResponse<bool> { Message = "User Deleted" });
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ErrorResponse { Message = "There is an error on deleting user" }
            );
        }
    }
}
