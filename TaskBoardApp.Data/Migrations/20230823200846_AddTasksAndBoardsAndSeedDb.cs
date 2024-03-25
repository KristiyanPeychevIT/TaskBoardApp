using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    public partial class AddTasksAndBoardsAndSeedDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Open" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "In Progress" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Done" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { new Guid("37ab5c42-5a3e-4022-ac61-84f0af867ec4"), 2, new DateTime(2023, 7, 23, 20, 8, 46, 97, DateTimeKind.Utc).AddTicks(7450), "Create Desktop client App for the RESTful TaskBoard service", "8ed158dd-4cdc-4065-ab4d-7923e877e0d0", "Desktop Client App" },
                    { new Guid("39bf853f-f7f7-4376-b61d-6ec266ebe8a6"), 3, new DateTime(2022, 8, 23, 20, 8, 46, 97, DateTimeKind.Utc).AddTicks(7451), "Implement [Create Task] page for adding tasks", "8ed158dd-4cdc-4065-ab4d-7923e877e0d0", "Create Tasks" },
                    { new Guid("74871626-bc29-4502-80a4-47e76f4f3c8c"), 1, new DateTime(2023, 3, 23, 20, 8, 46, 97, DateTimeKind.Utc).AddTicks(7447), "Create Android client App for the RESTful TaskBoard service", "0051562c-226e-48bb-a055-20712b30a33e", "Android Client App" },
                    { new Guid("7747fd46-b22e-430e-ae47-5638c4b41533"), 1, new DateTime(2023, 2, 4, 20, 8, 46, 97, DateTimeKind.Utc).AddTicks(7419), "Implement better styling for all public pages", "8ed158dd-4cdc-4065-ab4d-7923e877e0d0", "Improve CSS styles" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");
        }
    }
}
