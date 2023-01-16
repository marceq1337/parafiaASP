using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace parafia2.Models.DataLayer;

public partial class ParafiaContext : DbContext
{
    public ParafiaContext()
    {
    }

    public ParafiaContext(DbContextOptions<ParafiaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ksieza> Ksiezas { get; set; }

    public virtual DbSet<Ministranci> Ministrancis { get; set; }

    public virtual DbSet<Msze> Mszes { get; set; }

    public virtual DbSet<Stanowiska> Stanowiskas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=ParafiaContext");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ksieza>(entity =>
        {
            entity.HasOne(d => d.StanowiskoNavigation).WithMany(p => p.Ksiezas).HasConstraintName("FK_Ksieza_Stanowiska");
        });

        modelBuilder.Entity<Msze>(entity =>
        {
            entity.HasOne(d => d.KsiadzNavigation).WithMany(p => p.Mszes).HasConstraintName("FK_Msze_Ksieza");

            entity.HasOne(d => d.MinistrantNavigation).WithMany(p => p.Mszes).HasConstraintName("FK_Msze_Ministranci");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
