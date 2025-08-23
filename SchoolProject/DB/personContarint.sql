-- 1. ��� Email ������ ���� ��� ���� NULL
CREATE UNIQUE INDEX UQ_Persons_Email
ON Persons (Email)
WHERE Email IS NOT NULL;

-- 2. ��� Phone ������ (������ ��� ��� ���� ��� NULL ������)
ALTER TABLE Persons
ADD CONSTRAINT UQ_Persons_Phone UNIQUE (Phone);

-- 3. ��� IdentifierID ������ ���� ��� ���� NULL
CREATE UNIQUE INDEX UQ_Persons_IdentifierID
ON Persons (IdentifierID)
WHERE IdentifierID IS NOT NULL;

-- 4. ��� ������ �� ����� ��� 3 �70 ��� ������� (�������� ��� DATEDIFF ��������)
ALTER TABLE Persons
ADD CONSTRAINT CHK_Persons_AgeRange CHECK (
    DATEDIFF(YEAR, BirthDate, GETDATE()) BETWEEN 3 AND 70
);

-- 5. ������ �� ����� ��� ������ (10 ����� ���)
ALTER TABLE Persons
ADD CONSTRAINT CHK_Persons_Phone_Valid
CHECK (
    Phone NOT LIKE '%[^0-9]%'  -- �� ����� ��� �� ��� ��� ���
    AND LEN(Phone) BETWEEN 7 AND 15  -- ����� �� 7 ��� 15 ���
);


ALTER TABLE Persons
ADD CONSTRAINT CHK_Persons_Phone_Valid
CHECK (
    RTRIM(Phone) NOT LIKE '%[^0-9]%'   -- ��� ������� ��� ����� �������� ��������
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
