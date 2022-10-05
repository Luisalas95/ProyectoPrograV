USE [tiusr5pl_Proyecto1PrograV]
--AdminProyecto1P5
--*k1W8q2t7

GO
/****** Object:  User [AdminProyecto1P5]    Script Date: 10/2/2022 1:42:28 PM ******/
CREATE USER [AdminProyecto1P5] FOR LOGIN [AdminProyecto1P5] WITH DEFAULT_SCHEMA=[AdminProyecto1P5]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [AdminProyecto1P5]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [AdminProyecto1P5]
GO
ALTER ROLE [db_datareader] ADD MEMBER [AdminProyecto1P5]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [AdminProyecto1P5]
GO
/****** Object:  Schema [AdminProyecto1P5]    Script Date: 10/2/2022 1:42:28 PM ******/
CREATE SCHEMA [AdminProyecto1P5]
GO
/****** Object:  Schema [Invitado]    Script Date: 10/2/2022 1:42:28 PM ******/
CREATE SCHEMA [Invitado]
GO
/****** Object:  Schema [Josspg]    Script Date: 10/2/2022 1:42:28 PM ******/
CREATE SCHEMA [Josspg]
GO
/****** Object:  Schema [Planilla]    Script Date: 10/2/2022 1:42:28 PM ******/
CREATE SCHEMA [Planilla]
GO
/****** Object:  Table [dbo].[Asistencia]    Script Date: 10/2/2022 1:42:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Asistencia](
	[Codigo_Grupo] [tinyint] NOT NULL,
	[Codigo_Curso] [varchar](10) NOT NULL,
	[Fecha_Asistencia] [date] NOT NULL,
	[Tipo_Registro] [varchar](15) NOT NULL,
	[Tipo_ID_Esutiante] [varchar](25) NOT NULL,
	[Identificacion_Estudiante] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Asistencia] PRIMARY KEY CLUSTERED 
(
	[Codigo_Grupo] ASC,
	[Codigo_Curso] ASC,
	[Fecha_Asistencia] ASC,
	[Tipo_ID_Esutiante] ASC,
	[Identificacion_Estudiante] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carreras]    Script Date: 10/2/2022 1:42:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carreras](
	[Codigo_Carrera] [varchar](10) NOT NULL,
	[Nombre_Carrera] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Carreras_1] PRIMARY KEY CLUSTERED 
(
	[Codigo_Carrera] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Correos_Estudiantes]    Script Date: 10/2/2022 1:42:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Correos_Estudiantes](
	[Corre_Electronico] [varchar](40) NOT NULL,
	[Tipo_ID_Estudiante] [varchar](25) NOT NULL,
	[Identificacion_Estudiante] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Correos_Estudiantes] PRIMARY KEY CLUSTERED 
(
	[Corre_Electronico] ASC,
	[Tipo_ID_Estudiante] ASC,
	[Identificacion_Estudiante] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Correos_Profesores]    Script Date: 10/2/2022 1:42:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Correos_Profesores](
	[Corre_Electronico] [varchar](40) NOT NULL,
	[Tipo_ID_Profesor] [varchar](25) NOT NULL,
	[Identificacion_Profesor] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Correos_Profesores] PRIMARY KEY CLUSTERED 
(
	[Corre_Electronico] ASC,
	[Tipo_ID_Profesor] ASC,
	[Identificacion_Profesor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cursos]    Script Date: 10/2/2022 1:42:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cursos](
	[Codigo_Curso] [varchar](10) NOT NULL,
	[Nombre_Curso] [varchar](30) NOT NULL,
	[Codigo_Carrera] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Cursos] PRIMARY KEY CLUSTERED 
(
	[Codigo_Curso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estudiantes]    Script Date: 10/2/2022 1:42:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estudiantes](
	[Tipo_ID] [varchar](25) NOT NULL,
	[Identificacion] [varchar](30) NOT NULL,
	[Nombre] [varchar](20) NOT NULL,
	[Segundo_apellido] [varchar](20) NOT NULL,
	[Fecha_Nacimiento] [date] NOT NULL,
	[Primer_Apellido] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Estudiantes] PRIMARY KEY CLUSTERED 
(
	[Tipo_ID] ASC,
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Grupos]    Script Date: 10/2/2022 1:42:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grupos](
	[Numero_Grupo] [tinyint] NOT NULL,
	[Codigo_Curs] [varchar](10) NOT NULL,
	[Identificacion_Profesor] [varchar](30) NOT NULL,
	[Horario] [varchar](10) NOT NULL,
	[Anno] [int] NOT NULL,
	[NumeroPeriodo] [tinyint] NOT NULL,
	[Tipo_ID_Profeso] [varchar](25) NULL,
 CONSTRAINT [PK_Grupos] PRIMARY KEY CLUSTERED 
(
	[Numero_Grupo] ASC,
	[Codigo_Curs] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Matricula]    Script Date: 10/2/2022 1:42:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
select *from Grupos
CREATE TABLE [dbo].[Matricula](
	[Numero_Grupo] [tinyint] NOT NULL,
	[Tipo_Matricula] [varchar](15) NOT NULL,
	[Tipo_ID_Estudiante] [varchar](25) NOT NULL,
	[Identificacion_Estudiante] [varchar](30) NOT NULL,
	[Nota] [float] NULL,
	[Codigo_Curso] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Matricula] PRIMARY KEY CLUSTERED 
(
	[Tipo_ID_Estudiante] ASC,
	[Identificacion_Estudiante] ASC,
	[Codigo_Curso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Periodo]    Script Date: 10/2/2022 1:42:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Periodo](
	[Anno] [int] NOT NULL,
	[NumeroPeriodo] [tinyint] NOT NULL,
	[Fecha_Inicio] [date] NOT NULL,
	[Fecha_Fin] [date] NOT NULL,
	[Estado] [char](2) NOT NULL,
 CONSTRAINT [PK_Periodo] PRIMARY KEY CLUSTERED 
(Asis
	[Anno] ASC,
	[NumeroPeriodo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profesores]    Script Date: 10/2/2022 1:42:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profesores](
	[Tipo_ID] [varchar](25) NOT NULL,
	[Identificacion] [varchar](30) NOT NULL,
	[Nombre] [varchar](20) NOT NULL,
	[Primer_Apellido] [varchar](20) NOT NULL,
	[Segundo_apellido] [varchar](20) NOT NULL,
	[Fecha_Nacimiento] [date] NOT NULL,
 CONSTRAINT [PK_Profesores] PRIMARY KEY CLUSTERED 
(
	[Tipo_ID] ASC,
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Telefonos_Estudiantes]    Script Date: 10/2/2022 1:42:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Telefonos_Estudiantes](
	[Numero_Telefono] [nchar](10) NOT NULL,
	[Tipo_ID_Estudiante] [varchar](25) NOT NULL,
	[Identificacion_Estudiante] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Telefonos_Estudiantes] PRIMARY KEY CLUSTERED 
(
	[Numero_Telefono] ASC,
	[Tipo_ID_Estudiante] ASC,
	[Identificacion_Estudiante] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Telefonos_Profesores]    Script Date: 10/2/2022 1:42:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Telefonos_Profesores](
	[Numero_Telefono] [int] NOT NULL,
	[Tipo_ID_Profesor] [varchar](25) NOT NULL,
	[Identificacion_Profesor] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Telefonos_Profesores_1] PRIMARY KEY CLUSTERED 
(
	[Numero_Telefono] ASC,
	[Tipo_ID_Profesor] ASC,
	[Identificacion_Profesor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Asistencia]  WITH CHECK ADD  CONSTRAINT [FK_Asistencia_Matricula1] FOREIGN KEY([Tipo_ID_Esutiante], [Identificacion_Estudiante], [Codigo_Curso])
REFERENCES [dbo].[Matricula] ([Tipo_ID_Estudiante], [Identificacion_Estudiante], [Codigo_Curso])
GO
ALTER TABLE [dbo].[Asistencia] CHECK CONSTRAINT [FK_Asistencia_Matricula1]
GO
ALTER TABLE [dbo].[Correos_Estudiantes]  WITH CHECK ADD  CONSTRAINT [FK_Correos_Estudiantes_Estudiantes] FOREIGN KEY([Tipo_ID_Estudiante], [Identificacion_Estudiante])
REFERENCES [dbo].[Estudiantes] ([Tipo_ID], [Identificacion])
GO
ALTER TABLE [dbo].[Correos_Estudiantes] CHECK CONSTRAINT [FK_Correos_Estudiantes_Estudiantes]
GO
ALTER TABLE [dbo].[Correos_Profesores]  WITH CHECK ADD  CONSTRAINT [FK_Correos_Profesores_Profesores] FOREIGN KEY([Tipo_ID_Profesor], [Identificacion_Profesor])
REFERENCES [dbo].[Profesores] ([Tipo_ID], [Identificacion])
GO
ALTER TABLE [dbo].[Correos_Profesores] CHECK CONSTRAINT [FK_Correos_Profesores_Profesores]
GO
ALTER TABLE [dbo].[Cursos]  WITH CHECK ADD  CONSTRAINT [FK_Cursos_Carreras1] FOREIGN KEY([Codigo_Carrera])
REFERENCES [dbo].[Carreras] ([Codigo_Carrera])
GO
ALTER TABLE [dbo].[Cursos] CHECK CONSTRAINT [FK_Cursos_Carreras1]
GO
ALTER TABLE [dbo].[Grupos]  WITH CHECK ADD  CONSTRAINT [FK_Grupos_Cursos] FOREIGN KEY([Codigo_Curs])
REFERENCES [dbo].[Cursos] ([Codigo_Curso])
GO
ALTER TABLE [dbo].[Grupos] CHECK CONSTRAINT [FK_Grupos_Cursos]
GO
ALTER TABLE [dbo].[Grupos]  WITH CHECK ADD  CONSTRAINT [FK_Grupos_Periodo] FOREIGN KEY([Anno], [NumeroPeriodo])
REFERENCES [dbo].[Periodo] ([Anno], [NumeroPeriodo])
GO
ALTER TABLE [dbo].[Grupos] CHECK CONSTRAINT [FK_Grupos_Periodo]
GO
ALTER TABLE [dbo].[Grupos]  WITH CHECK ADD  CONSTRAINT [FK_Grupos_Profesores1] FOREIGN KEY([Tipo_ID_Profeso], [Identificacion_Profesor])
REFERENCES [dbo].[Profesores] ([Tipo_ID], [Identificacion])
GO
ALTER TABLE [dbo].[Grupos] CHECK CONSTRAINT [FK_Grupos_Profesores1]
GO
ALTER TABLE [dbo].[Matricula]  WITH CHECK ADD  CONSTRAINT [FK_Matricula_Estudiantes] FOREIGN KEY([Tipo_ID_Estudiante], [Identificacion_Estudiante])
REFERENCES [dbo].[Estudiantes] ([Tipo_ID], [Identificacion])
GO
ALTER TABLE [dbo].[Matricula] CHECK CONSTRAINT [FK_Matricula_Estudiantes]
GO
ALTER TABLE [dbo].[Matricula]  WITH CHECK ADD  CONSTRAINT [FK_Matricula_Grupos1] FOREIGN KEY([Numero_Grupo], [Codigo_Curso])
REFERENCES [dbo].[Grupos] ([Numero_Grupo], [Codigo_Curs])
GO
ALTER TABLE [dbo].[Matricula] CHECK CONSTRAINT [FK_Matricula_Grupos1]
GO
ALTER TABLE [dbo].[Telefonos_Estudiantes]  WITH CHECK ADD  CONSTRAINT [FK_Telefonos_Estudiantes_Estudiantes] FOREIGN KEY([Tipo_ID_Estudiante], [Identificacion_Estudiante])
REFERENCES [dbo].[Estudiantes] ([Tipo_ID], [Identificacion])
GO
ALTER TABLE [dbo].[Telefonos_Estudiantes] CHECK CONSTRAINT [FK_Telefonos_Estudiantes_Estudiantes]
GO
ALTER TABLE [dbo].[Telefonos_Profesores]  WITH CHECK ADD  CONSTRAINT [FK_Telefonos_Profesores_Profesores] FOREIGN KEY([Tipo_ID_Profesor], [Identificacion_Profesor])
REFERENCES [dbo].[Profesores] ([Tipo_ID], [Identificacion])
GO
ALTER TABLE [dbo].[Telefonos_Profesores] CHECK CONSTRAINT [FK_Telefonos_Profesores_Profesores]
GO
select *from Asistencia
select *from Correos_Estudiantes
select *from Correos_Profesores
select *from Grupos
select *from Matricula
select *from Periodo
select *from  Telefonos_Estudiantes 
select *from  Telefonos_Profesores
select *from Cursos
select*from Estudiantes
select *from Profesores