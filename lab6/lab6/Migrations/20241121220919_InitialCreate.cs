using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace lab6.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artefacts",
                columns: table => new
                {
                    Artefact_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Artefact_Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Other_Details = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artefacts", x => x.Artefact_ID);
                });

            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    Channel_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Channel_Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Other_Details = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Channel_ID);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDataPlatforms",
                columns: table => new
                {
                    Platform_Code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Customer_Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Customer_Platform_Details = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDataPlatforms", x => x.Platform_Code);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Customer_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Customer_Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Title = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Gender_MFU = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    Other_Details = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Customer_ID);
                });

            migrationBuilder.CreateTable(
                name: "EventSequences",
                columns: table => new
                {
                    Event_Sequence_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Next_Event_Sequence_ID = table.Column<int>(type: "integer", nullable: true),
                    Event_Code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Event_Date_Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Other_Details = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Book_Hotel = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    Check_Out_Pay = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSequences", x => x.Event_Sequence_ID);
                    table.ForeignKey(
                        name: "FK_EventSequences_EventSequences_Next_Event_Sequence_ID",
                        column: x => x.Next_Event_Sequence_ID,
                        principalTable: "EventSequences",
                        principalColumn: "Event_Sequence_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GenericServices",
                columns: table => new
                {
                    Service_Code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Service_Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Other_Details = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenericServices", x => x.Service_Code);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Location_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Location_Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Other_Details = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Location_ID);
                });

            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    Platform_Code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Platform_Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Other_Details = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Asset_Mgt = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    Hotel = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.Platform_Code);
                });

            migrationBuilder.CreateTable(
                name: "RefDocumentTypes",
                columns: table => new
                {
                    Document_Type_Code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Document_Type_Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Document_Type_Category = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefDocumentTypes", x => x.Document_Type_Code);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Staff_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Staff_Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Other_Details = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Staff_ID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Supplier_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Supplier_Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Other_Details = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Supplier_ID);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Event_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Artefact_ID = table.Column<int>(type: "integer", nullable: true),
                    Channel_ID = table.Column<int>(type: "integer", nullable: false),
                    Customer_ID = table.Column<int>(type: "integer", nullable: false),
                    Event_Sequence_ID = table.Column<int>(type: "integer", nullable: true),
                    Location_ID = table.Column<int>(type: "integer", nullable: false),
                    Platform_Code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Service_Code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Staff_ID = table.Column<int>(type: "integer", nullable: false),
                    Event_Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Event_Date_Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Booking_Date_From = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Booking_Date_To = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Other_Details = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Event_ID);
                    table.ForeignKey(
                        name: "FK_Events_Artefacts_Artefact_ID",
                        column: x => x.Artefact_ID,
                        principalTable: "Artefacts",
                        principalColumn: "Artefact_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_Channels_Channel_ID",
                        column: x => x.Channel_ID,
                        principalTable: "Channels",
                        principalColumn: "Channel_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_Customers_Customer_ID",
                        column: x => x.Customer_ID,
                        principalTable: "Customers",
                        principalColumn: "Customer_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_EventSequences_Event_Sequence_ID",
                        column: x => x.Event_Sequence_ID,
                        principalTable: "EventSequences",
                        principalColumn: "Event_Sequence_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_Locations_Location_ID",
                        column: x => x.Location_ID,
                        principalTable: "Locations",
                        principalColumn: "Location_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_Staff_Staff_ID",
                        column: x => x.Staff_ID,
                        principalTable: "Staff",
                        principalColumn: "Staff_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductServices",
                columns: table => new
                {
                    Prod_Service_Code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Supplier_ID = table.Column<int>(type: "integer", nullable: false),
                    Prod_Service_Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Other_Details = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductServices", x => x.Prod_Service_Code);
                    table.ForeignKey(
                        name: "FK_ProductServices_Suppliers_Supplier_ID",
                        column: x => x.Supplier_ID,
                        principalTable: "Suppliers",
                        principalColumn: "Supplier_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Document_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Document_Type_Code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Event_ID = table.Column<int>(type: "integer", nullable: false),
                    Document_Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Document_Text = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Other_Details = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Document_ID);
                    table.ForeignKey(
                        name: "FK_Documents_Events_Event_ID",
                        column: x => x.Event_ID,
                        principalTable: "Events",
                        principalColumn: "Event_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_RefDocumentTypes_Document_Type_Code",
                        column: x => x.Document_Type_Code,
                        principalTable: "RefDocumentTypes",
                        principalColumn: "Document_Type_Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Payment_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Event_ID = table.Column<int>(type: "integer", nullable: false),
                    Payment_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Payment_Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Other_Details = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Payment_ID);
                    table.ForeignKey(
                        name: "FK_Payments_Events_Event_ID",
                        column: x => x.Event_ID,
                        principalTable: "Events",
                        principalColumn: "Event_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDataPlatforms_Platform_Code",
                table: "CustomerDataPlatforms",
                column: "Platform_Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_Document_Type_Code",
                table: "Documents",
                column: "Document_Type_Code");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_Event_ID",
                table: "Documents",
                column: "Event_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Artefact_ID",
                table: "Events",
                column: "Artefact_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Channel_ID",
                table: "Events",
                column: "Channel_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Customer_ID",
                table: "Events",
                column: "Customer_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Event_Sequence_ID",
                table: "Events",
                column: "Event_Sequence_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Location_ID",
                table: "Events",
                column: "Location_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Staff_ID",
                table: "Events",
                column: "Staff_ID");

            migrationBuilder.CreateIndex(
                name: "IX_EventSequences_Next_Event_Sequence_ID",
                table: "EventSequences",
                column: "Next_Event_Sequence_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GenericServices_Service_Code",
                table: "GenericServices",
                column: "Service_Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Event_ID",
                table: "Payments",
                column: "Event_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_Platform_Code",
                table: "Platforms",
                column: "Platform_Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductServices_Prod_Service_Code",
                table: "ProductServices",
                column: "Prod_Service_Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductServices_Supplier_ID",
                table: "ProductServices",
                column: "Supplier_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RefDocumentTypes_Document_Type_Code",
                table: "RefDocumentTypes",
                column: "Document_Type_Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerDataPlatforms");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "GenericServices");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropTable(
                name: "ProductServices");

            migrationBuilder.DropTable(
                name: "RefDocumentTypes");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Artefacts");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "EventSequences");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Staff");
        }
    }
}
