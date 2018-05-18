using System;
using System.Linq;
using Common;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    internal sealed class Context : DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeAudit> EmployeeAudit { get; set; }
        public DbSet<Employer> Employer { get; set; }
        public DbSet<EmployerAudit> EmployerAudit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\;Database=Demo;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        /// <inheritdoc />
        public override int SaveChanges()
        {
            var inheritsFromBase = ChangeTracker.Entries().Where(e => e.Entity is Base).ToList();
            foreach (var item in inheritsFromBase)
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        item.Property("Created").CurrentValue = DateTimeOffset.Now;
                        break;
                    case EntityState.Modified:
                        item.Property("Modified").CurrentValue = DateTimeOffset.Now;
                        break;
                    case EntityState.Deleted:
                        item.Property("Deleted").CurrentValue = DateTimeOffset.Now;
                        break;
                }
            }

            return base.SaveChanges();
        }
    }
}
