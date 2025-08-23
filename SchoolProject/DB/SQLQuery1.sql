CREATE TABLE EmployeeJobTitles (
    JobTitleID INT PRIMARY KEY IDENTITY(1,1),                     -- ַבדÚׁÝ ַבÝׁםֿ בבזÙםÝֹ
    JobTitleName NVARCHAR(100) NOT NULL UNIQUE,                   -- ַ׃ד ַבזÙםÝֹ דֻב: דÚבד¡ ֵַֿׁם¡ דֿםׁ
    Description NVARCHAR(255) NULL,                               -- זױÝ ַ־Êםַׁם בבזÙםÝֹ
    IsTeaching BIT NOT NULL DEFAULT 0,                            -- וב ַבדהױָ ÊÚבםדם¿
    IsAdministrative BIT NOT NULL DEFAULT 0,                      -- וב ַבדהױָ ֵַֿׁם¿
    CanTeach BIT NOT NULL DEFAULT 0,                              -- וב ם׃דֽ בױַָֽ ַבדהױָ ָÊֿׁם׃ דזַֿ¿
    RequiresCertification BIT NOT NULL DEFAULT 0,                 -- וב ַבזÙםÝֹ ÊÊ״בָ דִובַÊ ֳז װוַַֿÊ ־ַױֹ¿
);


INSERT INTO EmployeeJobTitles (JobTitleName, Description, IsTeaching, IsAdministrative, CanTeach, RequiresCertification)
VALUES 
(N'דÚבד', N'םÞזד ָÊֿׁם׃ ַבדזַֿ ַבַֿׁ׃םֹ ַבד־ÊבÝֹ בב״בַָ.', 1, 0, 1, 1),

(N'דÚבד ד׃ַÚֿ', N'ם׃ַÚֿ ַבדÚבד ַבֶׁם׃ם Ýם ַבױÝזÝ ַבַֿׁ׃םֹ.', 1, 0, 1, 0),

(N'דװׁÝ Êָׁזם', N'םÊַָÚ ֱֳַֿ ַבדÚבדםה זםÞֿד ַבֿÚד ַבÊָׁזם.', 1, 1, 1, 1),

(N'דֿםׁ דֿׁ׃ֹ', N'םֿםׁ ַבדֿׁ׃ֹ זםװׁÝ Úבל ַב״ַÞד ַבֳßַֿםדם זַבֵַֿׁם.', 0, 1, 1, 1),

(N'זßםב דֿׁ׃ֹ', N'ם׃ַÚֿ ַבדֿםׁ Ýם ַבֵַֹֿׁ ַבֳßַֿםדםֹ זַבֵַֿׁםֹ.', 0, 1, 1, 1),

(N'ֳדםה דßÊָֹ', N'םֿםׁ ַבדßÊָֹ זםזÝׁ ַבדױַֿׁ ַבÊÚבםדםֹ.', 0, 1, 0, 0),

(N'דׁװֿ ״בַָם', N'םזּו ַב״בַָ הÝ׃םנַ ז׃בזßםנַ זÊÚבםדםנַ.', 0, 1, 0, 1),

(N'׃ßׁÊםׁ', N'םÊזבל ַבדוַד ַבֵַֿׁםֹ זַבÊהÙםדםֹ Ýם ַבדֿׁ׃ֹ.', 0, 1, 0, 0),

(N'ד׃ִזב ÊÞהםֹ דÚבזדַÊ', N'םֿםׁ ֳהÙדֹ ַבדÚבזדַÊ זַבֽזַ׃םָ Ýם ַבדֿׁ׃ֹ.', 0, 1, 0, 1),

(N'דַֽ׃ָ', N'ד׃ִזב Úה ַבװִזה ַבדַבםֹ זַבׁזַÊָ.', 0, 1, 0, 1),

(N'Úַדב ־ֿדַÊ', N'םÞזד ֳָÚדַב ַבהÙַÝֹ זַבױםַהֹ זַב־ֿדַÊ ַבד׃ַהֹֿ.', 0, 0, 0, 0);


CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    PersonID INT NOT NULL,
    JobTitleID INT NOT NULL,
    HireDate DATE NOT NULL CHECK (HireDate <= GETDATE()),
    TerminationDate DATE NULL, -- ֵַׂבֹ ַבװׁ״ והַ
    IsActive BIT NOT NULL DEFAULT 1,
    Notes NVARCHAR(500) NULL,

    CONSTRAINT FK_Employees_Person FOREIGN KEY (PersonID) REFERENCES Persons(PersonID),
    CONSTRAINT FK_Employees_JobTitle FOREIGN KEY (JobTitleID) REFERENCES EmployeeJobTitles(JobTitleID)
);


select * from Persons


