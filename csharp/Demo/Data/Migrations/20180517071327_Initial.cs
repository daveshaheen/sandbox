using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Demo.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "EmployeeAudit",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(defaultValueSql: "sysdatetimeoffset()"),
                    Deleted = table.Column<DateTimeOffset>(nullable: true),
                    Dependents = table.Column<int>(),
                    Email = table.Column<string>(maxLength: 254),
                    EmployeeId = table.Column<int>(),
                    EmployerId = table.Column<int>(),
                    FirstName = table.Column<string>(maxLength: 100),
                    LastName = table.Column<string>(maxLength: 100),
                    MiddleName = table.Column<string>(maxLength: 100, nullable: true),
                    Modified = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table => table.PrimaryKey("PK_EmployeeAudit", x => x.Id));

            migrationBuilder.CreateTable(
                "Employer",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(defaultValueSql: "sysdatetimeoffset()"),
                    Deleted = table.Column<DateTimeOffset>(nullable: true),
                    Modified = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(maxLength: 300)
                },
                constraints: table => table.PrimaryKey("PK_Employer", x => x.Id));

            migrationBuilder.CreateTable(
                "EmployerAudit",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(defaultValueSql: "sysdatetimeoffset()"),
                    Deleted = table.Column<DateTimeOffset>(nullable: true),
                    EmployerId = table.Column<int>(),
                    Modified = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(maxLength: 300)
                },
                constraints: table => table.PrimaryKey("PK_EmployerAudit", x => x.Id));

            migrationBuilder.CreateTable(
                "Employee",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(defaultValueSql: "sysdatetimeoffset()"),
                    Deleted = table.Column<DateTimeOffset>(nullable: true),
                    Dependents = table.Column<int>(),
                    Email = table.Column<string>(maxLength: 254),
                    EmployerId = table.Column<int>(),
                    FirstName = table.Column<string>(maxLength: 100),
                    LastName = table.Column<string>(maxLength: 100),
                    MiddleName = table.Column<string>(maxLength: 100, nullable: true),
                    Modified = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        "FK_Employee_Employer_EmployerId",
                        x => x.EmployerId,
                        "Employer",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Employee_EmployerId",
                "Employee",
                "EmployerId");

            migrationBuilder.CreateIndex(
                "IX_Employee_Email",
                "Employee",
                "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Employer_Name",
                "Employer",
                "Name",
                unique: true);

            migrationBuilder.Sql(@"
CREATE OR ALTER TRIGGER Employee_OnUpdate ON Employee
AFTER UPDATE AS
BEGIN
SET ANSI_DEFAULTS ON;
SET NOCOUNT ON;
    IF NOT UPDATE(Modified)
    BEGIN
        UPDATE Employee SET Modified = sysdatetimeoffset() WHERE Id IN (SELECT Id FROM deleted);
    END;
    INSERT INTO EmployeeAudit (Created, Deleted, Dependents, Email, EmployeeId, EmployerId, FirstName, LastName, MiddleName, Modified)
    SELECT Created, Deleted, Dependents, Email, Id AS EmployeeId, EmployerId, FirstName, LastName, MiddleName, Modified FROM deleted;
SET NOCOUNT OFF;
END;");

            migrationBuilder.Sql(@"
CREATE OR ALTER TRIGGER Employee_OnDelete ON Employee
AFTER DELETE AS
BEGIN
SET ANSI_DEFAULTS ON;
SET NOCOUNT ON;
    INSERT INTO EmployeeAudit (Created, Deleted, Dependents, Email, EmployeeId, EmployerId, FirstName, LastName, MiddleName, Modified)
    SELECT Created, ISNULL(Deleted, sysdatetimeoffset()), Dependents, Email, Id AS EmployeeId, EmployerId, FirstName, LastName, MiddleName, Modified FROM deleted;
SET NOCOUNT OFF;
END;");

            migrationBuilder.Sql(@"
CREATE OR ALTER TRIGGER Employer_OnUpdate ON Employer
AFTER UPDATE AS
BEGIN
SET ANSI_DEFAULTS ON;
SET NOCOUNT ON;
    IF NOT UPDATE(Modified)
    BEGIN
        UPDATE Employer SET Modified = sysdatetimeoffset() WHERE Id IN (SELECT Id FROM deleted);
    END;
    INSERT INTO EmployerAudit (Created, Deleted, EmployerId, Modified, Name)
    SELECT Created, Deleted, Id AS EmployerId, Modified, Name FROM deleted;
SET NOCOUNT OFF;
END;");

            migrationBuilder.Sql(@"
CREATE OR ALTER TRIGGER Employer_OnDelete ON Employer
AFTER DELETE AS
BEGIN
SET ANSI_DEFAULTS ON;
SET NOCOUNT ON;
    INSERT INTO EmployerAudit (Created, Deleted, EmployerId, Modified, Name)
    SELECT Created, ISNULL(Deleted, sysdatetimeoffset()), Id AS EmployerId, Modified, Name FROM deleted;
SET NOCOUNT OFF;
END;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Employee");

            migrationBuilder.DropTable(
                "EmployeeAudit");

            migrationBuilder.DropTable(
                "EmployerAudit");

            migrationBuilder.DropTable(
                "Employer");
        }
    }
}
