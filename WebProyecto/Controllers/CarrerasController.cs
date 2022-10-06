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
    public class CarrerasController : ApiController
    {
        private tiusr5pl_Proyecto1PrograVEntities db = new tiusr5pl_Proyecto1PrograVEntities();

        // GET: api/Carreras
        public IQueryable<Carrera> GetCarreras()
        {
            return db.Carreras;
        }

        // GET: api/Carreras/5
        [ResponseType(typeof(Carrera))]
        public async Task<IHttpActionResult> GetCarrera(string id)
        {
            Carrera carrera = await db.Carreras.FindAsync(id);
            if (carrera == null)
            {
                return NotFound();
            }

            return Ok(carrera);
        }

        // PUT: api/Carreras/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCarrera(string id, Carrera carrera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carrera.Codigo_Carrera)
            {
                return BadRequest();
            }

            db.Entry(carrera).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarreraExists(id))
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
        [Route("api/Carreras/CrearCarrera")]
        [ResponseType(typeof(carreras))]
        public async Task<IHttpActionResult> CrearCarrera
        ([FromBody] carreras c)
        
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Carrera c1 = new Carrera()
            {
                Codigo_Carrera = c.codigocarrera,
                Nombre_Carrera = c.nombreCarrera,
            };


            db.Carreras.Add(c1);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CarreraExists(c.codigocarrera))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(c);

        }



        
        // DELETE: api/Carreras/5
        [ResponseType(typeof(Carrera))]
        public async Task<IHttpActionResult> DeleteCarrera(string id)
        {
            Carrera carrera = await db.Carreras.FindAsync(id);
            if (carrera == null)
            {
                return NotFound();
            }

            db.Carreras.Remove(carrera);
            await db.SaveChangesAsync();

            return Ok(carrera);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarreraExists(string id)
        {
            return db.Carreras.Count(e => e.Codigo_Carrera == id) > 0;
        }
    }
}