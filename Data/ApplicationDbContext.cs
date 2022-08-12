using Microsoft.EntityFrameworkCore;
    
using CSFriendCircle.Models;
using CSFriendCircle.Models;

namespace CSFriendCircle.Data;
    
public class ApplicationDbContext : DbContext
{
    //ctor
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }
    public DbSet<UserClass> User { get; set; }
}