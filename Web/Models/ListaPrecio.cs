using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NuGet.ProjectModel;

namespace Web.Models
{

    // Efectivo 1000, Tarjeta Debito Macro x 1000, Tarjeta Debito HSBC x 1050, Tarjeta Credito 1100.
    public class ListaPrecio
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, ingresar la descripción")]
        [Display(Name = "Descripción")]
        [StringLength(50)]
        public string? Descripcion { get; set; }

        [Display(Name = "Fecha Hasta")]
        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FechaHasta { get; set; }

        [Display(Name = "Condición Pago")]
        public int? CondicionPagoRefId { get; set; }
        [ForeignKey("CondicionPagoRefId")]
        public virtual CondicionPago? CondicionPago { get; set; }

        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Precio { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaRegistro { get; set; }

    }
}
