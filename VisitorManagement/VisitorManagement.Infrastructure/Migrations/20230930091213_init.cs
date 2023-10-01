using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitorManagement.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HostVisitorRequests",
                columns: table => new
                {
                    HostVisitorRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitorEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitorContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitorArrivalDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitorCheckOutDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitorVisitPurpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    HostId = table.Column<int>(type: "int", nullable: false),
                    HostName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostVisitorRequests", x => x.HostVisitorRequestId);
                });

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    VisitorEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitorName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VisitorContactNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VisitorPersonalIdType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitorPersonalIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => new { x.VisitorName, x.VisitorContactNumber });
                });

            migrationBuilder.CreateTable(
                name: "AdminApprovalStatuses",
                columns: table => new
                {
                    HostVisitorRequestId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AdminFeedback = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminApprovalStatuses", x => x.HostVisitorRequestId);
                    table.ForeignKey(
                        name: "FK_AdminApprovalStatuses_HostVisitorRequests_HostVisitorRequestId",
                        column: x => x.HostVisitorRequestId,
                        principalTable: "HostVisitorRequests",
                        principalColumn: "HostVisitorRequestId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminApprovalStatuses");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.DropTable(
                name: "HostVisitorRequests");
        }
    }
}
