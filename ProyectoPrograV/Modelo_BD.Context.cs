﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoPrograV
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class tiusr5pl_Proyecto1PrograVEntities : DbContext
    {
        public tiusr5pl_Proyecto1PrograVEntities()
            : base("name=tiusr5pl_Proyecto1PrograVEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Asistencia> Asistencias { get; set; }
        public virtual DbSet<Carrera> Carreras { get; set; }
        public virtual DbSet<Correos_Estudiantes> Correos_Estudiantes { get; set; }
        public virtual DbSet<Correos_Profesores> Correos_Profesores { get; set; }
        public virtual DbSet<Curso> Cursos { get; set; }
        public virtual DbSet<Estudiante> Estudiantes { get; set; }
        public virtual DbSet<Grupos> Grupos { get; set; }
        public virtual DbSet<Matricula> Matriculas { get; set; }
        public virtual DbSet<Periodo> Periodoes { get; set; }
        public virtual DbSet<Profesore> Profesores { get; set; }
        public virtual DbSet<Telefonos_Estudiantes> Telefonos_Estudiantes { get; set; }
        public virtual DbSet<Telefonos_Profesores> Telefonos_Profesores { get; set; }
    }
}
