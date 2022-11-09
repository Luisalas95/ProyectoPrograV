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
using System.Web.WebPages;
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
        public async Task<IHttpActionResult> PutProfesore(string id, string tipoid, estudianteActualiza p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ProfesoreExists(tipoid) || !ProfesoreExists2(id))
            {
                return NotFound();
            }

            Profesore p2 = new Profesore()
            {
                Nombre = p.Nombre,
                Primer_Apellido = p.primer_Apellido,
                Segundo_apellido = p.segundo_apellido,
                Fecha_Nacimiento = p.fecha_Nacimiento,
                Identificacion = id,
                Tipo_ID = tipoid,
            };


            db.Entry(p2).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }

            return Ok(p2);
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
        public async Task<IHttpActionResult> DeleteProfesore(string TipoID, string id)
        {
            Profesore profesore = await db.Profesores.FindAsync(TipoID, id);

            if (profesore == null)
            {
                return NotFound();
            }

            var Tele =
              from telefono in db.Telefonos_Profesores
              where telefono.Tipo_ID_Profesor == TipoID && telefono.Identificacion_Profesor == id
              select telefono.Numero_Telefono;

            int tel = Int32.Parse(Tele.ToString()); 


            Telefonos_Profesores telefonos = await db.Telefonos_Profesores.FindAsync(tel, TipoID, id);

            var deleteTele =
                from telefono in db.Telefonos_Profesores
                where telefono.Tipo_ID_Profesor == TipoID && telefono.Identificacion_Profesor == id
                select telefono;
           
           

            foreach (var detail in deleteTele)
            {

                db.Telefonos_Profesores.Remove(detail);

            }
            try
            {
                db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Provide for exceptions.
            }
                  var Correo =
                      from correo in db.Correos_Profesores
                      where correo.Tipo_ID_Profesor == TipoID && correo.Identificacion_Profesor == id
                      select correo.Corre_Electronico;
                    var deleteCorreo =
                      from correo in db.Correos_Profesores
                      where correo.Tipo_ID_Profesor == TipoID && correo.Identificacion_Profesor == id
                      select correo;

                    Correos_Profesores correos = await db.Correos_Profesores.FindAsync(Correo.ToString(), TipoID, id);

                    foreach (var details in deleteCorreo)
                    {

                        db.Correos_Profesores.Remove(details);
                    }
                    try
                    {
                        db.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        // Provide for exceptions.
                    }
        
            db.Profesores.Remove(profesore);
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


    

        //--------------

 
               [ResponseType(typeof(Profesore))]
        [Route("api/Profesores/DatosProfesoresPorApellidos", Name ="getProfesoresPorApellidos")]

        public HttpResponseMessage getDatosProfesoresPorApellidos(string Apellido1,string Apellido2)
        {   //obtiene tipoID segun apellidos Profesor
            try {

                var idQuery = (from p in db.Profesores
                               where p.Primer_Apellido == Apellido1 && p.Segundo_apellido == Apellido2

                               select new
                               {
                                   p.Identificacion, p.Tipo_ID,p.Nombre,p.Primer_Apellido,p.Segundo_apellido,
                                   p.Fecha_Nacimiento ,p.Correos_Profesores,p.Telefonos_Profesores
                               }
                               ).ToList();
                Profesore profesor = db.Profesores.Where(x => x.Primer_Apellido == Apellido1 && x.Segundo_apellido == Apellido2).SingleOrDefault();

                if ( profesor != null) {
                    return Request.CreateResponse(HttpStatusCode.OK, idQuery);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Profesor "+Apellido1+" "+Apellido2+" no ha sido encontrado");

            } catch (Exception ex) {

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }


        }




        //--------------------------------


        [ResponseType(typeof(Profesore))]
        [Route("api/Profesores/DatosProfesoresPorNombre", Name = "getProfesoresPorNombre")]

        public HttpResponseMessage getDatosProfesoresPorNombre(string Nombre)
        {   //obtiene tipoID segun apellidos Profesor
            try
            {

                var idQuery = (from p in db.Profesores
                               where p.Nombre == Nombre 

                               select new
                               {
                                   p.Identificacion,
                                   p.Tipo_ID,
                                   p.Nombre,
                                   p.Primer_Apellido,
                                   p.Segundo_apellido,
                                   p.Fecha_Nacimiento,
                                   p.Correos_Profesores,
                                   p.Telefonos_Profesores
                               }
                               ).ToList();
                Profesore profesor = db.Profesores.Where(x => x.Nombre == Nombre).SingleOrDefault();

                if (profesor != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, idQuery);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Profesor " + Nombre + " no ha sido encontrado");

            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }


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