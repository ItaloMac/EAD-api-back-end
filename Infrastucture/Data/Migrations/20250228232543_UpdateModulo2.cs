using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModulo2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modulos_Cursos_CursoId",
                table: "Modulos");

            migrationBuilder.DropForeignKey(
                name: "FK_Modulos_Professores_ProfessorId",
                table: "Modulos");

            migrationBuilder.DropIndex(
                name: "IX_Modulos_CursoId",
                table: "Modulos");

            migrationBuilder.DropIndex(
                name: "IX_Modulos_ProfessorId",
                table: "Modulos");

            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "Modulos");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "Modulos");

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_Id_Curso",
                table: "Modulos",
                column: "Id_Curso");

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_Id_Professor",
                table: "Modulos",
                column: "Id_Professor");

            migrationBuilder.AddForeignKey(
                name: "FK_Modulos_Cursos_Id_Curso",
                table: "Modulos",
                column: "Id_Curso",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modulos_Professores_Id_Professor",
                table: "Modulos",
                column: "Id_Professor",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modulos_Cursos_Id_Curso",
                table: "Modulos");

            migrationBuilder.DropForeignKey(
                name: "FK_Modulos_Professores_Id_Professor",
                table: "Modulos");

            migrationBuilder.DropIndex(
                name: "IX_Modulos_Id_Curso",
                table: "Modulos");

            migrationBuilder.DropIndex(
                name: "IX_Modulos_Id_Professor",
                table: "Modulos");

            migrationBuilder.AddColumn<Guid>(
                name: "CursoId",
                table: "Modulos",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfessorId",
                table: "Modulos",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_CursoId",
                table: "Modulos",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_ProfessorId",
                table: "Modulos",
                column: "ProfessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modulos_Cursos_CursoId",
                table: "Modulos",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modulos_Professores_ProfessorId",
                table: "Modulos",
                column: "ProfessorId",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
