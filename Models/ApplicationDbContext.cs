using GradDemo.Data;
using Microsoft.EntityFrameworkCore;

namespace GradDemo.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<ReturnedOrder> ReturnedOrders { get; set; }
        public virtual DbSet<CancelledOrder> CancelledOrders { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<ItemSize> ItemSize { get; set; }
        public virtual DbSet<Size> Size { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany() 
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); 

            
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Cashier)
                .WithMany() 
                .HasForeignKey(o => o.CashierId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Waiter)
                .WithMany() 
                .HasForeignKey(o => o.WaiterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Captain)
                .WithMany() 
                .HasForeignKey(o => o.CaptainId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Table)
                .WithMany()
                .HasForeignKey(o => o.TableId)
                .OnDelete(DeleteBehavior.Restrict); 
                
                
                 modelBuilder.Seed();
        }

    }
}
