CREATE TABLE ClassSchedule (
    ScheduleID INT PRIMARY KEY IDENTITY(1,1),
    StageID INT NOT NULL,
    SubjectID INT NOT NULL,
    TeacherID INT NOT NULL,
    RoomID INT NULL,
    WeekDay NVARCHAR(10) NOT NULL CHECK (WeekDay IN ('Saturday', 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday','Friday')),
    Period NVARCHAR(20) NOT NULL, -- „À· "«·√Ê·Ï"° "«·À«‰Ì…"
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,

    CONSTRAINT FK_ClassSchedule_Stage FOREIGN KEY (StageID) REFERENCES Stages(StageID),
    CONSTRAINT FK_ClassSchedule_Subject FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID),
    CONSTRAINT FK_ClassSchedule_Teacher FOREIGN KEY (TeacherID) REFERENCES Employees(EmployeeID),
    CONSTRAINT FK_ClassSchedule_Room FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID)
);



CREATE TABLE StudentAttendance (
    AttendanceID INT PRIMARY KEY IDENTITY(1,1),
    StudentID INT NOT NULL,
    ScheduleID INT NOT NULL,
    AttendanceDate DATE NOT NULL,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN ('Present', 'Absent', 'Late', 'Excused')),
    Notes NVARCHAR(500) NULL,

    CONSTRAINT FK_StudentAttendance_Student FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    CONSTRAINT FK_StudentAttendance_Schedule FOREIGN KEY (ScheduleID) REFERENCES ClassSchedule(ScheduleID),
    CONSTRAINT UQ_StudentAttendance UNIQUE (StudentID, ScheduleID, AttendanceDate)
);


CREATE TABLE StudentStages (
    StudentStageID INT PRIMARY KEY IDENTITY(1,1),
    StudentID INT NOT NULL,
    StageID INT NOT NULL,
    AcademicYearID INT NOT NULL,
    EnrollmentDate DATE NOT NULL DEFAULT GETDATE(),
    IsActive BIT NOT NULL DEFAULT 1,

    CONSTRAINT FK_StudentStages_Student FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    CONSTRAINT FK_StudentStages_Stage FOREIGN KEY (StageID) REFERENCES Stages(StageID),
    CONSTRAINT FK_StudentStages_AcademicYear FOREIGN KEY (AcademicYearID) REFERENCES AcademicYears(AcademicYearID),
    CONSTRAINT UQ_StudentStages UNIQUE (StudentID, AcademicYearID)
);





CREATE TABLE Schedules (
    ScheduleID INT PRIMARY KEY IDENTITY(1,1),
    ClassID INT NOT NULL,           -- «·›’·/«·’›
    SubjectID INT NOT NULL,         -- «·„«œ…
    TeacherID INT NOT NULL,         -- «·„⁄·„
    RoomID INT NOT NULL,            -- «·€—›…
    DayOfWeek NVARCHAR(10) NOT NULL CHECK (DayOfWeek IN ('Saturday','Sunday','Monday','Tuesday','Wednesday','Thursday')),
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    AcademicYearID INT NOT NULL,    -- «·”‰… «·œ—«”Ì…
    Notes NVARCHAR(300) NULL,

    CONSTRAINT FK_Schedules_Class FOREIGN KEY (ClassID) REFERENCES Classes(ClassID),
    CONSTRAINT FK_Schedules_Subject FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID),
    CONSTRAINT FK_Schedules_Teacher FOREIGN KEY (TeacherID) REFERENCES Employees(EmployeeID),
    CONSTRAINT FK_Schedules_Room FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID),
    CONSTRAINT FK_Schedules_Year FOREIGN KEY (AcademicYearID) REFERENCES AcademicYears(AcademicYearID)
);


CREATE TABLE Classes (
    ClassID INT PRIMARY KEY IDENTITY(1,1),
    ClassName NVARCHAR(50) NOT NULL,
    StageID INT NOT NULL, -- «·„—Õ·… («» œ«∆Ì° „ Ê”ÿ... ≈·Œ)
    Capacity INT NOT NULL,
    Notes NVARCHAR(300) NULL,

    CONSTRAINT FK_Classes_Stage FOREIGN KEY (StageID) REFERENCES Stages(StageID)
);





