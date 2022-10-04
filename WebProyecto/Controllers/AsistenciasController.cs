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
    public class AsistenciasController : ApiController
    {
        private tiusr5pl_Proyecto1PrograVEntities db = new tiusr5pl_Proyecto1PrograVEntities();

        // GET: api/Asistencias
        public IQueryable<Asistencia> GetAsistencias()
        {
            return db.Asistencias;
        }

        // GET: api/Asistencias/5
        [ResponseType(typeof(Asistencia))]
        public async Task<IHttpActionResult> GetAsistencia(byte CodigoGrupo, string CodCurso, DateTime fecha, string tipoID,string ID)
        {
            Asistencia asistencia = await db.Asistencias.FindAsync(CodigoGrupo, CodCurso,fecha,tipoID,ID);
            if (asistencia == null)
            {
                return NotFound();
            }

            return Ok(asistencia);
        }

        // PUT: api/Asistencias/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAsistencia(byte id, Asistencia asistencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asistencia.Codigo_Grupo)
            {
                return BadRequest();
            }

            db.Entry(asistencia).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsistenciaExists(id))
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

        // POST: api/Asistencias
        [ResponseType(typeof(Asistencia))]
        public async Task<IHttpActionResult> PostAsistencia(Asistencia asistencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Asistencias.Add(asistencia);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AsistenciaExists(asistencia.Codigo_Grupo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = asistencia.Codigo_Grupo }, asistencia);
        }

        // DELETE: api/Asistencias/5
        [ResponseType(typeof(Asistencia))]
        public async Task<IHttpActionResult> DeleteAsistencia(byte CodigoGrupo, string CodCurso, string fecha, string tipoID, string ID)
        {
            Asistencia asistencia = await db.Asistencias.FindAsync(CodigoGrupo, CodCurso, fecha, tipoID, ID);
            if (asistencia == null)
            {
                return NotFound();
            }

            db.Asistencias.Remove(asistencia);
            await db.SaveChangesAsync();

            return Ok(asistencia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AsistenciaExists(byte id)
        {
            return db.Asistencias.Count(e => e.Codigo_Grupo == id) > 0;
        }
    }
}