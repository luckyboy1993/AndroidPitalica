using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AndroidPitalica.DAL.Migrations
{
    public partial class questionFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Row",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "WrongAnswers",
                table: "Questions",
                newName: "WrongAnswer3");

            migrationBuilder.RenameColumn(
                name: "Definition",
                table: "Questions",
                newName: "WrongAnswer2");

            migrationBuilder.AddColumn<string>(
                name: "WrongAnswer1",
                table: "Questions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WrongAnswer1",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "WrongAnswer3",
                table: "Questions",
                newName: "WrongAnswers");

            migrationBuilder.RenameColumn(
                name: "WrongAnswer2",
                table: "Questions",
                newName: "Definition");

            migrationBuilder.AddColumn<int>(
                name: "Row",
                table: "Questions",
                nullable: false,
                defaultValue: 0);
        }
    }
}
