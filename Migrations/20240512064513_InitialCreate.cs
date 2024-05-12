using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileWatcherApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "changeTypes",
                columns: table => new
                {
                    ChangeTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChangeTypeName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_changeTypes", x => x.ChangeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "fileDetails",
                columns: table => new
                {
                    FileDetailsId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TaskDetailsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChangeTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fileDetails", x => x.FileDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "logging",
                columns: table => new
                {
                    LoggingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LogDetails = table.Column<string>(type: "TEXT", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logging", x => x.LoggingId);
                });

            migrationBuilder.CreateTable(
                name: "taskDetails",
                columns: table => new
                {
                    TaskDetailsId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskNameId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taskDetails", x => x.TaskDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "taskName",
                columns: table => new
                {
                    TaskNameId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taskName", x => x.TaskNameId);
                });

            migrationBuilder.CreateTable(
                name: "taskStatus",
                columns: table => new
                {
                    TaskStatusId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StatusName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taskStatus", x => x.TaskStatusId);
                });

            migrationBuilder.InsertData(
                table: "changeTypes",
                columns: new[] { "ChangeTypeId", "ChangeTypeName" },
                values: new object[] { 1, "File Added" });

            migrationBuilder.InsertData(
                table: "changeTypes",
                columns: new[] { "ChangeTypeId", "ChangeTypeName" },
                values: new object[] { 2, "File Deleted" });

            migrationBuilder.InsertData(
                table: "taskName",
                columns: new[] { "TaskNameId", "Name" },
                values: new object[] { 1, "Task Started" });

            migrationBuilder.InsertData(
                table: "taskName",
                columns: new[] { "TaskNameId", "Name" },
                values: new object[] { 2, "Task Ended" });

            migrationBuilder.InsertData(
                table: "taskStatus",
                columns: new[] { "TaskStatusId", "StatusName" },
                values: new object[] { 1, "Running" });

            migrationBuilder.InsertData(
                table: "taskStatus",
                columns: new[] { "TaskStatusId", "StatusName" },
                values: new object[] { 2, "Ended" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "changeTypes");

            migrationBuilder.DropTable(
                name: "fileDetails");

            migrationBuilder.DropTable(
                name: "logging");

            migrationBuilder.DropTable(
                name: "taskDetails");

            migrationBuilder.DropTable(
                name: "taskName");

            migrationBuilder.DropTable(
                name: "taskStatus");
        }
    }
}
