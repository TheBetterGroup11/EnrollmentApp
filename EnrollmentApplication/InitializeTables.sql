--FOR GROUP11 DATABASE
DROP TABLE IF EXISTS [Student];
GO

CREATE TABLE Student (
    StudentID INT PRIMARY KEY,
    [FirstName] VARCHAR(255),
	[LastName] VARCHAR(255),
	Grade VARCHAR(255)
);

INSERT INTO Student (StudentID, [FirstName], [LastName], Grade) VALUES (123, 'Patrick', 'Arnold', 'Junior');
INSERT INTO Student (StudentID, [FirstName], [LastName], Grade) VALUES (234, 'Garrett', 'Love', 'Senior');

DELETE FROM Student WHERE StudentID = 345

SELECT *
FROM Student;
--END----------------------------