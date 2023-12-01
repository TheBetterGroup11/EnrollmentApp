--FOR GROUP11 DATABASE
DROP TABLE IF EXISTS [ScheduledCourse];
DROP TABLE IF EXISTS [Course];
DROP TABLE IF EXISTS [Department];
DROP TABLE IF EXISTS [Schedule];
DROP TABLE IF EXISTS [Student];
DROP TABLE IF EXISTS [Term];
GO


CREATE TABLE Student (
    StudentId INT PRIMARY KEY,
    [FirstName] VARCHAR(50),
	[LastName] VARCHAR(50),
	Grade VARCHAR(10)
);
GO
CREATE TABLE Term (
	TermId INT PRIMARY KEY,
	Season CHAR,
	[Year] INT
);
GO
CREATE TABLE Schedule (
	ScheduleId INT PRIMARY KEY,
	TermId	INT FOREIGN KEY REFERENCES Term(TermId),
	StudentId INT FOREIGN KEY REFERENCES Student(StudentId)
);
GO
CREATE TABLE Department (
	DepartmentId INT PRIMARY KEY,
	[Name] VARCHAR(255),
	Code VARCHAR(5)
);
GO
CREATE TABLE Course (
	CourseId INT PRIMARY KEY,
	[Name] VARCHAR(100),
	DepartmentId INT FOREIGN KEY REFERENCES Department(DepartmentId),
	CreditHours INT
);
GO
CREATE TABLE ScheduledCourse (
	ScheduledCourseId INT PRIMARY KEY,
	ScheduleId INT FOREIGN KEY REFERENCES Schedule(ScheduleId),
	CourseId INT FOREIGN KEY REFERENCES Course(CourseId),
	[Status] VARCHAR(15)
);
GO
--END--DECLARATIONS--------------------------

--START--INSERTS-----------------------------
INSERT INTO Student (StudentId, FirstName, LastName, Grade) VALUES (123, 'Patrick', 'Arnold', 'Senior');
INSERT INTO Student (StudentId, FirstName, LastName, Grade) VALUES (234, 'Garrett', 'Love', 'Senior');
GO

INSERT INTO Term (TermId, Season, Year) VALUES (0, 'F', 2020);
INSERT INTO Term (TermId, Season, Year) VALUES (1, 'S', 2021);
GO

INSERT INTO Schedule (ScheduleId, TermId, StudentId) VALUES (0, 0, 123);
INSERT INTO Schedule (ScheduleId, TermId, StudentId) VALUES (1, 1, 123);
INSERT INTO Schedule (ScheduleId, TermId, StudentId) VALUES (2, 0, 234);
INSERT INTO Schedule (ScheduleId, TermId, StudentId) VALUES (3, 1, 234);
GO

INSERT INTO Department (DepartmentId, Name, Code) VALUES (0, 'Computer Science', 'CIS');
INSERT INTO Department (DepartmentId, Name, Code) VALUES (1, 'Mathematics', 'MATH');
INSERT INTO Department (DepartmentId, Name, Code) VALUES (2, 'Chemistry', 'CHM');
INSERT INTO Department (DepartmentId, Name, Code) VALUES (3, 'English', 'ENGL');
GO

INSERT INTO Course (CourseId, Name, DepartmentId, CreditHours) VALUES (115, 'Introduction', 0, 3);
INSERT INTO Course (CourseId, Name, DepartmentId, CreditHours) VALUES (200, 'Fundamentals', 0, 4);
INSERT INTO Course (CourseId, Name, DepartmentId, CreditHours) VALUES (300, 'Data Structures', 0, 3);
INSERT INTO Course (CourseId, Name, DepartmentId, CreditHours) VALUES (301, 'Logic', 0, 3);
INSERT INTO Course (CourseId, Name, DepartmentId, CreditHours) VALUES (308, 'C Language', 0, 1);
INSERT INTO Course (CourseId, Name, DepartmentId, CreditHours) VALUES (400, 'Object-Orientated', 0, 3);
INSERT INTO Course (CourseId, Name, DepartmentId, CreditHours) VALUES (1220, 'Calc 1', 1, 4);
INSERT INTO Course (CourseId, Name, DepartmentId, CreditHours) VALUES (1221, 'Calc 2', 1, 4);
INSERT INTO Course (CourseId, Name, DepartmentId, CreditHours) VALUES (1222, 'Calc 3', 1, 4);
INSERT INTO Course (CourseId, Name, DepartmentId, CreditHours) VALUES (1510, 'Matrix', 1, 3);
INSERT INTO Course (CourseId, Name, DepartmentId, CreditHours) VALUES (1551, 'Discrete', 1, 3);
INSERT INTO Course (CourseId, Name, DepartmentId, CreditHours) VALUES (2210, 'Chem 1', 2, 4);
INSERT INTO Course (CourseId, Name, DepartmentId, CreditHours) VALUES (2230, 'Chem 2', 2, 4);
INSERT INTO Course (CourseId, Name, DepartmentId, CreditHours) VALUES (3220, 'Expo 1', 3, 4);
INSERT INTO Course (CourseId, Name, DepartmentId, CreditHours) VALUES (3221, 'Expo 2', 3, 4);
GO

INSERT INTO ScheduledCourse (ScheduledCourseId, ScheduleId, CourseId, Status) VALUES (0, 0, 115, 'Complete');
INSERT INTO ScheduledCourse (ScheduledCourseId, ScheduleId, CourseId, Status) VALUES (1, 0, 1220, 'Complete');
INSERT INTO ScheduledCourse (ScheduledCourseId, ScheduleId, CourseId, Status) VALUES (2, 0, 2210, 'Complete');
INSERT INTO ScheduledCourse (ScheduledCourseId, ScheduleId, CourseId, Status) VALUES (3, 0, 3220, 'Complete');
INSERT INTO ScheduledCourse (ScheduledCourseId, ScheduleId, CourseId, Status) VALUES (4, 1, 200, 'In Progress');
INSERT INTO ScheduledCourse (ScheduledCourseId, ScheduleId, CourseId, Status) VALUES (5, 1, 1221, 'In Progress');
INSERT INTO ScheduledCourse (ScheduledCourseId, ScheduleId, CourseId, Status) VALUES (6, 1, 2230, 'In Progress');
INSERT INTO ScheduledCourse (ScheduledCourseId, ScheduleId, CourseId, Status) VALUES (7, 1, 3221, 'In Progress');
INSERT INTO ScheduledCourse (ScheduledCourseId, ScheduleId, CourseId, Status) VALUES (8, 2, 300, 'Complete');
INSERT INTO ScheduledCourse (ScheduledCourseId, ScheduleId, CourseId, Status) VALUES (9, 2, 301, 'Complete');
INSERT INTO ScheduledCourse (ScheduledCourseId, ScheduleId, CourseId, Status) VALUES (10, 2, 1222, 'Complete');
INSERT INTO ScheduledCourse (ScheduledCourseId, ScheduleId, CourseId, Status) VALUES (11, 2, 2210, 'Complete');
INSERT INTO ScheduledCourse (ScheduledCourseId, ScheduleId, CourseId, Status) VALUES (12, 3, 308, 'In Progress');
INSERT INTO ScheduledCourse (ScheduledCourseId, ScheduleId, CourseId, Status) VALUES (13, 3, 400, 'In Progress');
INSERT INTO ScheduledCourse (ScheduledCourseId, ScheduleId, CourseId, Status) VALUES (14, 3, 1551, 'In Progress');
INSERT INTO ScheduledCourse (ScheduledCourseId, ScheduleId, CourseId, Status) VALUES (15, 3, 2230, 'In Progress');
GO
--END--INSERTS-------------------------------

--RUN----------------------------------------
SELECT *
FROM Student;

SELECT *
FROM Term;

SELECT *
FROM Schedule;

SELECT *
FROM Department;

SELECT *
FROM Course;

SELECT *
FROM ScheduledCourse;

SELECT 
    SUM(C.CreditHours) AS TotalCreditHours
FROM 
    ScheduledCourse SC
    INNER JOIN Schedule S ON SC.ScheduleId = S.ScheduleId
    INNER JOIN Course C ON SC.CourseId = C.CourseId
WHERE 
    S.StudentId = 123 AND
    SC.Status = 'Complete';

SELECT *
FROM Course C
	INNER JOIN ScheduledCourse SC ON SC.CourseId = C.CourseId
	INNER JOIN Schedule S ON S.ScheduleId = SC.ScheduleId
WHERE S.StudentId = @StudentId AND
	  SC.Status = 'In Progress';



SELECT *
FROM Course C
	INNER JOIN ScheduledCourse SC ON SC.CourseId = C.CourseId
	INNER JOIN Schedule S ON S.ScheduleId = SC.ScheduleId
WHERE S.StudentId = 123 AND
	  C.CourseId != 1220 AND
	  TermId = 0;