using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartGrades.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNamesMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Estudiantes_IdEstudiante",
                table: "Notas");

            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Profesores_IdProfesor",
                table: "Notas");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Profesores",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "Notas",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Notas",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "IdProfesor",
                table: "Notas",
                newName: "IdTeacher");

            migrationBuilder.RenameColumn(
                name: "IdEstudiante",
                table: "Notas",
                newName: "IdStudent");

            migrationBuilder.RenameIndex(
                name: "IX_Notas_IdProfesor",
                table: "Notas",
                newName: "IX_Notas_IdTeacher");

            migrationBuilder.RenameIndex(
                name: "IX_Notas_IdEstudiante",
                table: "Notas",
                newName: "IX_Notas_IdStudent");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Estudiantes",
                newName: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Estudiantes_IdStudent",
                table: "Notas",
                column: "IdStudent",
                principalTable: "Estudiantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Profesores_IdTeacher",
                table: "Notas",
                column: "IdTeacher",
                principalTable: "Profesores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Estudiantes_IdStudent",
                table: "Notas");

            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Profesores_IdTeacher",
                table: "Notas");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Profesores",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Notas",
                newName: "Valor");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Notas",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "IdTeacher",
                table: "Notas",
                newName: "IdProfesor");

            migrationBuilder.RenameColumn(
                name: "IdStudent",
                table: "Notas",
                newName: "IdEstudiante");

            migrationBuilder.RenameIndex(
                name: "IX_Notas_IdTeacher",
                table: "Notas",
                newName: "IX_Notas_IdProfesor");

            migrationBuilder.RenameIndex(
                name: "IX_Notas_IdStudent",
                table: "Notas",
                newName: "IX_Notas_IdEstudiante");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Estudiantes",
                newName: "Nombre");

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Estudiantes_IdEstudiante",
                table: "Notas",
                column: "IdEstudiante",
                principalTable: "Estudiantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Profesores_IdProfesor",
                table: "Notas",
                column: "IdProfesor",
                principalTable: "Profesores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
