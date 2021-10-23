using HrMan.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HrMan.Models.Db
{
    public class EmployeeDbContext : IdentityDbContext<AppUser>
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> contextOptions) : base(contextOptions)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Salary> Salaries { get; set; }

        public DbSet<EmployeeLeave> EmployeeLeaves { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Address>()
                .Property(a => a.TypeOfAddress)
                .HasConversion<int>();
           
            builder.Entity<Salary>()
                .Property(s => s.PayGrade)
                .HasConversion<string>();

            builder.Entity<EmployeeLeave>()
                .Property(e => e.LeaveType)
                .HasConversion<int>();

            builder.Entity<Organization>()
                .HasMany(o => o.Departments)
                .WithOne(d => d.Organization);

            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                // EF Core 5
                property.SetPrecision(18);
                property.SetScale(6);
            }


            base.OnModelCreating(builder);

        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

                foreach (var entityEntry in entries)
                {
                    ((BaseEntity)entityEntry.Entity).UpdatedOn = DateTime.Now;

                    if (entityEntry.State == EntityState.Added)
                    {
                        ((BaseEntity)entityEntry.Entity).CreatedOn = DateTime.Now;
                    }
                }
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedOn = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedOn = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
