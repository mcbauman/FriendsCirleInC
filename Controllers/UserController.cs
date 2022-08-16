using CSFriendCircle.Data;
using CSFriendCircle.Models;
using CSFriendCircle.Security;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace CSFriendCircle.Controllers;

public class UserController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly PasswordHandler _hashPassword;
    private readonly IConfiguration _configuration;

    public UserController(ApplicationDbContext context,PasswordHandler hashPassword,IConfiguration configuration)
    {
        _context = context;
        _hashPassword = hashPassword;    
        _configuration = configuration;
    }
    
// CreateToken
    public string CreateToken(UserClass user)
    {
        List<Claim> claims = new List<Claim> 
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
        };
        var key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires:DateTime.Now.AddDays(1),
            signingCredentials:cred);
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
// Login
    [HttpPost("/user/login")]
    public async Task<IActionResult> Login([FromBody] UserReqClass reqBody)
    {
        try
        {
            UserClass currentUser = await _context.User.FirstAsync(x => x.Email == reqBody.Email);
            if (!_hashPassword.PasswordMatch(reqBody.Password,currentUser.Password))
            {
                return Ok("Password missmatch");
            }
            return Ok(CreateToken(currentUser));
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
            reqBody.Password = _hashPassword.HashPassword(reqBody.Password);
            _context.User.Add(reqBody);
            await _context.SaveChangesAsync();
            UserClass newUser = await _context.User.FirstAsync(x => x.Email == reqBody.Email);
            return Ok(CreateToken(newUser));
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
    [HttpGet("user/updateProfile")]
    public async Task<IActionResult> GetUserToUpdate([FromBody] SearchUserReqClass reqBody)
    {
        return Ok(reqBody);
    }
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