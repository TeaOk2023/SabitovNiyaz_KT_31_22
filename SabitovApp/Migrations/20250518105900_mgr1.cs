using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SabitovApp.Migrations
{
    /// <inheritdoc />
    public partial class mgr1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicDegrees",
                columns: table => new
                {
                    AcademicDegreeId = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор ученой степени")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Название ученой степени")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicDegrees", x => x.AcademicDegreeId);
                });

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    DisciplineId = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор дисциплины")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Название дисциплины")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.DisciplineId);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionId = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор должности")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Название должности")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.PositionId);
                });

            migrationBuilder.CreateTable(
                name: "Workloads",
                columns: table => new
                {
                    WorkloadId = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор нагрузки")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hours = table.Column<int>(type: "int", nullable: false, comment: "Количество часов нагрузки"),
                    DisciplineId = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор дисциплины (ссылка на DisciplineId)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workloads", x => x.WorkloadId);
                    table.ForeignKey(
                        name: "FK_Workloads_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "DisciplineId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор кафедры")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Название кафедры"),
                    HeadOfDepartmentId = table.Column<int>(type: "int", nullable: true, comment: "Идентификатор заведующего кафедрой (ссылка на TeacherId)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор преподавателя")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Имя преподавателя"),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Фамилия преподавателя"),
                    AcademicDegreeId = table.Column<int>(type: "int", nullable: true, comment: "Ученая степень"),
                    PositionId = table.Column<int>(type: "int", nullable: false, comment: "Должность"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор кафедры")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherId);
                    table.ForeignKey(
                        name: "FK_Teachers_AcademicDegrees_AcademicDegreeId",
                        column: x => x.AcademicDegreeId,
                        principalTable: "AcademicDegrees",
                        principalColumn: "AcademicDegreeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teachers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teachers_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherDisciplines",
                columns: table => new
                {
                    DisciplinesDisciplineId = table.Column<int>(type: "int", nullable: false),
                    TeachersTeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherDisciplines", x => new { x.DisciplinesDisciplineId, x.TeachersTeacherId });
                    table.ForeignKey(
                        name: "FK_TeacherDisciplines_Disciplines_DisciplinesDisciplineId",
                        column: x => x.DisciplinesDisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "DisciplineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherDisciplines_Teachers_TeachersTeacherId",
                        column: x => x.TeachersTeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Связь между преподавателями и дисциплинами");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_HeadOfDepartmentId",
                table: "Departments",
                column: "HeadOfDepartmentId",
                unique: true,
                filter: "[HeadOfDepartmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherDisciplines_TeachersTeacherId",
                table: "TeacherDisciplines",
                column: "TeachersTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_AcademicDegreeId",
                table: "Teachers",
                column: "AcademicDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_DepartmentId",
                table: "Teachers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_PositionId",
                table: "Teachers",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Workloads_DisciplineId",
                table: "Workloads",
                column: "DisciplineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Teachers_HeadOfDepartmentId",
                table: "Departments",
                column: "HeadOfDepartmentId",
                principalTable: "Teachers",
                principalColumn: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Teachers_HeadOfDepartmentId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "TeacherDisciplines");

            migrationBuilder.DropTable(
                name: "Workloads");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "AcademicDegrees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
