﻿using System;
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
using Swashbuckle.Swagger;
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
        public async Task<IHttpActionResult> PutEstudiante(string id, string tipoId, estudianteActualiza e)
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
                Fecha_Nacimiento = e.FechaNacimiento,
                Identificacion = id,
                Tipo_ID = tipoId,
            };

            db.Entry(e2).State = EntityState.Modified;


            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstudianteExists(tipoId) & !EstudianteExists2(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(e2);
        }

        [HttpPost]
        [Route("api/Estudiantes/CrearEstudiante")]
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
            //Luego de agregar el estudiante validamos y agregamos el telefono y correo

            string[] telefonos = e.NumerosTelefono.Split();
          
                
            //Validamos que no vengan telefonos repetidos 
           bool estadoRepetidosTele = e.Validarepetidos(telefonos);
            if (estadoRepetidosTele == true)
            {
                return BadRequest ("No puede ingresar numeros de telefono repetidos");
            }
            else
            {
                foreach (string telefono in telefonos)
                {
                    Telefonos_Estudiantes T1 = new Telefonos_Estudiantes()
                    {
                        Identificacion_Estudiante = e2.Identificacion,
                        Tipo_ID_Estudiante = e2.Tipo_ID,
                        Numero_Telefono = telefono.ToString(),
                   
                    
                    };
                    db.Telefonos_Estudiantes.Add(T1);

                }

            }

            //Validamos que no vengan correos repetidos
            string[] correos = e.CorreoEle.Split();
          bool EstadoRepetidoCorreo =  e.Validarepetidos(correos);
            if (EstadoRepetidoCorreo == true)
            {
                return BadRequest("No puede ingresar correos electronicos repetidos");
            }
            else
            {
                //Si no vienen repetidos recorremos el arreglo de String y agregamos cada uno
                foreach (string correo in correos)
                {
                    Correos_Estudiantes C1 = new Correos_Estudiantes()
                    {
                        Identificacion_Estudiante = e2.Identificacion,
                        Tipo_ID_Estudiante = e2.Tipo_ID,
                        Corre_Electronico = correo.ToString(),
                    };
                    db.Correos_Estudiantes.Add(C1);

                }

            }

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

          //  var response = Request.CreateResponse(HttpStatusCode.Created);
            //Incluir el url del nuevo recurso creado
           // string uri = Url.Link("InserEstudian", new { id = e2.Identificacion });
           // response.Headers.Location = new Uri(uri);

           
            return Ok(e2);
        }







        // DELETE: api/Estudiantes/5
        [ResponseType(typeof(Estudiante))]
        public async Task<IHttpActionResult> DeleteEstudiante(string id, string TipoID, string telefono, string correo)
        {
            Correos_Estudiantes correos = await db.Correos_Estudiantes.FindAsync(correo,TipoID,id);
            Estudiante estudiante = await db.Estudiantes.FindAsync(TipoID,id);
            Telefonos_Estudiantes telefonos = await db.Telefonos_Estudiantes.FindAsync(telefono,TipoID,id);
            
            if (estudiante == null)
            {
                return NotFound();
            }

            db.Telefonos_Estudiantes.Remove(telefonos);
            db.Correos_Estudiantes.Remove(correos);
            db.Estudiantes.Remove(estudiante);

            await db.SaveChangesAsync();

            return Ok(estudiante);
        }



        //------------------------- getDatosEstudiante

        [Route("api/Estudiantes/DatosEstudiante")]
        [HttpGet]
        public async Task<IHttpActionResult> getDatosEstudiante(string id, string TipoID)
        {
            Estudiante estudiante = await db.Estudiantes.FindAsync(TipoID, id);

            if (estudiante == null )
            {
                 return NotFound();
              }

                var idQuery =
               from ord1 in db.Estudiantes
               from ord in db.Telefonos_Estudiantes
               from ord2 in db.Correos_Estudiantes
               where TipoID== ord.Tipo_ID_Estudiante && id==ord.Identificacion_Estudiante && ord.Identificacion_Estudiante == ord1.Identificacion && ord.Tipo_ID_Estudiante==ord1.Tipo_ID && ord2.Identificacion_Estudiante == ord1.Identificacion && ord2.Tipo_ID_Estudiante == ord1.Tipo_ID
               select new { ord.Tipo_ID_Estudiante, ord.Identificacion_Estudiante, ord1.Nombre, ord1.Primer_Apellido,ord1.Segundo_apellido,ord.Numero_Telefono,ord1.Fecha_Nacimiento,ord2.Corre_Electronico};
           


            return Ok(idQuery);
        }

        //------------------------------



      

        //------------------------- 



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EstudianteExists(string tipoid)
        {
            return db.Estudiantes.Count(e => e.Tipo_ID == tipoid) > 0;
        }

        private bool EstudianteExists2(string identificacion)
        {
            return db.Estudiantes.Count(e => e.Identificacion == identificacion) > 0;
        }

        


    }
}