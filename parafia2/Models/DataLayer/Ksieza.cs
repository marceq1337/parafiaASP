using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace parafia2.Models.DataLayer;

[Table("Ksieza")]
public partial class Ksieza
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Imie { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Nazwisko { get; set; }

    [NotMapped]
    public string FullName
    {
        get
        {
            return Imie + " " + Nazwisko;
        }
    }

    public int? Stanowisko { get; set; }

    [InverseProperty("KsiadzNavigation")]
    public virtual ICollection<Msze> Mszes { get; } = new List<Msze>();

    [ForeignKey("Stanowisko")]
    [InverseProperty("Ksiezas")]
    public virtual Stanowiska? StanowiskoNavigation { get; set; }
}
