using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web.Repos.Models;

namespace Web.Models
{
    public class Pelicula
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, ingresar la descripción.")]
        [Display(Name = "Descripción")]
        [StringLength(50)]
        public string? Descripcion { get; set; }

        [Display(Name = "Imagen")]
        [Required(ErrorMessage = "Por favor, seleccione un imagem.")]
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

        [Display(Name = "Subtitulada ?")]
        public bool? Subtitulada { get; set; }

        [Display(Name = "Fecha Estreno")]
        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FechaEstreno { get; set; }

        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.Date)]
        public DateTime? FechaRegistro { get; set; }
    }
}
