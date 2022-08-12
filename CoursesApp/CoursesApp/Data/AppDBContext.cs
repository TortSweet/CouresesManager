using CoursesApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoursesApp.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<Course> Courses { get; set; } = null;
        public DbSet<StudGroup> StudGroups { get; set; } = null;
        public DbSet<Student> Students { get; set; } = null;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseId);

                entity.ToTable("COURSES");
            });
            modelBuilder.Entity<StudGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId);
                entity.HasOne(e => e.Course).WithMany(p => p.StudGroup).HasForeignKey(e => e.CourseId);

                entity.ToTable("STUD_GROUPS");
            });
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentId);
                entity.HasOne(e => e.StudGroup).WithMany(p => p.Student).HasForeignKey(e => e.GroupId);

                entity.ToTable("STUDENTS");
            });

        }
    }
}
