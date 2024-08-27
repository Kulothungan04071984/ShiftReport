using DocumentFormat.OpenXml.InkML;
using Fuji_I.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace Fuji_I.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Prod_data> work_order_mapping { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the table name and primary key
            modelBuilder.Entity<Prod_data>()
                .ToTable("work_order_mapping") // Specify the table name in the database
                .HasKey(p => p.Id); // Specify the primary key

            // Configuring individual properties
            modelBuilder.Entity<Prod_data>()
                .Property(p => p.Workorder_no)
                .HasColumnName("Workorder_no"); // Specify the column name

            modelBuilder.Entity<Prod_data>()
                .Property(p => p.FG_Name)
                .HasColumnName("FG_Name"); // Specify the column name

            modelBuilder.Entity<Prod_data>()
                .Property(p => p.WO_Quantity)
                .HasColumnName("WO_Quantity"); // Specify the column name

            //modelBuilder.Entity<Prod_data>()
            //    .Property(p => p.uph)
            //    .HasColumnName("uph"); // Specify the column name

            //modelBuilder.Entity<Prod_data>()
            //    .Property(p => p.created_at)
            //    .HasColumnName("created_at"); // Specify the column name
        }
    }
}

