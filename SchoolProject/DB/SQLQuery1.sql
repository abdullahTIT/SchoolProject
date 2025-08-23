CREATE TABLE EmployeeJobTitles (
    JobTitleID INT PRIMARY KEY IDENTITY(1,1),                     -- ������ ������ �������
    JobTitleName NVARCHAR(100) NOT NULL UNIQUE,                   -- ��� ������� ���: ���� ����� ����
    Description NVARCHAR(255) NULL,                               -- ��� ������� �������
    IsTeaching BIT NOT NULL DEFAULT 0,                            -- �� ������ ������
    IsAdministrative BIT NOT NULL DEFAULT 0,                      -- �� ������ �����
    CanTeach BIT NOT NULL DEFAULT 0,                              -- �� ���� ����� ������ ������ ���Ͽ
    RequiresCertification BIT NOT NULL DEFAULT 0,                 -- �� ������� ����� ������ �� ������ ���ɿ
);


INSERT INTO EmployeeJobTitles (JobTitleName, Description, IsTeaching, IsAdministrative, CanTeach, RequiresCertification)
VALUES 
(N'����', N'���� ������ ������ �������� �������� ������.', 1, 0, 1, 1),

(N'���� �����', N'����� ������ ������� �� ������ ��������.', 1, 0, 1, 0),

(N'���� �����', N'����� ���� �������� ����� ����� �������.', 1, 1, 1, 1),

(N'���� �����', N'���� ������� ����� ��� ������ ��������� ��������.', 0, 1, 1, 1),

(N'���� �����', N'����� ������ �� ������� ���������� ���������.', 0, 1, 1, 1),

(N'���� �����', N'���� ������� ����� ������� ���������.', 0, 1, 0, 0),

(N'���� �����', N'���� ������ ������ �������� ���������.', 0, 1, 0, 1),

(N'������', N'����� ������ �������� ���������� �� �������.', 0, 1, 0, 0),

(N'����� ����� �������', N'���� ����� ��������� ��������� �� �������.', 0, 1, 0, 1),

(N'�����', N'����� �� ������ ������� ��������.', 0, 1, 0, 1),

(N'���� �����', N'���� ������ ������� �������� �������� ��������.', 0, 0, 0, 0);


CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    PersonID INT NOT NULL,
    JobTitleID INT NOT NULL,
    HireDate DATE NOT NULL CHECK (HireDate <= GETDATE()),
    TerminationDate DATE NULL, -- ����� ����� ���
    IsActive BIT NOT NULL DEFAULT 1,
    Notes NVARCHAR(500) NULL,

    CONSTRAINT FK_Employees_Person FOREIGN KEY (PersonID) REFERENCES Persons(PersonID),
    CONSTRAINT FK_Employees_JobTitle FOREIGN KEY (JobTitleID) REFERENCES EmployeeJobTitles(JobTitleID)
);


select * from Persons


