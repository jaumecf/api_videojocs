using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sa24api.Models;
using sa24api.Services.PartidasLevel2;
using System.Security.Claims;

namespace sa24api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidasLevel2Controller : ControllerBase
    {
        private readonly IPartidasLevel2Service _partidasLevel2Service;

        public PartidasLevel2Controller(IPartidasLevel2Service partidasLevel2Service)
        {
            _partidasLevel2Service = partidasLevel2Service;
        }

        [HttpPost("InsertPartida"), Authorize]
        public async Task<ActionResult<Partida>> InsertPartida(Partida game)
        {
            var userId = int.Parse(User?.FindFirstValue(ClaimTypes.Sid));
            var partida = await _partidasLevel2Service.InsertPartidaAsync(userId, game.IdPartida);
            if (partida == null)
            {
                return BadRequest("Error inserting game for user " + game.UserId);
            }

            game.IdPartida = partida.IdPartida;
            game.Puntuacio_Final = partida.Puntuacio_Final;
            game.UserId = partida.UserId;
            return Ok(game);
        }

        [HttpPost("UpdatePuntuacionPartida"), Authorize]
        public async Task<ActionResult> UpdatePuntuacionPartida(Partida partida)
        {
            // Security check 
            var userId = int.Parse(User?.FindFirstValue(ClaimTypes.Sid));
            var _partida = await _partidasLevel2Service.GetPartidaAsync(partida.IdPartida);
            if (_partida == null)
            {
                return StatusCode(500, "Partida with Id = " + partida.IdPartida + " does not exist.");
            }

            if (_partida.UserId != userId)
            {
                return StatusCode(500, "Red Alert! Someone is trying to hack the scores!!");
            }

            int rowsUpdated = await _partidasLevel2Service.UpdatePuntuacionPartidaAsync(partida);

            if (rowsUpdated == 1)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, "Partida with Id = " + partida.IdPartida + " does not exist.");
            }
        }

        [HttpGet("GetClassificationLevel2")]
        public async Task<ActionResult<IEnumerable<Partida>>> GetClassificationLevel2()
        {
            var partidas = await _partidasLevel2Service.GetClassificationLevel2();
            return Ok(partidas);
        }

        [HttpPost("DeletePartidaLevel2"), Authorize]
        public async Task<ActionResult> DeletePartidaLevel2(int idPartida)
        {
            int rowsDeleted = await _partidasLevel2Service.DeletePartidaLevel2Async(idPartida);
            return Ok(rowsDeleted);
        }

    }
}
