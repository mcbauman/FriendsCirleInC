namespace CSFriendCircle.Models;

public class SearchUserReqClass
{
    public int MinAge { get; set; }
    public int MaxAge { get; set; } = 150;
    public string Gender { get; set; } = "any";
    public string? Interests { get; set; }
}