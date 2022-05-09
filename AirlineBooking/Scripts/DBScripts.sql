USE [AirlineBooking]
GO

/****** Object:  Table [dbo].[Airline]    Script Date: 09-05-2022 17:44:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Airline](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[ContactNumber] [nvarchar](500) NULL,
	[ContactAddress] [nvarchar](500) NULL,
	[Status] [bit] NOT NULL,
	[ActiveStatus] [nvarchar](max) NULL,
 CONSTRAINT [PK_Airline] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


---------------++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

USE [AirlineBooking]
GO

/****** Object:  Table [dbo].[BookingPersons]    Script Date: 09-05-2022 17:46:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BookingPersons](
	[Id] [uniqueidentifier] NOT NULL,
	[FlightBookingId] [uniqueidentifier] NULL,
	[Veg] [bit] NOT NULL,
	[NonVeg] [bit] NOT NULL,
	[SeatNo] [int] NOT NULL,
	[Price] [decimal](16, 2) NULL,
	[Name] [nvarchar](500) NULL,
	[Age] [int] NOT NULL,
	[DOB] [datetime2](7) NULL,
	[Gender] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[ContactNumber] [nvarchar](50) NULL,
 CONSTRAINT [PK_BookingPersons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BookingPersons]  WITH CHECK ADD  CONSTRAINT [FK_BookingPersons_FlightBooking_FlightBookingId] FOREIGN KEY([FlightBookingId])
REFERENCES [dbo].[FlightBooking] ([Id])
GO

ALTER TABLE [dbo].[BookingPersons] CHECK CONSTRAINT [FK_BookingPersons_FlightBooking_FlightBookingId]
GO


-------------+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

USE [AirlineBooking]
GO

/****** Object:  Table [dbo].[Discount]    Script Date: 09-05-2022 17:46:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Discount](
	[Id] [uniqueidentifier] NOT NULL,
	[CouponName] [nvarchar](max) NULL,
	[Value] [decimal](18, 2) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Discount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



----------------------------------+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

USE [AirlineBooking]
GO

/****** Object:  Table [dbo].[FlightBooking]    Script Date: 09-05-2022 17:48:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FlightBooking](
	[Id] [uniqueidentifier] NOT NULL,
	[FlightId] [uniqueidentifier] NULL,
	[FlightNumber] [nvarchar](max) NULL,
	[AirlineId] [uniqueidentifier] NULL,
	[FromDate] [datetime2](7) NULL,
	[ToDate] [datetime2](7) NULL,
	[FromLocation] [nvarchar](500) NULL,
	[ToLocation] [nvarchar](500) NULL,
	[NoOfBUSeats] [int] NULL,
	[NoOfNONBUSeats] [int] NULL,
	[Remarks] [nvarchar](500) NULL,
	[TotalPrice] [decimal](16, 2) NULL,
	[PNRNumber] [nvarchar](500) NULL,
	[MailId] [nvarchar](500) NULL,
	[ContactNumber] [nvarchar](500) NULL,
	[UserRegistrestionId] [uniqueidentifier] NULL,
	[Status] [bit] NOT NULL,
	[SeatNos] [nvarchar](max) NULL,
	[Discount] [decimal](18, 2) NULL,
 CONSTRAINT [PK_FlightBooking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[FlightBooking]  WITH CHECK ADD  CONSTRAINT [FK_FlightBooking_Airline_AirlineId] FOREIGN KEY([AirlineId])
REFERENCES [dbo].[Airline] ([Id])
GO

ALTER TABLE [dbo].[FlightBooking] CHECK CONSTRAINT [FK_FlightBooking_Airline_AirlineId]
GO

ALTER TABLE [dbo].[FlightBooking]  WITH CHECK ADD  CONSTRAINT [FK_FlightBooking_Flights_FlightId] FOREIGN KEY([FlightId])
REFERENCES [dbo].[Flights] ([Id])
GO

ALTER TABLE [dbo].[FlightBooking] CHECK CONSTRAINT [FK_FlightBooking_Flights_FlightId]
GO

ALTER TABLE [dbo].[FlightBooking]  WITH CHECK ADD  CONSTRAINT [FK_FlightBooking_UserRegistrestion_UserRegistrestionId] FOREIGN KEY([UserRegistrestionId])
REFERENCES [dbo].[UserRegistrestion] ([Id])
GO

ALTER TABLE [dbo].[FlightBooking] CHECK CONSTRAINT [FK_FlightBooking_UserRegistrestion_UserRegistrestionId]
GO


----++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
USE [AirlineBooking]
GO

/****** Object:  Table [dbo].[Flights]    Script Date: 09-05-2022 17:48:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Flights](
	[Id] [uniqueidentifier] NOT NULL,
	[FlightId] [nvarchar](500) NULL,
	[AirlineId] [uniqueidentifier] NULL,
	[FromDate] [datetime2](7) NULL,
	[ToDate] [datetime2](7) NULL,
	[FromLocation] [nvarchar](500) NULL,
	[ToLocation] [nvarchar](500) NULL,
	[Veg] [bit] NOT NULL,
	[NonVeg] [bit] NOT NULL,
	[NoOfBUSeats] [int] NOT NULL,
	[NoOfNONBUSeats] [int] NOT NULL,
	[Remarks] [nvarchar](500) NULL,
	[NoOfRows] [int] NOT NULL,
	[Price] [decimal](16, 2) NOT NULL,
	[Sheduled] [nvarchar](500) NULL,
	[Status] [bit] NOT NULL,
	[AirlineName] [nvarchar](500) NULL,
 CONSTRAINT [PK_Flights] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Flights]  WITH CHECK ADD  CONSTRAINT [FK_Flights_Airline_AirlineId] FOREIGN KEY([AirlineId])
REFERENCES [dbo].[Airline] ([Id])
GO

ALTER TABLE [dbo].[Flights] CHECK CONSTRAINT [FK_Flights_Airline_AirlineId]
GO


-------------------+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
USE [AirlineBooking]
GO

/****** Object:  Table [dbo].[UserRegistrestion]    Script Date: 09-05-2022 17:49:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserRegistrestion](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Mobile] [nvarchar](500) NULL,
	[Email] [nvarchar](500) NULL,
	[Role] [nvarchar](100) NULL,
	[Status] [bit] NOT NULL,
	[Password] [nvarchar](100) NULL,
 CONSTRAINT [PK_UserRegistrestion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


