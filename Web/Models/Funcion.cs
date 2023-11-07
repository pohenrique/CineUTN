using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    [Table("Funcion")]
    public class Funcion
    {
        public int Id { get; set; }

        [Display(Name = "Fecha/Hora Función")]
        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Por favor, ingrese fecha y hora para la función.")]
        public DateTime? FechaHoraFuncion { get; set; }

        [Display(Name = "Pelicula")]
        [Required(ErrorMessage = "Por favor, seleccione una película.")]
        public int? PeliculaRefId { get; set; }

        [ForeignKey("PeliculaRefId")]
        public virtual Pelicula? Pelicula { get; set; }

        [Display(Name = "Sala")]
        [Required(ErrorMessage = "Por favor, seleccione una sala.")]
        public int? SalaRefId { get; set; }
        
        [ForeignKey("SalaRefId")]
        public virtual Sala? Sala { get; set; }

        public virtual List<FuncionTarifa> Tarifas { get; set; }

        public DateTime? FechaRegistro { get; set; } = DateTime.Now;

        [NotMapped]
        public string? ValidationError { get; set; }

        public int NumberOfTarifas
        {
            get => Tarifas.Count;
        }

        public Funcion()
        {
            Tarifas = new List<FuncionTarifa>();
        }
    }
}
