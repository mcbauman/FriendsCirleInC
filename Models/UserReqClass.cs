namespace CSFriendCircle.Models;

public class UserReqClass
{
    public int? id { get; set; }
    public string? Name { get; set; }
    public string? FamilyName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? UserName { get; set; }
    public string? Street { get; set; }
    public int? Number { get; set; }
    public string? City { get; set; }
    public int? ZipCode { get; set; }
    public string? Country { get; set; }
    public double? Latitude { get; set; }
    public double? Longetude { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public string[]? Interests { get; set; }
    public string? ProfileText { get; set; }
    public int[]? Friends { get; set; }
    public bool? EmailVerified { get; set; } = false;
    public int? Score { get; set; } = 0;
    public long? ProfilePicture { get; set; }
    public string? Lang { get; set; } = "De";
    public string? theme { get; set; } = "BW";
}