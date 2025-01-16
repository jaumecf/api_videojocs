using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sa24api.Models;
using sa24api.Services.LeaderboardL1;
using System.Security.Claims;

namespace sa24api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardL1Controller : ControllerBase
    {
        private readonly ILeaderboardL1Service _leaderboardL1Service;

        public LeaderboardL1Controller(ILeaderboardL1Service leaderboardL1Service)
        {
            _leaderboardL1Service = leaderboardL1Service;
        }

        [HttpPost("InsertGameLevel1"), Authorize]
        public async Task<ActionResult<GameLevel1Dto>> InsertGameLevel1(GameLevel1Dto game)
        {
            var userId = int.Parse(User?.FindFirstValue(ClaimTypes.Sid));
            game.UserId = userId;
            int id = await _leaderboardL1Service.InsertGameLevel1Async(game);
            if (id == 0)
            {
                return BadRequest("Error inserting game for user " + game.UserId);
            }
            game.Id = id;
            return Ok(game);
        }

        //[Authorize]
        [HttpGet("GetClassificationLevel1")]
        public async Task<ActionResult<IEnumerable<GameLevel1Dto>>> GetClassificationLevel1()
        {
            var games = await _leaderboardL1Service.GetClassificationLevel1();
            return Ok(games);
        }

        [HttpPost("DeleteGameLevel1"), Authorize]
        public async Task<ActionResult> DeleteGameLevel1(int gameId)
        {
            int rowsDeleted = await _leaderboardL1Service.DeleteGameLevel1Async(gameId);
            return Ok(rowsDeleted);
        }
    }
}
