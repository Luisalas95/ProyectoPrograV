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
using WebProyecto.Models;

namespace WebProyecto.Controllers
{
    public class CursoesController : ApiController
    {
        private tiusr5pl_Proyecto1PrograVEntities db = new tiusr5pl_Proyecto1PrograVEntities();

        // GET: api/Cursoes
        public IQueryable<Curso> GetCursos()
        {
            return db.Cursos;
        }

        // GET: api/Cursoes/5
        [ResponseType(typeof(Curso))]
        public async Task<IHttpActionResult> GetCurso(string id)
        {
            Curso curso = await db.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            return Ok(curso);
        }

        // PUT: api/Cursoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCurso(string id, Curso curso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != curso.Codigo_Curso)
            {
                return BadRequest();
            }

            db.Entry(curso).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(id))
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

   


        [HttpPost]
        [Route("api/Cursoes/CrearCurso")]
        [ResponseType(typeof(cursos))]
        public async Task<IHttpActionResult> Crearcurso
        ([FromBody] cursos c)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Validar que la carrera exista para poder ligarla al curso
            if (!CarreraExists(c.codigocarrera))
            {
                return NotFound();
            }
            Curso C1 = new Curso()
            {
                Codigo_Carrera = c.codigocarrera,
                Nombre_Curso = c.nombrecurso,
                Codigo_Curso = c.codigocurso,

            };


            db.Cursos.Add(C1);

            try
            {
                await db.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { Controller = "Cursoes", id = C1.Codigo_Curso }, C1);
            }
            //validar que el curso no este repetido
            catch (DbUpdateException)
            {
                if (CursoExists(c.codigocurso))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

         

        }




        // DELETE: api/Cursoes/5
        [ResponseType(typeof(Curso))]
        public async Task<IHttpActionResult> DeleteCurso(string id)
        {
            Curso curso = await db.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            db.Cursos.Remove(curso);
            await db.SaveChangesAsync();

            return Ok(curso);
        }
        //------------------------- getCursosPorCarrera

        [Route("api/Cursoes/CursosPorCarrera")]
        [HttpGet]
        public async Task<IHttpActionResult> getCursosPorCarrera(string CodigoCarrera)
        {
            Carrera carrera = await db.Carreras.FindAsync(CodigoCarrera);

            if (carrera == null)
            {
                return NotFound();
            }

            var idQuery =
           from ord1 in db.Cursos
           from ord in db.Carreras
           where CodigoCarrera == ord.Codigo_Carrera && CodigoCarrera == ord1.Codigo_Carrera 
           select new { ord.Codigo_Carrera,ord.Nombre_Carrera,ord1.Codigo_Curso,ord1.Nombre_Curso};



            return Ok(idQuery);
        }

        //------------------------------


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CursoExists(string id)
        {
            return db.Cursos.Count(e => e.Codigo_Curso == id) > 0;
        }


        private bool CarreraExists(string id)
        {
            return db.Carreras.Count(e => e.Codigo_Carrera == id) > 0;
        }
    }
}
