SET IDENTITY_INSERT [dbo].[InteractiveSpace] ON
INSERT INTO [dbo].[InteractiveSpace] ([Id], [AnchorId], [Name], [Description], [Visibility], [Discriminator]) VALUES (1, NULL, N'Espacio interactivo', N'donde interactua', N'1', N'InteractiveSpace3D')
SET IDENTITY_INSERT [dbo].[InteractiveSpace] OFF

SET IDENTITY_INSERT [dbo].[Resource] ON
INSERT INTO [dbo].[Resource] ([Id], [Name], [Type], [Description], [Size], [Path]) VALUES (1, N'Recurso', N'1', N'Descripcion', 1, NULL)
SET IDENTITY_INSERT [dbo].[Resource] OFF

SET IDENTITY_INSERT [dbo].[Location] ON
INSERT INTO [dbo].[Location] ([Id], [Discriminator], [X], [Y], [Z], [RotX], [RotY], [RotZ], [ScaleX], [ScaleY], [ScaleZ], [Latitude], [Longitude]) VALUES (1, N'Location3D', 1, 1, 1, 0, 0, 0, 1, 1, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Location] OFF


SET IDENTITY_INSERT [dbo].[Step] ON
INSERT INTO [dbo].[Step] ([Id], [Description], [Groupal], [IsSupervised], [Order], [InteractiveSpaceId], [ParentMandatoryStepId], [ParentAlternativeStepId], [ParentOptionalStepId]) VALUES (1, N'First Step', NULL, NULL, NULL, 1, NULL, NULL, NULL)
INSERT INTO [dbo].[Step] ([Id], [Description], [Groupal], [IsSupervised], [Order], [InteractiveSpaceId], [ParentMandatoryStepId], [ParentAlternativeStepId], [ParentOptionalStepId]) VALUES (2, N'First 1.Step', NULL, NULL, 1, 1, 1, NULL, NULL)
INSERT INTO [dbo].[Step] ([Id], [Description], [Groupal], [IsSupervised], [Order], [InteractiveSpaceId], [ParentMandatoryStepId], [ParentAlternativeStepId], [ParentOptionalStepId]) VALUES (3, N'First 2.Step', NULL, NULL, 2, 1, 1, NULL, NULL)
INSERT INTO [dbo].[Step] ([Id], [Description], [Groupal], [IsSupervised], [Order], [InteractiveSpaceId], [ParentMandatoryStepId], [ParentAlternativeStepId], [ParentOptionalStepId]) VALUES (4, N'First 3.Step', NULL, NULL, 3, 1, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Step] OFF



SET IDENTITY_INSERT [dbo].[Entity] ON
INSERT INTO [dbo].[Entity] ([Id], [Path], [Name], [Description], [Owner], [Discriminator]) VALUES (1, N'http://url', N'Entidad 3d', N'descripcion de entidad', N'usuario', N'Entity3D')
SET IDENTITY_INSERT [dbo].[Entity] OFF

SET IDENTITY_INSERT [dbo].[Activity] ON
INSERT INTO [dbo].[Activity] ([Id], [Name], [Description], [CreationDate], [FinalMessageOK], [FinalMessageerror], [MaxTime], [InitialHelpId], [FirstStepId]) VALUES (1, N'Actividad', N'Descripcion', N'2022-11-07 00:00:00', N'Bye', N'Try next time!', N'1753-01-01 10:00:00', 1, 1)
SET IDENTITY_INSERT [dbo].[Activity] OFF

SET IDENTITY_INSERT [dbo].[EntityStep] ON
INSERT INTO [dbo].[EntityStep] ([Id], [EntityId], [FeedbackId], [LocatedInId], [StepId]) VALUES (1, 1, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[EntityStep] OFF

