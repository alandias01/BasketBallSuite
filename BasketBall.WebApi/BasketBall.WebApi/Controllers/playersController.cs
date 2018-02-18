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
    public class playersController : ApiController
    {
        private basketballEntities db = new basketballEntities();

        // GET: api/players
        public IQueryable<player> Getplayers()
        {
            return db.players;
        }

        // GET: api/players/5
        [ResponseType(typeof(player))]
        public async Task<IHttpActionResult> Getplayer(int id)
        {
            player player = await db.players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        // PUT: api/players/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putplayer(int id, player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != player.id)
            {
                return BadRequest();
            }

            db.Entry(player).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!playerExists(id))
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

        // POST: api/players
        [ResponseType(typeof(player))]
        public async Task<IHttpActionResult> Postplayer(player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.players.Add(player);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = player.id }, player);
        }

        // DELETE: api/players/5
        [ResponseType(typeof(player))]
        public async Task<IHttpActionResult> Deleteplayer(int id)
        {
            player player = await db.players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            db.players.Remove(player);
            await db.SaveChangesAsync();

            return Ok(player);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool playerExists(int id)
        {
            return db.players.Count(e => e.id == id) > 0;
        }
    }
}