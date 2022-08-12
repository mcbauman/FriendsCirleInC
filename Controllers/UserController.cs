using CSFriendCircle.Data;
using CSFriendCircle.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSFriendCircle.Controllers;

public class UserController : Controller
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("/user/login")]
    public async Task<IActionResult> Login([FromBody] UserReqClass reqBody)
    {
        try
        {
            UserClass currentUser = await _context.User.FirstAsync(x => x.Email == reqBody.Email);
            if (currentUser.Password != reqBody.Password)
            {
                return Ok("Password missmatch");
            }
            return Ok(currentUser.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("/user/create")]
    public async Task<IActionResult> CreateUser([FromBody] UserClass reqBody)
    {
        try
        {
            _context.User.Add(reqBody);
            await _context.SaveChangesAsync();
            UserClass newUser = await _context.User.FirstAsync( x => x.Email == reqBody.Email);
            //UserClass newUser2 = await _context.User.FindAsync(reqBody.Id);
            return Ok(newUser.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
// {
// "Name":"Matthias",
// "FamilyName":"Baumann",
// "Email":"maze@online.de",
// "Password":"aA@123456",
// "UserName":"Maze",
// "Street":"Harksheider Str.",
// "Number":10,
// "ZipCode":22399,
// "City":"HAmburg",
// "Country":"DE",
// "DateOfBirth":"1984-04-18T00:00:00.000Z",
// "Gender":"Male"
// }