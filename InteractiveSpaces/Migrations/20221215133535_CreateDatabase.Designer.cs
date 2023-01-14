﻿// <auto-generated />
using System;
using InteractiveSpaces.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InteractiveSpaces.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20221215133535_CreateDatabase")]
    partial class CreateDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("InteractiveSpaces.Models.ActionEntityStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ActionType")
                        .HasColumnType("int");

                    b.Property<int>("AnimationId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EntityStepId")
                        .HasColumnType("int");

                    b.Property<string>("FeedbackMessage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnimationId");

                    b.HasIndex("EntityStepId");

                    b.ToTable("ActionEntityStep");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ActivityImageId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasPrecision(2)
                        .HasColumnType("datetime2(2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FinalMessageError")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FinalMessageId")
                        .HasColumnType("int");

                    b.Property<string>("FinalMessageOK")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FirstStepId")
                        .HasColumnType("int");

                    b.Property<int?>("InitialHelpId")
                        .HasColumnType("int");

                    b.Property<int?>("LastStepId")
                        .HasColumnType("int");

                    b.Property<decimal>("MaxTime")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.HasIndex("ActivityImageId");

                    b.HasIndex("FinalMessageId");

                    b.HasIndex("FirstStepId")
                        .IsUnique()
                        .HasFilter("[FirstStepId] IS NOT NULL");

                    b.HasIndex("InitialHelpId");

                    b.HasIndex("LastStepId");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.Activity2User", b =>
                {
                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<string>("User")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ActivityId", "User");

                    b.ToTable("Activity2User");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.Animation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AnimationId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EntityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.ToTable("Animation");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.Entity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Entity");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Entity");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.EntityStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("EntityId")
                        .HasColumnType("int");

                    b.Property<int>("StepDescriptionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("EntityId", "StepDescriptionId");

                    b.HasIndex("StepDescriptionId");

                    b.ToTable("EntityStep", (string)null);
                });

            modelBuilder.Entity("InteractiveSpaces.Models.InteractiveSpace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Visibility")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name", "Owner");

                    b.ToTable("InteractiveSpace");

                    b.HasDiscriminator<string>("Discriminator").HasValue("InteractiveSpace");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EntityStep", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("Location");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Size")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Resource");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.Step", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<bool?>("Groupal")
                        .HasColumnType("bit");

                    b.Property<int?>("InteractiveSpaceId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsSupervised")
                        .HasColumnType("bit");

                    b.Property<int?>("NextStepId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("InteractiveSpaceId");

                    b.HasIndex("NextStepId")
                        .IsUnique()
                        .HasFilter("[NextStepId] IS NOT NULL");

                    b.ToTable("Step");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.StepDescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("StepId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StepId");

                    b.ToTable("StepDescription");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.Entity3D", b =>
                {
                    b.HasBaseType("InteractiveSpaces.Models.Entity");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Entity3D");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.InteractiveSpace3D", b =>
                {
                    b.HasBaseType("InteractiveSpaces.Models.InteractiveSpace");

                    b.Property<string>("AnchorId")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("InteractiveSpace3D");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.Location3D", b =>
                {
                    b.HasBaseType("InteractiveSpaces.Models.Location");

                    b.Property<float>("RotX")
                        .HasColumnType("real");

                    b.Property<float>("RotY")
                        .HasColumnType("real");

                    b.Property<float>("RotZ")
                        .HasColumnType("real");

                    b.Property<float>("ScaleX")
                        .HasColumnType("real");

                    b.Property<float>("ScaleY")
                        .HasColumnType("real");

                    b.Property<float>("ScaleZ")
                        .HasColumnType("real");

                    b.Property<float>("X")
                        .HasColumnType("real");

                    b.Property<float>("Y")
                        .HasColumnType("real");

                    b.Property<float>("Z")
                        .HasColumnType("real");

                    b.HasDiscriminator().HasValue("Location3D");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.LocationGPS", b =>
                {
                    b.HasBaseType("InteractiveSpaces.Models.Location");

                    b.Property<float>("Latitude")
                        .HasColumnType("real");

                    b.Property<float>("Longitude")
                        .HasColumnType("real");

                    b.HasDiscriminator().HasValue("LocationGPS");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.ActionEntityStep", b =>
                {
                    b.HasOne("InteractiveSpaces.Models.Animation", "Animation")
                        .WithMany()
                        .HasForeignKey("AnimationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InteractiveSpaces.Models.EntityStep", "EntityStep")
                        .WithMany("HasActions")
                        .HasForeignKey("EntityStepId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Animation");

                    b.Navigation("EntityStep");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.Activity", b =>
                {
                    b.HasOne("InteractiveSpaces.Models.Resource", "ActivityImage")
                        .WithMany()
                        .HasForeignKey("ActivityImageId");

                    b.HasOne("InteractiveSpaces.Models.Resource", "FinalMessage")
                        .WithMany()
                        .HasForeignKey("FinalMessageId");

                    b.HasOne("InteractiveSpaces.Models.Step", "FirstStep")
                        .WithOne()
                        .HasForeignKey("InteractiveSpaces.Models.Activity", "FirstStepId");

                    b.HasOne("InteractiveSpaces.Models.Resource", "InitialHelp")
                        .WithMany()
                        .HasForeignKey("InitialHelpId");

                    b.HasOne("InteractiveSpaces.Models.Step", "LastStep")
                        .WithMany()
                        .HasForeignKey("LastStepId");

                    b.Navigation("ActivityImage");

                    b.Navigation("FinalMessage");

                    b.Navigation("FirstStep");

                    b.Navigation("InitialHelp");

                    b.Navigation("LastStep");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.Activity2User", b =>
                {
                    b.HasOne("InteractiveSpaces.Models.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.Animation", b =>
                {
                    b.HasOne("InteractiveSpaces.Models.Entity3D", "Entity")
                        .WithMany("Animations")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.EntityStep", b =>
                {
                    b.HasOne("InteractiveSpaces.Models.Entity3D", "Entity")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InteractiveSpaces.Models.StepDescription", "StepDescription")
                        .WithMany("EntityStep")
                        .HasForeignKey("StepDescriptionId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Entity");

                    b.Navigation("StepDescription");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.Location", b =>
                {
                    b.HasOne("InteractiveSpaces.Models.EntityStep", null)
                        .WithOne("LocatedIn")
                        .HasForeignKey("InteractiveSpaces.Models.Location", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InteractiveSpaces.Models.Step", b =>
                {
                    b.HasOne("InteractiveSpaces.Models.Activity", "Activity")
                        .WithMany("Steps")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("InteractiveSpaces.Models.InteractiveSpace3D", "InteractiveSpace")
                        .WithMany()
                        .HasForeignKey("InteractiveSpaceId");

                    b.HasOne("InteractiveSpaces.Models.Step", "NextStep")
                        .WithOne("PreviousStep")
                        .HasForeignKey("InteractiveSpaces.Models.Step", "NextStepId");

                    b.Navigation("Activity");

                    b.Navigation("InteractiveSpace");

                    b.Navigation("NextStep");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.StepDescription", b =>
                {
                    b.HasOne("InteractiveSpaces.Models.Step", "Step")
                        .WithMany("StepDescriptions")
                        .HasForeignKey("StepId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Step");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.Activity", b =>
                {
                    b.Navigation("Steps");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.EntityStep", b =>
                {
                    b.Navigation("HasActions");

                    b.Navigation("LocatedIn")
                        .IsRequired();
                });

            modelBuilder.Entity("InteractiveSpaces.Models.Step", b =>
                {
                    b.Navigation("PreviousStep");

                    b.Navigation("StepDescriptions");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.StepDescription", b =>
                {
                    b.Navigation("EntityStep");
                });

            modelBuilder.Entity("InteractiveSpaces.Models.Entity3D", b =>
                {
                    b.Navigation("Animations");
                });
#pragma warning restore 612, 618
        }
    }
}
