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
        public DateTime? FechaHoraFuncion { get; set; }

        [Display(Name = "Pelicula")]
        public int? PeliculaRefId { get; set; }
        [ForeignKey("PeliculaRefId")]
        public virtual Pelicula? Pelicula { get; set; }

        [Display(Name = "Sala")]
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
