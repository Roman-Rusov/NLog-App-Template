﻿SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[Logs] (
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [MachineName] [NVARCHAR](50) NOT NULL,
  [Logged] [DATETIME2] NOT NULL,
  [Level] [NVARCHAR](5) NOT NULL,
  [Message] [NVARCHAR](MAX) NOT NULL,
  [Logger] [NVARCHAR](250) NULL,
  [Callsite] [NVARCHAR](MAX) NULL,
  [Exception] [NVARCHAR](MAX) NULL,
  [EventProperties] [NVARCHAR](MAX) NULL
CONSTRAINT [PK_dbo.Log] PRIMARY KEY CLUSTERED ([Id] ASC)
    WITH (
    PAD_INDEX = OFF,
    STATISTICS_NORECOMPUTE = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS = ON,
    ALLOW_PAGE_LOCKS = ON
  ) ON [PRIMARY]
) ON [PRIMARY]
