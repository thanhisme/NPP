using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class Add_Assignmenttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Positions_positionId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "positionId",
                table: "Users",
                newName: "PositionId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_positionId",
                table: "Users",
                newName: "IX_Users_PositionId");

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssigneeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignments_Users_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_AssigneeId",
                table: "Assignments",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ProjectId",
                table: "Assignments",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Positions_PositionId",
                table: "Users",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Positions_PositionId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "Users",
                newName: "positionId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_PositionId",
                table: "Users",
                newName: "IX_Users_positionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Positions_positionId",
                table: "Users",
                column: "positionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
