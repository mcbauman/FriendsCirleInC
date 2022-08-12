using System.Xml.Schema;
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
// Login
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
// Create User
    [HttpPost("/user/create")]
    public async Task<IActionResult> CreateUser([FromBody] UserClass reqBody)
    {
        try
        {
            _context.User.Add(reqBody);
            await _context.SaveChangesAsync();
            UserClass newUser = await _context.User.FirstAsync(x => x.Email == reqBody.Email);
            //UserClass newUser2 = await _context.User.FindAsync(reqBody.Id);
            return Ok(newUser.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

// Find users matching criteria: POST "/user/find"
    [HttpPost("/user/find")]
    public async Task<IActionResult> GetUsers([FromBody] SearchUserReqClass reqBody)
    {
        List<UserClass> users = await _context.Set<UserClass>().Where(x => x.Age > reqBody.MinAge).ToListAsync();
        if (reqBody.Gender != "any")
        {
            users = users.Where(x => x.Gender == reqBody.Gender).ToList();
        }
        if (reqBody.Interests!=null && reqBody.Interests.Length > 0)
        {
            users = users.Where(x => x.Interests.Contains(reqBody.Interests)).ToList();
        }
        //IQueryable<>
        //&& x.Gender!="any"?x.Age<reqBody.MaxAge && x.Gender== reqBody.Gender:
        return Ok(users);
    }
// Find Profile to update: GET "/user/updateProfile"

// Update Profile POST "/user/updateProfile"
// Delete Profile DELETE "/users/delete"
// check friends GET "/users/checkfriends"
// add an Friend put("/user/addFriend"
// Delete Friend put("/deleteFriend/"


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