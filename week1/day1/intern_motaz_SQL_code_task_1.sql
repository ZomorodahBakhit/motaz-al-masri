--this code was written in 2026/6/22
--by motaz Haitham Al Masri
-- I have added this table as the first for the conflictes in the other tables and the relations between them
-- Syllabus
CREATE TABLE Syllabus (
SyllabusId INT PRIMARY KEY, 
Description TEXT NULL);
--Users
CREATE TABLE Users(
UserId INT PRIMARY KEY ,
UserName VARCHAR(64) NOT NULL,
FirstName VARCHAR(64) NOT NULL,
LastName VARCHAR(64) NOT NULL,
EmailAddress VARCHAR(128) NOT NULL UNIQUE,
PhoneNumber VARCHAR(16) NOT NULL,
Role VARCHAR(32) NOT NULL
);
--Courses
CREATE TABLE courses (
CourseId INT PRIMARY KEY,
CourseName VARCHAR(100) NOT NULL,
TeacherId INT NULL,
StartDate DateTime NOT NULL,
EndDate DateTime NOT NULL,
SyllabusId INT NULL,
FOREIGN KEY (TeacherId) REFERENCES Users(UserId),
FOREIGN KEY (SyllabusId) REFERENCES Syllabus(SyllabusId)
);
--Courses
CREATE TABLE assignments (
AssignmentId INT PRIMARY KEY,
CourseId INT NOT NULL,
AssignmentTitle VARCHAR(128) NOT NULL,
Description TEXT NULL,
Weight float NOT NULL,
MaxGrade INT NOT NULL,
DueDate DATE NOT NULL,
FOREIGN KEY (CourseId) REFERENCES courses(CourseId)
);
--comments
CREATE TABLE Comments(
CommentId INT PRIMARY KEY,
AssignmentId INT NOT NULL,
CreatedByUserId INT not null, 
CreatedDate DATETIME NOT NULL, 
CommentContent TEXT NULL,
FOREIGN KEY (AssignmentId) REFERENCES assignments(AssignmentId),
FOREIGN KEY (CreatedByUserId) REFERENCES Users(UserId)
);
--Grades
CREATE TABLE Grades (
GradeId INT PRIMARY KEY, 
AssignmentId INT NOT NULL, 
StudentId INT NOT NULL, 
Grade INT NULL,
FOREIGN KEY (AssignmentId) REFERENCES assignments(AssignmentId),
FOREIGN KEY (StudentId) REFERENCES Users(UserId)
);
--Insert Internes
INSERT INTO Users ( UserId, UserName, FirstName, LastName, EmailAddress, PhoneNumber, Role)
VALUES (1, 'motaz.almasri', 'Motaz', 'Al Masri', 'motaz@gmail.com', '0959493837', 'Student'),
(2, 'yehya.msouty', 'Yehya', 'Msouty', 'yehya@gmail.com', '0911111111', 'Student'),
(3, 'hiba.jazba', 'Hiba', 'Jazba', 'hiba@gmail.com', '0922222222', 'Student'),
(4, 'marah.aljumaat', 'Marah', 'Aljumaat', 'marah@gmail.com', '0933333333', 'Student'),
(5, 'aya.jazba', 'Aya', 'Jazba', 'aya@gmail.com', '0944444444', 'Student'),
(6, 'nawar.altibi', 'Nawar', 'Al Tibi', 'nawar@gmail.com', '0955555555', 'Student'),
(7, 'mehyar.khuder', 'Mehyar', 'Khuder', 'mehyar@gmail.com', '0966666666', 'Student'),
(8, 'ahmad.khaled', 'Ahmad', 'Khaled', 'ahmad@gmail.com', '0977777777', 'Student'),
(9, 'masa.hammoud', 'Masa', 'Hammoud', 'masa@gmail.com', '0988888888', 'Student'),
(10, 'zuhair.alhomsi', 'Zuhair', 'Alhomsi', 'zuhair@gmail.com', '0999999999', 'Student'),
(11, 'ayman.durra', 'Ayman', 'Durra', 'ayman@gmail.com', '0912345678', 'Student'),
(12, 'moaaz.zakaria', 'Moaaz', 'Zakaria', 'moaaz@gmail.com', '0923456789', 'Student'),
(13, 'fawzy.sukkar', 'Fawzy', 'Sukkar', 'fawzy@gmail.com', '0934567892', 'Student');
--insert teachers
INSERT INTO Users(UserId, UserName, FirstName, LastName, EmailAddress, PhoneNumber, Role)
VALUES 
(14, 'sami.teacher', 'Sami', 'Teacher', 'sami@example.com', '555-0201', 'Teacher'),
(15, 'feryal.teacher', 'Feryal', 'Teacher', 'feryal@example.com', '555-0202', 'Teacher');

--insert syllabus
INSERT INTO Syllabus (SyllabusId, Description)
VALUES 
(1, 'SQL Basics'),
(2, 'OOP'),
(3, 'Code, Migrations, and DbContext.'),
(4, 'RESTful APIs.'),
(5, 'React.js Basics');
--insert courses
INSERT INTO Courses (CourseId, CourseName, TeacherId, StartDate, EndDate, SyllabusId)
VALUES 
(1, 'SQL', 14, '2026-01-10', '2026-03-10', 1),
(2, 'C#', 14, '2026-03-15', '2026-05-15', 2),
(3, 'Entity Framework', 15, '2025-05-20', '2025-06-20', 3),
(4, 'Web API', 15, '2026-06-25', '2026-08-25', 4),
(5, 'React', 14, '2026-09-01', '2026-11-01', 5);
--inser Assignments, 5 for every course
INSERT INTO Assignments (AssignmentId, CourseId, AssignmentTitle, Description, Weight, MaxGrade, DueDate)
VALUES
--sql
(1,1,'SQL basics_1', 'sql basics_1 exam', 10.0, 100,'2026-3-15'),
(2,1, 'SQL basics_2', 'sql basics_2 exam', 20.0, 100,'2026-3-18'),
(3,1, 'joins', 'Inner joins', 20.0, 100, '2026-3-22'),
(4,1,'Grouping', 'Group by and Having', 20.0, 100, '2026-3-25'),
(5,1,'final', 'final exam in database',30.0,100,'2026-3-28'),
--C#
(6, 2, 'C# Basics', 'variables', 10, 100, '2026-3-18'),
(7,2, 'loops', 'exam in loops',10, 100, '2026-3-21'),
(8,2,'functions', 'call by value', 30,100,'2026-3-25'),
(9,2,'OOP Concepts', 'Classes and Inheritance', 20, 100, '2026-03-29'),
(10, 2, 'Interfaces', 'Polymorphism', 20, 100, '2026-04-1'),
--entity
(11, 3, 'Setup DbContext', 'Initial setup', 20, 100, '2026-05-25'),
(12, 3, 'Migrations', 'Adding tables', 20, 100, '2026-06-01'),
(13, 3, 'Relationships', '1-to-Many, Many-to-Many', 20, 100, '2026-06-08'),
(14, 3, 'Performance', 'Eager vs Lazy loading', 20, 100, '2026-06-15'),
(15, 3, 'Repository Pattern', 'Implementation', 20, 100, '2026-06-18'),
-- Web API
(16, 4, 'First Endpoint', 'Hello World API', 10, 100, '2026-07-05'),
(17, 4, 'CRUD Operations', 'GET, POST, PUT, DELETE', 20, 100, '2026-07-15'),
(18, 4, 'Authentication', 'JWT Tokens', 20, 100, '2026-08-01'),
(19, 4, 'Middleware', 'Custom logging', 20, 100, '2026-08-10'),
(20, 4, 'Final API Project', 'Complete API', 30, 100, '2026-08-20'),
-- React
(21, 5, 'JSX Basics', 'Rendering elements', 10, 100, '2026-09-10'),
(22, 5, 'Components', 'Props and State', 20, 100, '2026-09-20'),
(23, 5, 'Hooks', 'useEffect and useState', 20, 100, '2026-10-05'),
(24, 5, 'Routing', 'React Router DOM', 20, 100, '2026-10-15'),
(25, 5, 'Final App', 'Full SPA', 30, 100, '2026-10-25');

--10 Comments
INSERT INTO Comments (CommentId, AssignmentId, CreatedByUserId, CreatedDate, CommentContent)
VALUES 
(1, 1, 1, GETDATE(), 'Is this due on Friday?'),
(2, 5, 2, GETDATE(), 'I need help with the diagram.'),
(3, 10, 3, GETDATE(), 'Great assignment!'),
(4, 15, 14, GETDATE(), 'Make sure to check your connection string.'),
(5, 20, 5, GETDATE(), 'Postman is giving me a 401 error.'),
(6, 25, 6, GETDATE(), 'My components are not rendering.'),
(7, 2, 15, GETDATE(), 'Remember the difference between LEFT and INNER join.'),
(8, 7, 8, GETDATE(), 'Does C# OOP'),
(9, 12, 9, GETDATE(), 'migration failed.'),
(10, 22, 10, GETDATE(), 'Props drilling is confusing.');
--grades
INSERT INTO Grades (AssignmentId, StudentId, Grade)
SELECT 
    a.AssignmentId, 
    u.UserId, 
    ABS(CHECKSUM(NEWID()) % 41) + 60 -- Random grade between 60 and 100
FROM Assignments a
CROSS JOIN Users u
WHERE u.Role = 'Student';


--select Queries
SELECT * 
FROM courses; 

SELECT * 
FROM assignments
WHERE CourseId = 2;

SELECT *
FROM Users
WHERE Role = 'Student';

UPDATE Users
SET Role = 'Teacher'
WHERE UserId = 1;

DELETE FROM Comments WHERE CommentId = 9;
--new query
SELECT 
    c.CourseName, 
    s.Description AS SyllabusDescription
FROM courses c
LEFT JOIN Syllabus s ON c.SyllabusId = s.SyllabusId;
SELECT 
Users.FirstName, 
Users.LastName,
assignments.AssignmentTitle,
Grades.Grade
FROM Users
JOIN Grades ON Users.UserId = Grades.StudentId
JOIN assignments ON Grades.AssignmentId = assignments.AssignmentId
WHERE assignments.CourseId = 2 AND Users.Role = 'Student';

SELECT 
courses.CourseName,
AVG(Grades.Grade) AS AveregeGrade
FROM courses
JOIN assignments ON courses.CourseId = assignments.CourseId
JOIN Grades ON assignments.AssignmentId = Grades.AssignmentId
GROUP BY courses.CourseName;

--Here I have Remembered another way to write the queries, I have learned from SQL Intermediate
SELECT 
    c.CommentContent, 
    u.FirstName, 
    a.AssignmentTitle
FROM Comments c
JOIN Assignments a ON c.AssignmentId = a.AssignmentId
JOIN Users u ON c.CreatedByUserId = u.UserId
WHERE a.CourseId = 4;
GO
--1
CREATE PROCEDURE sp_AddStudent
    @UserId INT,
    @UserName VARCHAR(64),
    @FirstName VARCHAR(64),
    @LastName VARCHAR(64),
    @EmailAddress VARCHAR(128),
    @PhoneNumber VARCHAR(16)
AS
BEGIN
    INSERT INTO Users (UserId, UserName, FirstName, LastName, EmailAddress, PhoneNumber, Role)
    VALUES (@UserId, @UserName, @FirstName, @LastName, @EmailAddress, @PhoneNumber, 'Student');
END;
GO
--2
CREATE PROCEDURE sp_AddAssignment
    @AssignmentId INT,
    @CourseId INT,
    @AssignmentTitle VARCHAR(128),
    @Description TEXT,
    @Weight FLOAT,
    @MaxGrade INT,
    @DueDate DATE
AS
BEGIN
    DECLARE @CurrentTotalWeight FLOAT;
    
    SELECT @CurrentTotalWeight = ISNULL(SUM(Weight), 0)
    FROM Assignments 
    WHERE CourseId = @CourseId;

    IF (@CurrentTotalWeight + @Weight > 100)
    BEGIN
        RAISERROR('Cannot add assignment. Total weight for this course would exceed 100.', 16, 1);
        RETURN;
    END

    INSERT INTO Assignments (AssignmentId, CourseId, AssignmentTitle, Description, Weight, MaxGrade, DueDate)
    VALUES (@AssignmentId, @CourseId, @AssignmentTitle, @Description, @Weight, @MaxGrade, @DueDate);
END;
GO
--3
CREATE FUNCTION fn_CalculateStudentGradeInCourse
(
    @StudentId INT,
    @CourseId INT
)
RETURNS VARCHAR(2)
AS
BEGIN
    DECLARE @FinalScore FLOAT;
    DECLARE @LetterGrades VARCHAR(2);

    SELECT @FinalScore = ISNULL(SUM((CAST(g.Grade AS FLOAT) / a.MaxGrade) * a.Weight), 0)
    FROM Grades g
    JOIN Assignments a ON g.AssignmentId = a.AssignmentId
    WHERE g.StudentId = @StudentId AND a.CourseId = @CourseId;

    IF @FinalScore >= 90 SET @LetterGrades = 'A';
    ELSE IF @FinalScore >= 80 SET @LetterGrades = 'B';
    ELSE IF @FinalScore >= 70 SET @LetterGrades = 'C';
    ELSE IF @FinalScore >= 60 SET @LetterGrades = 'D';
    ELSE SET @LetterGrades = 'F';

    RETURN @LetterGrades;
END;
GO

CREATE FUNCTION fn_CalculateStudentGPA
(
    @StudentId INT
)
RETURNS FLOAT
AS
BEGIN
    DECLARE @TotalPoints FLOAT = 0;
    DECLARE @CourseCount INT = 0;
    DECLARE @GPA FLOAT;

 SELECT 
        @TotalPoints = SUM(
            CASE dbo.fn_CalculateStudentGradeInCourse(@StudentId, c.CourseId)
                WHEN 'A' THEN 4.0
                WHEN 'B' THEN 3.0
                WHEN 'C' THEN 2.0
                WHEN 'D' THEN 1.0
                ELSE 0.0
            END
        ),
        @CourseCount = COUNT(DISTINCT c.CourseId)
    FROM Courses c
    JOIN Assignments a ON c.CourseId = a.CourseId
    JOIN Grades g ON a.AssignmentId = g.AssignmentId
    WHERE g.StudentId = @StudentId;

    IF @CourseCount = 0 
        SET @GPA = 0;
    ELSE 
        SET @GPA = @TotalPoints / @CourseCount;

    RETURN ROUND(@GPA, 2);
END;
GO
--Ê «ŒÌ—« Œ·’  
--⁄”Ï «‰ Ì‰«· «⁄Ã«»ﬂ„
--Motaz wrote this code