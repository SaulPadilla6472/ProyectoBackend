using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectoBackend.Models;

public partial class PubContext : DbContext
{
    public PubContext()
    {
    }

    public PubContext(DbContextOptions<PubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Beer> Beers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Pub;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beer>(entity =>
        {
            entity.HasKey(e => e.BeerId).HasName("PK_Table_1");

            entity.ToTable("Beer");

            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
