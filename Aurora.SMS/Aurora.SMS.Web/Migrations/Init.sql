INSERT [dbo].[Provider] ([Name], [Url], [LogoUrl], [UserName], [PassWord], [SupportsScheduleMessage], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'FlashSms', N'http://localhost:8080/FlashSms.html', N'http://localhost:8080/images/FlashSms.png', N'Flash', N'', 0, NULL, NULL, N'', CAST(N'2017-07-27T18:40:03.027' AS DateTime))
GO
INSERT [dbo].[Provider] ([Name], [Url], [LogoUrl], [UserName], [PassWord], [SupportsScheduleMessage], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'SnailAbroad', N'http://localhost:8080/SnailAbroad.html', N'http://localhost:8080/images/snail.png', N'snail', N'', 0, NULL, NULL, N'', CAST(N'2016-12-10T18:28:18.080' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Template] ON 

GO

INSERT [dbo].[Template] ([Id], [Name], [Description], [Text], [IsInactive], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (3, N'Demo', N'Demo', N'Dear <div class="alert alert-dismissible alert-success" contenteditable="false" style="display:inline-block"><button type="button" class="close" alert-dismiss="alert">×</button><span>LastName</span></div>&nbsp;<div class="alert alert-dismissible alert-success" contenteditable="false" style="display:inline-block"><button type="button" class="close" alert-dismiss="alert">×</button><span>FirstName</span></div>,your insurance with contract number:<div class="alert alert-dismissible alert-success" contenteditable="false" style="display:inline-block"><button type="button" class="close" alert-dismiss="alert">×</button><span>ContractNumber</span></div> and Receipt:<div class="alert alert-dismissible alert-success" contenteditable="false" style="display:inline-block"><button type="button" class="close" alert-dismiss="alert">×</button><span>ReceiptNumber</span></div> that was issued at:<div class="alert alert-dismissible alert-success" contenteditable="false" style="display:inline-block"><button type="button" class="close" alert-dismiss="alert">×</button><span>IssueDate</span></div>, starts:<div class="alert alert-dismissible alert-success" contenteditable="false" style="display:inline-block"><button type="button" class="close" alert-dismiss="alert">×</button><span>StartDate</span></div>, expires:<div class="alert alert-dismissible alert-success" contenteditable="false" style="display:inline-block"><button type="button" class="close" alert-dismiss="alert">×</button><span>ExpireDate</span></div> at the company&nbsp;<div class="alert alert-dismissible alert-success" contenteditable="false" style="display:inline-block"><button type="button" class="close" alert-dismiss="alert">×</button><span>CompanyDescription</span></div>&nbsp;for the plate:<div class="alert alert-dismissible alert-success" contenteditable="false" style="display:inline-block"><button type="button" class="close" alert-dismiss="alert">×</button><span>PlateNumber</span></div>&nbsp;has been issued. The amounts are:Gross:<div class="alert alert-dismissible alert-success" contenteditable="false" style="display:inline-block"><button type="button" class="close" alert-dismiss="alert">×</button><span>GrossAmount</span></div>, Tax:<div class="alert alert-dismissible alert-success" contenteditable="false" style="display:inline-block"><button type="button" class="close" alert-dismiss="alert">×</button><span>TaxAmount</span></div>, net:<div class="alert alert-dismissible alert-success" contenteditable="false" style="display:inline-block"><button type="button" class="close" alert-dismiss="alert">×</button><span>NetAmount</span></div>.A hardcopy will be delivered at <div class="alert alert-dismissible alert-success" contenteditable="false" style="display:inline-block"><button type="button" class="close" alert-dismiss="alert">×</button><span>Address</span></div>, <div class="alert alert-dismissible alert-success" contenteditable="false" style="display:inline-block"><button type="button" class="close" alert-dismiss="alert">×</button><span>ZipCode</span></div>AdditionalInfo:&nbsp;<div class="alert alert-dismissible alert-success" contenteditable="false" style="display:inline-block"><button type="button" class="close" alert-dismiss="alert">×</button><span>ContractNumber</span></div>', 0, NULL, NULL, N'', CAST(N'2017-01-17T19:59:04.043' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Template] OFF
GO
INSERT [dbo].[TemplateField] ([Name], [Description], [GroupName], [DataFormat], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'Address', N'The LastName', N'Person', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[TemplateField] ([Name], [Description], [GroupName], [DataFormat], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'BirthDate', N'The LastName', N'Person', N'dd/MM/yyyy', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[TemplateField] ([Name], [Description], [GroupName], [DataFormat], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'CanceledDate', N'The CanceledDate', N'Contract', N'dd/MM/yyyy', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[TemplateField] ([Name], [Description], [GroupName], [DataFormat], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'CompanyDescription', N'The CompanyDescription', N'Contract', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[TemplateField] ([Name], [Description], [GroupName], [DataFormat], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'ContractNumber', N'The number of the contract', N'Contract', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[TemplateField] ([Name], [Description], [GroupName], [DataFormat], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'DrivingLicenceNum', N'The LastName', N'Person', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[TemplateField] ([Name], [Description], [GroupName], [DataFormat], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'ExpireDate', N'The Expire Date', N'Contract', N'dd/MM/yyyy', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[TemplateField] ([Name], [Description], [GroupName], [DataFormat], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'FirstName', N'The FirstName', N'Person', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[TemplateField] ([Name], [Description], [GroupName], [DataFormat], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'GrossAmount', N'The GrossAmount', N'Contract', N'0.00', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[TemplateField] ([Name], [Description], [GroupName], [DataFormat], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'IssueDate', N'The Issue Date', N'Contract', N'dd/MM/yyyy', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[TemplateField] ([Name], [Description], [GroupName], [DataFormat], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'LastName', N'The LastName', N'Person', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[TemplateField] ([Name], [Description], [GroupName], [DataFormat], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'NetAmount', N'The NetAmount', N'Contract', N'0.00', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[TemplateField] ([Name], [Description], [GroupName], [DataFormat], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'PlateNumber', N'The PlateNumber', N'Contract', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[TemplateField] ([Name], [Description], [GroupName], [DataFormat], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'ReceiptNumber', N'The number of the receipt', N'Contract', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[TemplateField] ([Name], [Description], [GroupName], [DataFormat], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'StartDate', N'The Start Date', N'Contract', N'dd/MM/yyyy', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[TemplateField] ([Name], [Description], [GroupName], [DataFormat], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'TaxAmount', N'The TaxAmount', N'Contract', N'0.00', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[TemplateField] ([Name], [Description], [GroupName], [DataFormat], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'TaxId', N'The LastName', N'Person', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[TemplateField] ([Name], [Description], [GroupName], [DataFormat], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'ZipCode', N'The LastName', N'Person', NULL, NULL, NULL, NULL, NULL)
GO
