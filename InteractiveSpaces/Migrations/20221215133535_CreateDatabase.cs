using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteractiveSpaces.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.Id);
                    table.UniqueConstraint("AK_Entity_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "InteractiveSpace",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Visibility = table.Column<int>(type: "int", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnchorId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteractiveSpace", x => x.Id);
                    table.UniqueConstraint("AK_InteractiveSpace_Name_Owner", x => new { x.Name, x.Owner });
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Id);
                    table.UniqueConstraint("AK_Resource_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Animation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AnimationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animation_Entity_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionEntityStep",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedbackMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionType = table.Column<int>(type: "int", nullable: false),
                    EntityStepId = table.Column<int>(type: "int", nullable: false),
                    AnimationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionEntityStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionEntityStep_Animation_AnimationId",
                        column: x => x.AnimationId,
                        principalTable: "Animation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2(2)", precision: 2, nullable: false),
                    FinalMessageOK = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalMessageError = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxTime = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    FirstStepId = table.Column<int>(type: "int", nullable: true),
                    LastStepId = table.Column<int>(type: "int", nullable: true),
                    ActivityImageId = table.Column<int>(type: "int", nullable: true),
                    InitialHelpId = table.Column<int>(type: "int", nullable: true),
                    FinalMessageId = table.Column<int>(type: "int", nullable: true),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.Id);
                    table.UniqueConstraint("AK_Activity_Name", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Activity_Resource_ActivityImageId",
                        column: x => x.ActivityImageId,
                        principalTable: "Resource",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Activity_Resource_FinalMessageId",
                        column: x => x.FinalMessageId,
                        principalTable: "Resource",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Activity_Resource_InitialHelpId",
                        column: x => x.InitialHelpId,
                        principalTable: "Resource",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Activity2User",
                columns: table => new
                {
                    User = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity2User", x => new { x.ActivityId, x.User });
                    table.ForeignKey(
                        name: "FK_Activity2User_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Step",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Groupal = table.Column<bool>(type: "bit", nullable: true),
                    IsSupervised = table.Column<bool>(type: "bit", nullable: true),
                    InteractiveSpaceId = table.Column<int>(type: "int", nullable: true),
                    NextStepId = table.Column<int>(type: "int", nullable: true),
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Step", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Step_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Step_InteractiveSpace_InteractiveSpaceId",
                        column: x => x.InteractiveSpaceId,
                        principalTable: "InteractiveSpace",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Step_Step_NextStepId",
                        column: x => x.NextStepId,
                        principalTable: "Step",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StepDescription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StepId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StepDescription_Step_StepId",
                        column: x => x.StepId,
                        principalTable: "Step",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EntityStep",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StepDescriptionId = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    X = table.Column<float>(type: "real", nullable: true),
                    Y = table.Column<float>(type: "real", nullable: true),
                    Z = table.Column<float>(type: "real", nullable: true),
                    RotX = table.Column<float>(type: "real", nullable: true),
                    RotY = table.Column<float>(type: "real", nullable: true),
                    RotZ = table.Column<float>(type: "real", nullable: true),
                    ScaleX = table.Column<float>(type: "real", nullable: true),
                    ScaleY = table.Column<float>(type: "real", nullable: true),
                    ScaleZ = table.Column<float>(type: "real", nullable: true),
                    Latitude = table.Column<float>(type: "real", nullable: true),
                    Longitude = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityStep", x => x.Id);
                    table.UniqueConstraint("AK_EntityStep_EntityId_StepDescriptionId", x => new { x.EntityId, x.StepDescriptionId });
                    table.ForeignKey(
                        name: "FK_EntityStep_Entity_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityStep_StepDescription_StepDescriptionId",
                        column: x => x.StepDescriptionId,
                        principalTable: "StepDescription",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionEntityStep_AnimationId",
                table: "ActionEntityStep",
                column: "AnimationId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionEntityStep_EntityStepId",
                table: "ActionEntityStep",
                column: "EntityStepId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_ActivityImageId",
                table: "Activity",
                column: "ActivityImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_FinalMessageId",
                table: "Activity",
                column: "FinalMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_FirstStepId",
                table: "Activity",
                column: "FirstStepId",
                unique: true,
                filter: "[FirstStepId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_InitialHelpId",
                table: "Activity",
                column: "InitialHelpId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_LastStepId",
                table: "Activity",
                column: "LastStepId");

            migrationBuilder.CreateIndex(
                name: "IX_Animation_EntityId",
                table: "Animation",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityStep_StepDescriptionId",
                table: "EntityStep",
                column: "StepDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Step_ActivityId",
                table: "Step",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Step_InteractiveSpaceId",
                table: "Step",
                column: "InteractiveSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Step_NextStepId",
                table: "Step",
                column: "NextStepId",
                unique: true,
                filter: "[NextStepId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StepDescription_StepId",
                table: "StepDescription",
                column: "StepId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionEntityStep_EntityStep_EntityStepId",
                table: "ActionEntityStep",
                column: "EntityStepId",
                principalTable: "EntityStep",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Step_FirstStepId",
                table: "Activity",
                column: "FirstStepId",
                principalTable: "Step",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Step_LastStepId",
                table: "Activity",
                column: "LastStepId",
                principalTable: "Step",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Resource_ActivityImageId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Resource_FinalMessageId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Resource_InitialHelpId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Step_FirstStepId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Step_LastStepId",
                table: "Activity");

            migrationBuilder.DropTable(
                name: "ActionEntityStep");

            migrationBuilder.DropTable(
                name: "Activity2User");

            migrationBuilder.DropTable(
                name: "Animation");

            migrationBuilder.DropTable(
                name: "EntityStep");

            migrationBuilder.DropTable(
                name: "Entity");

            migrationBuilder.DropTable(
                name: "StepDescription");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "Step");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "InteractiveSpace");
        }
    }
}
