using Migration.Reproducer.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Migration.Reproducer.Migrations
{
    public partial class initial_create : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE TYPE status_type AS ENUM ('active', 'inactive')");

            migrationBuilder.EnsureSchema(
            name: "test_schema");
            migrationBuilder.Sql("DROP SCHEMA test_schema CASCADE");
            migrationBuilder.EnsureSchema(
                      name: "test_schema");

            migrationBuilder.CreateTable(
                name: "MyEntities",
                schema: "test_schema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<StatusType>(type: "status_type", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyEntities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyEntities",
                schema: "test_schema");
        }
    }
}
