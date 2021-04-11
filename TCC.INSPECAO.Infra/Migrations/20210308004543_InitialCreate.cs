using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TCC.INSPECAO.Infra.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CLAIM",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Valor = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLAIM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ESTABELECIMENTO",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    CNPJ = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTABELECIMENTO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INSPECAO_STATUS",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INSPECAO_STATUS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TURNO",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Sigla = table.Column<string>(nullable: true),
                    HoraInicio = table.Column<DateTime>(nullable: false),
                    HoraFim = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TURNO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UNIDADE_MEDIDA",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    TipoDado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UNIDADE_MEDIDA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SISTEMA",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    NumeroOrdem = table.Column<int>(nullable: false),
                    EstabelecimentoId = table.Column<Guid>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SISTEMA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SISTEMA_ESTABELECIMENTO_EstabelecimentoId",
                        column: x => x.EstabelecimentoId,
                        principalTable: "ESTABELECIMENTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EstabelecimentoId = table.Column<Guid>(nullable: true),
                    IdFirebase = table.Column<string>(nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USUARIO_ESTABELECIMENTO_EstabelecimentoId",
                        column: x => x.EstabelecimentoId,
                        principalTable: "ESTABELECIMENTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SISTEMA_ITEM",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    NumeroOrdem = table.Column<int>(nullable: false),
                    UnidadeMedidaId = table.Column<Guid>(nullable: true),
                    SistemaId = table.Column<Guid>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SISTEMA_ITEM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SISTEMA_ITEM_SISTEMA_SistemaId",
                        column: x => x.SistemaId,
                        principalTable: "SISTEMA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SISTEMA_ITEM_UNIDADE_MEDIDA_UnidadeMedidaId",
                        column: x => x.UnidadeMedidaId,
                        principalTable: "UNIDADE_MEDIDA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INSPECAO",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EstabelecimentoId = table.Column<Guid>(nullable: true),
                    DataHoraInicio = table.Column<DateTime>(nullable: false),
                    DataHoraFim = table.Column<DateTime>(nullable: true),
                    Observacao = table.Column<string>(nullable: true),
                    TurnoId = table.Column<Guid>(nullable: true),
                    UsuarioId = table.Column<Guid>(nullable: true),
                    InspecaoStatusId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INSPECAO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INSPECAO_ESTABELECIMENTO_EstabelecimentoId",
                        column: x => x.EstabelecimentoId,
                        principalTable: "ESTABELECIMENTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INSPECAO_INSPECAO_STATUS_InspecaoStatusId",
                        column: x => x.InspecaoStatusId,
                        principalTable: "INSPECAO_STATUS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INSPECAO_TURNO_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "TURNO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INSPECAO_USUARIO_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "USUARIO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO_CLAIMS",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(nullable: false),
                    ClaimId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_CLAIMS", x => new { x.UsuarioId, x.ClaimId });
                    table.ForeignKey(
                        name: "FK_USUARIO_CLAIMS_CLAIM_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "CLAIM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USUARIO_CLAIMS_USUARIO_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "USUARIO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INSPECAO_ITEM",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InspecaoId = table.Column<Guid>(nullable: true),
                    DataHora = table.Column<DateTime>(nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    Valor = table.Column<string>(nullable: true),
                    SistemaItemId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INSPECAO_ITEM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INSPECAO_ITEM_INSPECAO_InspecaoId",
                        column: x => x.InspecaoId,
                        principalTable: "INSPECAO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INSPECAO_ITEM_SISTEMA_ITEM_SistemaItemId",
                        column: x => x.SistemaItemId,
                        principalTable: "SISTEMA_ITEM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CLAIM",
                columns: new[] { "Id", "Nome", "Valor" },
                values: new object[,]
                {
                    { new Guid("fc1e2284-21fe-4f8b-98d4-b53fd3d25d5b"), "PerfilAcesso", "Administrador" },
                    { new Guid("ba7c4d54-e892-4d1c-ba34-b2c292a38210"), "PerfilAcesso", "Supervisor" },
                    { new Guid("92547574-101a-4b68-9fbe-37502056898d"), "PerfilAcesso", "Técnico" },
                    { new Guid("24320ad6-dcc4-4287-9c19-70c97ba2cc40"), "PerfilAcesso", "Visitante" }
                });

            migrationBuilder.InsertData(
                table: "ESTABELECIMENTO",
                columns: new[] { "Id", "CNPJ", "Nome" },
                values: new object[] { new Guid("cb03e8ff-da79-4012-b309-48af60d36bef"), "19878404002235", "FUNDAÇÃO SÃO FRANCISCO XAVIER - HC HOSPITAL DE CUBATÃO" });

            migrationBuilder.InsertData(
                table: "INSPECAO_STATUS",
                columns: new[] { "Id", "Descricao", "Nome" },
                values: new object[,]
                {
                    { new Guid("b169792d-773e-4381-a9c5-f8dab8f2095f"), "Inspeção em andamento.", "INICIADA" },
                    { new Guid("56fa849f-ca54-4749-9c59-cb01b08c3f5f"), "Inspeção finalizada.", "FINALIZADA" }
                });

            migrationBuilder.InsertData(
                table: "SISTEMA",
                columns: new[] { "Id", "Ativo", "Descricao", "EstabelecimentoId", "Nome", "NumeroOrdem" },
                values: new object[,]
                {
                    { new Guid("36a15387-fd18-4fa4-8b48-c22da6938f3e"), false, "Breve descrição sobre o sistema", null, "SISTEMA ELÉTRICO DE OXIGÊNIO", 7 },
                    { new Guid("0f1c662e-c217-4f69-aba9-061ed5611c19"), false, "Breve descrição sobre o sistema", null, "SISTEMA CENTRAL DE AR COMPRIMIDO", 5 },
                    { new Guid("f30bb314-8b8d-4b28-a32a-76935d828c4c"), false, "Breve descrição sobre o sistema", null, "ABASTECIMENTO DE ÁGUA", 4 },
                    { new Guid("8403f969-5acc-41c7-9b57-930554de6e91"), false, "Breve descrição sobre o sistema", null, "SISTEMA CENTRAL DE VÁCUO", 6 },
                    { new Guid("e38e4d30-3f3f-4d49-a7b0-a4ce3c190279"), false, "Breve descrição sobre o sistema", null, "ABASTECIMENTO DE ÁGUA", 2 },
                    { new Guid("c3b8fc8e-bd6f-4d72-95cf-1af181f8bfa6"), false, "Breve descrição sobre o sistema", null, "CHILLERS E TORRES", 1 },
                    { new Guid("d74b115c-a6cf-482f-bc37-5c1b586927f0"), false, "Breve descrição sobre o sistema", null, "GRUPO GERADOR", 3 }
                });

            migrationBuilder.InsertData(
                table: "TURNO",
                columns: new[] { "Id", "HoraFim", "HoraInicio", "Nome", "Sigla" },
                values: new object[,]
                {
                    { new Guid("2c75a142-05f3-4d46-8c4e-1402149e7174"), new DateTime(1900, 1, 1, 19, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1900, 1, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), "TURNO A", "A" },
                    { new Guid("8751edf1-1b36-484b-8cd0-659ed119c3c5"), new DateTime(1900, 1, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1900, 1, 1, 19, 0, 0, 0, DateTimeKind.Unspecified), "TURNO B", "B" }
                });

            migrationBuilder.InsertData(
                table: "UNIDADE_MEDIDA",
                columns: new[] { "Id", "Nome", "TipoDado" },
                values: new object[,]
                {
                    { new Guid("64aea67a-e78d-4af3-b0f5-b5ab5224d247"), "PRESSÃO", 2 },
                    { new Guid("12433d88-777b-414c-bde2-cb79fdcc21ae"), "VOLTS", 2 },
                    { new Guid("c4967365-135d-4407-b2d6-dbfba3ed19b5"), "GRAUS", 2 },
                    { new Guid("e3ec3baa-74ef-4645-b91d-691daaf58e12"), "LITROS", 2 },
                    { new Guid("9168ab17-4649-4a90-9138-5b8782fd104b"), "ITENS", 1 },
                    { new Guid("bbbf15da-b624-49e9-b0b3-6b94ad4ccbd3"), "METROS", 2 },
                    { new Guid("563c3788-1757-4486-9872-0d95478fa54c"), "CHECK", 4 },
                    { new Guid("5503d65a-6566-4905-b6ea-5a2d49fa391a"), "OBSERVACAO", 3 },
                    { new Guid("adcfd4f3-8a56-4046-a938-c7e53851b6f4"), "KW/H", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_INSPECAO_EstabelecimentoId",
                table: "INSPECAO",
                column: "EstabelecimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_INSPECAO_InspecaoStatusId",
                table: "INSPECAO",
                column: "InspecaoStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_INSPECAO_TurnoId",
                table: "INSPECAO",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_INSPECAO_UsuarioId",
                table: "INSPECAO",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_INSPECAO_ITEM_InspecaoId",
                table: "INSPECAO_ITEM",
                column: "InspecaoId");

            migrationBuilder.CreateIndex(
                name: "IX_INSPECAO_ITEM_SistemaItemId",
                table: "INSPECAO_ITEM",
                column: "SistemaItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SISTEMA_EstabelecimentoId",
                table: "SISTEMA",
                column: "EstabelecimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_SISTEMA_ITEM_SistemaId",
                table: "SISTEMA_ITEM",
                column: "SistemaId");

            migrationBuilder.CreateIndex(
                name: "IX_SISTEMA_ITEM_UnidadeMedidaId",
                table: "SISTEMA_ITEM",
                column: "UnidadeMedidaId");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_EstabelecimentoId",
                table: "USUARIO",
                column: "EstabelecimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_CLAIMS_ClaimId",
                table: "USUARIO_CLAIMS",
                column: "ClaimId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "INSPECAO_ITEM");

            migrationBuilder.DropTable(
                name: "USUARIO_CLAIMS");

            migrationBuilder.DropTable(
                name: "INSPECAO");

            migrationBuilder.DropTable(
                name: "SISTEMA_ITEM");

            migrationBuilder.DropTable(
                name: "CLAIM");

            migrationBuilder.DropTable(
                name: "INSPECAO_STATUS");

            migrationBuilder.DropTable(
                name: "TURNO");

            migrationBuilder.DropTable(
                name: "USUARIO");

            migrationBuilder.DropTable(
                name: "SISTEMA");

            migrationBuilder.DropTable(
                name: "UNIDADE_MEDIDA");

            migrationBuilder.DropTable(
                name: "ESTABELECIMENTO");
        }
    }
}
