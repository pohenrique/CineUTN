using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Models;
using Web.Repos.Models;

namespace Web.ViewModels
{
    public class PeliculaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, ingresar la descripción.")]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Display(Name = "Duración")]
        public int? Duracion { get; set; }

        [Display(Name = "Clasificación")]
        public int? Clasificacion { get; set; }

        [Display(Name = "Género")]
        public int? GeneroRefId { get; set; }

        public virtual Genero? Genero { get; set; }

        [Display(Name = "Tipo")]
        public int? TipoRefId { get; set; }

        public virtual Tipo? Tipo { get; set; }

        [Display(Name = "Subtitulo")]
        public int? SubtituloRefId { get; set; }
        [ForeignKey("SubtituloRefId")]
        public virtual Subtitulo? Subtitulo { get; set; }

        [Display(Name = "Fecha Estreno")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FechaEstreno { get; set; }

        [Display(Name = "Fecha Registro")]
        [DataType(DataType.Date)]
        public DateTime? FechaRegistro { get; set; }

        [Display(Name = "Imagem Pelicula")]
        public IFormFile Imagem { get; set; }

        [Display(Name = "Imagen")]
        public string? ImagemPelicula { get; set; }
    }
}
