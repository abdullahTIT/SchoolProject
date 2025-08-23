CREATE TABLE RoomTypes (
    RoomTypeID INT PRIMARY KEY IDENTITY(1,1),
    TypeName NVARCHAR(50) NOT NULL,               -- „À·: ›’· œ—«”Ì° „Œ »—° „ﬂ »…° „ﬂ » ≈œ«—Ì
    Description NVARCHAR(250) NULL,               -- Ê’› ≈÷«›Ì ·«” Œœ«„ «·€—›… ≈‰ ÊÃœ
    IsActive BIT NOT NULL DEFAULT 1               -- Â· «·‰Ê⁄ „›⁄· Õ«·Ì«ø
)
    --MaxCapacity INT NULL CHECK (MaxCapacity >= 0),-- «·Õœ «·√ﬁ’Ï ··√‘Œ«’ «·–Ì‰ Ì„ﬂ‰ √‰ Ì‘€·Ê« «·€—›…
    --IsReservable BIT NOT NULL DEFAULT 1,          -- Â· Ì„ﬂ‰ ÕÃ“ Â–Â «·€—›…ø (‰⁄„/·«)






	INSERT INTO RoomTypes (TypeName, Description, IsActive) VALUES
(N'›’· œ—«”Ì', N'€—›… „Œ’’… ··œ—Ê” «·ÌÊ„Ì… ··ÿ·«»', 1),
(N'„Œ »— ⁄·Ê„', N'€—›… „“Ê¯œ… »√œÊ«  Ê ÃÂÌ“«  · Ã«—» «·⁄·Ê„', 1),
(N'„Œ »— Õ«”Ê»', N'€—›…  Õ ÊÌ ⁄·Ï √ÃÂ“… ﬂ„»ÌÊ — ·· ⁄·Ì„ «·≈·ﬂ —Ê‰Ì', 1),
(N'„ﬂ »…', N' ÷„ ﬂ »« Ê„—«Ã⁄ „ ‰Ê⁄… ··ÿ·«» Ê«·„⁄·„Ì‰', 1),
(N'„ﬂ » ≈œ«—Ì', N'„ﬂ » „œÌ— «·„œ—”… √Ê «·‘ƒÊ‰ «·≈œ«—Ì…', 1),
(N'ﬁ«⁄… «Ã „«⁄« ', N' ” Œœ„ ·«Ã „«⁄«  «·„⁄·„Ì‰ √Ê √Ê·Ì«¡ «·√„Ê—', 1),
(N'€—›… «·„⁄·„Ì‰', N'„ﬂ«‰ „Œ’’ ·—«Õ… Ê Õ÷Ì— «·„⁄·„Ì‰', 1),
(N'ﬁ«⁄… „ ⁄œœ… «·√€—«÷', N' ” Œœ„ ··√‰‘ÿ… «·Àﬁ«›Ì… √Ê «·‰œÊ« ', 1),
(N'€—›… „Ê”ÌﬁÏ', N'„ÃÂ“… »¬·«  „Ê”ÌﬁÌ… · ⁄·Ì„ «·„Ê”ÌﬁÏ', 1),
(N'€—›… ›‰Ì…', N'··√‰‘ÿ… «·›‰Ì… „À· «·—”„ Ê«· ’„Ì„', 1),
(N'„Œ“‰', N'· Œ“Ì‰ «·√œÊ«  Ê«·„” ·“„«  «·„œ—”Ì…', 1),
(N'⁄Ì«œ… „œ—”Ì…', N'„Œ’’… ··≈”⁄«›«  «·√Ê·Ì… Ê«·—⁄«Ì… «·’ÕÌ…', 1);



CREATE TABLE Rooms (
    RoomID INT PRIMARY KEY IDENTITY(1,1),
    RoomName NVARCHAR(100) NOT NULL,
    RoomTypeID INT NOT NULL,
    Capacity INT NOT NULL CHECK (Capacity >= 0),
    Location NVARCHAR(100) NOT NULL,
    IsAvailable BIT NOT NULL DEFAULT 1,
    IsReservable BIT NOT NULL DEFAULT 1,
    Notes NVARCHAR(500) NULL,

    CONSTRAINT FK_Rooms_RoomTypes FOREIGN KEY (RoomTypeID) REFERENCES RoomTypes(RoomTypeID)
);

INSERT INTO Rooms (RoomName, RoomTypeID, Capacity, Location, IsAvailable, IsReservable, Notes)
VALUES
('Main Lecture Hall A', 1, 100, 'Building A - Floor 1', 1, 1, 'Equipped with projector and sound system'),
('Computer Lab 1', 2, 30, 'Building B - Floor 2', 1, 1, 'Contains 30 computers with internet access'),
('Science Lab', 3, 25, 'Building C - Floor 1', 1, 0, 'Chemical lab with safety equipment'),
('Library Reading Room', 4, 60, 'Library Building', 1, 1, NULL),
('Meeting Room 1', 5, 15, 'Administration Floor', 1, 1, 'Used for staff meetings'),
('Art Room', 6, 20, 'Building D - Floor 3', 1, 0, 'Painting and sculpture materials available'),
('Music Room', 7, 18, 'Building E - Floor 1', 1, 0, 'Instruments provided'),
('Staff Room', 8, 10, 'Building A - Floor 3', 1, 0, 'Used by teachers for breaks'),
('Storage Room 1', 9, 0, 'Basement - Zone 1', 0, 0, 'Furniture storage'),
('Prayer Room (Male)', 10, 40, 'Building F - Floor 2', 1, 1, NULL),
('Prayer Room (Female)', 10, 40, 'Building F - Floor 3', 1, 1, NULL),
('Nursing Room', 11, 5, 'Clinic - Floor 1', 1, 0, 'For student medical needs'),
('Guest Waiting Room', 12, 8, 'Main Entrance', 1, 1, 'For visitor reception'),
('Computer Lab 2', 2, 35, 'Building B - Floor 3', 1, 1, 'Upgraded with dual screens'),
('Classroom 101', 1, 35, 'Building A - Floor 1', 1, 1, NULL),
('Classroom 102', 1, 35, 'Building A - Floor 1', 1, 1, NULL),
('Classroom 201', 1, 30, 'Building A - Floor 2', 1, 1, NULL),
('Classroom 202', 1, 30, 'Building A - Floor 2', 1, 1, NULL),
('Robotics Lab', 3, 20, 'Building C - Floor 2', 1, 1, 'Equipped with robot kits'),
('Training Hall', 5, 50, 'Building G - Floor 1', 1, 1, 'Used for workshops and seminars'),
('Examination Room', 1, 60, 'Building A - Floor 3', 1, 1, 'For final exams and assessments'),
('Storage Room 2', 9, 0, 'Basement - Zone 2', 0, 0, 'Books and maintenance tools'),
('Library Discussion Room', 4, 12, 'Library Building - Floor 2', 1, 1, NULL),
('Counseling Room', 11, 5, 'Building D - Floor 2', 1, 0, 'Psychological services'),
('Classroom 301', 1, 40, 'Building A - Floor 3', 1, 1, NULL),
('Classroom 302', 1, 40, 'Building A - Floor 3', 1, 1, NULL),
('Art Exhibition Room', 6, 30, 'Building D - Ground Floor', 1, 1, 'For student art exhibitions'),
('Server Room', 9, 0, 'Building B - Basement', 0, 0, 'Restricted access'),
('Audio Recording Room', 7, 6, 'Building E - Floor 2', 1, 0, 'Soundproofed'),
('VIP Meeting Room', 5, 12, 'Administration - Top Floor', 1, 1, 'For special visitors');





INSERT INTO Rooms (RoomName, RoomTypeID, Capacity, Location, IsAvailable, IsReservable, Notes)
VALUES
('Classroom 401', 1, 35, 'Building A - Floor 4', 1, 1, NULL),
('Classroom 402', 1, 35, 'Building A - Floor 4', 1, 1, NULL),
('Language Lab', 2, 25, 'Building B - Floor 2', 1, 1, 'For English & French classes'),
('Science Prep Room', 3, 5, 'Building C - Floor 1', 1, 0, 'Used by science teachers for preparation'),
('Quiet Study Room', 4, 10, 'Library - Floor 2', 1, 1, 'Silent study space'),
('Executive Meeting Room', 5, 20, 'Admin Building - Floor 3', 1, 1, 'Used by leadership team'),
('Art Storage', 6, 0, 'Building D - Back Area', 0, 0, 'Storage for art supplies'),
('Music Practice Booth 1', 7, 2, 'Building E - Floor 1', 1, 0, 'Soundproof booth'),
('Music Practice Booth 2', 7, 2, 'Building E - Floor 1', 1, 0, NULL),
('Teacher Lounge', 8, 12, 'Building A - Floor 3', 1, 0, 'Coffee machine available'),
('General Storage Room', 9, 0, 'Basement', 0, 0, NULL),
('Prayer Room (Shared)', 10, 50, 'Building F - Floor 1', 1, 1, NULL),
('Medical Room', 11, 6, 'Clinic - Floor 1', 1, 0, 'For first aid and checkups'),
('Reception Waiting Room', 12, 10, 'Main Gate', 1, 1, NULL),
('Classroom 403', 1, 40, 'Building A - Floor 4', 1, 1, NULL),
('Classroom 404', 1, 40, 'Building A - Floor 4', 1, 1, NULL),
('Computer Lab 3', 2, 28, 'Building B - Floor 4', 1, 1, NULL),
('Physics Lab', 3, 22, 'Building C - Floor 2', 1, 0, 'Equipped with lab apparatus'),
('Library Archives', 4, 0, 'Library Basement', 0, 0, 'Old books and materials'),
('Staff Conference Room', 5, 18, 'Admin Floor 2', 1, 1, NULL),
('Painting Room', 6, 15, 'Building D - Floor 1', 1, 1, NULL),
('Music Composition Room', 7, 10, 'Building E - Floor 2', 1, 1, NULL),
('Staff Kitchen', 8, 6, 'Building A - Floor 3', 1, 0, 'Microwave and fridge inside'),
('Science Storage Room', 9, 0, 'Building C - Storage Wing', 0, 0, NULL),
('Female Prayer Area', 10, 30, 'Building F - Floor 3', 1, 1, NULL),
('Isolation Room', 11, 2, 'Clinic - Back Section', 1, 0, 'For contagious cases'),
('VIP Waiting Lounge', 12, 8, 'Guest Wing', 1, 1, 'For special guests'),
('Classroom 501', 1, 35, 'Building A - Floor 5', 1, 1, NULL),
('Classroom 502', 1, 35, 'Building A - Floor 5', 1, 1, NULL),
('Typing Lab', 2, 20, 'Building B - Floor 1', 1, 1, 'With typing machines'),
('Chemistry Lab', 3, 24, 'Building C - Floor 2', 1, 0, 'Chemical storage cabinet included'),
('E-Learning Hub', 4, 20, 'Library - Digital Section', 1, 1, 'Used for online courses'),
('Budget Meeting Room', 5, 14, 'Finance Department', 1, 1, NULL),
('Clay Workshop', 6, 12, 'Building D - Workshop', 1, 0, 'For clay modeling'),
('Band Rehearsal Room', 7, 8, 'Building E - Floor 3', 1, 0, NULL),
('Teacher Printing Room', 8, 3, 'Admin Zone', 1, 0, 'With printer and copier'),
('Maintenance Storage', 9, 0, 'Backyard Storage Unit', 0, 0, NULL),
('Prayer Corner', 10, 10, 'Library Corner', 1, 1, NULL),
('Counselor Room 1', 11, 4, 'Building D - Floor 2', 1, 0, NULL),
('Parent Waiting Room', 12, 10, 'Main Entrance', 1, 1, NULL),
('Classroom 503', 1, 40, 'Building A - Floor 5', 1, 1, NULL),
('Robotics Workshop', 2, 25, 'Building B - Robotics Center', 1, 1, 'Advanced robots training'),
('Biology Lab', 3, 20, 'Building C - Floor 3', 1, 0, NULL),
('Library Conference Hall', 4, 80, 'Library - Floor 3', 1, 1, 'Used for reading contests'),
('Training Room A', 5, 25, 'Building G - Floor 2', 1, 1, 'Teacher development'),
('Calligraphy Room', 6, 18, 'Building D - Floor 2', 1, 1, NULL),
('Recording Studio', 7, 4, 'Media Center', 1, 0, NULL),
('Staff Rest Room', 8, 6, 'Building A - Floor 2', 1, 0, NULL),
('Tech Storage', 9, 0, 'Building B - Tech Basement', 0, 0, NULL);

select * from Bookings

CREATE TABLE Bookings (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    RoomID INT NOT NULL,
    EmployeeID INT NOT NULL,
    Purpose NVARCHAR(100) NOT NULL,
    BookingDate DATE NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    Status NVARCHAR(20) NOT NULL, -- Ì„ﬂ‰ —»ÿÂ »ÃœÊ· „—Ã⁄Ì
    Notes NVARCHAR(500) NULL,
    RecurrenceType NVARCHAR(50) NULL,
    RecurrenceEndDate DATE NULL,

    CONSTRAINT FK_Bookings_Rooms FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID),
    CONSTRAINT FK_Bookings_Employees FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID),
);

select * from Rooms

select * from Employees where JobTitleID in(3,4,5,6,8,9,11);

select * from EmployeeJobTitles





INSERT INTO Bookings (RoomID, EmployeeID, Purpose, BookingDate, StartTime, EndTime, Status, Notes, RecurrenceType, RecurrenceEndDate)
VALUES
(10, 3, 'Meeting with HR', '2025-06-20', '2025-06-20 09:00', '2025-06-20 10:00', 'Confirmed', 'Discuss recruitment plan', NULL, NULL),
(25, 4, 'Project Discussion', '2025-06-21', '2025-06-21 11:00', '2025-06-21 12:30', 'Pending', NULL, 'Weekly', '2025-08-30'),
(5, 5, 'Maintenance Check', '2025-06-22', '2025-06-22 14:00', '2025-06-22 15:00', 'Confirmed', 'Check AC units', NULL, NULL),
(12, 6, 'Training Session', '2025-06-23', '2025-06-23 13:00', '2025-06-23 16:00', 'Confirmed', NULL, 'Monthly', '2025-12-23'),
(30, 8, 'Client Meeting', '2025-06-24', '2025-06-24 10:00', '2025-06-24 11:00', 'Cancelled', 'Client postponed', NULL, NULL),
(48, 9, 'Budget Review', '2025-06-25', '2025-06-25 09:30', '2025-06-25 11:00', 'Confirmed', NULL, NULL, NULL),
(7, 11, 'Strategy Meeting', '2025-06-26', '2025-06-26 15:00', '2025-06-26 17:00', 'Pending', 'Waiting for approval', 'Weekly', '2025-09-26'),
(18, 14, 'Team Meeting', '2025-06-27', '2025-06-27 08:00', '2025-06-27 09:00', 'Confirmed', NULL, NULL, NULL),
(21, 15, 'Software Demo', '2025-06-28', '2025-06-28 14:00', '2025-06-28 15:30', 'Confirmed', NULL, NULL, NULL),
(33, 16, 'Maintenance Planning', '2025-06-29', '2025-06-29 10:00', '2025-06-29 11:30', 'Confirmed', NULL, 'Monthly', '2025-12-29'),
(40, 17, 'Annual Review', '2025-06-30', '2025-06-30 09:00', '2025-06-30 12:00', 'Confirmed', 'Includes all department heads', NULL, NULL),
(5, 19, 'Equipment Setup', '2025-07-01', '2025-07-01 13:00', '2025-07-01 15:00', 'Pending', NULL, NULL, NULL),
(22, 20, 'Sales Presentation', '2025-07-02', '2025-07-02 11:00', '2025-07-02 12:30', 'Confirmed', NULL, NULL, NULL),
(11, 22, 'Workshop', '2025-07-03', '2025-07-03 09:00', '2025-07-03 17:00', 'Confirmed', NULL, NULL, NULL),
(9, 25, 'Inventory Check', '2025-07-04', '2025-07-04 08:00', '2025-07-04 10:00', 'Confirmed', 'Monthly check', 'Monthly', '2025-12-04'),
(19, 26, 'Team Building', '2025-07-05', '2025-07-05 14:00', '2025-07-05 18:00', 'Confirmed', NULL, NULL, NULL),
(27, 27, 'Board Meeting', '2025-07-06', '2025-07-06 10:00', '2025-07-06 12:00', 'Pending', NULL, NULL, NULL),
(14, 28, 'Marketing Plan', '2025-07-07', '2025-07-07 09:30', '2025-07-07 11:00', 'Confirmed', NULL, NULL, NULL),
(37, 30, 'Audit Meeting', '2025-07-08', '2025-07-08 13:00', '2025-07-08 15:00', 'Confirmed', NULL, NULL, NULL),
(55, 3, 'Client Visit', '2025-07-09', '2025-07-09 10:00', '2025-07-09 11:30', 'Cancelled', 'Client cancelled last minute', NULL, NULL);




