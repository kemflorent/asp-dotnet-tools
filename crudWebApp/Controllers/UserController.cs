using System.Collections.Generic;
using System.Linq;
using CrudWebApp.Interfaces;
using CrudWebApp.Models;
using CrudWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        return _userService.All();
    }

    [HttpGet("{id}")]
    public ActionResult<User?> GetUserById(int id)
    {
        var user = _userService.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        return user;
    }

    [HttpPost]
    public ActionResult<User> CreateUser([FromBody] User user)
    {
        ActionResult<User> newUser = _userService.Insert(user); 
        return CreatedAtAction(nameof(GetUserById), new { id = newUser.Value.Id }, newUser.Value);
    }

    [HttpPut]
    public ActionResult<User> UpdateUser([FromBody] User updatedUser)
    {
        return _userService.Update(updatedUser);
    }

    /// <summary>
    /// Deletes a specific user
    /// </summary>
    /// <param name="id"> </param>
    /// <returns> </returns>
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        _userService.Delete(id);  
        return NoContent();
    }

}