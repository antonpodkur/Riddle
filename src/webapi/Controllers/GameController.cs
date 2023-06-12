using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Models.Requests;
using webapi.Models.Responses;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController: ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationContext _context;

    public GameController(UserManager<User> userManager, ApplicationContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpPost("saveGame")]
    public async Task<IActionResult> SaveGame([FromBody] SaveGameRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null)
        {
            user = new User()
            {
                UserName = request.Username,
                Game = new Game()
                {
                    Time = request.Time,
                    Slides = request.Slides
                }
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) return BadRequest();
        }
        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return BadRequest("Wrong password");
        }

        var game = await _context.Games.FirstOrDefaultAsync(g => g.UserId == user.Id);
        
        game.Time = request.Time;
        game.Slides = request.Slides;

        _context.Games.Update(game);
        
        await _context.SaveChangesAsync();
        return Ok(game);
    }

    [HttpGet("getBests")]
    public async Task<IActionResult> GetBests()
    {
        var users = await _context.Users.Include(u => u.Game).OrderBy(u => u.Game.Time).ThenBy(u => u.Game.Slides).Take(15)
            .ToListAsync();
        var response = new BestResultsResponse(users);
        return Ok(response);
    }
    
    
}