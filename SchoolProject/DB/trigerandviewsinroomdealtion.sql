CREATE TRIGGER TRG_PreventRoomBookingOverlap
ON Bookings
INSTEAD OF INSERT
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM Inserted i
        JOIN Bookings b ON i.RoomID = b.RoomID
        WHERE
            i.BookingDate = b.BookingDate
            AND (
                (i.StartTime BETWEEN b.StartTime AND b.EndTime) OR
                (i.EndTime BETWEEN b.StartTime AND b.EndTime) OR
                (b.StartTime BETWEEN i.StartTime AND i.EndTime)
            )
            AND b.Status NOT IN ('Cancelled')
    )
    BEGIN
        RAISERROR('Room is already booked during the selected time.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    INSERT INTO Bookings(RoomID, EmployeeID, Purpose, BookingDate, StartTime, EndTime, Status, Notes, RecurrenceType, RecurrenceEndDate)
    SELECT RoomID, EmployeeID, Purpose, BookingDate, StartTime, EndTime, Status, Notes, RecurrenceType, RecurrenceEndDate
    FROM Inserted;
END



CREATE VIEW vw_WeeklyRoomSchedule AS
SELECT 
    R.RoomName,
    C.ClassName,
    S.SubjectName,
    E.EmployeeID,
    SC.DayOfWeek,
    SC.StartTime,
    SC.EndTime,
    AY.AcademicYearID
FROM Schedules SC
JOIN Rooms R ON SC.RoomID = R.RoomID
JOIN Classes C ON SC.ClassID = C.ClassID
JOIN Subjects S ON SC.SubjectID = S.SubjectID
JOIN Employees E ON SC.TeacherID = E.EmployeeID
JOIN AcademicYears AY ON SC.AcademicYearID = AY.AcademicYearID










CREATE TRIGGER TRG_ValidateClassRoomCapacity
ON Schedules
INSTEAD OF INSERT
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM Inserted i
        JOIN Rooms r ON i.RoomID = r.RoomID
        JOIN Classes c ON i.ClassID = c.ClassID
        WHERE c.Capacity > r.Capacity
    )
    BEGIN
        RAISERROR('Cannot assign a class to a room with insufficient capacity.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- ÅÐÇ ÊÍÞÞ ÇáÔÑØ¡ Þã ÈÅÏÎÇá ÇáÓÌáÇÊ ÈÔßá ØÈíÚí
    INSERT INTO Schedules (ClassID, SubjectID, TeacherID, RoomID, DayOfWeek, StartTime, EndTime, AcademicYearID, Notes)
    SELECT ClassID, SubjectID, TeacherID, RoomID, DayOfWeek, StartTime, EndTime, AcademicYearID, Notes
    FROM Inserted;
END
 go




CREATE TRIGGER TRG_ValidateClassRoomCapacity_Update
ON Schedules
AFTER UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM Inserted i
        JOIN Rooms r ON i.RoomID = r.RoomID
        JOIN Classes c ON i.ClassID = c.ClassID
        WHERE c.Capacity > r.Capacity
    )
    BEGIN
        RAISERROR('Cannot assign a class to a room with insufficient capacity (during update).', 16, 1);
        ROLLBACK TRANSACTION;
    END
END












CREATE VIEW vw_DailyRoomSchedule AS
SELECT 
    R.RoomName,
    C.ClassName,
    S.SubjectName,
    SC.DayOfWeek,
    FORMAT(SC.StartTime, 'hh\\:mm') AS StartTime,
    FORMAT(SC.EndTime, 'hh\\:mm') AS EndTime,
    AY.AcademicYearID
FROM Schedules SC
JOIN Rooms R ON SC.RoomID = R.RoomID
JOIN Classes C ON SC.ClassID = C.ClassID
JOIN Subjects S ON SC.SubjectID = S.SubjectID
JOIN AcademicYears AY ON SC.AcademicYearID = AY.AcademicYearID;





CREATE TABLE DaysOfWeek (
    DayName NVARCHAR(10) PRIMARY KEY,     -- Sunday, Monday, etc
    IsWorkingDay BIT NOT NULL DEFAULT 1   -- 1 = íæã ÏæÇã¡ 0 = ÚØáÉ
);


IF NOT EXISTS (SELECT 1 FROM DaysOfWeek)
BEGIN
    INSERT INTO DaysOfWeek (DayName, IsWorkingDay)
    VALUES
        ('Saturday', 0),
        ('Sunday', 1),
        ('Monday', 1),
        ('Tuesday', 1),
        ('Wednesday', 1),
        ('Thursday', 1),
        ('Friday', 0);
END



CREATE TRIGGER TRG_PreventNonWorkingDaySchedule
ON Schedules
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM Inserted i
        LEFT JOIN DaysOfWeek d ON i.DayOfWeek = d.DayName
        WHERE d.IsWorkingDay = 0 OR d.DayName IS NULL
    )
    BEGIN
        RAISERROR('Cannot schedule class on a non-working day.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END





CREATE VIEW vw_WeeklyClassSchedule AS
SELECT 
    C.ClassName,
    S.SubjectName,
    T.EmployeeID AS TeacherID,
    R.RoomName,
    SC.DayOfWeek,
    FORMAT(SC.StartTime, 'hh\\:mm') AS StartTime,
    FORMAT(SC.EndTime, 'hh\\:mm') AS EndTime,
    AY.AcademicYearID
FROM Schedules SC
JOIN Classes C ON SC.ClassID = C.ClassID
JOIN Subjects S ON SC.SubjectID = S.SubjectID
JOIN Rooms R ON SC.RoomID = R.RoomID
JOIN Employees T ON SC.TeacherID = T.EmployeeID
JOIN AcademicYears AY ON SC.AcademicYearID = AY.AcademicYearID



CREATE VIEW vw_AvailableRoomsNow AS
SELECT *
FROM Rooms
WHERE IsAvailable = 1 AND IsReservable = 1 AND RoomID NOT IN (
    SELECT RoomID
    FROM Schedules
    WHERE
        DATENAME(WEEKDAY, GETDATE()) = DayOfWeek AND
        CAST(GETDATE() AS TIME) BETWEEN StartTime AND EndTime
);




CREATE PROCEDURE ps_GetAvailableRoomsAtTime
    @CheckTime TIME,
    @CheckDay NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Rooms
    WHERE IsAvailable = 1 
      AND IsReservable = 1
      AND RoomID NOT IN (
          SELECT RoomID
          FROM Schedules
          WHERE DayOfWeek = @CheckDay
            AND @CheckTime BETWEEN StartTime AND EndTime
      );
END;








CREATE TRIGGER TRG_SetConflictStatus
ON Bookings
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE b
    SET Status = 'Conflict'
    FROM Bookings b
    JOIN Inserted i ON b.BookingID = i.BookingID
    WHERE EXISTS (
        SELECT 1
        FROM Bookings other
        WHERE other.BookingID <> b.BookingID
        AND other.RoomID = b.RoomID
        AND other.BookingDate = b.BookingDate
        AND (
            b.StartTime BETWEEN other.StartTime AND other.EndTime OR
            b.EndTime BETWEEN other.StartTime AND other.EndTime OR
            other.StartTime BETWEEN b.StartTime AND b.EndTime
        )
        AND other.Status <> 'Cancelled'
    );
END







CREATE VIEW vw_TeacherSchedules AS
SELECT 
    T.EmployeeID,
    CONCAT(P.FirstName, ' ', P.LastName) AS FullName,
    C.ClassName,
    S.SubjectName,
    R.RoomName,
    SC.DayOfWeek,
    SC.StartTime,
    SC.EndTime,
    AY.AcademicYearID
FROM Schedules SC
JOIN Employees T ON SC.TeacherID = T.EmployeeID
JOIN Persons P ON T.PersonID = P.PersonID
JOIN Classes C ON SC.ClassID = C.ClassID
JOIN Subjects S ON SC.SubjectID = S.SubjectID
JOIN Rooms R ON SC.RoomID = R.RoomID
JOIN AcademicYears AY ON SC.AcademicYearID = AY.AcademicYearID;




























