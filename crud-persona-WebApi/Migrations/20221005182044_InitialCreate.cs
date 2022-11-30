using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CrudPersonasWebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactMeans",
                columns: table => new
                {
                    ContactMeansId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ContactM__9F789A2F1523E0AF", x => x.ContactMeansId);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Identification = table.Column<string>(type: "character varying(11)", unicode: false, maxLength: 11, nullable: false),
                    Birth = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "ContactMeansPerson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContactMeansId = table.Column<int>(type: "integer", nullable: true),
                    PersonId = table.Column<int>(type: "integer", nullable: true),
                    Contact = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMeansPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ContactMe__Conta__3F466844",
                        column: x => x.ContactMeansId,
                        principalTable: "ContactMeans",
                        principalColumn: "ContactMeansId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ContactMe__Perso__403A8C7D",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactMeansPerson_ContactMeansId",
                table: "ContactMeansPerson",
                column: "ContactMeansId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactMeansPerson_PersonId",
                table: "ContactMeansPerson",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactMeansPerson");

            migrationBuilder.DropTable(
                name: "ContactMeans");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
