﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NewtonLibraryManager.Models
{
    public partial class NewtonLibraryContext : DbContext
    {
        public NewtonLibraryContext()
        {
        }

        public NewtonLibraryContext(DbContextOptions<NewtonLibraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<AuthorDetail> AuthorDetails { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Language> Languages { get; set; } = null!;
        public virtual DbSet<LendingDetail> LendingDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Type> Types { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:newtonservertest.database.windows.net,1433;Initial Catalog=NewtonLibrary;Persist Security Info=False;User ID=serveradmin;Password=MariaDB123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FirstName).HasMaxLength(30);

                entity.Property(e => e.LastName).HasMaxLength(30);
            });

            modelBuilder.Entity<AuthorDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.AuthorDetails)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_AuthorDetails.AuthorId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.AuthorDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_AuthorDetails.ProductID");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category1)
                    .HasMaxLength(70)
                    .HasColumnName("Category");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("Language");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Language1)
                    .HasMaxLength(50)
                    .HasColumnName("Language");
            });

            modelBuilder.Entity<LendingDetail>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.BorrowedFrom).HasColumnType("date");

                entity.Property(e => e.BorrowedTo).HasColumnType("date");

                entity.Property(e => e.ReturnDate).HasColumnType("date");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.LendingDetail)
                    .HasForeignKey<LendingDetail>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LendingDetails.ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LendingDetails)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_LendingDetails.UserId");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Dewey).HasColumnType("decimal(6, 3)");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("ISBN")
                    .IsFixedLength();

                entity.Property(e => e.LanguageId).HasColumnName("LanguageID");

                entity.Property(e => e.Title).HasMaxLength(100);
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("Type");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Type1)
                    .HasMaxLength(70)
                    .HasColumnName("Type");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EMail)
                    .HasMaxLength(90)
                    .HasColumnName("E-mail");

                entity.Property(e => e.FirstName).HasMaxLength(30);

                entity.Property(e => e.LastName).HasMaxLength(30);

                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}