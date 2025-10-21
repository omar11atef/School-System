
-- Alter the table and add new columns
ALTER TABLE Courses
ADD 
    [description] VARCHAR(50) NOT NULL,
    Subject_name VARCHAR(50) NOT NULL,
    Active INT NOT NULL;

--  Insert sample data (without specifying courses_id because it's an IDENTITY column)

INSERT INTO Courses (Name, [description], Subject_name, Active)
VALUES 
-- Intro to AI variations
('Intro to AI', 'Basics of AI and ML', 'Artificial Intelligence', 1),
('Intro to AI', 'Basics of AI and ML', 'Machine Learning', 1),
('Intro to AI', 'Basics of AI and ML', 'Deep Learning', 1),
('Intro to AI', 'Basics of AI and ML', 'Data Mining', 1),

-- Database Design variations
('Database Design', 'ERD & Normalization', 'Database Systems', 1),
('Database Design', 'ERD & Normalization', 'Relational Databases', 1),
('Database Design', 'ERD & Normalization', 'SQL Programming', 1),
('Database Design', 'ERD & Normalization', 'NoSQL Databases', 1),

-- Web Development variations
('Web Development', 'Frontend + Backend', 'Software Engineering', 1),
('Web Development', 'Frontend + Backend', 'Frontend Development', 1),
('Web Development', 'Frontend + Backend', 'Backend Development', 1),
('Web Development', 'Frontend + Backend', 'Full Stack Development', 1),

-- Data Structures variations
('Data Structures', 'Arrays, Trees, Graphs', 'Computer Science', 1),
('Data Structures', 'Arrays, Trees, Graphs', 'Algorithms', 1),
('Data Structures', 'Arrays, Trees, Graphs', 'Data Organization', 1),
('Data Structures', 'Arrays, Trees, Graphs', 'Problem Solving', 1);

ALTER TABLE Courses
ADD teacher_id INT NULL;

INSERT INTO Teachers (First_name, Last_name, Age, phone, deparemnt, Email, City, NationalID, gender_id)
VALUES
-- Teachers for "Intro to AI"
('Alice', 'Johnson', 30, '0000000000', 'AI Dept', 'alice@example.com', 'CityA', '123456789', 1),
('Bob', 'Smith', 32, '0000000001', 'AI Dept', 'bob@example.com', 'CityA', '123456780', 1),
('Carol', 'Davis', 28, '0000000002', 'AI Dept', 'carol@example.com', 'CityA', '123456781', 2),
('David', 'Lee', 35, '0000000003', 'AI Dept', 'david@example.com', 'CityA', '123456782', 1),

-- Teachers for "Database Design"
('Emma', 'Wilson', 29, '0000000004', 'DB Dept', 'emma@example.com', 'CityB', '123456783', 2),
('Frank', 'Taylor', 31, '0000000005', 'DB Dept', 'frank@example.com', 'CityB', '123456784', 1),
('Grace', 'Anderson', 27, '0000000006', 'DB Dept', 'grace@example.com', 'CityB', '123456785', 2),
('Henry', 'Thomas', 34, '0000000007', 'DB Dept', 'henry@example.com', 'CityB', '123456786', 1),

-- Teachers for "Web Development"
('Ivy', 'Moore', 30, '0000000008', 'Web Dept', 'ivy@example.com', 'CityC', '123456787', 2),
('Jack', 'Martin', 33, '0000000009', 'Web Dept', 'jack@example.com', 'CityC', '123456788', 1),
('Karen', 'White', 28, '0000000010', 'Web Dept', 'karen@example.com', 'CityC', '1234567890', 2),
('Leo', 'Harris', 36, '0000000011', 'Web Dept', 'leo@example.com', 'CityC', '1234567891', 1),

-- Teachers for "Data Structures"
('Mia', 'Clark', 29, '0000000012', 'CS Dept', 'mia@example.com', 'CityD', '1234567892', 2),
('Nathan', 'Lewis', 32, '0000000013', 'CS Dept', 'nathan@example.com', 'CityD', '1234567893', 1),
('Olivia', 'Walker', 27, '0000000014', 'CS Dept', 'olivia@example.com', 'CityD', '1234567894', 2),
('Peter', 'Hall', 35, '0000000015', 'CS Dept', 'peter@example.com', 'CityD', '1234567895', 1);


-- Example: Assign first teacher of each course to a course_id
UPDATE Courses
SET teacher_id = t.teacher_id
FROM Courses c
JOIN Teachers t ON t.First_name = 'Alice' AND c.Name = 'Intro to AI';


select First_name , Last_name, Name,[description],Subject_name,Active
from Courses o join Teachers s
on o.courses_id =s.teacher_id ;

CREATE TABLE CourseTeachers (
    course_id INT NOT NULL,
    teacher_id INT NOT NULL,
    PRIMARY KEY(course_id, teacher_id),
    FOREIGN KEY(course_id) REFERENCES Courses(courses_id),
    FOREIGN KEY(teacher_id) REFERENCES Teachers(teacher_id)
);

-- =====================================
-- Step 6: Assign teachers to courses in junction table
-- Example: First 4 teachers to "Intro to AI", next 4 to "Database Design", etc.
-- =====================================
-- Intro to AI courses (courses_id 1-4) assigned to teachers 1-4
INSERT INTO CourseTeachers (course_id, teacher_id)
VALUES
(1,1),(2,2),(3,3),(4,4),

-- Database Design courses (courses_id 5-8) assigned to teachers 5-8
(5,5),(6,6),(7,7),(8,8),

-- Web Development courses (courses_id 9-12) assigned to teachers 9-12
(9,9),(10,10),(11,11),(12,12),

-- Data Structures courses (courses_id 13-16) assigned to teachers 13-16
(13,13),(14,14),(15,15),(16,16);

-- =====================================
-- Step 7: Create View to combine Teacher + Course info
-- =====================================
SELECT 
    t.First_name,
    t.Last_name,
    c.Name AS Course_Name,
    c.[description],
    c.Subject_name,
    c.Active
INTO CoursesWithTeachersTable2
FROM Courses c
JOIN CourseTeachers ct
    ON c.courses_id = ct.course_id
JOIN Teachers t
    ON ct.teacher_id = t.teacher_id;
-- =====================================
-- Step 8: Query the view
-- =====================================

INSERT INTO CoursesWithTeachersTable2 (First_name, Last_name, Course_Name, [description], Subject_name, Active)
VALUES
('Alice', 'Johnson', 'Database Design', 'ERD & Normalization', 'Database Systems', 1),
('Bob', 'Smith', 'Database Design', 'ERD & Normalization', 'Relational Databases', 1),
('Carol', 'Davis', 'Database Design', 'ERD & Normalization', 'SQL Programming', 1),
('David', 'Lee', 'Database Design', 'ERD & Normalization', 'NoSQL Databases', 1),
('Emma', 'Wilson', 'Web Development', 'Frontend + Backend', 'Software Engineering', 1),
('Frank', 'Taylor', 'Web Development', 'Frontend + Backend', 'Frontend Development', 1),
('Grace', 'Anderson', 'Web Development', 'Frontend + Backend', 'Backend Development', 1),
('Henry', 'Thomas', 'Web Development', 'Frontend + Backend', 'Full Stack Development', 1),
('Ivy', 'Moore', 'Data Structures', 'Arrays, Trees, Graphs', 'Computer Science', 1),
('Jack', 'Martin', 'Data Structures', 'Arrays, Trees, Graphs', 'Algorithms', 1),
('Karen', 'White', 'Data Structures', 'Arrays, Trees, Graphs', 'Data Organization', 1),
('Leo', 'Harris', 'Data Structures', 'Arrays, Trees, Graphs', 'Problem Solving', 1);

SELECT * FROM CoursesWithTeachersTable2;

ALTER TABLE CoursesWithTeachersTable2
ADD 
    Credit_Houers DECIMAL(3,1) NOT NULL DEFAULT 0.0,
    Number_Register INT NOT NULL DEFAULT 0,
    Year_name VARCHAR(9) NOT NULL DEFAULT '2025/2026',
    Degree INT NOT NULL DEFAULT 0;



    
SELECT * FROM CoursesWithTeachersTable2;

INSERT INTO CoursesWithTeachersTable2 
(First_name, Last_name, Course_Name, [description], Subject_name, Active, Credit_Houers, Number_Register, Year_name, Degree)
VALUES
('Alice', 'Johnson', 'Database Design', 'ERD & Normalization', 'Database Systems', 1, 3.0, 45, '2024/2025', 90),
('Bob', 'Smith', 'Database Design', 'ERD & Normalization', 'Relational Databases', 1, 4.0, 52, '2023/2024', 85),
('Carol', 'Davis', 'Database Design', 'ERD & Normalization', 'SQL Programming', 1, 2.0, 38, '2025/2026', 88),
('David', 'Lee', 'Database Design', 'ERD & Normalization', 'NoSQL Databases', 1, 3.5, 40, '2022/2023', 92),
('Emma', 'Wilson', 'Web Development', 'Frontend + Backend', 'Software Engineering', 1, 1.0, 27, '2024/2025', 75),
('Frank', 'Taylor', 'Web Development', 'Frontend + Backend', 'Frontend Development', 1, 2.5, 30, '2025/2026', 80),
('Grace', 'Anderson', 'Web Development', 'Frontend + Backend', 'Backend Development', 1, 3.0, 60, '2023/2024', 95),
('Henry', 'Thomas', 'Web Development', 'Frontend + Backend', 'Full Stack Development', 1, 4.0, 20, '2022/2023', 70),
('Ivy', 'Moore', 'Data Structures', 'Arrays, Trees, Graphs', 'Computer Science', 1, 1.5, 33, '2025/2026', 65),
('Jack', 'Martin', 'Data Structures', 'Arrays, Trees, Graphs', 'Algorithms', 1, 3.5, 48, '2024/2025', 89),
('Karen', 'White', 'Data Structures', 'Arrays, Trees, Graphs', 'Data Organization', 1, 2.0, 56, '2023/2024', 91),
('Leo', 'Harris', 'Data Structures', 'Arrays, Trees, Graphs', 'Problem Solving', 1, 4.0, 25, '2022/2023', 78);


DELETE FROM CoursesWithTeachersTable2
WHERE Credit_Houers = 0.0 
  AND Number_Register = 0
  AND Degree = 0;







