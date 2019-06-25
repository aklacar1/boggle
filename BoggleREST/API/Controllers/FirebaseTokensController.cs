using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoggleREST;

namespace BoggleREST.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirebaseTokensController : ControllerBase
    {
        private readonly BoggleContext _context;

        public FirebaseTokensController(BoggleContext context)
        {
            _context = context;
        }

        // GET: api/FirebaseTokens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FirebaseTokens>>> GetFirebaseTokens()
        {
            return await _context.FirebaseTokens.ToListAsync();
        }

        // GET: api/FirebaseTokens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FirebaseTokens>> GetFirebaseTokens(long id)
        {
            var firebaseTokens = await _context.FirebaseTokens.FindAsync(id);

            if (firebaseTokens == null)
            {
                return NotFound();
            }

            return firebaseTokens;
        }

        // PUT: api/FirebaseTokens/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFirebaseTokens(long id, FirebaseTokens firebaseTokens)
        {
            if (id != firebaseTokens.Id)
            {
                return BadRequest();
            }

            _context.Entry(firebaseTokens).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FirebaseTokensExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FirebaseTokens
        [HttpPost]
        public async Task<ActionResult<FirebaseTokens>> PostFirebaseTokens(FirebaseTokens firebaseTokens)
        {
            _context.FirebaseTokens.Add(firebaseTokens);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFirebaseTokens", new { id = firebaseTokens.Id }, firebaseTokens);
        }

        // DELETE: api/FirebaseTokens/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FirebaseTokens>> DeleteFirebaseTokens(long id)
        {
            var firebaseTokens = await _context.FirebaseTokens.FindAsync(id);
            if (firebaseTokens == null)
            {
                return NotFound();
            }

            _context.FirebaseTokens.Remove(firebaseTokens);
            await _context.SaveChangesAsync();

            return firebaseTokens;
        }

        private bool FirebaseTokensExists(long id)
        {
            return _context.FirebaseTokens.Any(e => e.Id == id);
        }
    }
}
