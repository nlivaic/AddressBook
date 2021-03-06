﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AddressBook.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressBooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Address_Street = table.Column<string>(nullable: true),
                    Address_StreetNr = table.Column<string>(nullable: true),
                    Address_City = table.Column<string>(nullable: true),
                    Address_Country = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    AddressBookId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_AddressBooks_AddressBookId",
                        column: x => x.AddressBookId,
                        principalTable: "AddressBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelephoneNumbers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContactId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelephoneNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelephoneNumbers_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_AddressBookId",
                table: "Contacts",
                column: "AddressBookId");

            migrationBuilder.CreateIndex(
                name: "IX_TelephoneNumbers_ContactId",
                table: "TelephoneNumbers",
                column: "ContactId");

            migrationBuilder.Sql("ALTER TABLE \"Contacts\" ADD CONSTRAINT UQ_Contacts_Name_Address UNIQUE (\"Name\", \"Address_Street\", \"Address_StreetNr\", \"Address_City\", \"Address_Country\")");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TelephoneNumbers");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "AddressBooks");
        }
    }
}
