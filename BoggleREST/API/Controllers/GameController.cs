using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoggleREST.API.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoggleREST.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Game")]
    public class GameController : Controller
    {

        private readonly IGameService gameService;

        public GameController(IGameService gameService) {
            this.gameService = gameService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateGameRoom()
        {

            var result = gameService.CreateGameRoom();
            return Ok(result);
        }

        [HttpGet("GetGameRoomLettersByRoomId/{roomId}")]
        public async Task<IActionResult> GetGameRoomLettersByRoomId(long roomId)
        {

            var result = gameService.GetGameRoomLettersByRoomId(roomId);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("GetGameRoomParticipantsByRoomId/{roomId}")]
        public async Task<IActionResult> GetGameRoomParticipantsByRoomId(long roomId)
        {

            var result = gameService.GetGameRoomParticipantsByRoomId(roomId);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPatch("JoinGameRoom")]
        public async Task<IActionResult> JoinGameRoom(long roomId)
        {

            var result = gameService.JoinGameRoom(roomId);
            return Ok(result);
        }

        [HttpPatch("StartGame")]
        public async Task<IActionResult> StartGame(long roomId)
        {

            var result = gameService.StartGame(roomId);
            return Ok(result);
        }

        [HttpPost("AddWord")]
        public async Task<IActionResult> AddWord(long roomId, string word)
        {

            var result = gameService.AddWord(roomId, word);
            return Ok(result);
        }

    }
}