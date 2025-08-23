CREATE TABLE Exams (
    ExamID INT IDENTITY(1,1) PRIMARY KEY,
    SubjectID INT NOT NULL,
    TeacherID INT NOT NULL,
    ExamDate DATE NOT NULL,
    TakeDate DATE NOT NULL,  -- ÅÒÇáÉ ÞíÏ CHECK åäÇ
    Grade SMALLINT NOT NULL CHECK (Grade BETWEEN 0 AND 100),
    MaxGrade SMALLINT NOT NULL DEFAULT 100 CHECK (MaxGrade > 0),
    ExamDocument NVARCHAR(255) NULL,
    Notes NVARCHAR(500) NULL,
    ExamPeriodID INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,

    CONSTRAINT FK_Exams_Subjects FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID),
    CONSTRAINT FK_Exams_Teachers FOREIGN KEY (TeacherID) REFERENCES Employees(EmployeeID),
    CONSTRAINT FK_Exams_ExamPeriods FOREIGN KEY (ExamPeriodID) REFERENCES ExamPeriods(ExamPeriodID)
);

select * from Employees where JobTitleID = 1;

select * from Subjects
where SubjectName in ('ÇáÑíÇÖíÇÊ','')


select * from ExamPeriods


select * from Exams


CREATE TABLE Results (
    ResultID INT PRIMARY KEY IDENTITY(1,1),

    StudentID INT NOT NULL,
    ExamID INT NOT NULL,

    Score DECIMAL(5, 2) NULL CHECK (Score >= 0 AND Score <= 100),
    IsAbsent BIT NOT NULL DEFAULT 0,

    Notes NVARCHAR(500),

    CONSTRAINT FK_Results_Students FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    CONSTRAINT FK_Results_Exams FOREIGN KEY (ExamID) REFERENCES Exams(ExamID),
    CONSTRAINT CK_AbsentScore CHECK (
        (IsAbsent = 1 AND Score IS NULL) OR
        (IsAbsent = 0 AND Score IS NOT NULL)
    ),

    CONSTRAINT UQ_Student_Exam UNIQUE (StudentID, ExamID) -- áãäÚ ÊßÑÇÑ äÊíÌÉ ØÇáÈ áäÝÓ ÇáÇãÊÍÇä
);

select * from Results