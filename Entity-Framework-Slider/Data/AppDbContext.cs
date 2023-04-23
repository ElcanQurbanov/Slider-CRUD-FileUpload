using Entity_Framework_Slider.Models;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Slider.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
            
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<SliderInfo> SliderInfos { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Advantage> Advantages { get; set; }
        public DbSet<Instagram> Instagrams { get; set; }
        public DbSet<Say> Says { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<BlogHeader> BlogHeaders { get; set; }
        public DbSet<ExpertHeader> ExpertHeaders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Blog>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<BlogHeader>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Category>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Slider>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<SliderInfo>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<ExpertHeader>().HasQueryFilter(m => !m.SoftDelete);


            modelBuilder.Entity<Setting>()
                   .HasData(
                    new Setting
                    {
                        Id = 1,
                        Key = "HeaderLogo",
                        Value = "logo.png"
                    },
                    new Setting
                    {
                        Id = 2,
                        Key = "Phone",
                        Value = "357289953"
                    },
                    new Setting
                    {
                        Id = 3,
                        Key = "Email",
                        Value = "p135@code.edu.az"
                    }
              );

            modelBuilder.Entity<BlogHeader>()
                       .HasData(
                        new BlogHeader
                        {
                            Id = 1,
                            Title = "Hello P135",
                            Description = "how are you?",
                            
                        },
                        new BlogHeader
                        {
                            Id = 2,
                            Title = "Hello P414",
                            Description = "how are you?"
                        }

                  );
        }

    }    
    
}
