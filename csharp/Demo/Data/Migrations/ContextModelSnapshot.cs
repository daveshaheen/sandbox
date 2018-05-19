// <auto-generated />

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;

namespace Demo.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    internal class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Employee", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<DateTimeOffset>("Created");

                b.Property<DateTimeOffset?>("Deleted");

                b.Property<int>("Dependents");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasMaxLength(254);

                b.Property<int>("EmployerId");

                b.Property<string>("FirstName")
                    .IsRequired()
                    .HasMaxLength(100);

                b.Property<string>("LastName")
                    .IsRequired()
                    .HasMaxLength(100);

                b.Property<string>("MiddleName")
                    .HasMaxLength(100);

                b.Property<DateTimeOffset?>("Modified");

                b.HasKey("Id");

                b.HasIndex("EmployerId");

                b.ToTable("Employee");
            });

            modelBuilder.Entity("Data.EmployeeAudit", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<DateTimeOffset>("Created");

                b.Property<DateTimeOffset?>("Deleted");

                b.Property<int>("Dependents");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasMaxLength(254);

                b.Property<int>("EmployeeId");

                b.Property<int>("EmployerId");

                b.Property<string>("FirstName")
                    .IsRequired()
                    .HasMaxLength(100);

                b.Property<string>("LastName")
                    .IsRequired()
                    .HasMaxLength(100);

                b.Property<string>("MiddleName")
                    .HasMaxLength(100);

                b.Property<DateTimeOffset?>("Modified");

                b.HasKey("Id");

                b.ToTable("EmployeeAudit");
            });

            modelBuilder.Entity("Data.Employer", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<DateTimeOffset>("Created");

                b.Property<DateTimeOffset?>("Deleted");

                b.Property<DateTimeOffset?>("Modified");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(300);

                b.HasKey("Id");

                b.ToTable("Employer");
            });

            modelBuilder.Entity("Data.EmployerAudit", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<DateTimeOffset>("Created");

                b.Property<DateTimeOffset?>("Deleted");

                b.Property<int>("EmployerId");

                b.Property<DateTimeOffset?>("Modified");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(300);

                b.HasKey("Id");

                b.ToTable("EmployerAudit");
            });

            modelBuilder.Entity("Data.Employee", b => b.HasOne("Data.Employer", "Employer")
                                    .WithMany("Employees")
                                    .HasForeignKey("EmployerId")
                                    .OnDelete(DeleteBehavior.Cascade));
#pragma warning restore 612, 618
        }
    }
}
