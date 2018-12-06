namespace DrivingSchoolWebApp.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        private DbSet<Course> Courses { get; set; }
        private DbSet<School> Schools { get; set; }
        private DbSet<Customer> Customers { get; set; }
        private DbSet<Trainer> Trainers { get; set; }
        private DbSet<Car> Cars { get; set; }
        private DbSet<Lesson> Lessons { get; set; }
        private DbSet<Order> Orders { get; set; }
        private DbSet<Payment> Payments { get; set; }
        private DbSet<Feedback> Feedbacks { get; set; }
        private DbSet<Exam> Exams { get; set; }
       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
