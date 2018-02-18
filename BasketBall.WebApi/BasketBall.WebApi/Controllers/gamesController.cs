using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BasketBall.Data.basketball;

namespace BasketBall.WebApi.Controllers
{
    public class gamesController : ApiController
    {
        private basketballEntities db = new basketballEntities();

        // GET: api/games
        public IQueryable<game> Getgames()
        {
            return db.games;
        }

        // GET: api/games/5
        [ResponseType(typeof(game))]
        public async Task<IHttpActionResult> Getgame(int id)
        {
            game game = await db.games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        // PUT: api/games/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putgame(int id, game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != game.id)
            {
                return BadRequest();
            }

            db.Entry(game).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!gameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/games
        [ResponseType(typeof(game))]
        public async Task<IHttpActionResult> Postgame(game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.games.Add(game);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = game.id }, game);
        }

        // DELETE: api/games/5
        [ResponseType(typeof(game))]
        public async Task<IHttpActionResult> Deletegame(int id)
        {
            game game = await db.games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            db.games.Remove(game);
            await db.SaveChangesAsync();

            return Ok(game);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool gameExists(int id)
        {
            return db.games.Count(e => e.id == id) > 0;
        }
    }
}