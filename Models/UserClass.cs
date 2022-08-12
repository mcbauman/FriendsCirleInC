using System.ComponentModel.DataAnnotations;

namespace CSFriendCircle.Models;

public class UserClass
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string FamilyName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Street { get; set; }
    [Required]
    public int Number { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public int ZipCode { get; set; }
    [Required]
    public string Country { get; set; }
    public double? Latitude { get; set; }
    public double? Longetude { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int? Age { get; set; }
    [Required]
    public string Gender { get; set; }
    public List<string>? Interests { get; set; } 
    public string? ProfileText { get; set; }
    public List<int>? Friends { get; set; }
    public bool EmailVerified { get; set; } = false;
    public int Score { get; set; } = 0;
    public long? ProfilePicture { get; set; }
    [MaxLength(2)]
    public string Lang { get; set; } = "De";
    [MaxLength(2)]
    public string theme { get; set; } = "BW";
}