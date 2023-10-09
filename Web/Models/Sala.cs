using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Sala
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, ingresar la descripción")]
        [Display(Name = "Descripción")]
        [StringLength(50)]
        public string? Descripcion { get; set; }

        [Display(Name = "2D ?")]
        public bool? DosD { get; set; }

        [Display(Name = "3D ?")]
        public bool? TresD { get; set; }

        [Display(Name = "XD ?")]
        public bool? Xd { get; set; }

        [Display(Name = "DBOX ?")]
        public bool? Dbox { get; set; }

        [Display(Name = "Premium ?")]
        public bool? Premium { get; set; }

        [Display(Name = "Comfort ?")]
        public bool? Comfort { get; set; }

        [Display(Name = "E-Motion ?")]
        public bool? Emotion { get; set; }

        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.Date)]
        public DateTime? FechaRegistro { get; set; }
    }
}
