using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Programar
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Display(Name = "Fecha/Hora Función")]
        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.DateTime)]
        public DateTime? FechaHoraFuncion { get; set; }

        [Display(Name = "Pelicula")]
        public int? PeliculaRefId { get; set; }
        [ForeignKey("PeliculaRefId")]
        public virtual Pelicula? Pelicula { get; set; }

        [Display(Name = "Sala")]
        public int? SalaRefId { get; set; }
        [ForeignKey("SalaRefId")]
        public virtual Sala? Sala { get; set; }

        [Display(Name = "Tarifa 1")]
        public int? Tarifa1RefId { get; set; }
        [ForeignKey("Tarifa1RefId")]
        public virtual Tarifa? Tarifa1 { get; set; }

        [Display(Name = "Tarifa 2")]
        public int? Tarifa2RefId { get; set; }
        [ForeignKey("Tarifa2RefId")]
        public virtual Tarifa? Tarifa2 { get; set; }

        [Display(Name = "Tarifa 3")]
        public int? Tarifa3RefId { get; set; }
        [ForeignKey("Tarifa3RefId")]
        public virtual Tarifa? Tarifa3 { get; set; }

        [Display(Name = "Tarifa 4")]
        public int? Tarifa4RefId { get; set; }
        [ForeignKey("Tarifa4RefId")]
        public virtual Tarifa? Tarifa4 { get; set; }

        [Display(Name = "Tarifa 5")]
        public int? Tarifa5RefId { get; set; }
        [ForeignKey("Tarifa5RefId")]
        public virtual Tarifa? Tarifa5 { get; set; }

        [Display(Name = "Tarifa 6")]
        public int? Tarifa6RefId { get; set; }
        [ForeignKey("Tarifa6RefId")]
        public virtual Tarifa? Tarifa6 { get; set; }

        [Display(Name = "Fecha Registro")]
        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FechaRegistro { get; set; }

        [NotMapped]
        public string? ValidationError { get; set; }
    }
}
