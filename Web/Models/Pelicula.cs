using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web.Repos.Models;

namespace Web.Models
{
    [Table("Pelicula")]
    public class Pelicula
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        
        [Display(Name = "Descripción")]
        [StringLength(50)]
        public string? Descripcion { get; set; }

        [Display(Name = "Imagen")]
        public string? ImagemPelicula { get; set; }

        [Display(Name = "Duración")]
        public int? Duracion { get; set; }

        [Display(Name = "Clasificación")]
        public int? Clasificacion { get; set; }

        [Display(Name = "Género")]
        public int? GeneroRefId { get; set; }
        [ForeignKey("GeneroRefId")]
        public virtual Genero? Genero { get; set; }

        [Display(Name = "Tipo")]
        public int? TipoRefId { get; set; }
        [ForeignKey("TipoRefId")]
        public virtual Tipo? Tipo { get; set; }

        [Display(Name = "Subtitulo")]
        public int? SubtituloRefId { get; set; }
        [ForeignKey("SubtituloRefId")]
        public virtual Subtitulo? Subtitulo { get; set; }

        [Display(Name = "Fecha Estreno")]
        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FechaEstreno { get; set; }

        [Display(Name = "Fecha Registro")]
        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FechaRegistro { get; set; }
    }
}
