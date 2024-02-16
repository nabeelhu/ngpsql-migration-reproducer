using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migration.Reproducer.Migrations
{
    /// <inheritdoc />
    public partial class update_enum_types : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:test_schema.status_type", "active,inactive")
                .OldAnnotation("Npgsql:Enum:status_type", "active,inactive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:status_type", "active,inactive")
                .OldAnnotation("Npgsql:Enum:test_schema.status_type", "active,inactive");
        }
    }
}
