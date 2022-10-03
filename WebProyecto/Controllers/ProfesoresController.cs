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
using ProyectoPrograV;

namespace WebProyecto.Controllers
{
    public class ProfesoresController : ApiController
    {
        private tiusr5pl_Proyecto1PrograVEntities db = new tiusr5pl_Proyecto1PrograVEntities();

        // GET: api/Profesores
        public IQueryable<Profesore> GetProfesores()
        {
            return db.Profesores;
        }

        // GET: api/Profesores/5
        [ResponseType(typeof(Profesore))]
        public async Task<IHttpActionResult> GetProfesore(string tipoID,string id)
        {
            Profesore profesore = await db.Profesores.FindAsync(tipoID,id);
            if (profesore == null)
            {
                return NotFound();
            }

            return Ok(profesore);
        }

        // PUT: api/Profesores/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProfesore(string id, Profesore profesore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != profesore.Tipo_ID)
            {
                return BadRequest();
            }

            db.Entry(profesore).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesoreExists(id))
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

        // POST: api/Profesores
        [ResponseType(typeof(Profesore))]
        public async Task<IHttpActionResult> PostProfesore(Profesore profesore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Profesores.Add(profesore);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProfesoreExists(profesore.Tipo_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = profesore.Tipo_ID }, profesore);
        }

        // DELETE: api/Profesores/5
        [ResponseType(typeof(Profesore))]
        public async Task<IHttpActionResult> DeleteProfesore(string tipoID, string id)
        {
            Profesore profesore = await db.Profesores.FindAsync(tipoID,id);
            if (profesore == null)
            {
                return NotFound();
            }

            db.Profesores.Remove(profesore);
            await db.SaveChangesAsync();

            return Ok(profesore);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfesoreExists(string id)
        {
            return db.Profesores.Count(e => e.Tipo_ID == id) > 0;
        }
    }
}