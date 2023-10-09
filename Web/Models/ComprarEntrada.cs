using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Web.Models
{
    public class ComprarEntrada
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        public int Cantidad { get; set; }

        //Link con Contacto 
        [Display(Name = "Función")]
        public int? FuncionRefId { get; set; }
        [ForeignKey("FuncionRefId")]
        public virtual Funcion? Funcion { get; set; }

        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaRegistro { get; set; }

    }
}
