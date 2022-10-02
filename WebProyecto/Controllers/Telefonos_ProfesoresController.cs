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
    public class Telefonos_ProfesoresController : ApiController
    {
        private tiusr5pl_Proyecto1PrograVEntities db = new tiusr5pl_Proyecto1PrograVEntities();

        // GET: api/Telefonos_Profesores
        public IQueryable<Telefonos_Profesores> GetTelefonos_Profesores()
        {
            return db.Telefonos_Profesores;
        }

        // GET: api/Telefonos_Profesores/5
        [ResponseType(typeof(Telefonos_Profesores))]
        public async Task<IHttpActionResult> GetTelefonos_Profesores(int id)
        {
            Telefonos_Profesores telefonos_Profesores = await db.Telefonos_Profesores.FindAsync(id);
            if (telefonos_Profesores == null)
            {
                return NotFound();
            }

            return Ok(telefonos_Profesores);
        }

        // PUT: api/Telefonos_Profesores/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTelefonos_Profesores(int id, Telefonos_Profesores telefonos_Profesores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != telefonos_Profesores.Numero_Telefono)
            {
                return BadRequest();
            }

            db.Entry(telefonos_Profesores).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Telefonos_ProfesoresExists(id))
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

        // POST: api/Telefonos_Profesores
        [ResponseType(typeof(Telefonos_Profesores))]
        public async Task<IHttpActionResult> PostTelefonos_Profesores(Telefonos_Profesores telefonos_Profesores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Telefonos_Profesores.Add(telefonos_Profesores);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Telefonos_ProfesoresExists(telefonos_Profesores.Numero_Telefono))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = telefonos_Profesores.Numero_Telefono }, telefonos_Profesores);
        }

        // DELETE: api/Telefonos_Profesores/5
        [ResponseType(typeof(Telefonos_Profesores))]
        public async Task<IHttpActionResult> DeleteTelefonos_Profesores(int id)
        {
            Telefonos_Profesores telefonos_Profesores = await db.Telefonos_Profesores.FindAsync(id);
            if (telefonos_Profesores == null)
            {
                return NotFound();
            }

            db.Telefonos_Profesores.Remove(telefonos_Profesores);
            await db.SaveChangesAsync();

            return Ok(telefonos_Profesores);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Telefonos_ProfesoresExists(int id)
        {
            return db.Telefonos_Profesores.Count(e => e.Numero_Telefono == id) > 0;
        }
    }
}