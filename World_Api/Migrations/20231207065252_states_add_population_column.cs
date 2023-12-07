using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace World_Api.Migrations
{
    /// <inheritdoc />
    public partial class states_add_population_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Population",
                table: "States",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Population",
                table: "States");
        }
    }
}
