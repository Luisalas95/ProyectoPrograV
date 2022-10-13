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

        [HttpPost]
        [Route("api/Profesores/CrearProfesor")]
        [ResponseType(typeof(profesor))]
        public async Task<IHttpActionResult> postProfesor
      ([FromBody] profesor p)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Profesore p1 = new Profesore()
            {
                Nombre = p.Nombre,
                Primer_Apellido = p.primerApellido,
                Segundo_apellido = p.SegundoApellido,
                Tipo_ID = p.tipo_ID,
                Identificacion = p.Identificacion,
                Fecha_Nacimiento = p.FechaNacimiento
            };
            db.Profesores.Add(p1);
            //Luego de agregar el estudiante validamos y agregamos el telefono y correo

            string[] telefonos = p.NumerosTelefono.Split();


            //Validamos que no vengan telefonos repetidos 
            bool estadoRepetidosTele = p.Validarepetidos(telefonos);
            if (estadoRepetidosTele == true)
            {
                return BadRequest("No puede ingresar numeros de telefono repetidos");
            }
            else
            {
                foreach (string telefono in telefonos)
                {
                    Telefonos_Profesores T1 = new Telefonos_Profesores()
                    {
                        Identificacion_Profesor = p1.Identificacion,
                        Tipo_ID_Profesor = p1.Tipo_ID,
                        Numero_Telefono = int.Parse(telefono.ToString()),


                    };
                    db.Telefonos_Profesores.Add(T1);

                }

            }

            //Validamos que no vengan correos repetidos
            string[] correos = p.CorreoEle.Split();
            bool EstadoRepetidoCorreo = p.Validarepetidos(correos);
            if (EstadoRepetidoCorreo == true)
            {
                return BadRequest("No puede ingresar correos electronicos repetidos");
            }
            else
            {
                //Si no vienen repetidos recorremos el arreglo de String y agregamos cada uno
                foreach (string correo in correos)
                {
                    Correos_Profesores C1 = new Correos_Profesores()
                    {
                        Identificacion_Profesor = p1.Identificacion,
                        Tipo_ID_Profesor = p1.Tipo_ID,
                        Corre_Electronico = correo.ToString(),
                    };
                    db.Correos_Profesores.Add(C1);

                }

            }

            try
            {
                await db.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                if (ProfesoreExists(p.tipo_ID) & ProfesoreExists2(p.Identificacion))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { Controller = "Profesores", id = p1.Identificacion, p1.Tipo_ID }, p1);
        }

            //  var response = Request.CreateResponse(HttpStatusCode.Created);
            //Incluir el url del nuevo recurso creado
            // string uri = Url.Link("InserEstudian", new { id = e2.Identificacion });
            // response.Headers.Location = new Uri(uri);



        //------------------------
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }




        // DELETE: api/Profesores/5
        [ResponseType(typeof(Profesore))]
        public async Task<IHttpActionResult> DeleteProfesore(int numero,string correo,string tipoID, string id)
        {
            Profesore profesore = await db.Profesores.FindAsync(tipoID, id);
            Correos_Profesores correos = await db.Correos_Profesores.FindAsync(correo,tipoID,id);
            Telefonos_Profesores telefono = await db.Telefonos_Profesores.FindAsync(numero, tipoID,id);
            if (profesore == null)
            {
                return NotFound();
            }
           

            db.Correos_Profesores.Remove(correos);
            db.Profesores.Remove(profesore);
            db.Telefonos_Profesores.Remove(telefono);
            await db.SaveChangesAsync();

            return Ok(profesore);
        }
        //--------------------------------


        [Route("api/Profesores/DatosProfesoresPorID")]
        [HttpGet]
        public async Task<IHttpActionResult> getDatosProfesoresPorID(string id, string TipoID)
        {
            Profesore Profesor = await db.Profesores.FindAsync(TipoID, id);

            if (Profesor == null)
            {
                return NotFound();
            }

            var idQuery =
           from ord1 in db.Profesores
           from ord in db.Telefonos_Profesores
           from ord2 in db.Correos_Profesores
           where TipoID == ord1.Tipo_ID && id == ord1.Identificacion && ord.Identificacion_Profesor == ord1.Identificacion && ord.Tipo_ID_Profesor == ord1.Tipo_ID && ord2.Identificacion_Profesor == ord1.Identificacion && ord2.Tipo_ID_Profesor == ord1.Tipo_ID
           select new { ord.Tipo_ID_Profesor, ord.Identificacion_Profesor, ord1.Nombre, ord1.Primer_Apellido, ord1.Segundo_apellido, ord.Numero_Telefono, ord1.Fecha_Nacimiento, ord2.Corre_Electronico };



            return Ok(idQuery);
        }

        //-----------------------------
        //--------------------------------


        [Route("api/Profesores/DatosProfesoresPorApellidos")]
        [HttpGet]
        public async Task<IHttpActionResult> getDatosProfesoresPorApellidos(string Apellido1, string Apellido2)
        {   //obtiene tipoID segun apellidos Profesor
            var idQuery1=
            from ordP in db.Profesores
            where Apellido1 ==ordP.Primer_Apellido && Apellido2==ordP.Segundo_apellido
            select new { ordP.Tipo_ID };
            string tipoID =idQuery1.ToString();

            //obtiene ID segun Apellidos  Profesor
            var idQuery2 =
            from ordP1 in db.Profesores
            where  Apellido1 == ordP1.Primer_Apellido && Apellido2 == ordP1.Segundo_apellido
            select new { ordP1.Identificacion };
            string ID=idQuery2.ToString() ;

            Profesore Profesor = await db.Profesores.FindAsync(tipoID, ID);

            if (Profesor == null)
            {
                return NotFound();
            }

            var idQuery =
           from ord1 in db.Profesores
           from ord in db.Telefonos_Profesores
           from ord2 in db.Correos_Profesores
           where  Apellido1 == ord1.Primer_Apellido && 
           Apellido2==ord1.Segundo_apellido  &&ord1.Tipo_ID==ord.Tipo_ID_Profesor && ord1.Identificacion==ord.Identificacion_Profesor
           select new { ord.Tipo_ID_Profesor, ord.Identificacion_Profesor, ord1.Nombre, ord1.Primer_Apellido, ord1.Segundo_apellido, ord.Numero_Telefono, ord1.Fecha_Nacimiento, ord2.Corre_Electronico };



            return Ok(idQuery);
        }


        //--------------



        //--------------------------------


        [Route("api/Profesores/DatosProfesoresPorNombre")]
        [HttpGet]
        public async Task<IHttpActionResult> DatosProfesoresPorNombre(string Nombre)
        {   //obtiene tipoID segun Nombre  Profesor
            var idQuery1 =
            from ordP in db.Profesores
            where Nombre == ordP.Primer_Apellido
            select new { ordP.Tipo_ID };
            string tipoID = idQuery1.ToString();

            //obtiene ID segun Nombre  Profesor
            var idQuery2 =
            from ordP1 in db.Profesores
            where Nombre == ordP1.Nombre
            select new { ordP1.Identificacion };
            string ID = idQuery2.ToString();

            Profesore Profesor = await db.Profesores.FindAsync(tipoID, ID);

            if (Profesor != null)
            {

            

            var idQuery =
           from ord1 in db.Profesores
           from ord in db.Telefonos_Profesores
           from ord2 in db.Correos_Profesores
           where Nombre == ord1.Primer_Apellido &&
           ord1.Tipo_ID == ord.Tipo_ID_Profesor && ord1.Identificacion == ord.Identificacion_Profesor
           select new { ord.Tipo_ID_Profesor, ord.Identificacion_Profesor, ord1.Nombre, ord1.Primer_Apellido, ord1.Segundo_apellido, ord.Numero_Telefono, ord1.Fecha_Nacimiento, ord2.Corre_Electronico };



            return Ok(idQuery); 
            }
        return NotFound();
        }


        //--------------

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