using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SAMLTester.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartnerServiceProviderConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SignSamlResponse = table.Column<bool>(nullable: false),
                    EncryptAssertion = table.Column<bool>(nullable: false),
                    AssertionConsumerServiceUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerServiceProviderConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartnerCertificates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    PartnerServiceProviderConfigurationId = table.Column<Guid>(nullable: false),
                    Thumbprint = table.Column<string>(nullable: true),
                    String = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartnerCertificates_PartnerServiceProviderConfigurations_PartnerServiceProviderConfigurationId",
                        column: x => x.PartnerServiceProviderConfigurationId,
                        principalTable: "PartnerServiceProviderConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartnerCertificates_PartnerServiceProviderConfigurationId",
                table: "PartnerCertificates",
                column: "PartnerServiceProviderConfigurationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartnerCertificates");

            migrationBuilder.DropTable(
                name: "PartnerServiceProviderConfigurations");
        }
    }
}
