using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace parafia2.Models.DataLayer;

[Table("Stanowiska")]
public partial class Stanowiska
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("Nazwa_stanowiska")]
    public string NazwaStanowiska { get; set; } = null!;

    [InverseProperty("StanowiskoNavigation")]
    public virtual ICollection<Ksieza> Ksiezas { get; } = new List<Ksieza>();
}
