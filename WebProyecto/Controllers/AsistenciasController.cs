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

        

        [ResponseType(typeof(Asistencia))]
        [Route("api/Asistencia/AsistenciaPorGrupo", Name = "getAsistenciaPorGrupo")]

        public HttpResponseMessage getAsistencia(int NumeroGrupo, string CodigoCurso, DateTime Fecha)
        {   //obtiene tipoID segun apellidos Profesor
            try
            {

                var idQuery = (from p in db.Asistencias
                               where p.Numero_Grupo == NumeroGrupo && p.Codigo_Curso == CodigoCurso && p.Fecha_Asistencia==Fecha

                               select new
                               {
                                   p.Numero_Grupo,
                                   p.Codigo_Curso,
                                   p.Fecha_Asistencia,
                                   p.Tipo_Registro,
                                   p.Tipo_ID_Esutiante,
                                   p.Identificacion_Estudiante
                               }
                               ).ToList();
                Asistencia asistencia = db.Asistencias.Where(x => x.Numero_Grupo == NumeroGrupo && x.Codigo_Curso == CodigoCurso && x.Fecha_Asistencia==Fecha).SingleOrDefault();

                if (asistencia != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, idQuery);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Asistencia no ha sido encontrado");

            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }


        }

        // PUT: api/Asistencias/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAsistencia(byte id, Asistencia asistencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asistencia.Numero_Grupo)
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
                if (AsistenciaExists(asistencia.Numero_Grupo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = asistencia.Numero_Grupo }, asistencia);
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


        //--------------------------------DatosASistencia


        [Route("api/Asistencia/AsistenciaDesgloce")]
        [HttpGet]
        public async Task<IHttpActionResult> getDatosAsistencia(string id, string TipoID)
        {
           Estudiante estudiante = await db.Estudiantes.FindAsync(TipoID, id);

            if (estudiante == null)
            {
                return NotFound();
            }

            var idQuery =
           from ord1 in db.Estudiantes
           from ord in db.Asistencias
           
           where TipoID== ord.Tipo_ID_Esutiante &&id == ord1.Identificacion && ord.Identificacion_Estudiante == ord1.Identificacion && ord.Tipo_ID_Esutiante == ord1.Tipo_ID
           select new { ord.Tipo_ID_Esutiante, ord.Identificacion_Estudiante, ord1.Nombre, ord1.Primer_Apellido, ord1.Segundo_apellido, ord.Codigo_Curso,ord.Numero_Grupo,ord.Fecha_Asistencia,ord.Tipo_Registro };



            return Ok(idQuery);
        }


        //------------------------

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
            return db.Asistencias.Count(e => e.Numero_Grupo == id) > 0;
        }
    }
}