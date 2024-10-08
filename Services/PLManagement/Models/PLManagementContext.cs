﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TMS.Models;

namespace PLManagement.Models
{
    public partial class PLManagementContext : DbContext
    {
        public PLManagementContext()
        {
        }

        public PLManagementContext(DbContextOptions<PLManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Form> Forms { get; set; }
        public virtual DbSet<Plstatus> Plstatuses { get; set; }
        public virtual DbSet<ProposalLetter> ProposalLetters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=ASPLAP3102;Database=PLManagement;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Form>(entity =>
            {
                entity.Property(e => e.Plid).HasColumnName("PLId");

                entity.HasOne(d => d.Pl)
                    .WithMany(p => p.Forms)
                    .HasForeignKey(d => d.Plid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Forms__PLId__5535A963");
            });

            modelBuilder.Entity<Plstatus>(entity =>
            {
                entity.ToTable("PLStatus");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Status)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProposalLetter>(entity =>
            {
                entity.ToTable("ProposalLetter");

                entity.Property(e => e.AssessmentYear).HasMaxLength(20);

                entity.Property(e => e.PdfUrl).HasColumnName("pdfUrl");

                entity.Property(e => e.PlstatusId).HasColumnName("PLStatusId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
