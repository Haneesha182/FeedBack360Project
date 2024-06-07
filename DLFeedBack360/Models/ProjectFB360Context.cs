using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DLFeedBack360.Models
{
    public partial class ProjectFB360Context : DbContext
    {
        public ProjectFB360Context()
        {
        }

        public ProjectFB360Context(DbContextOptions<ProjectFB360Context> options)
            : base(options)
        {
        }

        public virtual DbSet<CountryDetail> CountryDetails { get; set; } = null!;
        public virtual DbSet<FeedbackCategoryDetail> FeedbackCategoryDetails { get; set; } = null!;
        public virtual DbSet<FeedbackQuestion> FeedbackQuestions { get; set; } = null!;
        public virtual DbSet<FeedbackRating> FeedbackRatings { get; set; } = null!;
        public virtual DbSet<FeedbackScheduleDetail> FeedbackScheduleDetails { get; set; } = null!;
        public virtual DbSet<LanguageDetail> LanguageDetails { get; set; } = null!;
        public virtual DbSet<RoleDetail> RoleDetails { get; set; } = null!;
        public virtual DbSet<UserDetail> UserDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-TRS943T;Database=ProjectFB360;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
