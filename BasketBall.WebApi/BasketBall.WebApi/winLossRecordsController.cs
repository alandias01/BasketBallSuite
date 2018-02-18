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

namespace BasketBall.WebApi
{
    public class winLossRecordsController : ApiController
    {
        private basketballEntities db = new basketballEntities();

        // GET: api/winLossRecords
        public IQueryable<winLossRecord> GetwinLossRecords()
        {
            return db.winLossRecords;
        }

        // GET: api/winLossRecords/5
        [ResponseType(typeof(winLossRecord))]
        public async Task<IHttpActionResult> GetwinLossRecord(int id)
        {
            winLossRecord winLossRecord = await db.winLossRecords.FindAsync(id);
            if (winLossRecord == null)
            {
                return NotFound();
            }

            return Ok(winLossRecord);
        }

        // PUT: api/winLossRecords/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutwinLossRecord(int id, winLossRecord winLossRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != winLossRecord.id)
            {
                return BadRequest();
            }

            db.Entry(winLossRecord).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!winLossRecordExists(id))
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

        // POST: api/winLossRecords
        [ResponseType(typeof(winLossRecord))]
        public async Task<IHttpActionResult> PostwinLossRecord(winLossRecord winLossRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.winLossRecords.Add(winLossRecord);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = winLossRecord.id }, winLossRecord);
        }

        // DELETE: api/winLossRecords/5
        [ResponseType(typeof(winLossRecord))]
        public async Task<IHttpActionResult> DeletewinLossRecord(int id)
        {
            winLossRecord winLossRecord = await db.winLossRecords.FindAsync(id);
            if (winLossRecord == null)
            {
                return NotFound();
            }

            db.winLossRecords.Remove(winLossRecord);
            await db.SaveChangesAsync();

            return Ok(winLossRecord);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool winLossRecordExists(int id)
        {
            return db.winLossRecords.Count(e => e.id == id) > 0;
        }
    }
}