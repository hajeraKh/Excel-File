CREATE DATABASE HolyFamilyRed
GO
USE HolyFamilyRed
GO
CREATE TABLE Doctors (
    DoctorID INT IDENTITY PRIMARY KEY,
    DoctorName VARCHAR(255) NOT NULL,
    ContactNumber VARCHAR(15) NOT NULL
)
GO
CREATE TABLE Departments (
    DepartmentID INT IDENTITY PRIMARY KEY,
    DepartmentName VARCHAR(255) NOT NULL
)
GO
CREATE TABLE ServicePoints (
    ServicePointID INT IDENTITY PRIMARY KEY,
    ServicePoint VARCHAR(255) NOT NULL
)
GO

CREATE TABLE DoctorServicePoints (
    DoctorServicePointID INT IDENTITY PRIMARY KEY,
    DoctorID INT,
	DepartmentID INT,
    ServicePointID INT,
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID),
	FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID),
    FOREIGN KEY (ServicePointID) REFERENCES ServicePoints(ServicePointID)
)
GO

GO
INSERT INTO Doctors VALUES
('Dr. Lissa Mwenda','+260766219936'),
('Dr. Yvonne Sishuwa','+260766219937'),
('Dr. Machalo Mbale','+260766219938')
GO
INSERT INTO Departments VALUES
('Gynecology'),
('Pediatrics'),
('Radiology and Imaging')
GO
INSERT INTO ServicePoints VALUES
('Antenatal Care, Family Planning, Postnatal Care'),
('Family Planning, Postnatal Care'),
('Antenatal Care')
GO
INSERT INTO DoctorServicePoints VALUES
(1,1,1),
(2,2,2),
(3,3,3)
GO
SELECT D.DoctorName, D.ContactNumber,SP.ServicePoint, Dt.DepartmentName FROM Doctors D
JOIN DoctorServicePoints DS ON D.DoctorID = DS.DoctorID
JOIN Departments Dt ON DS.DepartmentID = Dt.DepartmentID
JOIN ServicePoints SP ON DS.ServicePointID = SP.ServicePointID