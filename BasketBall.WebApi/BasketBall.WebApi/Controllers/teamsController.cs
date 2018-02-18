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
    public class teamsController : ApiController
    {
        private basketballEntities db = new basketballEntities();

        // GET: api/teams
        public IQueryable<team> Getteams()
        {
            return db.teams;
        }

        // GET: api/teams/5
        [ResponseType(typeof(team))]
        public async Task<IHttpActionResult> Getteam(int id)
        {
            team team = await db.teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        // PUT: api/teams/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putteam(int id, team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != team.id)
            {
                return BadRequest();
            }

            db.Entry(team).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!teamExists(id))
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

        // POST: api/teams
        [ResponseType(typeof(team))]
        public async Task<IHttpActionResult> Postteam(team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.teams.Add(team);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = team.id }, team);
        }

        // DELETE: api/teams/5
        [ResponseType(typeof(team))]
        public async Task<IHttpActionResult> Deleteteam(int id)
        {
            team team = await db.teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            db.teams.Remove(team);
            await db.SaveChangesAsync();

            return Ok(team);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool teamExists(int id)
        {
            return db.teams.Count(e => e.id == id) > 0;
        }
    }
}