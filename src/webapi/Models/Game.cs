namespace webapi.Models;

public class Game
{
    public Guid Id { get; set; }
    public int Time { get; set; }
    public int Slides { get; set; }
    public Guid UserId { get; set; }
}