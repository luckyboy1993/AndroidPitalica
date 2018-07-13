using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AndroidPitalica.DAL.Entities
{
    public partial class PitalicaContext : DbContext
    {
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<QuestionResult> QuestionResults { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserExamTaken> UserExamTaken { get; set; }


        public PitalicaContext(DbContextOptions<PitalicaContext> options)
        : base(options)
        { }

        public PitalicaContext()
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserExamTaken>()
            //.HasKey(t => new { t.UserId, t.ExamId });
        }
    }
}
