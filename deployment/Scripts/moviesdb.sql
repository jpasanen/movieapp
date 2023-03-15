USE movies
GO

ALTER DATABASE movies
    COLLATE Latin1_General_100_BIN2_UTF8

--Script to drop all ext tables to enable dropping and recreating datasource and ext tables
DECLARE @sql NVARCHAR(max)=''
SELECT @sql += ' DROP EXTERNAL TABLE ' + QUOTENAME(TABLE_SCHEMA) + '.'+ QUOTENAME(TABLE_NAME) + '; '
FROM INFORMATION_SCHEMA.TABLES
WHERE  TABLE_TYPE = 'BASE TABLE'
Exec sp_executesql @sql
GO

IF EXISTS(select *
from sys.external_data_sources
where name = 'moviesSource')
DROP EXTERNAL DATA SOURCE moviesSource
CREATE EXTERNAL DATA SOURCE moviesSource WITH (
    LOCATION = 'https://$(datalakeName).blob.core.windows.net/bronze',
    CREDENTIAL = [movies-credential]
)
GO

IF EXISTS (SELECT *
FROM sys.external_tables
WHERE object_id = OBJECT_ID('Movies'))  
DROP EXTERNAL TABLE dbo.Movies
CREATE EXTERNAL TABLE dbo.Movies (
	[Id] int,
	[Adult] BIT,
	[Homepage] nvarchar(255),
	[OriginalLanguage] nvarchar(40),
	[ImdbId] nvarchar(40),
	[OriginalTitle] nvarchar(255),
	[Overview] nvarchar(1000),
	[PosterPath] nvarchar(255),
	[ReleaseDate] date,
	[Title] nvarchar(255),
	[Status] nvarchar(20)
	)
	WITH (
	LOCATION = 'latest/movies.parquet',
	DATA_SOURCE = [moviesSource],
	FILE_FORMAT = [ParquetFormat]
	)
GO

IF EXISTS (SELECT *
FROM sys.external_tables
WHERE object_id = OBJECT_ID('MovieGenres'))  
DROP EXTERNAL TABLE dbo.MovieGenres
CREATE EXTERNAL TABLE dbo.MovieGenres (
	[Id] int,
	[GenreId] int,
	[GenreName] nvarchar(255)
	)
	WITH (
	LOCATION = 'latest/moviegenres.parquet',
	DATA_SOURCE = [moviesSource],
	FILE_FORMAT = [ParquetFormat]
	)
GO