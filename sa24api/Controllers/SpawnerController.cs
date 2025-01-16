using Microsoft.AspNetCore.Mvc;
using sa24api.Models;
using sa24api.Services.Events;
using System;
using System.Reflection;


namespace sa24api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpawnerController : ControllerBase
    {
        private readonly IEventsService _eventsService;
        private readonly ILogger<SpawnerController> _logger;

        public SpawnerController(ILogger<SpawnerController> logger, IEventsService eventsService)
        {
            _logger = logger;
            _eventsService = eventsService;
        }

        [HttpPost("GetSpawner/{versio}")]
        public ActionResult<Vector3> GetRandomPosition(string versio,PlayerInfo playerInfo)
        {
            Vector3 vector1 = new Vector3(84, 1, 41);

            Vector3 vector2 = new Vector3(-53, 1, 89);

            Vector3 vector3 = new Vector3(-79, 1, -54);

            Vector3[] vectors = new[]
            { vector1, vector2, vector3 };

            if (versio == "v0")
            {
                var rng = new Random();
                return Ok(vectors[rng.Next(vectors.Length)]);
            }
            else if (versio == "v1")
            {
                int iSelected = 0;

                double tempAngle = 0;

                for (int i = 0; i < vectors.Length; i++)
                {
                    //Vector3 vector4 = new Vector3(playerInfo.posX +5 * playerInfo.dirX + 1, 1, playerInfo.posZ + 5 * playerInfo.dirZ);

                    Vector3 vector5 = new Vector3(vectors[i].X - playerInfo.PosX, 1, vectors[i].Z - playerInfo.PosZ); //destino menos origen

                    double angle1 = Math.Abs(Math.Atan2(playerInfo.DirZ, playerInfo.DirX));
                    double moduloA = Math.Sqrt((vector5.X * vector5.X) + (vector5.Z * vector5.Z));
                    double moduloB = Math.Sqrt((playerInfo.DirX * playerInfo.DirX) + (playerInfo.DirZ * playerInfo.DirZ));
                    double angle = Math.Abs((vector5.X * playerInfo.DirX + vector5.Z * playerInfo.DirZ) / (moduloA * moduloB));

                    if (tempAngle < angle)
                    {
                        tempAngle = angle;
                        iSelected = i;
                    }
                    System.Diagnostics.Debug.WriteLine(angle * (180 / Math.PI) + " " + tempAngle * (180/Math.PI));
                }

                Event _event = new Event();
                _event.idPartida = playerInfo.PartidaCreada;
                _event.pos_PlayerX = playerInfo.PosX;
                _event.pos_PlayerY = playerInfo.PosY;
                _event.pos_PlayerZ = playerInfo.PosZ;
                _event.rot_PlayerX = playerInfo.DirX;
                _event.rot_PlayerY = playerInfo.DirY;
                _event.rot_PlayerZ = playerInfo.DirZ;
                _event.enemicX = vectors[iSelected].X;
                _event.enemicY = vectors[iSelected].Y;
                _event.enemicZ = vectors[iSelected].Z;


               _eventsService.InsertEventDimoniCreat(_event);
                return Ok(vectors[iSelected]);
            }
            else
            {
                return BadRequest("versio no implementada");
            }
        }
    }
}
