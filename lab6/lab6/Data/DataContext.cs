using lab6.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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

            SeedData(modelBuilder);
        }

        public void SeedData(ModelBuilder modelBuilder)
        {
            // Platforms
            modelBuilder.Entity<Platform>().HasData(
                new Platform { Platform_Code = "WEB", Platform_Name = "Web Platform" },
                new Platform { Platform_Code = "MOB", Platform_Name = "Mobile Platform" }
            );

            // RefDocumentTypes
            modelBuilder.Entity<RefDocumentType>().HasData(
                new RefDocumentType { Document_Type_Code = "INV", Document_Type_Description = "Invoice" },
                new RefDocumentType { Document_Type_Code = "RCP", Document_Type_Description = "Receipt" }
            );

            // Suppliers
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { Supplier_ID = 1, Supplier_Name = "Tech Supplier" },
                new Supplier { Supplier_ID = 2, Supplier_Name = "Service Provider" }
            );

            // Locations
            modelBuilder.Entity<Location>().HasData(
                new Location { Location_ID = 1, Location_Name = "Main Office", Other_Details = "123 Business St" },
                new Location { Location_ID = 2, Location_Name = "Branch Office", Other_Details = "456 Corporate Ave" }
            );

            // Staff
            modelBuilder.Entity<Staff>().HasData(
                new Staff { Staff_ID = 1, Staff_Name = "John Doe", Other_Details = "john@company.com" },
                new Staff { Staff_ID = 2, Staff_Name = "Jane Smith", Other_Details = "jane@company.com" }
            );

            // Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Customer_ID = 1, Customer_Name = "Acme Corp", Other_Details = "contact@acme.com" },
                new Customer { Customer_ID = 2, Customer_Name = "Tech Solutions", Other_Details = "info@techsolutions.com" }
            );

            // Channels
            modelBuilder.Entity<Channel>().HasData(
                new Channel { Channel_ID = 1, Channel_Name = "Online Sales" },
                new Channel { Channel_ID = 2, Channel_Name = "Direct Sales" }
            );

            // ProductServices
            modelBuilder.Entity<ProductService>().HasData(
                new ProductService
                {
                    Prod_Service_Code = "SOFT01",
                    Prod_Service_Name = "Software License",
                    Supplier_ID = 1
                },
                new ProductService
                {
                    Prod_Service_Code = "HARD02",
                    Prod_Service_Name = "Hardware Support",
                    Supplier_ID = 2
                }
            );

            // EventSequences
            modelBuilder.Entity<EventSequence>().HasData(
                new EventSequence
                {
                    Event_Sequence_ID = 1,
                    Other_Details = "Sales Process",
                    Next_Event_Sequence_ID = 2
                },
                new EventSequence
                {
                    Event_Sequence_ID = 2,
                    Other_Details = "Delivery Process"
                }
            );

            // Events
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Event_ID = 1,
                    Other_Details = "Product Sale",
                    Event_Date_Time = DateTime.Now,
                    Event_Amount = 1000.50m,
                    Location_ID = 1,
                    Staff_ID = 1,
                    Customer_ID = 1,
                    Channel_ID = 1,
                    Artefact_ID = 1,
                    Event_Sequence_ID = 1
                }
            );

            // Payments
            modelBuilder.Entity<Payment>().HasData(
                new Payment
                {
                    Payment_ID = 1,
                    Payment_Amount = 1000.50m,
                    Payment_Date = DateTime.Now,
                    Event_ID = 1
                }
            );

            // Documents
            modelBuilder.Entity<Document>().HasData(
                new Document
                {
                    Document_ID = 1,
                    Document_Name = "Sales Invoice",
                    Event_ID = 1,
                    Document_Type_Code = "INV"
                }
            );

            // Artefacts
            modelBuilder.Entity<Artefact>().HasData(
                new Artefact
                {
                    Artefact_ID = 1,
                    Artefact_Name = "Product Sale Artefact"
                }
            );

            // CustomerDataPlatforms
            modelBuilder.Entity<CustomerDataPlatform>().HasData(
                new CustomerDataPlatform
                {
                    Customer_Name = "1",
                    Platform_Code = "WEB"
                }
            );

            // GenericServices
            modelBuilder.Entity<GenericService>().HasData(
                new GenericService
                {
                    Service_Code = "CONSULT",
                    Service_Name = "Consulting Service"
                }
            );
        }
    }
}