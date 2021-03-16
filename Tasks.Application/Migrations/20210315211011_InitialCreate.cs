using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Tasks.Application.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "task");

            migrationBuilder.CreateTable(
                name: "task",
                schema: "task",
                columns: table => new
                {
                    id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(150)", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    completed = table.Column<bool>(type: "bool", nullable: false),
                    estimated_required_hours = table.Column<int>(type: "int4", nullable: false),
                    stack = table.Column<string>(type: "varchar(150)", nullable: false),
                    minimum_roles = table.Column<string>(type: "text", nullable: true),
                    assigned_id_number = table.Column<long>(type: "bigint", nullable: false),
                    parent_task_id = table.Column<int>(type: "int4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_task_task_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_task_task_parent_task_id",
                        column: x => x.parent_task_id,
                        principalSchema: "task",
                        principalTable: "task",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_task_parent_task_id",
                schema: "task",
                table: "task",
                column: "parent_task_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "task",
                schema: "task");
        }
    }
}
