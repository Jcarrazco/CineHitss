//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CineHitssApi
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public partial class Pelicula
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pelicula()
        {
            this.Carteleras = new HashSet<Cartelera>();
        }
    
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public System.TimeSpan Duracion { get; set; }
        [DataMember]
        public string Sinopsis { get; set; }
        [DataMember]
        public int Costo { get; set; }
        [DataMember]
        public string Clasification { get; set; }
        [DataMember]
        public int GeneroID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cartelera> Carteleras { get; set; }
        public virtual Genero Genero { get; set; }
    }
}
