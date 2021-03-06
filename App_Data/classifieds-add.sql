CREATE TABLE [classifieds_Ads] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[MemberId] [int] NOT NULL ,
	[CategoryId] [int] NOT NULL ,
	[Title] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Description] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[URL] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Price] [money] NOT NULL ,
	[Location] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ExpirationDate] [smalldatetime] NULL ,
	[DateCreated] [smalldatetime] NOT NULL ,
	[DateApproved] [smalldatetime] NULL ,
	[NumViews] [int] NOT NULL ,
	[NumResponses] [int] NOT NULL ,
	[AdLevel] [int] NOT NULL ,
	[AdStatus] [int] NOT NULL ,
	[AdType] [int] NOT NULL ,
	[PreviewImageId] [int] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [classifieds_Categories] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ParentCategoryId] [int] NULL ,
	[Path] [varchar] (800) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[NumActiveAds] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [classifieds_Locations] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [classifieds_Members] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[AspNetUsername] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[AspNetApplicationName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[DateCreated] [smalldatetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [classifieds_Photos] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[AdId] [int] NOT NULL ,
	[BytesFull] [image] NULL ,
	[BytesSmall] [image] NULL ,
	[BytesMedium] [image] NULL ,
	[IsMainPreview] [bit] NOT NULL ,
	[DateCreated] [smalldatetime] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [classifieds_SavedAds] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[MemberId] [int] NOT NULL ,
	[AdId] [int] NOT NULL ,
	[DateCreated] [smalldatetime] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [classifieds_Ads] WITH NOCHECK ADD 
	CONSTRAINT [PK_Ads] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [classifieds_Categories] WITH NOCHECK ADD 
	CONSTRAINT [PK_Categories] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [classifieds_Locations] WITH NOCHECK ADD 
	CONSTRAINT [PK_Locations] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [classifieds_Members] WITH NOCHECK ADD 
	CONSTRAINT [PK_Members] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [classifieds_Photos] WITH NOCHECK ADD 
	CONSTRAINT [PK_Photos] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [classifieds_SavedAds] WITH NOCHECK ADD 
	CONSTRAINT [PK_SavedAds] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [classifieds_Ads] WITH NOCHECK ADD 
	CONSTRAINT [DF_Ads_Price] DEFAULT (0) FOR [Price],
	CONSTRAINT [DF_Ads_DateAdded] DEFAULT (getdate()) FOR [DateCreated],
	CONSTRAINT [DF_Ads_NumViews] DEFAULT (0) FOR [NumViews],
	CONSTRAINT [DF_Ads_NumResponses] DEFAULT (0) FOR [NumResponses],
	CONSTRAINT [DF_Ads_AdLevel] DEFAULT (0) FOR [AdLevel],
	CONSTRAINT [DF_Ads_AdStatus] DEFAULT (0) FOR [AdStatus],
	CONSTRAINT [DF_Ads_IsForSale] DEFAULT (1) FOR [AdType]
GO

ALTER TABLE [classifieds_Categories] WITH NOCHECK ADD 
	CONSTRAINT [DF_Categories_NumActiveAds] DEFAULT (0) FOR [NumActiveAds]
GO

ALTER TABLE [classifieds_Members] WITH NOCHECK ADD 
	CONSTRAINT [DF_Members_DateAdded] DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [classifieds_Photos] WITH NOCHECK ADD 
	CONSTRAINT [DF_Photos_IsMainPreview] DEFAULT (0) FOR [IsMainPreview],
	CONSTRAINT [DF_Photos_DateCreated] DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [classifieds_SavedAds] WITH NOCHECK ADD 
	CONSTRAINT [DF_SavedAds_DateCreated] DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [classifieds_Ads] ADD 
	CONSTRAINT [FK_Ads_Categories] FOREIGN KEY 
	(
		[CategoryId]
	) REFERENCES [classifieds_Categories] (
		[Id]
	) ON DELETE CASCADE  ON UPDATE CASCADE ,
	CONSTRAINT [FK_Ads_Members] FOREIGN KEY 
	(
		[MemberId]
	) REFERENCES [classifieds_Members] (
		[Id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

ALTER TABLE [classifieds_Categories] ADD 
	CONSTRAINT [FK_Categories_Categories] FOREIGN KEY 
	(
		[ParentCategoryId]
	) REFERENCES [classifieds_Categories] (
		[Id]
	)
GO

ALTER TABLE [classifieds_Photos] ADD 
	CONSTRAINT [FK_Photos_Ads] FOREIGN KEY 
	(
		[AdId]
	) REFERENCES [classifieds_Ads] (
		[Id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

ALTER TABLE [classifieds_SavedAds] ADD 
	CONSTRAINT [FK_SavedAds_Ads] FOREIGN KEY 
	(
		[AdId]
	) REFERENCES [classifieds_Ads] (
		[Id]
	) ON DELETE CASCADE  ON UPDATE CASCADE ,
	CONSTRAINT [FK_SavedAds_Members] FOREIGN KEY 
	(
		[MemberId]
	) REFERENCES [classifieds_Members] (
		[Id]
	)
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE VIEW ClassifiedsView_Ads
AS
SELECT     classifieds_Ads.Id, classifieds_Ads.MemberId, classifieds_Ads.CategoryId, classifieds_Ads.Title, classifieds_Ads.Description, classifieds_Ads.URL, classifieds_Ads.Price, classifieds_Ads.Location, 
                      classifieds_Ads.ExpirationDate, classifieds_Ads.DateCreated, classifieds_Ads.DateApproved, classifieds_Ads.NumViews, classifieds_Ads.NumResponses, classifieds_Ads.AdLevel, 
                      classifieds_Ads.AdStatus, classifieds_Ads.AdType, classifieds_Ads.PreviewImageId, classifieds_Members.AspNetUsername AS MemberName, 
                      classifieds_Categories.Name AS CategoryName
FROM         classifieds_Ads INNER JOIN
                      classifieds_Members ON classifieds_Ads.MemberId = classifieds_Members.Id INNER JOIN
                      classifieds_Categories ON classifieds_Ads.CategoryId = classifieds_Categories.Id

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE VIEW PhotosView
AS
SELECT     Id, AdId, IsMainPreview, DateCreated
FROM         classifieds_Photos

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE CountAdResponsesByStatus
(
	@AdStatus int = NULL
)
AS
SET NOCOUNT ON;

	SELECT      ISNULL(SUM(NumResponses), 0) AS Count
	FROM         classifieds_Ads
	WHERE (@AdStatus IS NULL OR AdStatus = @AdStatus)
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE CountAdViewsByStatus
(
	@AdStatus int
)
AS
SET NOCOUNT ON;

	SELECT     SUM(NumViews) AS Count
	FROM         classifieds_Ads
	WHERE     (@AdStatus IS NULL OR AdStatus = @AdStatus)
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE CountAdsByCategory
@CategoryId int,
@AdStatus int = 0
AS
SELECT Count(Id) AS AdCount FROM classifieds_Ads
WHERE
	(@AdStatus = 0 OR AdStatus = @AdStatus) AND
	(@CategoryId = 0
		OR 
		CategoryId IN (
		SELECT Id FROM classifieds_Categories
		WHERE Path LIKE
			  (SELECT Path
			   FROM classifieds_Categories
			   WHERE Id = @CategoryId ) + '%'))
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE CountAdsByStatus
(
	@AdStatus int = NULL,
	@MinDateCreated datetime = NULL
)
AS
SET NOCOUNT ON;

	SELECT     COUNT(*) AS Count
	FROM classifieds_Ads 
	WHERE (@AdStatus IS NULL OR AdStatus = @AdStatus) AND
	(@MinDateCreated IS NULL OR DateCreated > @MinDateCreated)
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE CountLocations
AS
	SET NOCOUNT ON;
SELECT     COUNT(*) AS Count
FROM         classifieds_Locations

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE CountMembersByDateRange
(
	@StartDate smalldatetime = NULL,
	@EndDate smalldatetime = NULL
)
AS
SET NOCOUNT ON;

	SELECT     COUNT(*) AS Count
	FROM         classifieds_Members
	WHERE   
	(@StartDate IS NULL OR DateCreated > @StartDate) AND
	(@EndDate IS NULL OR DateCreated < @EndDate)
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE CountTopCategories
AS
	SET NOCOUNT ON;
SELECT     COUNT(*) AS Count
FROM         classifieds_Categories
WHERE     (ParentCategoryId IS NULL)

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE CreateCategory
(
	@ParentCategoryId int,
	@Name nvarchar(50)
)
AS
	SET NOCOUNT OFF;
INSERT INTO [classifieds_Categories] ([ParentCategoryId], [Name] ) VALUES (@ParentCategoryId, @Name)

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE ExpireAd 
@AdId int,
@MemberId int,
@AdStatus int
AS
UPDATE classifieds_Ads
SET
	AdStatus = @AdStatus,
	ExpirationDate = getdate()
WHERE
	Id = @AdId AND MemberId = @MemberId
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE GetAdById
(
	@Id int
)
AS
	SET NOCOUNT ON;
SELECT     classifieds_Ads.Id, classifieds_Ads.MemberId, classifieds_Ads.CategoryId, classifieds_Ads.Title, classifieds_Ads.Description, classifieds_Ads.URL, classifieds_Ads.Price, classifieds_Ads.Location, 
                      classifieds_Ads.ExpirationDate, classifieds_Ads.DateCreated, classifieds_Ads.DateApproved, classifieds_Ads.NumViews, classifieds_Ads.NumResponses, classifieds_Ads.AdLevel, 
                      classifieds_Ads.AdStatus, classifieds_Ads.AdType, classifieds_Ads.PreviewImageId, classifieds_Members.AspNetUsername AS MemberName, 
                      classifieds_Categories.Name AS CategoryName
FROM         classifieds_Ads INNER JOIN
                      classifieds_Members ON classifieds_Ads.MemberId = classifieds_Members.Id INNER JOIN
                      classifieds_Categories ON classifieds_Ads.CategoryId = classifieds_Categories.Id
WHERE classifieds_Ads.Id = @Id

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE GetAdsByRandomOrder
@NumRecords int,
@AdStatus int,
@AdLevel int
AS
SET ROWCOUNT @NumRecords
SELECT * FROM ClassifiedsView_Ads
WHERE AdStatus = @AdStatus AND AdLevel = @AdLevel And PreviewImageId IS NOT NULL
ORDER BY newid()
SET ROWCOUNT 0
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE GetAdsByStatus
(
	@AdStatus int,
	@MemberId int = 0
)
AS
SET NOCOUNT ON;

SELECT * FROM ClassifiedsView_Ads
WHERE
	AdStatus = @AdStatus AND
	(@MemberId = 0 OR MemberId = @MemberId)
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE GetAllAdsByQuery
@LimitResultCount int = 50,
@CategoryId int = 0,
@MemberId int = 0,
@MaxPrice money = -1,
@SearchTerm nvarchar(50) = N'',
@Location nvarchar(50) = N'',
@AdType int = 0,
@AdStatus int = 0,
@AdLevel int = 0,
@MinDateCreated smalldatetime = NULL,
@MustHaveImage bit = false
AS
	SET ROWCOUNT @LimitResultCount

	SELECT *
	FROM ClassifiedsView_Ads
	WHERE
	(@CategoryId = 0 OR 
	CategoryId IN (
	SELECT Id FROM classifieds_Categories
	WHERE Path LIKE
	  (SELECT Path
	   FROM classifieds_Categories
	   WHERE Id = @CategoryId ) + '%'
	)) AND 
	(@MaxPrice = -1 OR Price <= @MaxPrice) AND
	(@MustHaveImage = 0 OR PreviewImageId IS NOT NULL) AND
	(@AdStatus = 0 OR AdStatus = @AdStatus) AND
	(@AdLevel = 0 OR AdLevel = @AdLevel) AND
	(@AdType = 0 OR AdType = @AdType) AND 
	(@MemberId = 0 OR MemberId = @MemberId) AND 
	(@MinDateCreated IS NULL OR DateCreated > @MinDateCreated) AND
	Title LIKE '%' + @SearchTerm + '%' AND 
	Location LIKE '%' + @Location + '%'
    ORDER BY DateCreated DESC
    
	SET ROWCOUNT 0
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE GetAllLocations
AS
SELECT     Id, Name
FROM         classifieds_Locations
ORDER BY Name

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE GetCategoriesByParentId
(
	@ParentCategoryId int = 0
)
AS
SET NOCOUNT ON;
SELECT     [Id], [ParentCategoryId], [Name], NumActiveAds
FROM         classifieds_Categories
WHERE    (ParentCategoryId = @ParentCategoryId)
	OR (@ParentCategoryId = 0 AND ParentCategoryId IS NULL)
ORDER BY Name
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE GetCategoryById
(
	@Id int
)
AS
	SET NOCOUNT ON;
SELECT Id, ParentCategoryId, Name, NumActiveAds FROM classifieds_Categories WHERE Id = @Id

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE GetExpiredAds
@ExpirationDate smalldatetime = getdate,
@AdStatus int = 0
AS
SELECT * FROM ClassifiedsView_Ads
WHERE
ExpirationDate < @ExpirationDate AND
(@AdStatus = 0 OR AdStatus = @AdStatus)
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE GetMemberByUsername
(
	@AspNetUsername nvarchar(256),
	@AspNetApplicationName nvarchar(256)
)
AS
	SET NOCOUNT ON;
SELECT     Id, AspNetUsername, AspNetApplicationName, DateCreated
FROM         classifieds_Members
WHERE     (AspNetUsername = @AspNetUsername) AND (AspNetApplicationName = @AspNetApplicationName)

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE GetParentCategoriesById 
@Id int
AS
SELECT Id, ParentCategoryId, Name, NumActiveAds
FROM classifieds_Categories
WHERE (SELECT Path
       FROM classifieds_Categories
       WHERE Id = @Id) LIKE Path + '%'
ORDER BY Path
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE GetPhotoById 
@Id int,
@Size int
AS
IF @Size = 1
	SELECT     	*
	FROM         PhotosView
	WHERE Id = @Id
ELSE
	SELECT     	*
	FROM         PhotosView
	WHERE Id = @Id
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE GetPhotoBytesById 
@Id int,
@Size int
AS
IF @Size = 3
	SELECT     	BytesFull AS Bytes
	FROM         classifieds_Photos
	WHERE Id = @Id
ELSE IF @Size = 2
	SELECT     	BytesMedium AS Bytes
	FROM         classifieds_Photos
	WHERE Id = @Id
ELSE
	SELECT     	BytesSmall AS Bytes
	FROM         classifieds_Photos
	WHERE Id = @Id
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE GetPhotosByAdId
(
	@AdId int
)
AS
	SET NOCOUNT ON;
SELECT     classifieds_Photos.Id, classifieds_Photos.AdId, IsMainPreview, DateCreated
FROM         classifieds_Photos
WHERE     (AdId = @AdId)
ORDER BY IsMainPreview DESC, DateCreated
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE GetPhotosByAdStatus
@AdStatus int
AS
DECLARE @NullBytes binary
SET @NullBytes = NULL

SELECT     classifieds_Photos.Id, classifieds_Photos.AdId, classifieds_Photos.IsMainPreview, 
                      classifieds_Photos.DateCreated
FROM         classifieds_Photos INNER JOIN
                      classifieds_Ads ON classifieds_Photos.Id = classifieds_Ads.Id
WHERE     (classifieds_Ads.AdStatus = @AdStatus)
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE GetSavedAds
@MemberId int
AS
SELECT     classifieds_Ads.Id, classifieds_Ads.MemberId, classifieds_Ads.CategoryId, classifieds_Ads.Title, classifieds_Ads.Description, classifieds_Ads.URL, classifieds_Ads.Price, classifieds_Ads.Location, 
                      classifieds_Ads.ExpirationDate, classifieds_Ads.DateCreated, classifieds_Ads.DateApproved, classifieds_Ads.NumViews, classifieds_Ads.NumResponses, classifieds_Ads.AdLevel, 
                      classifieds_Ads.AdStatus, classifieds_Ads.AdType, classifieds_Ads.PreviewImageId, classifieds_Members.AspNetUsername AS MemberName, 
                      classifieds_Categories.Name AS CategoryName
FROM         classifieds_Ads INNER JOIN
                      classifieds_SavedAds ON classifieds_Ads.Id = classifieds_SavedAds.AdId INNER JOIN
                      classifieds_Members ON classifieds_Ads.MemberId = classifieds_Members.Id INNER JOIN
                      classifieds_Categories ON classifieds_Ads.CategoryId = classifieds_Categories.Id
WHERE classifieds_SavedAds.MemberId = @MemberId
ORDER BY classifieds_SavedAds.DateCreated DESC
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE InsertAd
(
	@MemberId int,
	@CategoryId int,
	@Title nvarchar(100),
	@Description ntext,
	@URL nvarchar(500),
	@Price money,
	@Location nvarchar(50),
	@ExpirationDate smalldatetime = NULL,
	@DateCreated smalldatetime = NULL,
	@DateApproved smalldatetime = NULL,
	@NumViews int,
	@NumResponses int,
	@AdLevel int,
	@AdStatus int,
	@AdType int
)
AS
DECLARE @Id int;
SET NOCOUNT OFF;
INSERT INTO classifieds_Ads
                      (MemberId, CategoryId, Title, Description, URL, Price, Location, ExpirationDate, DateCreated, DateApproved, NumViews, NumResponses, AdLevel, 
                      AdStatus, AdType)
VALUES     (@MemberId,@CategoryId,@Title,@Description,@URL,@Price,@Location,@ExpirationDate,@DateCreated,@DateApproved,@NumViews,@NumResponses,@AdLevel,@AdStatus,@AdType)
SET @Id = @@IDENTITY
SELECT  @Id  AS [Id]
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE InsertCategory
(
	@ParentCategoryId int,
	@Name nvarchar(50)
)
AS
SET NOCOUNT OFF;
DECLARE @Id int;
INSERT INTO classifieds_Categories
                      (ParentCategoryId, Name)
VALUES     (@ParentCategoryId,@Name)
SET @Id = @@IDENTITY;
SELECT @Id AS [Id]

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE InsertLocation 
@Name nvarchar(50)
AS
INSERT INTO classifieds_Locations (Name)
VALUES (@Name)

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE InsertMember
	@AspNetUsername nvarchar(256),
	@AspNetApplicationName nvarchar(256),
	@DateCreated smalldatetime = getdate
AS
DECLARE @Id int;
SET NOCOUNT ON;
INSERT INTO [classifieds_Members] ([AspNetUsername], [AspNetApplicationName], [DateCreated]) VALUES (@AspNetUsername, @AspNetApplicationName, @DateCreated);
SET @Id = @@IDENTITY
SELECT  @Id  AS [Id]

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE InsertPhoto
@AdId int,
@BytesFull image = NULL,
@BytesMedium image = NULL,
@BytesSmall image = NULL,
@IsMainPreview bit = 0,
@DateCreated smalldatetime = getdate
AS
DECLARE @Id int;
INSERT classifieds_Photos
(AdId, BytesFull, BytesMedium, BytesSmall, IsMainPreview, DateCreated)
VALUES
(@AdId, @BytesFull, @BytesMedium, @BytesSmall, @IsMainPreview, @DateCreated)
SET @Id = @@IDENTITY;

IF @IsMainPreview = 1
UPDATE classifieds_Ads SET PreviewImageId = @Id WHERE Id = @AdId	

SELECT @Id AS [Id]
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE InsertSavedAd 
@AdId int,
@MemberId int
AS
SET NOCOUNT ON;
SELECT AdId FROM classifieds_SavedAds WHERE AdId = @AdId AND MemberId = @MemberId
IF @@ROWCOUNT = 0
INSERT INTO classifieds_SavedAds
	(AdId, MemberId, DateCreated)
VALUES
	(@AdId, @MemberId, getdate())

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE MoveAds
@CurrentCategoryId int,
@NewCategoryId int
AS
UPDATE classifieds_Ads
SET
	CategoryId = @NewCategoryId
WHERE
	CategoryId = @CurrentCategoryId
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE MoveCategory
@CategoryId int,
@NewParentCategoryId int
AS
declare @NumActiveAdsInCategory int;
declare @OldPath varchar(800);
declare @NewPath varchar(800);

-- Stored Procedure will not execute, if:

-- 1st case: Current Category = New Parent Category (it cannot become a parent of itself)
IF @CategoryId = @NewParentCategoryId
	RETURN -1

-- 2nd case: the input category is a parent of the new category
-- (a parent category cannot become a child category under one of its own children)
IF EXISTS 	(SELECT Id FROM classifieds_Categories
		WHERE
			(SELECT Path
			FROM classifieds_Categories
			WHERE
			Id = @CategoryId) LIKE Path + '%'
		AND
			Id = @NewParentCategoryId)
	RETURN -1 -- exits

SELECT @OldPath = ParentCategories.Path

FROM         classifieds_Categories INNER JOIN
                      classifieds_Categories ParentCategories ON classifieds_Categories.ParentCategoryId = ParentCategories.Id
WHERE     (classifieds_Categories.Id = @CategoryId)

SELECT @NewPath =
	CASE WHEN
		@NewParentCategoryId IS NULL THEN '.'
	ELSE 
		Path
	END
	FROM classifieds_Categories
	WHERE @NewParentCategoryId is NULL OR Id = @NewParentCategoryId

IF @OldPath IS NULL
BEGIN
	SELECT @OldPath = Path FROM classifieds_Categories WHERE Id = @CategoryId
	SET @NewPath = @NewPath + CAST(@CategoryId AS VARCHAR(10)) + '.'
END


SELECT @NumActiveAdsInCategory  = Count(Id) FROM ClassifiedsView_Ads WHERE 
AdStatus >= 100 AND 
ExpirationDate > getdate() AND
ClassifiedsView_Ads.CategoryId IN (SELECT Id FROM classifieds_Categories
WHERE Path LIKE
  (SELECT Path
   FROM classifieds_Categories
   WHERE Id = @CategoryId ) + '%' )

DECLARE @NegativeCount int;
SET @NegativeCount = 0 - @NumActiveAdsInCategory 
EXEC UpdateCategoryAdCounts @CategoryId, @NegativeCount 

UPDATE classifieds_Categories
SET Path = 
	REPLACE(Path, @OldPath, @NewPath)

WHERE Path LIKE

  (SELECT Path
   FROM classifieds_Categories
   WHERE Id = @CategoryId ) + '%'

EXEC UpdateCategoryAdCounts @CategoryId, @NumActiveAdsInCategory 

UPDATE classifieds_Categories SET ParentCategoryId = @NewParentCategoryId WHERE Id = @CategoryId

RETURN 1
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE RelistAd
	@AdId int,
	@CategoryId int,
	@Title nvarchar(100),
	@Description ntext,
	@URL nvarchar(500),
	@Price money,
	@Location nvarchar(50),
	@ExpirationDate smalldatetime = NULL,
	@DateCreated smalldatetime = NULL,
	@DateApproved smalldatetime = NULL,
	@AdLevel int,
	@AdStatus int,
	@AdType int
AS
UPDATE classifieds_Ads
SET

	CategoryId = @CategoryId,
	Title = @Title ,
	Description = @Description ,
	URL = @URL,
	Price = @Price, 
	Location = @Location, 
	ExpirationDate = @ExpirationDate ,
	-- @DateCreated smalldatetime = NULL,
	DateApproved = @DateApproved, 
	AdLevel = @AdLevel, 
	AdStatus = @AdStatus, 
	AdType = @AdType,
	NumResponses = 0,
	NumViews = 0	

WHERE Id = @AdId

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE RemoveAd 
@AdId int
AS
DELETE FROM classifieds_Photos WHERE AdId = @AdId
DELETE FROM classifieds_SavedAds WHERE AdId = @AdId
DELETE FROM classifieds_Ads WHERE Id = @AdId
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE RemoveAdsByStatus
@AdStatus int
AS
SET XACT_ABORT ON
SET NOCOUNT ON
BEGIN TRANSACTION
DELETE FROM classifieds_Photos WHERE AdId IN (SELECT Id FROM classifieds_Ads WHERE AdStatus = @AdStatus)
DELETE FROM classifieds_SavedAds WHERE AdId IN (SELECT Id FROM classifieds_Ads WHERE AdStatus = @AdStatus)
DELETE FROM classifieds_Ads WHERE AdStatus = @AdStatus
COMMIT
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE RemoveCategory
(
	@Id int
)
AS
SET NOCOUNT ON;

DECLARE @returnval int
SELECT @returnval = 1

-- cannot remove a category with ads in it (they must be moved first)
---- return -1
IF EXISTS(SELECT Id FROM classifieds_Ads WHERE
			(classifieds_Ads.CategoryId IN 
				(SELECT Id FROM classifieds_Categories
				WHERE Path LIKE
					(SELECT Path
					FROM classifieds_Categories
					WHERE Id = @Id ) + '%')))
	SELECT @returnval = -1

-- cannot remove category if it is a parent of other Categories
---- return -2
IF EXISTS(SELECT Id FROM classifieds_Categories WHERE ParentCategoryId = @Id)
    SELECT @returnval = -2

-- can remove category
IF (@returnval = 1)
  DELETE FROM [classifieds_Categories] WHERE [Id] = @Id

SELECT @returnval

RETURN 0

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE RemoveLocation
@Id int
AS
DELETE FROM classifieds_Locations
WHERE [Id] = @Id

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE RemovePhoto
@Id int
AS
DECLARE @AdId int

SELECT @AdId = AdId
FROM classifieds_Photos
WHERE Id = @Id

DELETE FROM classifieds_Photos WHERE Id = @Id

IF NOT EXISTS(SELECT Id FROM classifieds_Photos WHERE AdId = @AdId AND IsMainPreview = 1)
BEGIN
	UPDATE
		classifieds_Photos SET IsMainPreview = 1
		WHERE Id =
		(SELECT TOP 1 Id FROM classifieds_Photos WHERE AdId = @AdId)
	UPDATE    
		classifieds_Ads SET PreviewImageId =
        (SELECT Id
         FROM classifieds_Photos
         WHERE (AdId = @AdId) AND (IsMainPreview = 1)
         )
		WHERE (Id = @AdId)
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE RemovePhotosByAdStatus
@AdStatus int
AS
UPDATE classifieds_Ads SET PreviewImageId = NULL WHERE AdStatus = @AdStatus
DELETE FROM classifieds_Photos
WHERE AdId IN
	(SELECT Id FROM classifieds_Ads WHERE AdStatus = @AdStatus)
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE RemoveSavedAd
@AdId int,
@MemberId int
AS
DELETE FROM classifieds_SavedAds
WHERE AdId = @AdId AND MemberId = @MemberId
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE SetAdPreviewPhoto
@PhotoId int,
@AdId int
AS
UPDATE classifieds_Photos SET
	IsMainPreview = 1 - ABS(SIGN(@PhotoId - Id))
WHERE AdId = @AdId

UPDATE classifieds_Ads
	SET PreviewImageId = @PhotoId
WHERE
	Id = @AdId
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE UpdateAd
	@Id int,
	@MemberId int,
	@Title nvarchar(100),
	@Description ntext,
	@URL nvarchar(500),
	@Price money,
	@Location nvarchar(50)
AS
UPDATE classifieds_Ads
SET
	Title = @Title,
	[Description] = @Description,
	Url = @URL,
	Price = @Price,
	Location = @Location
WHERE
	Id = @Id
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE UpdateAdCategory
@Id int,
@CategoryId int
AS
UPDATE classifieds_Ads
SET 
	CategoryId = @CategoryId
WHERE
	Id = @Id
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE UpdateAdLevel
@Id int,
@AdLevel int
AS
UPDATE classifieds_Ads
SET 
	AdLevel = @AdLevel
WHERE
	Id = @Id
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE UpdateAdStats
@AdId int,
@NumberToAddToViews int = 0,
@NumberToAddToResponses int = 0
AS
UPDATE classifieds_Ads SET
	NumViews = NumViews + @NumberToAddToViews,
	NumResponses = NumResponses + @NumberToAddToResponses
WHERE
	Id = @AdId

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE UpdateAdStatus
@Id int,
@AdStatus int
AS
UPDATE classifieds_Ads
SET 
	AdStatus = @AdStatus
WHERE
	Id = @Id
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE UpdateCategory
(
	@Id int,
	@ParentCategoryId int = NULL,
	@Name nvarchar(50),
	@NumActiveAds int
)
AS
	SET NOCOUNT OFF;
UPDATE [classifieds_Categories] SET [ParentCategoryId] = @ParentCategoryId,  [Name] = @Name, [NumActiveAds] = @NumActiveAds WHERE (([Id] = @Id))

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE UpdateCategoryAdCounts
@LeafCategoryId int,
@NumberToAdd int
AS

SET NOCOUNT ON

UPDATE classifieds_Categories SET NumActiveAds = NumActiveAds + @NumberToAdd
WHERE Id IN
	(SELECT Id
	FROM classifieds_Categories
	WHERE (SELECT Path
	       FROM classifieds_Categories
	       WHERE Id = @LeafCategoryId) LIKE Path + '%')
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE UpdateCategoryName 
@Id int,
@Name nvarchar(50)
AS
UPDATE classifieds_Categories
SET Name = @Name
WHERE Id = @Id
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE UpdateLocation 
@Id int,
@Name nvarchar(50)
AS
UPDATE classifieds_Locations
SET [Name] = @Name
WHERE [Id] = @Id

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

CREATE TRIGGER AdDeleted
ON classifieds_Ads
FOR	DELETE
AS

DECLARE @CurrentAdId AS int
SET @CurrentAdId = 	(SELECT TOP 1 [Id]
			FROM Deleted
			ORDER BY [Id])


	-- loop through each row	
	WHILE @CurrentAdId IS NOT NULL
	BEGIN
	
		DECLARE	@AdStatus	    int;
		DECLARE	@CategoryId	int;
		
		SELECT @AdStatus = AdStatus, @CategoryId = CategoryId
		FROM Deleted
		WHERE Id = @CurrentAdId
		
		IF @AdStatus >= 100
		BEGIN
		    -- decrement the affected category counts by adding a -1
		    EXEC UpdateCategoryAdCounts @CategoryId, -1
		END

		SET @CurrentAdId = 	(SELECT TOP 1 [Id]
					FROM Deleted
					WHERE [Id] > @CurrentAdId
					ORDER BY [Id])

END




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

CREATE TRIGGER AdInserted
ON classifieds_Ads
FOR INSERT
AS

DECLARE @CurrentAdId AS int

SET @CurrentAdId = 	(SELECT TOP 1 [Id]
			FROM Inserted
			ORDER BY [Id])


	-- loop through each row	
	WHILE @CurrentAdId IS NOT NULL
	BEGIN
		
		
		DECLARE	@AdStatus   int;
		DECLARE	@CategoryId int;

		SELECT @AdStatus = AdStatus, @CategoryId = CategoryId
		FROM Inserted
		WHERE Id = @CurrentAdId;

		IF @AdStatus >= 100
		BEGIN
		    -- increment the affected category counts by adding +1
		    EXEC UpdateCategoryAdCounts @CategoryId, 1
		END

		SET @CurrentAdId = 	(SELECT TOP 1 [Id]
						FROM Inserted
						WHERE [Id] > @CurrentAdId
						ORDER BY [Id])

	END



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

CREATE TRIGGER AdUpdated
ON classifieds_Ads
FOR	UPDATE
AS

DECLARE @CurrentAdId AS int

SET @CurrentAdId = 	(SELECT TOP 1 [Id]
			FROM Inserted
			ORDER BY [Id])


	-- loop through each row	
	WHILE @CurrentAdId IS NOT NULL
	BEGIN

		DECLARE	@AdStatus	        int;
		DECLARE	@CategoryId	        int;
		DECLARE	@OldAdStatus	    int;
		DECLARE	@OldCategoryId	    int;
	
		SELECT      @AdStatus = Inserted.AdStatus,
			      @CategoryId = Inserted.CategoryId
		FROM        Inserted
		WHERE Id = @CurrentAdId
		                    
		SELECT      @OldAdStatus = Deleted.AdStatus,
		            @OldCategoryId = Deleted.CategoryId
		FROM        Deleted
		WHERE Id = @CurrentAdId

		IF UPDATE(AdStatus) 
		BEGIN
		    IF (@AdStatus < 100 AND @OldAdStatus >= 100)
		        EXEC UpdateCategoryAdCounts @OldCategoryId, -1
		    ELSE IF (@AdStatus >= 100 AND @OldAdStatus < 100)
		        EXEC UpdateCategoryAdCounts @OldCategoryId, 1
		END
		
		IF UPDATE(CategoryId)
		BEGIN
		  IF (@OldCategoryId <> @CategoryId)
		     BEGIN
		         EXEC UpdateCategoryAdCounts @CategoryId, 1
		         EXEC UpdateCategoryAdCounts @OldCategoryId, -1
		     END
		END
			
		SET @CurrentAdId = 	(SELECT TOP 1 [Id]
					FROM Inserted
					WHERE [Id] > @CurrentAdId
					ORDER BY [Id])

	END




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE TRIGGER OnCategoryInsert
ON classifieds_Categories
FOR INSERT 
AS
DECLARE @numrows AS int
SET @numrows = @@rowcount
IF @numrows > 1
BEGIN
  RAISERROR('Only single row inserts are supported', 16, 1)
  ROLLBACK TRAN
END
ELSE
IF @numrows = 1
BEGIN
UPDATE classifieds_Categories
SET    
    Path = 
      CASE
          WHEN Inserted.ParentCategoryId IS NULL THEN '.' 
          ELSE ParentCategory.Path
          END + CAST(Inserted.Id AS varchar(10)) + '.'
FROM        Inserted INNER JOIN
                      classifieds_Categories ON Inserted.Id = classifieds_Categories.Id 
                      LEFT OUTER JOIN
                      classifieds_Categories AS ParentCategory ON classifieds_Categories.ParentCategoryId = ParentCategory.Id
                    
END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO
