using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace parafia2.Models.DataLayer;

[Table("Ministranci")]
public partial class Ministranci
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

    [Column("Data_urodzenia", TypeName = "date")]
    public DateTime? DataUrodzenia { get; set; }

    [InverseProperty("MinistrantNavigation")]
    public virtual ICollection<Msze> Mszes { get; } = new List<Msze>();
}
