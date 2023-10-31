using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models
{
    [Table("FuncionTarifa")]
    public class FuncionTarifa
    {
        public int Id { get; set; }

        [Display(Name = "Tarifa")]
        public int? TarifaRefId { get; set; }
        [ForeignKey("TarifaRefId")]
        public virtual Tarifa? Tarifa { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

    }
}
