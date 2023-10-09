using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Web.Repos.Models;

[Table("Genero")]
public partial class Genero
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Por favor, ingresar la descripción")]
    [StringLength(50)]
    public string? Descripcion { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? FechaRegistro { get; set; }
}
