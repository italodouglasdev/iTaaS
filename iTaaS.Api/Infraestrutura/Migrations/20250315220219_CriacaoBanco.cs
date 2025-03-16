using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iTaaS.Api.Migrations
{
    public partial class CriacaoBanco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Hash = table.Column<string>(nullable: true),
                    DataHoraRecebimento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogsLinhas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LogId = table.Column<int>(nullable: false),
                    MetodoHttp = table.Column<string>(nullable: true),
                    CodigoStatus = table.Column<int>(nullable: false),
                    CaminhoUrl = table.Column<string>(nullable: true),
                    TempoResposta = table.Column<decimal>(nullable: false),
                    TamahoResposta = table.Column<int>(nullable: false),
                    CacheStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogsLinhas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogsLinhas_Logs_LogId",
                        column: x => x.LogId,
                        principalTable: "Logs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogsLinhas_LogId",
                table: "LogsLinhas",
                column: "LogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogsLinhas");

            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
