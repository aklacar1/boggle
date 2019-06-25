using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoggleREST;
using BoggleREST.API.ServiceInterfaces;

namespace BoggleREST.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Firebase")]
    public class FirebaseTokensController : Controller
    {
        private readonly IFirebaseTokensService firebaseTokensService;
        public FirebaseTokensController(IFirebaseTokensService firebaseTokensService)
        {
            this.firebaseTokensService = firebaseTokensService;
        }

        [HttpPost]
        public async Task<IActionResult> PostFirebaseTokens(string token)
        {

            var success = firebaseTokensService.AddDeviceToken(token);
            if (!success) {
                return BadRequest();
            }

            return Ok();
        }


    }
}
