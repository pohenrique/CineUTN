using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    [Table("Sala")]
    public class Sala
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, ingresar la descripción")]
        [Display(Name = "Descripción")]
        [StringLength(50)]
        public string? Descripcion { get; set; }

        [Display(Name = "Tipo")]
        public int? TipoRefId { get; set; }
        [ForeignKey("TipoRefId")]
        public virtual Tipo? Tipo { get; set; }

        //XD, DBOX, Premium, Comfort, E-Motion
        [Display(Name = "Sonido")]
        public int? SonidoRefId { get; set; }
        [ForeignKey("SonidoRefId")]
        public virtual Sonido? Sonido { get; set; }

        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.Date)]
        public DateTime? FechaRegistro { get; set; }
    }
}
