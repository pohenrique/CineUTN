using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{

    //Dias de Semama + lista de precio Efectivo + Aplicar Descuento si hay
    //Fines de Semana + lista de precio Tarjeta Debito, Aplicar Descuento si hay
    //Promo Miercoles, Promo Bancos, Estudiantes, Invitación Especial...
    [Table("Tarifa")]
    public class Tarifa
    {

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, ingresar la descripción")]
        [StringLength(50)]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Por favor, ingresar un porcentaje")]
        public int PorcentajeDescuento { get; set; }

        [Display(Name = "Lista de Precio")]
        public int? ListaPrecioRefId { get; set; }
        [ForeignKey("ListaPrecioRefId")]
        public virtual ListaPrecio? ListaPrecio { get; set; }

        [NotMapped]
        public decimal PrecioConDescuento
        {

            get
            {
                if (this.ListaPrecio != null)
                {
                    return (this.ListaPrecio.Precio - (this.ListaPrecio.Precio * this.PorcentajeDescuento / 100));
                }
                else
                {
                    return 0;
                }
                
            }
            set
            {
                PrecioConDescuento = 0;
            }
        }
     
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaRegistro { get; set; }
    }
}
