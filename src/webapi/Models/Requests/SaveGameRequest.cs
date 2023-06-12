namespace webapi.Models.Requests;

public class SaveGameRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public int Time { get; set; }
    public int Slides { get; set; }
}