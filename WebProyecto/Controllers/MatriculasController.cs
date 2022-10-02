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
    public class MatriculasController : ApiController
    {
        private tiusr5pl_Proyecto1PrograVEntities db = new tiusr5pl_Proyecto1PrograVEntities();

        // GET: api/Matriculas
        public IQueryable<Matricula> GetMatriculas()
        {
            return db.Matriculas;
        }

        // GET: api/Matriculas/5
        [ResponseType(typeof(Matricula))]
        public async Task<IHttpActionResult> GetMatricula(string id)
        {
            Matricula matricula = await db.Matriculas.FindAsync(id);
            if (matricula == null)
            {
                return NotFound();
            }

            return Ok(matricula);
        }

        // PUT: api/Matriculas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMatricula(string id, Matricula matricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != matricula.Tipo_ID_Estudiante)
            {
                return BadRequest();
            }

            db.Entry(matricula).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatriculaExists(id))
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

        // POST: api/Matriculas
        [ResponseType(typeof(Matricula))]
        public async Task<IHttpActionResult> PostMatricula(Matricula matricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Matriculas.Add(matricula);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MatriculaExists(matricula.Tipo_ID_Estudiante))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = matricula.Tipo_ID_Estudiante }, matricula);
        }

        // DELETE: api/Matriculas/5
        [ResponseType(typeof(Matricula))]
        public async Task<IHttpActionResult> DeleteMatricula(string id)
        {
            Matricula matricula = await db.Matriculas.FindAsync(id);
            if (matricula == null)
            {
                return NotFound();
            }

            db.Matriculas.Remove(matricula);
            await db.SaveChangesAsync();

            return Ok(matricula);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MatriculaExists(string id)
        {
            return db.Matriculas.Count(e => e.Tipo_ID_Estudiante == id) > 0;
        }
    }
}