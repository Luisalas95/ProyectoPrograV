//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Grupos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Grupos()
        {
            this.Matriculas = new HashSet<Matricula>();
        }
    
        public byte Numero_Grupo { get; set; }
        public string Codigo_Curs { get; set; }
        public string Identificacion_Profesor { get; set; }
        public string Horario { get; set; }
        public int Anno { get; set; }
        public byte NumeroPeriodo { get; set; }
        public string Tipo_ID_Profeso { get; set; }
    
        public virtual Curso Curso { get; set; }
        public virtual Periodo Periodo { get; set; }
        public virtual Profesore Profesore { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Matricula> Matriculas { get; set; }
    }
}
