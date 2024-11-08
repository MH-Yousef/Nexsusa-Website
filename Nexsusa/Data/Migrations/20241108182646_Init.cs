﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChooseUsId",
                table: "HomePages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientSaysId",
                table: "HomePages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OurCompanyId",
                table: "HomePages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OurEmployeesId",
                table: "HomePages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegularBlogsId",
                table: "HomePages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServicesId",
                table: "HomePages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SliderId",
                table: "HomePages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WhoWeAreId",
                table: "HomePages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkShowCaseId",
                table: "HomePages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkingProcessId",
                table: "HomePages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChooseUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChooseUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientSays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OurCompanys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurCompanys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OurEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurEmployees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegularBlogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegularBlogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WhoWeAres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhoWeAres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkingProcesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingProcesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkShowCases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShowCases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChooseUsId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_ChooseUs_ChooseUsId",
                        column: x => x.ChooseUsId,
                        principalTable: "ChooseUs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientSaysItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientSaysId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSaysItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientSaysItems_ClientSays_ClientSaysId",
                        column: x => x.ClientSaysId,
                        principalTable: "ClientSays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OurEmployeesItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OurEmployeesId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurEmployeesItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OurEmployeesItems_OurEmployees_OurEmployeesId",
                        column: x => x.OurEmployeesId,
                        principalTable: "OurEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegularBlogsItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegularBlogsId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegularBlogsItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegularBlogsItems_RegularBlogs_RegularBlogsId",
                        column: x => x.RegularBlogsId,
                        principalTable: "RegularBlogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServicesId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceItems_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WhoWeAreItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhoWeAreId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhoWeAreItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WhoWeAreItems_WhoWeAres_WhoWeAreId",
                        column: x => x.WhoWeAreId,
                        principalTable: "WhoWeAres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingProcessItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkingProcessId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingProcessItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingProcessItems_WorkingProcesses_WorkingProcessId",
                        column: x => x.WorkingProcessId,
                        principalTable: "WorkingProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkShowCaseNavBars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasSubItem = table.Column<bool>(type: "bit", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    WorkShowCaseId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShowCaseNavBars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkShowCaseNavBars_WorkShowCases_WorkShowCaseId",
                        column: x => x.WorkShowCaseId,
                        principalTable: "WorkShowCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkShowCaseNavBarItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    WorkShowCaseNavBarId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShowCaseNavBarItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkShowCaseNavBarItems_WorkShowCaseNavBars_WorkShowCaseNavBarId",
                        column: x => x.WorkShowCaseNavBarId,
                        principalTable: "WorkShowCaseNavBars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkShowCaseServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkShowCaseNavBarItemId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShowCaseServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkShowCaseServices_WorkShowCaseNavBarItems_WorkShowCaseNavBarItemId",
                        column: x => x.WorkShowCaseNavBarItemId,
                        principalTable: "WorkShowCaseNavBarItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomePages_ChooseUsId",
                table: "HomePages",
                column: "ChooseUsId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePages_ClientSaysId",
                table: "HomePages",
                column: "ClientSaysId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePages_OurCompanyId",
                table: "HomePages",
                column: "OurCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePages_OurEmployeesId",
                table: "HomePages",
                column: "OurEmployeesId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePages_RegularBlogsId",
                table: "HomePages",
                column: "RegularBlogsId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePages_ServicesId",
                table: "HomePages",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePages_SliderId",
                table: "HomePages",
                column: "SliderId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePages_WhoWeAreId",
                table: "HomePages",
                column: "WhoWeAreId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePages_WorkingProcessId",
                table: "HomePages",
                column: "WorkingProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePages_WorkShowCaseId",
                table: "HomePages",
                column: "WorkShowCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSaysItems_ClientSaysId",
                table: "ClientSaysItems",
                column: "ClientSaysId");

            migrationBuilder.CreateIndex(
                name: "IX_OurEmployeesItems_OurEmployeesId",
                table: "OurEmployeesItems",
                column: "OurEmployeesId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ChooseUsId",
                table: "Questions",
                column: "ChooseUsId");

            migrationBuilder.CreateIndex(
                name: "IX_RegularBlogsItems_RegularBlogsId",
                table: "RegularBlogsItems",
                column: "RegularBlogsId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItems_ServiceId",
                table: "ServiceItems",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_WhoWeAreItems_WhoWeAreId",
                table: "WhoWeAreItems",
                column: "WhoWeAreId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingProcessItems_WorkingProcessId",
                table: "WorkingProcessItems",
                column: "WorkingProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShowCaseNavBarItems_WorkShowCaseNavBarId",
                table: "WorkShowCaseNavBarItems",
                column: "WorkShowCaseNavBarId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShowCaseNavBars_WorkShowCaseId",
                table: "WorkShowCaseNavBars",
                column: "WorkShowCaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkShowCaseServices_WorkShowCaseNavBarItemId",
                table: "WorkShowCaseServices",
                column: "WorkShowCaseNavBarItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomePages_ChooseUs_ChooseUsId",
                table: "HomePages",
                column: "ChooseUsId",
                principalTable: "ChooseUs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomePages_ClientSays_ClientSaysId",
                table: "HomePages",
                column: "ClientSaysId",
                principalTable: "ClientSays",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomePages_OurCompanys_OurCompanyId",
                table: "HomePages",
                column: "OurCompanyId",
                principalTable: "OurCompanys",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomePages_OurEmployees_OurEmployeesId",
                table: "HomePages",
                column: "OurEmployeesId",
                principalTable: "OurEmployees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomePages_RegularBlogs_RegularBlogsId",
                table: "HomePages",
                column: "RegularBlogsId",
                principalTable: "RegularBlogs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomePages_Services_ServicesId",
                table: "HomePages",
                column: "ServicesId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomePages_Sliders_SliderId",
                table: "HomePages",
                column: "SliderId",
                principalTable: "Sliders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomePages_WhoWeAres_WhoWeAreId",
                table: "HomePages",
                column: "WhoWeAreId",
                principalTable: "WhoWeAres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomePages_WorkShowCases_WorkShowCaseId",
                table: "HomePages",
                column: "WorkShowCaseId",
                principalTable: "WorkShowCases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomePages_WorkingProcesses_WorkingProcessId",
                table: "HomePages",
                column: "WorkingProcessId",
                principalTable: "WorkingProcesses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomePages_ChooseUs_ChooseUsId",
                table: "HomePages");

            migrationBuilder.DropForeignKey(
                name: "FK_HomePages_ClientSays_ClientSaysId",
                table: "HomePages");

            migrationBuilder.DropForeignKey(
                name: "FK_HomePages_OurCompanys_OurCompanyId",
                table: "HomePages");

            migrationBuilder.DropForeignKey(
                name: "FK_HomePages_OurEmployees_OurEmployeesId",
                table: "HomePages");

            migrationBuilder.DropForeignKey(
                name: "FK_HomePages_RegularBlogs_RegularBlogsId",
                table: "HomePages");

            migrationBuilder.DropForeignKey(
                name: "FK_HomePages_Services_ServicesId",
                table: "HomePages");

            migrationBuilder.DropForeignKey(
                name: "FK_HomePages_Sliders_SliderId",
                table: "HomePages");

            migrationBuilder.DropForeignKey(
                name: "FK_HomePages_WhoWeAres_WhoWeAreId",
                table: "HomePages");

            migrationBuilder.DropForeignKey(
                name: "FK_HomePages_WorkShowCases_WorkShowCaseId",
                table: "HomePages");

            migrationBuilder.DropForeignKey(
                name: "FK_HomePages_WorkingProcesses_WorkingProcessId",
                table: "HomePages");

            migrationBuilder.DropTable(
                name: "ClientSaysItems");

            migrationBuilder.DropTable(
                name: "OurCompanys");

            migrationBuilder.DropTable(
                name: "OurEmployeesItems");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "RegularBlogsItems");

            migrationBuilder.DropTable(
                name: "ServiceItems");

            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.DropTable(
                name: "WhoWeAreItems");

            migrationBuilder.DropTable(
                name: "WorkingProcessItems");

            migrationBuilder.DropTable(
                name: "WorkShowCaseServices");

            migrationBuilder.DropTable(
                name: "ClientSays");

            migrationBuilder.DropTable(
                name: "OurEmployees");

            migrationBuilder.DropTable(
                name: "ChooseUs");

            migrationBuilder.DropTable(
                name: "RegularBlogs");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "WhoWeAres");

            migrationBuilder.DropTable(
                name: "WorkingProcesses");

            migrationBuilder.DropTable(
                name: "WorkShowCaseNavBarItems");

            migrationBuilder.DropTable(
                name: "WorkShowCaseNavBars");

            migrationBuilder.DropTable(
                name: "WorkShowCases");

            migrationBuilder.DropIndex(
                name: "IX_HomePages_ChooseUsId",
                table: "HomePages");

            migrationBuilder.DropIndex(
                name: "IX_HomePages_ClientSaysId",
                table: "HomePages");

            migrationBuilder.DropIndex(
                name: "IX_HomePages_OurCompanyId",
                table: "HomePages");

            migrationBuilder.DropIndex(
                name: "IX_HomePages_OurEmployeesId",
                table: "HomePages");

            migrationBuilder.DropIndex(
                name: "IX_HomePages_RegularBlogsId",
                table: "HomePages");

            migrationBuilder.DropIndex(
                name: "IX_HomePages_ServicesId",
                table: "HomePages");

            migrationBuilder.DropIndex(
                name: "IX_HomePages_SliderId",
                table: "HomePages");

            migrationBuilder.DropIndex(
                name: "IX_HomePages_WhoWeAreId",
                table: "HomePages");

            migrationBuilder.DropIndex(
                name: "IX_HomePages_WorkingProcessId",
                table: "HomePages");

            migrationBuilder.DropIndex(
                name: "IX_HomePages_WorkShowCaseId",
                table: "HomePages");

            migrationBuilder.DropColumn(
                name: "ChooseUsId",
                table: "HomePages");

            migrationBuilder.DropColumn(
                name: "ClientSaysId",
                table: "HomePages");

            migrationBuilder.DropColumn(
                name: "OurCompanyId",
                table: "HomePages");

            migrationBuilder.DropColumn(
                name: "OurEmployeesId",
                table: "HomePages");

            migrationBuilder.DropColumn(
                name: "RegularBlogsId",
                table: "HomePages");

            migrationBuilder.DropColumn(
                name: "ServicesId",
                table: "HomePages");

            migrationBuilder.DropColumn(
                name: "SliderId",
                table: "HomePages");

            migrationBuilder.DropColumn(
                name: "WhoWeAreId",
                table: "HomePages");

            migrationBuilder.DropColumn(
                name: "WorkShowCaseId",
                table: "HomePages");

            migrationBuilder.DropColumn(
                name: "WorkingProcessId",
                table: "HomePages");
        }
    }
}
