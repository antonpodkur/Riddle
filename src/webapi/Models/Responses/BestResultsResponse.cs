namespace webapi.Models.Responses;

public class Result
{
   public string Username { get; set; } 
   public int Time { get; set; }
   public int Slides { get; set; }
}

public class BestResultsResponse
{
   public List<Result> Results { get; set; }

   public BestResultsResponse(List<User> users)
   {
      Results = new List<Result>();
      foreach (var user in users)
      {
         Results.Add(new Result()
         {
            Username = user.UserName,
            Time = user.Game.Time,
            Slides = user.Game.Slides
         });
      }
   }
}