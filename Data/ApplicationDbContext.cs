using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LearningManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<AssignmentSubmission> AssignmentSubmissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // one Course(c) -> many Enrollments(e)
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Enrollments)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId);

            // one course(c) -> many Assignments(a)
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Assignments)
                .WithOne(a => a.Course)
                .HasForeignKey(a => a.CourseId);

            // one Assignment(a) -> many AssignmentSubmissions(s)
            modelBuilder.Entity<Assignment>()
                .HasMany(a => a.Submissions)
                .WithOne(s => s.Assignment)
                .HasForeignKey(s => s.AssignmentId);

            // one Student(s)-> many Enrollments(e)
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(s => s.Enrollments)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            //one Student(s) -> many AssignmentSubmissions(a)
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(s => s.AssignmentSubmissions)
                .WithOne(a => a.Student)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            //one Instructor(i) -> many Courses(c)
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(i => i.CoursesCreated)
                .WithOne(c => c.Instructor)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);


            // all DefaultValues needed
            // Enrollment
            modelBuilder.Entity<Enrollment>()
                .Property(e => e.EnrollmentDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Enrollment>()
                .Property(e => e.Status)
                .HasDefaultValue("Enrolled");

            // Assignment
            modelBuilder.Entity<Assignment>()
                .Property(a => a.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            // make the dueDate after 7 days form the Creation by Default
            modelBuilder.Entity<Assignment>()
                .Property(a => a.DueDate)
                .HasDefaultValueSql("DATEADD(day, 7, GETDATE())");

            // Submission
            modelBuilder.Entity<AssignmentSubmission>()
                .Property(a => a.SubmittedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<AssignmentSubmission>()
                .Property(a => a.Grade)
                .HasDefaultValue(0);

            modelBuilder.Entity<AssignmentSubmission>()
                .Property(a => a.IsCorrected)
                .HasDefaultValue(false);

        }
        

    }
}
