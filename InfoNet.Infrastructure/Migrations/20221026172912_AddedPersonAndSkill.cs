using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoNet.Infrastructure.Migrations
{
    public partial class AddedPersonAndSkill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    ResumeLink = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PersonId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Name", "PersonId" },
                values: new object[] { new Guid("4e2bb074-953f-4bc4-991e-2f2bfa2d77ff"), "C++", null });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Name", "PersonId" },
                values: new object[] { new Guid("8599cc01-17c2-437b-a557-cfe142707ddd"), "PHP", null });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Name", "PersonId" },
                values: new object[] { new Guid("c4e0ed66-6b79-4c3a-99bb-4bba72624d78"), "Java", null });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Name", "PersonId" },
                values: new object[] { new Guid("cef4f3c3-b936-438b-8d25-9b24b7f4491b"), "C#", null });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Name", "PersonId" },
                values: new object[] { new Guid("d7fe9808-5b69-40c3-a819-d5bb82ea0387"), "SQL", null });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_PersonId",
                table: "Skills",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
