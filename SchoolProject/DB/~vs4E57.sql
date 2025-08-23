CREATE TABLE IdentifierTypes (
    IdentifierTypeID INT PRIMARY KEY IDENTITY(1,1),
    IdentifierName NVARCHAR(50) NOT NULL
);





CREATE TABLE Identifiers (
    IdentifierID INT PRIMARY KEY IDENTITY(1,1),
    IdentifierTypeID INT NOT NULL,
    IdentifierValue NCHAR(20) NULL,
    CONSTRAINT FK_Identifiers_IdentifierTypes 
        FOREIGN KEY (IdentifierTypeID) REFERENCES IdentifierTypes(IdentifierTypeID),
    CONSTRAINT UQ_IdentifierType_Value 
        UNIQUE (IdentifierTypeID, IdentifierValue)
);



-- توليد 100 سجل تجريبي في جدول Identifiers
DECLARE @i INT = 1;

WHILE @i <= 100
BEGIN
    INSERT INTO Identifiers (IdentifierTypeID, IdentifierValue)
    VALUES (
        -- رقم نوع المعرف عشوائي من 1 إلى 10
        FLOOR(RAND(CHECKSUM(NEWID())) * 10) + 1,
        -- قيمة المعرف: رقم عشوائي ثابت الطول
        RIGHT('0000000000' + CAST(ABS(CHECKSUM(NEWID())) % 10000000000 AS VARCHAR(10)), 10)
    );

    SET @i = @i + 1;
END;



select * from Identifiers

INSERT INTO IdentifierTypes (IdentifierName) VALUES
(N'National ID'),       -- بطاقة الهوية الوطنية
(N'Passport'),          -- جواز السفر
(N'Driver License'),    -- رخصة القيادة
(N'Student ID'),        -- بطاقة الطالب
(N'Military ID'),       -- الهوية العسكرية
(N'Employee ID'),       -- هوية الموظف
(N'Birth Certificate'), -- شهادة الميلاد
(N'Tax Number'),        -- الرقم الضريبي
(N'Insurance Number'),  -- رقم التأمين
(N'Residence Permit');  -- تصريح الإقامة





CREATE TABLE Persons (
    PersonID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(20) NOT NULL,
    SecondName NVARCHAR(20) NOT NULL,
    ThirdName NVARCHAR(20) NOT NULL,
    LastName NVARCHAR(20) NOT NULL,
    IdentifierID INT NULL,
    Gender BIT NOT NULL,
    BirthDate DATE NULL,
    Phone NCHAR(10) NOT NULL,
    Email NCHAR(100) NULL,
    Address NVARCHAR(255) NULL,
    IsActive BIT NOT NULL DEFAULT 1,

    CONSTRAINT FK_Persons_Identifiers 
        FOREIGN KEY (IdentifierID) REFERENCES Identifiers(IdentifierID)
        ON DELETE SET NULL,

    CONSTRAINT UQ_Persons_Phone UNIQUE (Phone),

    CONSTRAINT CHK_Persons_PhoneOnlyDigits CHECK (Phone NOT LIKE '%[^0-9]%'),
    CONSTRAINT CHK_Persons_BirthDate CHECK (BirthDate IS NULL OR BirthDate <= CAST(GETDATE() AS DATE)),
    CONSTRAINT CHK_Persons_Gender CHECK (Gender IN (0, 1)),
    CONSTRAINT CHK_Persons_EmailFormat CHECK (Email IS NULL OR Email LIKE '_%@_%._%')
);


INSERT INTO Persons
    (FirstName, SecondName, ThirdName, LastName, IdentifierID, Gender, BirthDate, Phone, Email, Address, IsActive)
VALUES
('محمد', 'أحمد', 'سعيد', 'القحطاني', 1, 1, DATEADD(YEAR, -45, GETDATE()), '0501000001', NULL, N'الرياض', 1),
('أحمد', 'سعيد', 'قاسم', 'القحطاني', 2, 1, DATEADD(YEAR, -40, GETDATE()), '0501000002', NULL, N'الرياض', 1),
('سعيد', 'قاسم', 'محمد', 'القحطاني', 3, 1, DATEADD(YEAR, -60, GETDATE()), '0501000003', NULL, N'الرياض', 1),
('نورة', 'محمد', 'أحمد', 'القحطاني', 4, 0, DATEADD(YEAR, -38, GETDATE()), '0501000004', NULL, N'الرياض', 1),
('علي', 'سعيد', 'قاسم', 'القحطاني', 5, 1, DATEADD(YEAR, -30, GETDATE()), '0501000005', NULL, N'الرياض', 1),
('فاطمة', 'علي', 'سعيد', 'القحطاني', 6, 0, DATEADD(YEAR, -25, GETDATE()), '0501000006', NULL, N'الرياض', 1),
('سلمان', 'محمد', 'سعيد', 'القحطاني', 7, 1, DATEADD(YEAR, -50, GETDATE()), '0501000007', NULL, N'الرياض', 1),
('مريم', 'أحمد', 'سعيد', 'القحطاني', 8, 0, DATEADD(YEAR, -28, GETDATE()), '0501000008', NULL, N'الرياض', 1),
('عبدالله', 'سعيد', 'قاسم', 'القحطاني', 9, 1, DATEADD(YEAR, -35, GETDATE()), '0501000009', NULL, N'الرياض', 1),
('هند', 'علي', 'سعيد', 'القحطاني', 10, 0, DATEADD(YEAR, -20, GETDATE()), '0501000010', NULL, N'الرياض', 1),

('عبدالله', 'محمد', 'سالم', 'الحربي', 11, 1, DATEADD(YEAR, -44, GETDATE()), '0501000011', NULL, N'جدة', 1),
('سالم', 'عبدالله', 'محمد', 'الحربي', 12, 1, DATEADD(YEAR, -59, GETDATE()), '0501000012', NULL, N'جدة', 1),
('ريم', 'سالم', 'عبدالله', 'الحربي', 13, 0, DATEADD(YEAR, -35, GETDATE()), '0501000013', NULL, N'جدة', 1),
('موسى', 'سالم', 'محمد', 'الحربي', 14, 1, DATEADD(YEAR, -30, GETDATE()), '0501000014', NULL, N'جدة', 1),
('سارة', 'عبدالله', 'سالم', 'الحربي', 15, 0, DATEADD(YEAR, -28, GETDATE()), '0501000015', NULL, N'جدة', 1),
('فهد', 'محمد', 'سالم', 'الحربي', 16, 1, DATEADD(YEAR, -42, GETDATE()), '0501000016', NULL, N'جدة', 1),
('نجلاء', 'سالم', 'محمد', 'الحربي', 17, 0, DATEADD(YEAR, -24, GETDATE()), '0501000017', NULL, N'جدة', 1),
('زيد', 'عبدالله', 'سالم', 'الحربي', 18, 1, DATEADD(YEAR, -37, GETDATE()), '0501000018', NULL, N'جدة', 1),
('عائشة', 'محمد', 'سالم', 'الحربي', 19, 0, DATEADD(YEAR, -31, GETDATE()), '0501000019', NULL, N'جدة', 1),
('خالد', 'سالم', 'عبدالله', 'الحربي', 20, 1, DATEADD(YEAR, -39, GETDATE()), '0501000020', NULL, N'جدة', 1),

('خالد', 'سعيد', 'محمد', 'الزهراني', 21, 1, DATEADD(YEAR, -43, GETDATE()), '0501000021', NULL, N'الدمام', 1),
('سعيد', 'خالد', 'محمد', 'الزهراني', 22, 1, DATEADD(YEAR, -57, GETDATE()), '0501000022', NULL, N'الدمام', 1),
('ريم', 'سعيد', 'خالد', 'الزهراني', 23, 0, DATEADD(YEAR, -34, GETDATE()), '0501000023', NULL, N'الدمام', 1),
('فاطمة', 'خالد', 'سعيد', 'الزهراني', 24, 0, DATEADD(YEAR, -29, GETDATE()), '0501000024', NULL, N'الدمام', 1),
('سلمان', 'سعيد', 'خالد', 'الزهراني', 25, 1, DATEADD(YEAR, -27, GETDATE()), '0501000025', NULL, N'الدمام', 1),
('هند', 'سعيد', 'محمد', 'الزهراني', 26, 0, DATEADD(YEAR, -22, GETDATE()), '0501000026', NULL, N'الدمام', 1),
('ماجد', 'خالد', 'سعيد', 'الزهراني', 27, 1, DATEADD(YEAR, -45, GETDATE()), '0501000027', NULL, N'الدمام', 1),
('مريم', 'سعيد', 'خالد', 'الزهراني', 28, 0, DATEADD(YEAR, -26, GETDATE()), '0501000028', NULL, N'الدمام', 1),
('علياء', 'خالد', 'سعيد', 'الزهراني', 29, 0, DATEADD(YEAR, -21, GETDATE()), '0501000029', NULL, N'الدمام', 1),
('يوسف', 'سعيد', 'خالد', 'الزهراني', 30, 1, DATEADD(YEAR, -32, GETDATE()), '0501000030', NULL, N'الدمام', 1),

('سلمان', 'عبدالله', 'محمد', 'الهاشمي', 31, 1, DATEADD(YEAR, -41, GETDATE()), '0501000031', NULL, N'مكة', 1),
('عبدالله', 'سلمان', 'خالد', 'الهاشمي', 32, 1, DATEADD(YEAR, -58, GETDATE()), '0501000032', NULL, N'مكة', 1),
('ليلى', 'عبدالله', 'سلمان', 'الهاشمي', 33, 0, DATEADD(YEAR, -33, GETDATE()), '0501000033', NULL, N'مكة', 1),
('نور', 'سلمان', 'خالد', 'الهاشمي', 34, 0, DATEADD(YEAR, -28, GETDATE()), '0501000034', NULL, N'مكة', 1),
('فيصل', 'سلمان', 'عبدالله', 'الهاشمي', 35, 1, DATEADD(YEAR, -39, GETDATE()), '0501000035', NULL, N'مكة', 1),
('هدى', 'عبدالله', 'سلمان', 'الهاشمي', 36, 0, DATEADD(YEAR, -26, GETDATE()), '0501000036', NULL, N'مكة', 1),
('مازن', 'خالد', 'سلمان', 'الهاشمي', 37, 1, DATEADD(YEAR, -47, GETDATE()), '0501000037', NULL, N'مكة', 1),
('رنا', 'سلمان', 'عبدالله', 'الهاشمي', 38, 0, DATEADD(YEAR, -25, GETDATE()), '0501000038', NULL, N'مكة', 1),
('حاتم', 'عبدالله', 'سلمان', 'الهاشمي', 39, 1, DATEADD(YEAR, -44, GETDATE()), '0501000039', NULL, N'مكة', 1),
('جود', 'خالد', 'سلمان', 'الهاشمي', 40, 0, DATEADD(YEAR, -23, GETDATE()), '0501000040', NULL, N'مكة', 1),

('مها', 'يوسف', 'سعيد', 'القادري', 41, 0, DATEADD(YEAR, -39, GETDATE()), '0501000041', NULL, N'الخبر', 1),
('يوسف', 'مها', 'سعيد', 'القادري', 42, 1, DATEADD(YEAR, -60, GETDATE()), '0501000042', NULL, N'الخبر', 1),
('عمر', 'يوسف', 'مها', 'القادري', 43, 1, DATEADD(YEAR, -36, GETDATE()), '0501000043', NULL, N'الخبر', 1),
('سلمى', 'عمر', 'يوسف', 'القادري', 44, 0, DATEADD(YEAR, -30, GETDATE()), '0501000044', NULL, N'الخبر', 1),
('رامي', 'يوسف', 'سعيد', 'القادري', 45, 1, DATEADD(YEAR, -41, GETDATE()), '0501000045', NULL, N'الخبر', 1),

-- هنا يكمل باقي الـ 100 سجل بنفس الأسلوب
('عبدالله', 'سلمان', 'خالد', 'المصري', 46, 1, DATEADD(YEAR, -50, GETDATE()), '0501000046', NULL, N'الرياض', 1),
('ليلى', 'محمد', 'علي', 'السعيد', 47, 0, DATEADD(YEAR, -29, GETDATE()), '0501000047', NULL, N'جدة', 1),
('زيد', 'خالد', 'سلمان', 'العتيبي', 48, 1, DATEADD(YEAR, -35, GETDATE()), '0501000048', NULL, N'الدمام', 1),
('سارة', 'سعيد', 'علي', 'الحمد', 49, 0, DATEADD(YEAR, -40, GETDATE()), '0501000049', NULL, N'مكة', 1),
('فهد', 'محمد', 'سلمان', 'النجار', 50, 1, DATEADD(YEAR, -27, GETDATE()), '0501000050', NULL, N'الرياض', 1),
('منى', 'عبدالله', 'سعيد', 'الزايد', 51, 0, DATEADD(YEAR, -22, GETDATE()), '0501000051', NULL, N'جدة', 1),
('ياسر', 'خالد', 'محمد', 'البلوي', 52, 1, DATEADD(YEAR, -48, GETDATE()), '0501000052', NULL, N'الدمام', 1),
('أمل', 'سعيد', 'عبدالله', 'الشريف', 53, 0, DATEADD(YEAR, -34, GETDATE()), '0501000053', NULL, N'مكة', 1),
('خالد', 'محمد', 'سلمان', 'السهلي', 54, 1, DATEADD(YEAR, -37, GETDATE()), '0501000054', NULL, N'الرياض', 1),
('رنا', 'علي', 'سعيد', 'العنزي', 55, 0, DATEADD(YEAR, -28, GETDATE()), '0501000055', NULL, N'جدة', 1),
('سلمان', 'عبدالله', 'محمد', 'القصيبي', 56, 1, DATEADD(YEAR, -41, GETDATE()), '0501000056', NULL, N'الدمام', 1),
('مريم', 'سعيد', 'خالد', 'الشعلان', 57, 0, DATEADD(YEAR, -26, GETDATE()), '0501000057', NULL, N'مكة', 1),
('فواز', 'محمد', 'سلمان', 'الحميد', 58, 1, DATEADD(YEAR, -33, GETDATE()), '0501000058', NULL, N'الرياض', 1),
('هدى', 'عبدالله', 'سعيد', 'الزيدان', 59, 0, DATEADD(YEAR, -21, GETDATE()), '0501000059', NULL, N'جدة', 1),
('علي', 'خالد', 'سلمان', 'البقمي', 60, 1, DATEADD(YEAR, -45, GETDATE()), '0501000060', NULL, N'الدمام', 1),
('سعاد', 'سعيد', 'محمد', 'الشامسي', 61, 0, DATEADD(YEAR, -30, GETDATE()), '0501000061', NULL, N'مكة', 1),
('موسى', 'عبدالله', 'خالد', 'العمري', 62, 1, DATEADD(YEAR, -36, GETDATE()), '0501000062', NULL, N'الرياض', 1),
('ريم', 'محمد', 'سلمان', 'الزبيدي', 63, 0, DATEADD(YEAR, -25, GETDATE()), '0501000063', NULL, N'جدة', 1),
('زيد', 'سعيد', 'عبدالله', 'الشريف', 64, 1, DATEADD(YEAR, -29, GETDATE()), '0501000064', NULL, N'الدمام', 1),
('أمل', 'خالد', 'محمد', 'القحطاني', 65, 0, DATEADD(YEAR, -38, GETDATE()), '0501000065', NULL, N'مكة', 1),
('سامي', 'سعيد', 'علي', 'العنزي', 66, 1, DATEADD(YEAR, -42, GETDATE()), '0501000066', NULL, N'الرياض', 1),
('منى', 'محمد', 'سلمان', 'الحمد', 67, 0, DATEADD(YEAR, -27, GETDATE()), '0501000067', NULL, N'جدة', 1),
('فيصل', 'عبدالله', 'خالد', 'النجار', 68, 1, DATEADD(YEAR, -31, GETDATE()), '0501000068', NULL, N'الدمام', 1),
('هند', 'سعيد', 'محمد', 'الزايد', 69, 0, DATEADD(YEAR, -24, GETDATE()), '0501000069', NULL, N'مكة', 1),
('علي', 'خالد', 'سلمان', 'البلوي', 70, 1, DATEADD(YEAR, -39, GETDATE()), '0501000070', NULL, N'الرياض', 1),
('رنا', 'سعيد', 'عبدالله', 'الشريف', 71, 0, DATEADD(YEAR, -35, GETDATE()), '0501000071', NULL, N'جدة', 1),
('حاتم', 'محمد', 'سلمان', 'السهلي', 72, 1, DATEADD(YEAR, -33, GETDATE()), '0501000072', NULL, N'الدمام', 1),
('جود', 'علي', 'سعيد', 'العنزي', 73, 0, DATEADD(YEAR, -29, GETDATE()), '0501000073', NULL, N'مكة', 1),
('سلمان', 'عبدالله', 'محمد', 'القصيبي', 74, 1, DATEADD(YEAR, -40, GETDATE()), '0501000074', NULL, N'الرياض', 1),
('مريم', 'سعيد', 'خالد', 'الشعلان', 75, 0, DATEADD(YEAR, -26, GETDATE()), '0501000075', NULL, N'جدة', 1),
('فواز', 'محمد', 'سلمان', 'الحميد', 76, 1, DATEADD(YEAR, -32, GETDATE()), '0501000076', NULL, N'الدمام', 1),
('هدى', 'عبدالله', 'سعيد', 'الزيدان', 77, 0, DATEADD(YEAR, -22, GETDATE()), '0501000077', NULL, N'مكة', 1),
('علي', 'خالد', 'سلمان', 'البقمي', 78, 1, DATEADD(YEAR, -46, GETDATE()), '0501000078', NULL, N'الرياض', 1),
('سعاد', 'سعيد', 'محمد', 'الشامسي', 79, 0, DATEADD(YEAR, -31, GETDATE()), '0501000079', NULL, N'جدة', 1),
('موسى', 'عبدالله', 'خالد', 'العمري', 80, 1, DATEADD(YEAR, -37, GETDATE()), '0501000080', NULL, N'الدمام', 1),
('ريم', 'محمد', 'سلمان', 'الزبيدي', 81, 0, DATEADD(YEAR, -24, GETDATE()), '0501000081', NULL, N'مكة', 1),
('زيد', 'سعيد', 'عبدالله', 'الشريف', 82, 1, DATEADD(YEAR, -28, GETDATE()), '0501000082', NULL, N'الرياض', 1),
('أمل', 'خالد', 'محمد', 'القحطاني', 83, 0, DATEADD(YEAR, -37, GETDATE()), '0501000083', NULL, N'جدة', 1),
('سامي', 'سعيد', 'علي', 'العنزي', 84, 1, DATEADD(YEAR, -43, GETDATE()), '0501000084', NULL, N'الدمام', 1),
('منى', 'محمد', 'سلمان', 'الحمد', 85, 0, DATEADD(YEAR, -29, GETDATE()), '0501000085', NULL, N'مكة', 1),
('فيصل', 'عبدالله', 'خالد', 'النجار', 86, 1, DATEADD(YEAR, -33, GETDATE()), '0501000086', NULL, N'الرياض', 1),
('هند', 'سعيد', 'محمد', 'الزايد', 87, 0, DATEADD(YEAR, -25, GETDATE()), '0501000087', NULL, N'جدة', 1),
('علي', 'خالد', 'سلمان', 'البلوي', 88, 1, DATEADD(YEAR, -40, GETDATE()), '0501000088', NULL, N'الدمام', 1),
('رنا', 'سعيد', 'عبدالله', 'الشريف', 89, 0, DATEADD(YEAR, -34, GETDATE()), '0501000089', NULL, N'مكة', 1),
('حاتم', 'محمد', 'سلمان', 'السهلي', 90, 1, DATEADD(YEAR, -32, GETDATE()), '0501000090', NULL, N'الرياض', 1),
('جود', 'علي', 'سعيد', 'العنزي', 91, 0, DATEADD(YEAR, -27, GETDATE()), '0501000091', NULL, N'جدة', 1),
('سلمان', 'عبدالله', 'محمد', 'القصيبي', 92, 1, DATEADD(YEAR, -41, GETDATE()), '0501000092', NULL, N'الدمام', 1),
('مريم', 'سعيد', 'خالد', 'الشعلان', 93, 0, DATEADD(YEAR, -23, GETDATE()), '0501000093', NULL, N'مكة', 1),
('فواز', 'محمد', 'سلمان', 'الحميد', 94, 1, DATEADD(YEAR, -31, GETDATE()), '0501000094', NULL, N'الرياض', 1),
('هدى', 'عبدالله', 'سعيد', 'الزيدان', 95, 0, DATEADD(YEAR, -22, GETDATE()), '0501000095', NULL, N'جدة', 1),
('علي', 'خالد', 'سلمان', 'البقمي', 96, 1, DATEADD(YEAR, -44, GETDATE()), '0501000096', NULL, N'الدمام', 1),
('سعاد', 'سعيد', 'محمد', 'الشامسي', 97, 0, DATEADD(YEAR, -30, GETDATE()), '0501000097', NULL, N'مكة', 1),
('موسى', 'عبدالله', 'خالد', 'العمري', 98, 1, DATEADD(YEAR, -38, GETDATE()), '0501000098', NULL, N'الرياض', 1),
('ريم', 'محمد', 'سلمان', 'الزبيدي', 99, 0, DATEADD(YEAR, -25, GETDATE()), '0501000099', NULL, N'جدة', 1),
('زيد', 'سعيد', 'عبدالله', 'الشريف', 100, 1, DATEADD(YEAR, -29, GETDATE()), '0501000100', NULL, N'الدمام', 1);




CREATE TABLE Guardians (
    GuardianID INT PRIMARY KEY IDENTITY(1,1),
    PersonID INT NOT NULL,
    Jobs NVARCHAR(50) NOT NULL,
    Notes NVARCHAR(255) NULL,
    CONSTRAINT FK_Guardians_Persons FOREIGN KEY (PersonID) REFERENCES Persons(PersonID)
);


select PersonID,BirthDate from Persons 
where BirthDate between DATEADD(YEAR, -30, GETDATE()) and DATEADD(YEAR, -5, GETDATE())

INSERT INTO Guardians (PersonID, Jobs, Notes)
VALUES
(6, N'مهندس', N'ولي أمر مسؤول وملتزم'),
(8, N'طبيب', N'ولي أمر داعم'),
(10, N'معلم', N'ولي أمر متابع دراسيًا'),
(15, N'موظف حكومي', N'ولي أمر متعاون'),
(17, N'مدير مشروع', N'ولي أمر مهتم'),
(24, N'محاسب', N'ولي أمر متفهم'),
(25, N'مبرمج', N'ولي أمر ملتزم'),
(26, N'موظف بنك', N'ولي أمر حريص'),
(28, N'صاحب محل', N'ولي أمر متابع'),
(29, N'طبيب أسنان', N'ولي أمر نشيط'),
(64, N'ممرض', N'ولي أمر متعاون'),
(67, N'فني كهرباء', N'ولي أمر مهتم'),
(38, N'مدرب رياضي', N'ولي أمر داعم'),
(40, N'مصمم جرافيك', N'ولي أمر ملتزم'),
(73, N'صيدلي', N'ولي أمر متفهم'),
(50, N'باحث', N'ولي أمر متعاون'),
(51, N'مدير مبيعات', N'ولي أمر مهتم'),
(55, N'محامي', N'ولي أمر داعم'),
(77, N'فني كمبيوتر', N'ولي أمر نشيط'),
(100, N'طباخ', N'ولي أمر حريص');

select * from Guardians


CREATE TABLE Students (
    StudentID INT PRIMARY KEY IDENTITY(1,1),
    PersonID INT NOT NULL,
    GuardianID INT NOT NULL,
    EnrollmentDate DATE NOT NULL CHECK (EnrollmentDate <= CAST(GETDATE() AS DATE)),
    StageID INT NOT NULL,
    AcademicYearID INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    DocumentPath NVARCHAR(512) NULL,
    Notes NVARCHAR(255) NULL,
    CONSTRAINT FK_Students_Persons FOREIGN KEY (PersonID) REFERENCES Persons(PersonID),
    CONSTRAINT FK_Students_Guardians FOREIGN KEY (GuardianID) REFERENCES Guardians(GuardianID),
    CONSTRAINT FK_Students_Stages FOREIGN KEY (StageID) REFERENCES Stages(StageID),
    CONSTRAINT FK_Students_AcademicYears FOREIGN KEY (AcademicYearID) REFERENCES AcademicYears(AcademicYearID),
    CONSTRAINT UQ_Student_Stage_Year UNIQUE (PersonID, StageID, AcademicYearID)
);


CREATE TABLE Stages (
    StageID INT IDENTITY(1,1) PRIMARY KEY,      -- مفتاح أساسي مع زيادة تلقائية
    StageName NVARCHAR(50) NOT NULL,            -- غير NULL لأن اسم المرحلة يجب أن يكون موجودًا
    IsActive BIT NOT NULL DEFAULT 1              -- قيمة افتراضية 1 (مفعّل) لتسهيل الإدخال
);

INSERT INTO Stages (StageName, IsActive) VALUES
('روضة أولى', 1),
('روضة ثانية', 1),
('الصف الأول', 1),
('الصف الثاني', 1),
('الصف الثالث', 1),
('الصف الرابع', 1),
('الصف الخامس', 1),
('الصف السادس', 1),
('الصف الأول متوسط', 1),
('الصف الثاني متوسط', 1),
('الصف الثالث متوسط', 1),
('الصف الأول ثانوي', 1),
('الصف الثاني ثانوي', 1),
('الصف الثالث ثانوي', 1);



CREATE TABLE AcademicYears (
    AcademicYearID INT IDENTITY(1,1) PRIMARY KEY,
    AcademicYearName NVARCHAR(50) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    CONSTRAINT chk_Start_EndDate CHECK (StartDate < EndDate)  -- شرط التأكد من أن تاريخ البداية قبل النهاية
);


INSERT INTO AcademicYears (AcademicYearName, StartDate, EndDate) VALUES
('2010-2011', '2010-09-01', '2011-06-30'),
('2011-2012', '2011-09-01', '2012-06-30'),
('2012-2013', '2012-09-01', '2013-06-30'),
('2013-2014', '2013-09-01', '2014-06-30'),
('2014-2015', '2014-09-01', '2015-06-30'),
('2015-2016', '2015-09-01', '2016-06-30'),
('2016-2017', '2016-09-01', '2017-06-30'),
('2017-2018', '2017-09-01', '2018-06-30'),
('2018-2019', '2018-09-01', '2019-06-30'),
('2019-2020', '2019-09-01', '2020-06-30'),
('2020-2021', '2020-09-01', '2021-06-30'),
('2021-2022', '2021-09-01', '2022-06-30'),
('2022-2023', '2022-09-01', '2023-06-30'),
('2023-2024', '2023-09-01', '2024-06-30'),
('2024-2025', '2024-09-01', '2025-06-30');


INSERT INTO Students (PersonID, GuardianID, EnrollmentDate, StageID, AcademicYearID, IsActive, DocumentPath, Notes) VALUES
(6, 5, '2024-01-10', 14, 10, 1, NULL, NULL),       -- عمر 24، مرحلة ثانوي (14)
(8, 3, '2023-09-01', 14, 12, 1, NULL, NULL),       -- عمر 27، مرحلة ثانوي
(10, 7, '2024-01-15', 10, 9, 1, NULL, NULL),       -- عمر 18، مرحلة إعدادي (10)
(15, 1, '2023-08-20', 14, 15, 1, NULL, NULL),      -- عمر 27، ثانوي
(17, 6, '2024-02-05', 12, 7, 1, NULL, NULL),       -- عمر 22، إعدادي
(24, 2, '2023-09-10', 14, 11, 1, NULL, NULL),      -- عمر 28، ثانوي
(25, 15, '2023-10-01', 14, 13, 1, NULL, NULL),     -- عمر 26، ثانوي
(26, 14, '2024-01-25', 11, 8, 1, NULL, NULL),      -- عمر 21، إعدادي
(28, 9, '2023-09-20', 14, 10, 1, NULL, NULL),      -- عمر 25، ثانوي
(29, 8, '2024-02-01', 9, 6, 1, NULL, NULL),        -- عمر 19، إعدادي
(34, 11, '2023-08-30', 14, 14, 1, NULL, NULL),     -- عمر 27، ثانوي
(36, 16, '2024-01-10', 12, 7, 1, NULL, NULL),      -- عمر 25، إعدادي
(38, 7, '2023-09-15', 14, 12, 1, NULL, NULL),      -- عمر 24، ثانوي
(40, 10, '2024-02-10', 11, 8, 1, NULL, NULL),      -- عمر 22، إعدادي
(47, 13, '2023-08-25', 14, 15, 1, NULL, NULL),     -- عمر 28، ثانوي
(50, 18, '2023-09-05', 14, 13, 1, NULL, NULL),     -- عمر 26، ثانوي
(51, 19, '2024-01-20', 10, 9, 1, NULL, NULL),      -- عمر 21، إعدادي
(55, 4, '2023-10-10', 14, 14, 1, NULL, NULL),      -- عمر 27، ثانوي
(57, 6, '2024-01-30', 12, 7, 1, NULL, NULL),       -- عمر 25، إعدادي
(59, 1, '2023-09-01', 9, 6, 1, NULL, NULL),        -- عمر 19، إعدادي
(63, 20, '2024-02-15', 14, 11, 1, NULL, NULL),     -- عمر 24، ثانوي
(64, 2, '2023-08-20', 14, 10, 1, NULL, NULL),      -- عمر 28، ثانوي
(67, 12, '2023-09-12', 10, 9, 1, NULL, NULL),      -- عمر 26، إعدادي
(69, 8, '2024-01-28', 11, 8, 1, NULL, NULL),       -- عمر 22، إعدادي
(73, 14, '2023-10-05', 14, 15, 1, NULL, NULL),     -- عمر 28، ثانوي
(75, 3, '2023-09-18', 14, 12, 1, NULL, NULL),      -- عمر 25، ثانوي
(77, 5, '2024-02-20', 10, 9, 1, NULL, NULL),       -- عمر 21، إعدادي
(81, 7, '2023-08-28', 11, 8, 1, NULL, NULL),       -- عمر 22، إعدادي
(82, 15, '2023-09-22', 14, 13, 1, NULL, NULL),     -- عمر 26، ثانوي
(85, 9, '2024-01-15', 14, 14, 1, NULL, NULL),      -- عمر 28، ثانوي
(87, 10, '2023-09-10', 12, 7, 1, NULL, NULL),      -- عمر 24، إعدادي
(91, 11, '2024-02-01', 10, 9, 1, NULL, NULL),      -- عمر 26، إعدادي
(93, 20, '2023-08-25', 14, 15, 1, NULL, NULL),     -- عمر 22، ثانوي
(95, 19, '2023-09-12', 11, 8, 1, NULL, NULL),      -- عمر 21، إعدادي
(99, 1, '2024-01-05', 14, 10, 1, NULL, NULL),      -- عمر 24، ثانوي
(100, 6, '2023-08-30', 14, 13, 1, NULL, NULL);     -- عمر 28، ثانوي








CREATE TABLE StudentFees (
    StudentFeeID INT IDENTITY(1,1) PRIMARY KEY,  -- رقم تسلسلي تلقائي لكل رسم
    StudentID INT NOT NULL,                       -- معرف الطالب (مفتاح خارجي لاحقًا)
    StageFeeItemID INT NOT NULL,                  -- معرف بند الرسوم (مفتاح خارجي لاحقًا)
    DueDate DATE NOT NULL CHECK (DueDate >= CAST(GETDATE() AS DATE)),  -- تاريخ الاستحقاق لا يمكن أن يكون في الماضي
    Status NVARCHAR(10) NOT NULL CHECK (Status IN ('Pending', 'Paid', 'Overdue', 'Cancelled')),  
        -- حالة الرسوم مقيدة بقيم محددة فقط
    Notes NVARCHAR(500) NULL                      -- ملاحظات اختيارية
);

-- إضافة قيود المفاتيح الخارجية (لو موجودة الجداول المرتبطة)
ALTER TABLE StudentFees
ADD CONSTRAINT FK_StudentFees_Students FOREIGN KEY (StudentID) REFERENCES Students(StudentID);

ALTER TABLE StudentFees
ADD CONSTRAINT FK_StudentFees_StageFeeItems FOREIGN KEY (StageFeeItemID) REFERENCES StageFeeItems(StageFeeItemID);


CREATE TABLE FeeTypes (
    FeeTypeID INT PRIMARY KEY IDENTITY(1,1), -- معرف تلقائي
    FeeName NVARCHAR(100) NOT NULL UNIQUE,   -- يجب أن يكون اسم نوع الرسوم فريدًا وغير فارغ
    Description NVARCHAR(255) NULL,          -- وصف اختياري
    IsActive BIT NOT NULL DEFAULT 1);





INSERT INTO FeeTypes (FeeName, Description)
VALUES 
(N'رسوم تسجيل', N'يتم دفعها مرة واحدة عند تسجيل الطالب لأول مرة'),
(N'رسوم شهرية', N'الرسوم الشهرية لتغطية تكاليف الدراسة'),
(N'رسوم حافلة', N'رسوم النقل المدرسي للطلاب'),
(N'رسوم كتب', N'تغطية تكلفة الكتب الدراسية'),
(N'رسوم أنشطة', N'رسوم الفعاليات والأنشطة اللاصفية'),
(N'رسوم اختبارات', N'رسوم تغطية تنظيم الاختبارات الفصلية والنهائية'),
(N'رسوم زي مدرسي', N'تغطية تكلفة الزي المدرسي الرسمي');




CREATE TABLE StageFeeItems (
    StageFeeItemID INT PRIMARY KEY IDENTITY(1,1), -- رقم معرف تلقائي
    StageID INT NOT NULL,                         -- المرحلة الدراسية (ابتدائي، متوسط، إلخ)
    FeeTypeID INT NOT NULL,                       -- نوع الرسوم (شهرية، تسجيل، حافلة، إلخ)
    AcademicYearID INT NOT NULL,                  -- السنة الدراسية
    Amount DECIMAL(18, 2) NOT NULL CHECK (Amount >= 0), -- المبلغ ويجب ألا يكون سالبًا
    Description NCHAR(100) NULL,                  -- وصف اختياري للرسوم
    IsActive BIT NOT NULL DEFAULT 1,              -- لتفعيل/تعطيل البند
    
    -- العلاقات (القيود الخارجية)
    CONSTRAINT FK_StageFeeItems_Stages FOREIGN KEY (StageID) REFERENCES Stages(StageID),
    CONSTRAINT FK_StageFeeItems_FeeTypes FOREIGN KEY (FeeTypeID) REFERENCES FeeTypes(FeeTypeID),
    CONSTRAINT FK_StageFeeItems_AcademicYear FOREIGN KEY (AcademicYearID) REFERENCES AcademicYears(AcademicYearID),

    -- منع التكرار لنفس المرحلة ونوع الرسوم والسنة
    CONSTRAINT UQ_Stage_Fee_Year UNIQUE (StageID, FeeTypeID, AcademicYearID)
);




select * from AcademicYears

select * from StudentFees


-- بيانات الرسوم الدراسية لكل مرحلة ونوع وسنة
DECLARE @StageID INT, @FeeTypeID INT, @AcademicYearID INT;

SET NOCOUNT ON;

-- تبدأ الحلقات التكرارية
BEGIN
    SET @StageID = 1
    WHILE @StageID <= 14
    BEGIN
        SET @FeeTypeID = 1
        WHILE @FeeTypeID <= 7
        BEGIN
            SET @AcademicYearID = 1
            WHILE @AcademicYearID <= 15
            BEGIN
                INSERT INTO StageFeeItems (
                    StageID, FeeTypeID, AcademicYearID, Amount, Description, IsActive
                )
                VALUES (
                    @StageID,
                    @FeeTypeID,
                    @AcademicYearID,
                    CAST(1000 + (@StageID * 100) + (@FeeTypeID * 10) AS DECIMAL(18,2)), -- مبلغ منطقي
                    N'رسوم للمرحلة ' + CAST(@StageID AS NCHAR(2)) + N'، النوع ' + CAST(@FeeTypeID AS NCHAR(2)) + N'، سنة ' + CAST(@AcademicYearID AS NCHAR(2)),
                    IIF(@AcademicYearID = 15, 1, 0)
                );
                SET @AcademicYearID += 1
            END
            SET @FeeTypeID += 1
        END
        SET @StageID += 1
    END
END

select * from StageFeeItems

select StageFeeItemID,StageID,AcademicYearID,Amount from StageFeeItems where AcademicYearID = 15 and FeeTypeID = 1

select * from FeeTypes

select StudentID,EnrollmentDate,StageID,AcademicYearID from Students


select * from StudentFees




--INSERT INTO StudentFees (StudentID, StageFeeItemID, DueDate, Status, Notes)
--VALUES
--(1, 15, '2024-03-15', 'Pending', NULL),
--(2, 120, '2023-12-01', 'Paid', NULL),
--(3, 225, '2024-03-20', 'Pending', NULL),
--(4, 330, '2023-11-20', 'Overdue', NULL),
--(5, 435, '2024-05-10', 'Pending', NULL),
--(6, 540, '2023-12-10', 'Paid', NULL),
--(7, 645, '2024-01-01', 'Cancelled', NULL),
--(8, 750, '2024-04-01', 'Pending', NULL),
--(9, 855, '2023-12-20', 'Overdue', NULL),
--(10, 960, '2024-04-05', 'Pending', NULL),
--(11, 1065, '2023-11-30', 'Paid', NULL),
--(12, 1170, '2024-03-15', 'Pending', NULL),
--(13, 1275, '2023-12-15', 'Paid', NULL),
--(14, 1380, '2024-05-10', 'Pending', NULL),
--(15, 15, '2023-11-25', 'Cancelled', NULL),
--(16, 120, '2023-12-15', 'Paid', NULL),
--(17, 225, '2024-03-25', 'Pending', NULL),
--(18, 330, '2024-01-15', 'Paid', NULL),
--(19, 435, '2024-04-30', 'Pending', NULL),
--(20, 540, '2023-12-01', 'Overdue', NULL),
--(21, 645, '2024-05-15', 'Pending', NULL),
--(22, 750, '2023-11-15', 'Overdue', NULL),
--(23, 855, '2023-12-12', 'Paid', NULL),
--(24, 960, '2024-04-20', 'Pending', NULL),
--(25, 1065, '2024-01-15', 'Pending', NULL),
--(26, 1170, '2023-12-18', 'Paid', NULL),
--(27, 1275, '2024-05-25', 'Pending', NULL),
--(28, 1380, '2023-11-28', 'Overdue', NULL),
--(29, 15, '2023-12-22', 'Paid', NULL),
--(30, 120, '2024-03-15', 'Pending', NULL),
--(31, 225, '2023-12-10', 'Cancelled', NULL),
--(32, 330, '2024-04-01', 'Pending', NULL),
--(33, 435, '2023-11-25', 'Overdue', NULL),
--(34, 540, '2023-12-12', 'Paid', NULL),
--(35, 645, '2024-03-10', 'Pending', NULL),
--(36, 750, '2023-12-20', 'Paid', NULL);


INSERT INTO StudentFees (StudentID, StageFeeItemID, DueDate, Status, Notes)
VALUES 
(1, 15, '2025-01-20', 'Pending', N'قسط دراسي لفصل الربيع'),
(2, 15, '2024-09-15', 'Paid', N'تم الدفع نقداً'),
(3, 15, '2025-02-10', 'Pending', NULL),
(4, 15, '2024-09-10', 'Overdue', N'تأخر الدفع 10 أيام'),
(5, 15, '2025-02-20', 'Pending', NULL),
(6, 15, '2024-10-05', 'Paid', NULL),
(7, 15, '2024-11-01', 'Pending', N'دفعة أولى'),
(8, 15, '2025-03-01', 'Pending', NULL),
(9, 15, '2024-10-20', 'Paid', NULL),
(10, 15, '2025-02-20', 'Pending', NULL),
(11, 15, '2024-09-20', 'Paid', NULL),
(12, 15, '2025-01-25', 'Pending', N'قسط ثاني'),
(13, 15, '2024-10-10', 'Overdue', N'تم إرسال تذكير بالدفع'),
(14, 15, '2025-03-01', 'Pending', NULL),
(15, 15, '2024-09-05', 'Paid', NULL),
(16, 15, '2024-10-01', 'Pending', NULL),
(17, 15, '2025-02-01', 'Pending', NULL),
(18, 15, '2024-11-10', 'Paid', NULL),
(19, 15, '2025-03-05', 'Pending', NULL),
(20, 15, '2024-09-15', 'Cancelled', N'انسحب من المدرسة'),
(21, 15, '2025-03-10', 'Pending', NULL),
(22, 15, '2024-09-10', 'Paid', NULL),
(23, 15, '2024-10-05', 'Pending', NULL),
(24, 15, '2025-02-28', 'Pending', NULL),
(25, 15, '2024-11-01', 'Overdue', N'تخلف عن السداد'),
(26, 15, '2024-10-15', 'Paid', NULL),
(27, 15, '2025-03-15', 'Pending', NULL),
(28, 15, '2024-09-20', 'Cancelled', N'تحويل لمدرسة أخرى'),
(29, 15, '2024-10-20', 'Paid', NULL),
(30, 15, '2025-02-01', 'Pending', N'دفعة مستحقة في فبراير'),
(31, 15, '2024-10-01', 'Paid', NULL),
(32, 15, '2025-02-20', 'Pending', NULL),
(33, 15, '2024-09-05', 'Overdue', NULL),
(34, 15, '2024-10-10', 'Paid', NULL),
(35, 15, '2025-01-20', 'Pending', NULL),
(36, 15, '2024-09-20', 'Paid', NULL);


ALTER TABLE StudentFees
DROP CONSTRAINT CK__StudentFe__DueDa__6A30C649;


select * from StudentFees

select  distinct status from StudentFees



CREATE TABLE StudentPayments (
    PaymentID INT IDENTITY(1,1) PRIMARY KEY,  -- معرف الدفع
    StudentFeeID INT NOT NULL,                -- معرف رسم الطالب (مفتاح خارجي)
    PaymentDate DATE NOT NULL DEFAULT GETDATE(), -- تاريخ الدفع، مع قيمة افتراضية هي اليوم
    AmountPaid DECIMAL(18, 2) NOT NULL CHECK (AmountPaid > 0), -- المبلغ المدفوع ويجب أن يكون موجبًا
    PaymentMethod NVARCHAR(50) NOT NULL CHECK (PaymentMethod IN ('Cash', 'Card', 'Bank Transfer', 'Check', 'Other')), -- طريقة الدفع مع تحقق من القيم المسموح بها
    Notes NVARCHAR(255) NULL,                 -- ملاحظات إضافية
    CONSTRAINT FK_Payments_StudentFees FOREIGN KEY (StudentFeeID) REFERENCES StudentFees(StudentFeeID)
);


ALTER TABLE StudentFees
ADD TotalPaid DECIMAL(18,2) NOT NULL DEFAULT 0 CHECK (TotalPaid >= 0);



select * from StudentFees

SELECT StudentFees.StudentFeeID,StudentFees.TotalPaid, StudentFees.Status, StageFeeItems.Amount
FROM   StudentFees INNER JOIN
             StageFeeItems ON StudentFees.StageFeeItemID = StageFeeItems.StageFeeItemID
			 where StudentFees.Status in ('pending')




select * from StudentPayments


INSERT INTO StudentPayments (StudentFeeID, PaymentDate, AmountPaid, PaymentMethod, Notes)
VALUES 
(3, GETDATE(), 370.00, 'Cash', N'الدفعة الأولى'),
(5, GETDATE(), 370.00, 'Card', N'الدفعة الأولى'),
(7, GETDATE(), 370.00, 'Bank Transfer', N'الدفعة الأولى'),
(9, GETDATE(), 370.00, 'Check', N'الدفعة الأولى'),
(10, GETDATE(), 370.00, 'Cash', N'الدفعة الأولى'),
(12, GETDATE(), 370.00, 'Other', N'الدفعة الأولى'),
(14, GETDATE(), 370.00, 'Cash', N'الدفعة الأولى'),
(16, GETDATE(), 370.00, 'Card', N'الدفعة الأولى'),
(18, GETDATE(), 370.00, 'Bank Transfer', N'الدفعة الأولى'),
(19, GETDATE(), 370.00, 'Check', N'الدفعة الأولى'),
(21, GETDATE(), 370.00, 'Cash', N'الدفعة الأولى'),
(23, GETDATE(), 370.00, 'Card', N'الدفعة الأولى'),
(25, GETDATE(), 370.00, 'Bank Transfer', N'الدفعة الأولى'),
(26, GETDATE(), 370.00, 'Check', N'الدفعة الأولى'),
(29, GETDATE(), 370.00, 'Cash', N'الدفعة الأولى'),
(32, GETDATE(), 370.00, 'Card', N'الدفعة الأولى'),
(34, GETDATE(), 370.00, 'Bank Transfer', N'الدفعة الأولى'),
(37, GETDATE(), 370.00, 'Other', N'الدفعة الأولى');



CREATE TABLE ExamTypes (
    ExamTypeID INT IDENTITY(1,1) PRIMARY KEY,      -- معرف فريد تلقائي للامتحان
    ExamTypeName NVARCHAR(50) NOT NULL UNIQUE,     -- اسم نوع الامتحان، لا يمكن تكراره
    Description NVARCHAR(255) NULL,                 -- وصف اختياري لنوع الامتحان
);

INSERT INTO ExamTypes (ExamTypeName, Description) VALUES
('الامتحان النهائي', 'الامتحان النهائي للفصل الدراسي'),
('امتحان منتصف الفصل', 'امتحان منتصف الفصل الدراسي'),
('اختبار قصير', 'اختبار قصير أو سريع'),
('الامتحان العملي', 'امتحان عملي أو تطبيقي'),
('الامتحان الشفهي', 'امتحان شفهي أمام المعلم'),
('امتحان تعويضي', 'امتحان للطلاب الذين تغيبوا'),
('اختبار تحديد المستوى', 'اختبار لتحديد مستوى الطالب'),
('امتحان القبول', 'امتحان دخول أو قبول للمدرسة');


CREATE TABLE ExamPeriods (
    ExamPeriodID INT IDENTITY(1,1) PRIMARY KEY,          -- معرف فريد تلقائي للفترة الامتحانية
    ExamPeriodName NVARCHAR(50) NOT NULL,                -- اسم الفترة الامتحانية (مثلاً: "الفصل الأول", "النصف الأول من السنة")
    ExamTypeID INT NOT NULL,                              -- معرف نوع الامتحان (مفتاح خارجي لجدول ExamTypes)
    AcademicYearID INT NOT NULL,                          -- معرف السنة الأكاديمية (مفتاح خارجي لجدول AcademicYears)
    StartDate DATE NOT NULL,                              -- تاريخ بداية الفترة الامتحانية
    EndDate DATE NOT NULL,                                -- تاريخ نهاية الفترة الامتحانية
    CONSTRAINT CHK_ExamPeriod_Dates CHECK (StartDate <= EndDate),  -- تحقق من صحة تواريخ الفترة
    CONSTRAINT UQ_ExamPeriod UNIQUE (ExamPeriodName, ExamTypeID, AcademicYearID), -- ضمان عدم تكرار نفس الفترة لنفس نوع الامتحان والسنة الأكاديمية
    
    CONSTRAINT FK_ExamPeriods_ExamTypes FOREIGN KEY (ExamTypeID) REFERENCES ExamTypes(ExamTypeID),
    CONSTRAINT FK_ExamPeriods_AcademicYears FOREIGN KEY (AcademicYearID) REFERENCES AcademicYears(AcademicYearID)
);




-- المرحلة 1: روضة أولى
INSERT INTO Subjects (SubjectName, StageID, IsActive, Notes) VALUES
(N'اللغة العربية', 1, 1, NULL),
(N'الرياضيات', 1, 1, NULL),
(N'التربية الإسلامية', 1, 1, NULL),
(N'مهارات حياتية', 1, 1, NULL);

-- المرحلة 2: روضة ثانية
INSERT INTO Subjects (SubjectName, StageID, IsActive, Notes) VALUES
(N'اللغة العربية', 2, 1, NULL),
(N'الرياضيات', 2, 1, NULL),
(N'التربية الإسلامية', 2, 1, NULL),
(N'مهارات حياتية', 2, 1, NULL);

-- المرحلة 3 إلى 6: ابتدائي (أول إلى رابع)
DECLARE @Stage INT = 3;
WHILE @Stage <= 6
BEGIN
    INSERT INTO Subjects (SubjectName, StageID, IsActive, Notes) VALUES
    (N'اللغة العربية', @Stage, 1, NULL),
    (N'الرياضيات', @Stage, 1, NULL),
    (N'العلوم', @Stage, 1, NULL),
    (N'التربية الإسلامية', @Stage, 1, NULL),
    (N'التربية الفنية', @Stage, 1, NULL),
    (N'التربية البدنية', @Stage, 1, NULL);
    SET @Stage = @Stage + 1;
END;

-- المرحلة 7 إلى 9: نهاية ابتدائي + متوسط
SET @Stage = 7;
WHILE @Stage <= 9
BEGIN
    INSERT INTO Subjects (SubjectName, StageID, IsActive, Notes) VALUES
    (N'اللغة العربية', @Stage, 1, NULL),
    (N'الرياضيات', @Stage, 1, NULL),
    (N'العلوم', @Stage, 1, NULL),
    (N'اللغة الإنجليزية', @Stage, 1, NULL),
    (N'الدراسات الاجتماعية', @Stage, 1, NULL),
    (N'التربية الإسلامية', @Stage, 1, NULL),
    (N'الحاسوب', @Stage, 1, NULL);
    SET @Stage = @Stage + 1;
END;

-- المرحلة 10 إلى 12: ثانوي علمي وأدبي
SET @Stage = 10;
WHILE @Stage <= 12
BEGIN
    INSERT INTO Subjects (SubjectName, StageID, IsActive, Notes) VALUES
    (N'اللغة العربية', @Stage, 1, NULL),
    (N'اللغة الإنجليزية', @Stage, 1, NULL),
    (N'الرياضيات', @Stage, 1, NULL),
    (N'الفيزياء', @Stage, 1, NULL),
    (N'الكيمياء', @Stage, 1, NULL),
    (N'الأحياء', @Stage, 1, NULL),
    (N'التاريخ', @Stage, 1, NULL),
    (N'الجغرافيا', @Stage, 1, NULL),
    (N'علم النفس', @Stage, 1, NULL);
    SET @Stage = @Stage + 1;
END;

-- المرحلة 13 و14: شرعي أو مهني
INSERT INTO Subjects (SubjectName, StageID, IsActive, Notes) VALUES
(N'الفقه', 13, 1, NULL),
(N'العقيدة', 13, 1, NULL),
(N'الحديث', 13, 1, NULL),
(N'التفسير', 13, 1, NULL),
(N'المهارات المهنية', 14, 1, NULL),
(N'الفيزياء التطبيقية', 14, 1, NULL),
(N'كهرباء عملي', 14, 1, NULL);
