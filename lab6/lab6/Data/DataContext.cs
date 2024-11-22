using lab6.Models;
using Microsoft.EntityFrameworkCore;

namespace lab6.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        // DbSet properties
        public DbSet<Artefact> Artefacts { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerDataPlatform> CustomerDataPlatforms { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventSequence> EventSequences { get; set; }
        public DbSet<GenericService> GenericServices { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<ProductService> ProductServices { get; set; }
        public DbSet<RefDocumentType> RefDocumentTypes { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Artefact configurations
            modelBuilder.Entity<Artefact>()
                .HasMany(a => a.Events)
                .WithOne(e => e.Artefact)
                .HasForeignKey(e => e.Artefact_ID)
                .OnDelete(DeleteBehavior.Restrict);

            // Channel configurations
            modelBuilder.Entity<Channel>()
                .HasMany(c => c.Events)
                .WithOne(e => e.Channel)
                .HasForeignKey(e => e.Channel_ID)
                .OnDelete(DeleteBehavior.Restrict);

            // Customer configurations
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Events)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.Customer_ID)
                .OnDelete(DeleteBehavior.Restrict);

            // Event configurations
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Location)
                .WithMany(l => l.Events)
                .HasForeignKey(e => e.Location_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Staff)
                .WithMany(s => s.Events)
                .HasForeignKey(e => e.Staff_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.EventSequence)
                .WithMany(es => es.Events)
                .HasForeignKey(e => e.Event_Sequence_ID)
                .OnDelete(DeleteBehavior.Restrict);

            // EventSequence configurations
            modelBuilder.Entity<EventSequence>()
                .HasOne(es => es.NextSequence)
                .WithMany()
                .HasForeignKey(es => es.Next_Event_Sequence_ID)
                .OnDelete(DeleteBehavior.Restrict);

            // Payment configurations
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Event)
                .WithMany(e => e.Payments)
                .HasForeignKey(p => p.Event_ID)
                .OnDelete(DeleteBehavior.Restrict);

            // ProductService configurations
            modelBuilder.Entity<ProductService>()
                .HasOne(ps => ps.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(ps => ps.Supplier_ID)
                .OnDelete(DeleteBehavior.Restrict);

            // Document configurations
            modelBuilder.Entity<Document>()
                .HasOne(d => d.Event)
                .WithMany(e => e.Documents)
                .HasForeignKey(d => d.Event_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.DocumentType)
                .WithMany(dt => dt.Documents)
                .HasForeignKey(d => d.Document_Type_Code)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure decimal properties for money
            modelBuilder.Entity<Event>()
                .Property(e => e.Event_Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Payment>()
                .Property(p => p.Payment_Amount)
                .HasColumnType("decimal(18,2)");

            // Configure unique constraints
            modelBuilder.Entity<CustomerDataPlatform>()
                .HasIndex(cdp => cdp.Platform_Code)
                .IsUnique();

            modelBuilder.Entity<Platform>()
                .HasIndex(p => p.Platform_Code)
                .IsUnique();

            modelBuilder.Entity<ProductService>()
                .HasIndex(ps => ps.Prod_Service_Code)
                .IsUnique();

            modelBuilder.Entity<GenericService>()
                .HasIndex(gs => gs.Service_Code)
                .IsUnique();

            modelBuilder.Entity<RefDocumentType>()
                .HasIndex(rdt => rdt.Document_Type_Code)
                .IsUnique();
        }
    }    
}
