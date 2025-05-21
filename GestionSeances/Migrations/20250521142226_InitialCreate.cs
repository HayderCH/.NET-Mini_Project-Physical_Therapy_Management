using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionSeances.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comptes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comptes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kines",
                columns: table => new
                {
                    IdK = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomK = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PrenomK = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kines", x => x.IdK);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    IdP = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nomp = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PrenomP = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    NumTel = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.IdP);
                });

            migrationBuilder.CreateTable(
                name: "Seances",
                columns: table => new
                {
                    SeanceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdK = table.Column<int>(type: "INTEGER", nullable: false),
                    IdP = table.Column<int>(type: "INTEGER", nullable: false),
                    DateS = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HeureS = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    TypeSoin = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seances", x => x.SeanceId);
                    table.ForeignKey(
                        name: "FK_Seances_Kines_IdK",
                        column: x => x.IdK,
                        principalTable: "Kines",
                        principalColumn: "IdK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seances_Patients_IdP",
                        column: x => x.IdP,
                        principalTable: "Patients",
                        principalColumn: "IdP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seances_IdK",
                table: "Seances",
                column: "IdK");

            migrationBuilder.CreateIndex(
                name: "IX_Seances_IdP",
                table: "Seances",
                column: "IdP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comptes");

            migrationBuilder.DropTable(
                name: "Seances");

            migrationBuilder.DropTable(
                name: "Kines");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
