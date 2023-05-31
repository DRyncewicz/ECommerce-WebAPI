using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Entities
{
    public class RestaurantDbContext : DbContext
    {
        private string _connectionString = "Data Source=DESKTOP-357FT14\\SQLEXPRESS;Initial Catalog=Restaurant-course;Integrated Security=True;TrustServerCertificate=True";
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();
            
            modelBuilder.Entity<Role>()
                .Property(i=>i.Name)
                .IsRequired(); 
       
            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Dish>()
                .Property(d=> d.Name)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(e=> e.City)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Address>()
                .Property(e => e.Street)
                .IsRequired()
                .HasMaxLength(50);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString); 
        }
    }
}
