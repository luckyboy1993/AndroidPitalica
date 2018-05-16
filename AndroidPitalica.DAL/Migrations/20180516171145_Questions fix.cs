using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AndroidPitalica.DAL.Migrations
{
    public partial class Questionsfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CorrectAnswer",
                table: "QuestionResults",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Answered",
                table: "QuestionResults",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ExamId",
                table: "QuestionResults",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResults_ExamId",
                table: "QuestionResults",
                column: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionResults_Exams_ExamId",
                table: "QuestionResults",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionResults_Exams_ExamId",
                table: "QuestionResults");

            migrationBuilder.DropIndex(
                name: "IX_QuestionResults_ExamId",
                table: "QuestionResults");

            migrationBuilder.DropColumn(
                name: "ExamId",
                table: "QuestionResults");

            migrationBuilder.AlterColumn<int>(
                name: "CorrectAnswer",
                table: "QuestionResults",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Answered",
                table: "QuestionResults",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
