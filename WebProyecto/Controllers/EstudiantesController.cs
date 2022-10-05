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
    public class EstudiantesController : ApiController
    {
        private tiusr5pl_Proyecto1PrograVEntities db = new tiusr5pl_Proyecto1PrograVEntities();

        // GET: api/Estudiantes
        public IQueryable<Estudiante> GetEstudiantes()
        {
            return db.Estudiantes;
        }

        // GET: api/Estudiantes/5
        [ResponseType(typeof(Estudiante))]
        public async Task<IHttpActionResult> GetEstudiante(string id, string TipoID)
        {
            Estudiante estudiante = await db.Estudiantes.FindAsync(TipoID,id);
            if (estudiante == null)
            {
                return NotFound();
            }
            
            return Ok(estudiante);
         
        }

        // PUT: api/Estudiantes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEstudiante(string id, Estudiante estudiante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != estudiante.Tipo_ID)
            {
                return BadRequest();
            }

            db.Entry(estudiante).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstudianteExists(id))
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

        // POST: api/Estudiantes
        [ResponseType(typeof(Estudiante))]
        public async Task<IHttpActionResult> PostEstudiante(Estudiante estudiante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Estudiantes.Add(estudiante);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EstudianteExists(estudiante.Tipo_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = estudiante.Tipo_ID }, estudiante);
        }

        [HttpPost]
        [Route("api/Estudiantes/crearEstudiante")]
        [ResponseType(typeof(estudiante))]
        public async Task<IHttpActionResult> CrearEstudiante
         ([FromBody] estudiante e) 
        
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Estudiante e2 = new Estudiante()
            {
                Nombre = e.Nombre,
                Primer_Apellido = e.primerApellido,
                Segundo_apellido = e.SegundoApellido,
                Tipo_ID = e.tipo_ID,
                Identificacion = e.Identificacion,
                Fecha_Nacimiento = e.FechaNacimiento
            };

            db.Estudiantes.Add(e2);


            ///



            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EstudianteExists(e.tipo_ID) & EstudianteExists2(e.Identificacion))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = e.tipo_ID }, e.Identificacion);
        }







        // DELETE: api/Estudiantes/5
        [ResponseType(typeof(Estudiante))]
        public async Task<IHttpActionResult> DeleteEstudiante(string id, string TipoID)
        {
            Estudiante estudiante = await db.Estudiantes.FindAsync(TipoID,id);
            if (estudiante == null)
            {
                return NotFound();
            }

            db.Estudiantes.Remove(estudiante);
            await db.SaveChangesAsync();

            return Ok(estudiante);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EstudianteExists(string id)
        {
            return db.Estudiantes.Count(e => e.Tipo_ID == id) > 0;
        }

        private bool EstudianteExists2(string id)
        {
            return db.Estudiantes.Count(e => e.Identificacion == id) > 0;
        }

    }
}