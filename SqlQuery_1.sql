﻿DECLARE @CurrentMigration [nvarchar](max)

IF object_id('[dbo].[__MigrationHistory]') IS NOT NULL
    SELECT @CurrentMigration =
        (SELECT TOP (1) 
        [Project1].[MigrationId] AS [MigrationId]
        FROM ( SELECT 
        [Extent1].[MigrationId] AS [MigrationId]
        FROM [dbo].[__MigrationHistory] AS [Extent1]
        WHERE [Extent1].[ContextKey] = N'LvivCinema.Migrations.Configuration'
        )  AS [Project1]
        ORDER BY [Project1].[MigrationId] DESC)

IF @CurrentMigration IS NULL
    SET @CurrentMigration = '0'

IF @CurrentMigration < '201612072005092_MigrationDB'
BEGIN
    CREATE TABLE [dbo].[Actors] (
        [Id] [int] NOT NULL IDENTITY,
        [Name] [nvarchar](50) NOT NULL,
        [Surname] [nvarchar](50) NOT NULL,
        [Year] [int] NOT NULL,
        CONSTRAINT [PK_dbo.Actors] PRIMARY KEY ([Id])
    )
    CREATE TABLE [dbo].[Films] (
        [Id] [int] NOT NULL IDENTITY,
        [Name] [nvarchar](max),
        [Description] [nvarchar](max),
        [Director] [nvarchar](max),
        [year] [int] NOT NULL,
        CONSTRAINT [PK_dbo.Films] PRIMARY KEY ([Id])
    )
    CREATE TABLE [dbo].[Genres] (
        [Id] [int] NOT NULL IDENTITY,
        [Name] [nvarchar](max),
        CONSTRAINT [PK_dbo.Genres] PRIMARY KEY ([Id])
    )
    CREATE TABLE [dbo].[Cinemas] (
        [Id] [int] NOT NULL IDENTITY,
        [Name] [nvarchar](max),
        [Adress] [nvarchar](max),
        CONSTRAINT [PK_dbo.Cinemas] PRIMARY KEY ([Id])
    )
    CREATE TABLE [dbo].[Halls] (
        [Id] [int] NOT NULL IDENTITY,
        [Number] [int] NOT NULL,
        [NumberSeats] [int] NOT NULL,
        [CinemaId] [int],
        CONSTRAINT [PK_dbo.Halls] PRIMARY KEY ([Id])
    )
    CREATE INDEX [IX_CinemaId] ON [dbo].[Halls]([CinemaId])
    CREATE TABLE [dbo].[Sessions] (
        [Id] [int] NOT NULL IDENTITY,
        [dataTime] [datetime] NOT NULL,
        [FreeSeats] [int] NOT NULL,
        [HallId] [int],
        [film_Id] [int],
        CONSTRAINT [PK_dbo.Sessions] PRIMARY KEY ([Id])
    )
    CREATE INDEX [IX_HallId] ON [dbo].[Sessions]([HallId])
    CREATE INDEX [IX_film_Id] ON [dbo].[Sessions]([film_Id])
    CREATE TABLE [dbo].[GenreFilm] (
        [GenreId] [int] NOT NULL,
        [FilmId] [int] NOT NULL,
        CONSTRAINT [PK_dbo.GenreFilm] PRIMARY KEY ([GenreId], [FilmId])
    )
    CREATE INDEX [IX_GenreId] ON [dbo].[GenreFilm]([GenreId])
    CREATE INDEX [IX_FilmId] ON [dbo].[GenreFilm]([FilmId])
    CREATE TABLE [dbo].[ActorFilm] (
        [ActorId] [int] NOT NULL,
        [FilmId] [int] NOT NULL,
        CONSTRAINT [PK_dbo.ActorFilm] PRIMARY KEY ([ActorId], [FilmId])
    )
    CREATE INDEX [IX_ActorId] ON [dbo].[ActorFilm]([ActorId])
    CREATE INDEX [IX_FilmId] ON [dbo].[ActorFilm]([FilmId])
    ALTER TABLE [dbo].[Halls] ADD CONSTRAINT [FK_dbo.Halls_dbo.Cinemas_CinemaId] FOREIGN KEY ([CinemaId]) REFERENCES [dbo].[Cinemas] ([Id])
    ALTER TABLE [dbo].[Sessions] ADD CONSTRAINT [FK_dbo.Sessions_dbo.Films_film_Id] FOREIGN KEY ([film_Id]) REFERENCES [dbo].[Films] ([Id])
    ALTER TABLE [dbo].[Sessions] ADD CONSTRAINT [FK_dbo.Sessions_dbo.Halls_HallId] FOREIGN KEY ([HallId]) REFERENCES [dbo].[Halls] ([Id])
    ALTER TABLE [dbo].[GenreFilm] ADD CONSTRAINT [FK_dbo.GenreFilm_dbo.Genres_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [dbo].[Genres] ([Id]) ON DELETE CASCADE
    ALTER TABLE [dbo].[GenreFilm] ADD CONSTRAINT [FK_dbo.GenreFilm_dbo.Films_FilmId] FOREIGN KEY ([FilmId]) REFERENCES [dbo].[Films] ([Id]) ON DELETE CASCADE
    ALTER TABLE [dbo].[ActorFilm] ADD CONSTRAINT [FK_dbo.ActorFilm_dbo.Actors_ActorId] FOREIGN KEY ([ActorId]) REFERENCES [dbo].[Actors] ([Id]) ON DELETE CASCADE
    ALTER TABLE [dbo].[ActorFilm] ADD CONSTRAINT [FK_dbo.ActorFilm_dbo.Films_FilmId] FOREIGN KEY ([FilmId]) REFERENCES [dbo].[Films] ([Id]) ON DELETE CASCADE
    CREATE TABLE [dbo].[__MigrationHistory] (
        [MigrationId] [nvarchar](150) NOT NULL,
        [ContextKey] [nvarchar](300) NOT NULL,
        [Model] [varbinary](max) NOT NULL,
        [ProductVersion] [nvarchar](32) NOT NULL,
        CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])
    )
    INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
    VALUES (N'201612072005092_MigrationDB', N'LvivCinema.Migrations.Configuration',  0x1F8B0800000000000400ED1CDB6EE4B6F5BD40FE41D0535B3833B617015A6326C166BCD31859AF173BDEA07D32688933162A515389323C08FA6579C827F5174A8A94C4AB4469AEBB3102043B24CFE1B9928747E7F87FBFFD3EF9E12589BD6798E5518AA6FEC5E8DCF7200AD23042ABA95FE0E5B77FF37FF8FE9B3F4DDE85C98BF74BB5EE0D5D4720513EF59F305E5F8DC779F00413908F9228C8D23C5DE25190266310A6E3CBF3F3BF8F2F2EC690A0F0092ECF9B7C2A108E1258FE203F67290AE01A1720BE4D4318E77C9CCC2C4AACDE0790C07C0D0238F5DF3F47CFB30891D1115BEC7B6FE308104216305EFA1E4028C5001332AF3EE77081B314AD166B3200E2FBCD1A92754B10E790937FD52C77E5E4FC9272326E002B544191E334E989F0E20D17CD58051F2460BF161D11DE3B2264BCA15C97029CFA6F039C66BEA7EE74358B33BACA20DC510972E6351367B5151063A1FF9D79B322C64506A708163803F199F7B1788CA3E067B8B94FFF0DD11415712C1246482373D20019FA98A56B98E1CD27B8E4E4DE84BE3796E1C62A600D26C0305E6E107E73E97B1FC8E6E03186B5DE05BE178437F80F886006300C3F028C6186280E584A4EDB5DD98BFEBFDA8D181A7119DFBB052FEF215AE1A7A9FF1DF19179F402C36A8013F01945C4C1080CCE0A6820B07DD34591A163ECFB2F08B22ED12A183E80E768554A5AC1358FE284F8ED271897B3F953B466EECBCCED81CFCFB334F994C695E1B2E187455A6401E53FD5E7EE41B682582664326EFCA0D53B288A7ECE41215E7D63886F907F3A1969FB26D7300FB268CD4ECB7DEF1565901D9F7BDE68B343472B7D63B0A755DE64F2B4CA0B5D29219694413325E5944E8930AC5122CE992871F6F912513FA72F415EBDFE805E3FE01E71B029F51E31D9DB209B6256D1CFA82A4B7AB5AA8359957A5486E47CCA0F68BC3F8138361B2F9D7908B81535C62B0C6B07A238B7D5814811F5335D0AF16AB866C32D9247D8F73237E2584080F3ED1031ED68ECBB9A6B658FC3EC553D6C4DB6EC4AC982F82925C0480B9F7C782AEDB821461CD7BC479ADCCA7D38A67E1EC4815E9DC8B8570830B88F9A1BE09A6061BF7B3AC03C8370077E444D77B0172DCB37668BDDB205BADDD271CD8BA4C9BE6EC43C64A80BD94891FCCBC585DEE6791A44250D624CCE633799977728F45A02397673D7E11FB9BD89CB446BE22464DFA9FF574D36667CF5E9D0E063A981567493B1C0493B83E2D3CB469031E3D110C43379EE0C1ADF75FB6250BC106C04196F8786201687B8F3670C8E1A74554C2E233C1F8D2E349CE46484193D9A403C237E41CEDA0861FD188D5010AD416CDF5E01713C7BA99C6BE4EACC355C43448F4DBB0C5D766D22017DEF7A0BE546E8924A0FEB908E3A9B3ECDE75EA3D0FAA2753711E369D9E103BA850C60B43C48BBA8924FD59D302AC73AEDBEB53B5730ED7E005F30C9D165DBEA3A3F802BB00B90C060020133218941C7E00B36848D9F73C823C79C3F3055AD539C0B88958C5F73D9CA378666343238CF9868D0CC373A80AB1C9F06CDAFE30E70762C99E0ABD3BB03017F516BE0CCDE3B809B5785065FBBA0824250B322842AF324AC3024A6548B6B0F6F6A821B416B36DB1ED00818387D6ADC2F73E4C0AD9445D6B9B5463A9DB18E406B65D22DDC9AA29B3D702B3D74756EAD614F67E023D0CA8DB8855953A82320A8DD686B76E53789CEAFFD22EFBECA05821BCF6B61DA7879EF41C5F2E3C7CEB37EA777DFEA437996EEF12E5369E1B97A82D537505367316685165541C6D8529131B905EB7584564285061FF116AC3C63F6EDA27FE142C2708C83DC50BF50535BEF447C1DACA0324BB62694CEA32CC7D7008347405FF2B330D19689F7ADE54AA87692AE545D4FD535512DA7FF6620963A0A4340C261E784A384063365B2443DFC74388F96C680186486C4CC2C8D8B04D9832A3B344BB48BF06CC41D435D9D2022A907DDF1B06A0311091BD1314CC68A08B5384E5394E22FAADA9D8CC276A7F5B189B9E1DC74300933D8A95A84F48D5E44244DF4C0577F879790D5A3EE98369A8D6D4ECBC67898B79D91B1D8BBBF9559E08E6D6647524515546DA78B9931367450860DF0D8DAB061A83EA68A38AAB193D1290B9FB6D3E84F8630D0419F66B03D69937F8494F4C9C7FA62E19F4F74547CC21D5F93831491D9339347B3923A52DFCE50AA1C427F5BB142EEC75C9ACF6D228E66D41D93F0BD4D44250CBBE3AAB27422225BE66EFFA622BFB00C97769511E9BC98AB85C6DBD714E8D1E7A1A9B44E4E9FE81271B294129339192A6CDB8F226B06D889228AA31F41EAFBB7B702A59456E793CEA6C072D2518186FCD74071959876A040433EEDCB51A09CB6723894F94AC783D7202E53726AA0BC4A143BD0A029E93690A4DED4CC521446E577B09B9C1657D485150E8C766B5FCB62A94BEAD3BFCE662959AB09CF2075371B692925B6C4F708EBCF5148D3498B4D8E6132A20B468BFFC4B33822F74DB3E016A0680973CC6A74FCCBF38B4BA561E9749A87C6791EC6860C9CD641246BEC005546111569671D51DFCA3AD01415A16790054FF4F5AF34D26CD79DB32BB462F34D298B56F87E1D2F5FAF32FF9C8097BF889806F6946C874FE91BD90AD9663F7660C8F5FCD10CA16F75FF1F5A583A22B97A7E1F72D753265FAED4A5B2F06E3FB661908A5987A0514BC24B1C5A09CA0D0AE1CBD4FFB504BAF26EFEF950C19D7977198971AEBC73EFBF3B52B331E8FE6235ADD62E93DF10EFA4767988BAE5CA65576533287755EBFBD6A17EBF8D39D81E8CCC925D7133337B92445F6B7EFD769B65BD473F8971309BC4DC2C8D93DC6F6706D56363675D5912296EBAB2E74376A7AB7A8F7E12E3605FB2AE5A4BF78D4907B540ADBB68FF424BBFDCA16B18430C3D22C1F2A13C037900425D583445D1BAFBBC4E1089143894C27B034B636DDF62775E0C6B4D1FF73CD10E510EDE952C16B4A629CCA4ABAFCA629C75785083B127750F602F5DD972B52CB4BB6F664F16D3DC5C1A057BB4185BA1D9CE4DC6FA85A3E74D7C689B39E133E6381673A833A69FC11CF58C716960AB9A12867599C9E5DC035ADF06A9DA5A80B497D6B4AFA619CDE94CE85073DD4432A4B7EBA4FDDA5EDAA26FD5F279F380CA6E6DC873F2C79353B6B3BF1D50D9476FADD33B025485199AE7ECBD73ECE3EFD40F1F53A259F65C36372719FBEAAC6D7526BCE67E1663C79DBDE1CE84D8DC3B6669C66BE9C533E1B6B51E991AF5AC7D7A26C4E64E174B0B5F5B079F09B9AD17C72C6D8B268539ABD49D9A288568CF6C87F64D84E99E9D8A758B526BC3A2A58E444FEA6865E49D9D8A22C34A01DCF63D5DA65EC87646CD353C5A44A036651C974D531B656B4B66B73EA50351E847EAECC5D418B5DBE6768CDAF5D95DC3B6953EF7CFE64EBA4DE5735C6C26E86A31D55A0D4FA7A3B4BFB694AB41AAEF3B99A65127A9EF81AD1E7DA17AE51C09DA843FE64DE2C53C5A3528682D20828114AED56B6ED032ADA24685A26A89F2C1E3166240BFA9BECD70B4040126D3016597FEA9BE5F405C9025EF924718DEA0BB02AF0B4C5886C9632CFD8D291A7DB6ED5F36BFCA344FEECA7AA07C172C103223FA19F80EFD58447158D33D377C77B1A0A0612DFF484D7589E9C7EAD5A6C6F421458E88B8F8EA68FC1E26EB9820CBEFD0023CC321B47DCEE17BB802C1A62A80B423E956842CF6C97504561948728EA381273F890D87C9CBF7FF079533308AD35E0000 , N'6.1.3-40302')
END
