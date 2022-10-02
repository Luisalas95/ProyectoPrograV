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

        // POST: api/Cursoes
        [ResponseType(typeof(Curso))]
        public async Task<IHttpActionResult> PostCurso(Curso curso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cursos.Add(curso);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CursoExists(curso.Codigo_Curso))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = curso.Codigo_Curso }, curso);
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
    }
}