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


        [HttpPost]
        [Route("api/Grupos/CrearGrupo")]
        [ResponseType(typeof(grupos))]
        public async Task<IHttpActionResult> CrearGrupo
        ([FromBody] grupos g)

        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Validamos que los campos foranenos en la tabla existan
            if (!CursoExists(g.codigocurso))
            {
                return BadRequest("El codigo del curso ingresado no existe");
            }



            

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GruposExists(g.numerogrupo) & GruposExistsCodigoCurso(g.codigocurso))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }



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
        //--------------------------------


        [Route("api/Grupos/DatosGrupo")]
        [HttpGet]
        public async Task<IHttpActionResult> getDatosGrupo(int NumGrupo, string CodCurso)
        {
            Grupos grupos = await db.Grupos .FindAsync(NumGrupo, CodCurso);

            if (grupos == null)
            {
                return NotFound();
            }

            var idQuery =
           from ord1 in db.Cursos
           from ord in db.Grupos
           from ord2 in db.Profesores
           where NumGrupo==ord.Numero_Grupo&& CodCurso==ord.Codigo_Curs&& ord.Identificacion_Profesor == ord2.Identificacion && ord.Tipo_ID_Profeso == ord2.Tipo_ID && ord.Codigo_Curs==ord1.Codigo_Curso 
           select new { ord.Numero_Grupo,ord1.Codigo_Curso,ord1.Nombre_Curso,ord.Periodo,ord.Horario,ord2.Nombre,ord2.Primer_Apellido,ord2.Segundo_apellido};



            return Ok(idQuery);
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

        private bool GruposExistsCodigoCurso(string id)
        {
            return db.Grupos.Count(e => e.Codigo_Curs == id) > 0;
        }

        private bool CursoExists(string id)
        {
            return db.Cursos.Count(e => e.Codigo_Curso == id) > 0;
        }


        private bool ProfesoreExists(string tipoid)
        {
            return db.Profesores.Count(e => e.Tipo_ID == tipoid) > 0;
        }

        private bool ProfesoreExists2(string id)
        {
            return db.Profesores.Count(e => e.Identificacion == id) > 0;
        }

    }
}