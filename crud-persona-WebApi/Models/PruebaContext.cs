using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CrudPersonasWebApi.Models
{
    public partial class CrudPersonasWebApiContext : DbContext
    {
        public CrudPersonasWebApiContext()
        {
        }

        public CrudPersonasWebApiContext(DbContextOptions<CrudPersonasWebApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContactMean> ContactMeans { get; set; }
        public virtual DbSet<ContactMeansPerson> ContactMeansPeople { get; set; }
        public virtual DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ContactMean>(entity =>
            {
                entity.HasKey(e => e.ContactMeansId)
                    .HasName("PK__ContactM__9F789A2F1523E0AF");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ContactMeansPerson>(entity =>
            {
                entity.ToTable("ContactMeansPerson");

                entity.Property(e => e.Contact)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ContactMeans)
                    .WithMany(p => p.ContactMeansPeople)
                    .HasForeignKey(d => d.ContactMeansId)
                    .HasConstraintName("FK__ContactMe__Conta__3F466844");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.ContactMeansPeople)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__ContactMe__Perso__403A8C7D");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Birth).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Identification)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
