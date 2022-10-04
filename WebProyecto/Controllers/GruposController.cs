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
    public class GruposController : ApiController
    {
        private tiusr5pl_Proyecto1PrograVEntities db = new tiusr5pl_Proyecto1PrograVEntities();

        // GET: api/Grupos
        public IQueryable<Grupos> GetGrupos()
        {
            return db.Grupos;
        }

        // GET: api/Grupos/5
        [ResponseType(typeof(Grupos))]
        public async Task<IHttpActionResult> GetGrupos(byte id,string Codigo )
        {
            Grupos grupos = await db.Grupos.FindAsync(id,Codigo);
            if (grupos == null)
            {
                return NotFound();
            }

            return Ok(grupos);
        }

        // PUT: api/Grupos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGrupos(byte id, Grupos grupos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grupos.Numero_Grupo)
            {
                return BadRequest();
            }

            db.Entry(grupos).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GruposExists(id))
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

        // POST: api/Grupos
        [ResponseType(typeof(Grupos))]
        public async Task<IHttpActionResult> PostGrupos(Grupos grupos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Grupos.Add(grupos);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GruposExists(grupos.Numero_Grupo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = grupos.Numero_Grupo }, grupos);
        }

        // DELETE: api/Grupos/5
        [ResponseType(typeof(Grupos))]
        public async Task<IHttpActionResult> DeleteGrupos(byte id)
        {
            Grupos grupos = await db.Grupos.FindAsync(id);
            if (grupos == null)
            {
                return NotFound();
            }

            db.Grupos.Remove(grupos);
            await db.SaveChangesAsync();

            return Ok(grupos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GruposExists(byte id)
        {
            return db.Grupos.Count(e => e.Numero_Grupo == id) > 0;
        }
    }
}