using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web.Repos.Models;

namespace Web.Models
{
    public class Funcion
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Display(Name = "Película")]
        public int? PeliculaRefId { get; set; }
        [ForeignKey("PeliculaRefId")]
        public virtual Pelicula? Pelicula { get; set; }

        [Display(Name = "Sala")]
        public int? SalaRefId { get; set; }
        [ForeignKey("SalaRefId")]
        public virtual Sala? Sala { get; set; }

        [Display(Name = "Tarifa")]
        public int? TarifaRefId { get; set; }
        [ForeignKey("TarifaRefId")]
        public virtual Tarifa? Tarifa { get; set; }

        [Display(Name = "Fecha/Hora")]
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaHoraFuncion { get; set; }

        [Display(Name = "Subtitulada")]
        public bool? Subtitulada { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaRegistro { get; set; }
    }
}
