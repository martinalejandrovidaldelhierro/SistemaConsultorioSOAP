USE SistemaConsultorioSOAP;
GO

CREATE TABLE Paciente (
    IdPaciente INT IDENTITY(1,1) PRIMARY KEY,
    Cedula NVARCHAR(20) NOT NULL,
    Nombre NVARCHAR(50) NOT NULL,
    Apellido NVARCHAR(50) NOT NULL
);
GO


CREATE TABLE Cita (
    IdCita INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATETIME NOT NULL,
    Hora DATETIME NOT NULL,
    Motivo NVARCHAR(200),
    Tratamiento NVARCHAR(100),
    Estado BIT,
    IdPaciente INT,
    IdMedico INT,
    CONSTRAINT FK_Cita_Paciente FOREIGN KEY (IdPaciente) REFERENCES Paciente(IdPaciente),
    CONSTRAINT FK_Cita_Medico FOREIGN KEY (IdMedico) REFERENCES Medico(IdMedico)
);
GO
