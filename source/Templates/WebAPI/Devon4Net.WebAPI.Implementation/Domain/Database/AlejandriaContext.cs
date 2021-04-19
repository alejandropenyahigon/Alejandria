using System;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Devon4Net.WebAPI.Implementation.Domain.Database
{
    public partial class AlejandriaContext : DbContext
    {
        public AlejandriaContext()
        {
        }

        public AlejandriaContext(DbContextOptions<AlejandriaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<AuthorBook> AuthorBook { get; set; }
        public virtual DbSet<Book> Book { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=changeme");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("author_id_idx")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasComment("Id that will be autoincremental")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasComment("Email of the author");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasComment("Name of the Auhtor");

                entity.Property(e => e.Phone).HasComment("Phone number of the author");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasComment("Surname of the author");
            });

            modelBuilder.Entity<AuthorBook>(entity =>
            {
                entity.ToTable("Author_book");

                entity.HasIndex(e => e.Id)
                    .HasName("author_book_id_idx")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasComment("Id that will be autoincremental ")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AuthorId).HasColumnName("Author_Id");

                entity.Property(e => e.BookId).HasColumnName("Book_Id");

                entity.Property(e => e.PublishDate)
                    .HasColumnType("date")
                    .HasComment("Date of publication ");

                entity.Property(e => e.ValidityDate)
                    .HasColumnType("date")
                    .HasComment("Date of expiration");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.AuthorBook)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("author_book_fk");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.AuthorBook)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("author_book_fk_1");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("book_id_idx")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasComment("Id that will be autoincremental ")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Genere)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasComment("Genere of the book ");

                entity.Property(e => e.Summary)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasComment("Summary of the book ");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasComment("Title of the book ");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
