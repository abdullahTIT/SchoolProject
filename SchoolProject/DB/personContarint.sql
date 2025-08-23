-- 1. Ã⁄· Email ›—Ìœ« »‘—ÿ √·« ÌﬂÊ‰ NULL
CREATE UNIQUE INDEX UQ_Persons_Email
ON Persons (Email)
WHERE Email IS NOT NULL;

-- 2. Ã⁄· Phone ›—Ìœ« (Ìı› —÷ √‰Â €Ì— ﬁ«»· ··‹ NULL „”»ﬁ«)
ALTER TABLE Persons
ADD CONSTRAINT UQ_Persons_Phone UNIQUE (Phone);

-- 3. Ã⁄· IdentifierID ›—Ìœ« »‘—ÿ √·« ÌﬂÊ‰ NULL
CREATE UNIQUE INDEX UQ_Persons_IdentifierID
ON Persons (IdentifierID)
WHERE IdentifierID IS NOT NULL;

-- 4. ﬁÌœ «· Õﬁﬁ √‰ «·⁄„— »Ì‰ 3 Ê70 ”‰…  ﬁ—Ì»« («⁄ „«œ« ⁄·Ï DATEDIFF »«·”‰Ê« )
ALTER TABLE Persons
ADD CONSTRAINT CHK_Persons_AgeRange CHECK (
    DATEDIFF(YEAR, BirthDate, GETDATE()) BETWEEN 3 AND 70
);

-- 5. «· Õﬁﬁ „‰  ‰”Ìﬁ —ﬁ„ «·Â« › (10 √—ﬁ«„ ›ﬁÿ)
ALTER TABLE Persons
ADD CONSTRAINT CHK_Persons_Phone_Valid
CHECK (
    Phone NOT LIKE '%[^0-9]%'  -- ·« ÌÕ ÊÌ ⁄·Ï √Ì Õ—› €Ì— —ﬁ„
    AND LEN(Phone) BETWEEN 7 AND 15  -- «·ÿÊ· „‰ 7 ≈·Ï 15 —ﬁ„
);


ALTER TABLE Persons
ADD CONSTRAINT CHK_Persons_Phone_Valid
CHECK (
    RTRIM(Phone) NOT LIKE '%[^0-9]%'   -- ›ﬁÿ «·√—ﬁ«„ »⁄œ ≈“«·… «·„”«›«  «·Ì„Ì‰Ì…
    AND LEN(RTRIM(Phone)) BETWEEN 7 AND 15
);


SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH 
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Persons' AND COLUMN_NAME = 'Phone';



select Phone from Persons

SELECT 
    Phone,
    LEN(Phone) AS LengthWithoutTrailingSpaces,
    DATALENGTH(Phone) AS LengthInBytes,
    CAST(Phone AS VARBINARY(MAX)) AS PhoneBinary
FROM Persons
WHERE Phone LIKE '%[^0-9]%'
   OR LEN(Phone) < 7
   OR LEN(Phone) > 15;


   UPDATE Persons
SET Phone = LTRIM(RTRIM(Phone))
WHERE Phone LIKE '%[^0-9]%'
   OR LEN(Phone) < 7
   OR LEN(Phone) > 15;



SELECT definition
FROM sys.check_constraints
WHERE name = 'CHK_Persons_Phone_Valid';


ALTER TABLE Persons DROP CONSTRAINT CHK_Persons_Phone_Valid;

SELECT 
    name, 
    definition 
FROM sys.check_constraints
WHERE parent_object_id = OBJECT_ID('Persons');


EXEC sp_helpconstraint 'Persons';
