//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CineHitss
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pelicula
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pelicula()
        {
            this.Carteleras = new HashSet<Cartelera>();
        }
    
        public int id { get; set; }
        public string Nombre { get; set; }
        public System.TimeSpan Duracion { get; set; }
        public string Sinopsis { get; set; }
        public int Costo { get; set; }
        public string Clasification { get; set; }
        public int GeneroID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cartelera> Carteleras { get; set; }
        public virtual Genero Genero { get; set; }
    }
}
