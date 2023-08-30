using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TesteApiGmillView.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "empresa",
                columns: table => new
                {
                    idempresa = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    endereco = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresa", x => x.idempresa);
                });

            migrationBuilder.CreateTable(
                name: "funcionario",
                columns: table => new
                {
                    idfuncionario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idempresa = table.Column<int>(type: "integer", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    documento = table.Column<string>(type: "text", nullable: false),
                    celular = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_funcionario", x => x.idfuncionario);
                    table.ForeignKey(
                        name: "FK_funcionario_empresa_idempresa",
                        column: x => x.idempresa,
                        principalTable: "empresa",
                        principalColumn: "idempresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "projeto",
                columns: table => new
                {
                    idprojeto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idempresa = table.Column<int>(type: "integer", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projeto", x => x.idprojeto);
                    table.ForeignKey(
                        name: "FK_projeto_empresa_idempresa",
                        column: x => x.idempresa,
                        principalTable: "empresa",
                        principalColumn: "idempresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProject",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "integer", nullable: false),
                    ProjectsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProject", x => new { x.EmployeesId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_EmployeeProject_funcionario_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "funcionario",
                        principalColumn: "idfuncionario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeProject_projeto_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "projeto",
                        principalColumn: "idprojeto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "funcionario_projeto",
                columns: table => new
                {
                    idfuncionario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idprojeto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_funcionario_projeto", x => x.idfuncionario);
                    table.ForeignKey(
                        name: "FK_funcionario_projeto_funcionario_idfuncionario",
                        column: x => x.idfuncionario,
                        principalTable: "funcionario",
                        principalColumn: "idfuncionario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_funcionario_projeto_projeto_idprojeto",
                        column: x => x.idprojeto,
                        principalTable: "projeto",
                        principalColumn: "idprojeto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProject_ProjectsId",
                table: "EmployeeProject",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_funcionario_idempresa",
                table: "funcionario",
                column: "idempresa");

            migrationBuilder.CreateIndex(
                name: "IX_funcionario_projeto_idprojeto",
                table: "funcionario_projeto",
                column: "idprojeto");

            migrationBuilder.CreateIndex(
                name: "IX_projeto_idempresa",
                table: "projeto",
                column: "idempresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeProject");

            migrationBuilder.DropTable(
                name: "funcionario_projeto");

            migrationBuilder.DropTable(
                name: "funcionario");

            migrationBuilder.DropTable(
                name: "projeto");

            migrationBuilder.DropTable(
                name: "empresa");
        }
    }
}
