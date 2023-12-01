--FOR GROUP11 DATABASE
DROP TABLE IF EXISTS [Student];
DROP TABLE IF EXISTS [Schedule];
DROP TABLE IF EXISTS [Term];
DROP TABLE IF EXISTS [ScheduledCourse];
DROP TABLE IF EXISTS [Course];
DROP TABLE IF EXISTS [Department];

GO

CREATE TABLE Student (
    StudentId INT PRIMARY KEY,
    [FirstName] VARCHAR(50),
	[LastName] VARCHAR(50),
	Grade VARCHAR(10)
);
CREATE TABLE Schedule (
	ScheduleId INT PRIMARY KEY,
	TermId	INT FOREIGN KEY REFERENCES Term(TermId),
	StudentId INT FOREIGN KEY REFERENCES Student(StudentId)
);
CREATE TABLE Term (
	TermId INT PRIMARY KEY,
	Season CHAR,
	[Year] INT
);
CREATE TABLE ScheduledCourse (
	ScheduledCourseId INT PRIMARY KEY,
	ScheduleId INT FOREIGN KEY REFERENCES Schedule(ScheduleId),
	CourseId INT FOREIGN KEY REFERENCES Course(CourseId),
	[Status] VARCHAR(15)
);
CREATE TABLE Department (
	DepartmentId INT PRIMARY KEY,
	[Name] VARCHAR(5),
	Code VARCHAR(15)
);

INSERT INTO Student (StudentID, [FirstName], [LastName], Grade) VALUES (123, 'Patrick', 'Arnold', 'Junior');
INSERT INTO Student (StudentID, [FirstName], [LastName], Grade) VALUES (234, 'Garrett', 'Love', 'Senior');

DELETE FROM Student WHERE StudentID = 345

SELECT *
FROM Student;
--END----------------------------