CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `AreasCobertura` (
        `Id` char(36) NOT NULL,
        `Latitud` float NOT NULL,
        `Longitud` float NOT NULL,
        `Radio` float NOT NULL,
        PRIMARY KEY (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `Direcciones` (
        `Id` char(36) NOT NULL,
        `Calle` longtext NOT NULL,
        `Numero` longtext NOT NULL,
        `Localidad` longtext NOT NULL,
        `Piso` longtext NULL,
        `Departamento` longtext NULL,
        `CodigoPostal` longtext NOT NULL,
        PRIMARY KEY (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `DocumentosIdentidad` (
        `Id` char(36) NOT NULL,
        `TipoDocumento` int NOT NULL,
        `NroDocumento` int NOT NULL,
        `FechaNacimiento` date NULL,
        PRIMARY KEY (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `ModelosHeladera` (
        `Id` char(36) NOT NULL,
        `Capacidad` int NOT NULL,
        `TemperaturaMinima` int NOT NULL,
        `TemperaturaMaxima` int NOT NULL,
        PRIMARY KEY (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `Reportes` (
        `Id` char(36) NOT NULL,
        `Tipo` int NOT NULL,
        `FechaCreacion` datetime(6) NOT NULL,
        `FechaExpiracion` datetime(6) NOT NULL,
        `Cuerpo` longtext NOT NULL,
        PRIMARY KEY (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `ViandasEstandar` (
        `Id` char(36) NOT NULL,
        `Largo` float NOT NULL,
        `Ancho` float NOT NULL,
        `Profundidad` float NOT NULL,
        PRIMARY KEY (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `PuntosEstrategicos` (
        `Id` char(36) NOT NULL,
        `Nombre` longtext NOT NULL,
        `Longitud` float NOT NULL,
        `Latitud` float NOT NULL,
        `DireccionId` char(36) NOT NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_PuntosEstrategicos_Direcciones_DireccionId` FOREIGN KEY (`DireccionId`) REFERENCES `Direcciones` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `Personas` (
        `Id` char(36) NOT NULL,
        `Nombre` longtext NOT NULL,
        `DireccionId` char(36) NULL,
        `DocumentoIdentidadId` char(36) NULL,
        `FechaAlta` datetime(6) NOT NULL,
        `Discriminador` varchar(8) NOT NULL,
        `Apellido` longtext NULL,
        `Sexo` int NULL,
        `RazonSocial` longtext NULL,
        `Tipo` int NULL,
        `Rubro` longtext NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Personas_Direcciones_DireccionId` FOREIGN KEY (`DireccionId`) REFERENCES `Direcciones` (`Id`),
        CONSTRAINT `FK_Personas_DocumentosIdentidad_DocumentoIdentidadId` FOREIGN KEY (`DocumentoIdentidadId`) REFERENCES `DocumentosIdentidad` (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `Heladeras` (
        `Id` char(36) NOT NULL,
        `PuntoEstrategicoId` char(36) NOT NULL,
        `Estado` int NOT NULL,
        `FechaInstalacion` datetime(6) NOT NULL,
        `TemperaturaActual` float NOT NULL,
        `TemperaturaMinimaConfig` float NOT NULL,
        `TemperaturaMaximaConfig` float NOT NULL,
        `ModeloId` char(36) NOT NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Heladeras_ModelosHeladera_ModeloId` FOREIGN KEY (`ModeloId`) REFERENCES `ModelosHeladera` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_Heladeras_PuntosEstrategicos_PuntoEstrategicoId` FOREIGN KEY (`PuntoEstrategicoId`) REFERENCES `PuntosEstrategicos` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `MediosContacto` (
        `Id` char(36) NOT NULL,
        `Preferida` tinyint(1) NOT NULL,
        `Discriminador` varchar(13) NOT NULL,
        `PersonaId` char(36) NULL,
        `Direccion` longtext NULL,
        `Numero` longtext NULL,
        `Whatsapp_Numero` longtext NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_MediosContacto_Personas_PersonaId` FOREIGN KEY (`PersonaId`) REFERENCES `Personas` (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `Tecnicos` (
        `Id` char(36) NOT NULL,
        `PersonaId` char(36) NOT NULL,
        `AreaCoberturaId` char(36) NOT NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Tecnicos_AreasCobertura_AreaCoberturaId` FOREIGN KEY (`AreaCoberturaId`) REFERENCES `AreasCobertura` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_Tecnicos_Personas_PersonaId` FOREIGN KEY (`PersonaId`) REFERENCES `Personas` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `UsuariosSistema` (
        `Id` char(36) NOT NULL,
        `PersonaId` char(36) NOT NULL,
        `UserName` longtext NOT NULL,
        `Password` longtext NOT NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_UsuariosSistema_Personas_PersonaId` FOREIGN KEY (`PersonaId`) REFERENCES `Personas` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `Sensores` (
        `Id` char(36) NOT NULL,
        `HeladeraId` char(36) NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Sensores_Heladeras_HeladeraId` FOREIGN KEY (`HeladeraId`) REFERENCES `Heladeras` (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `SensoresMovimiento` (
        `Id` char(36) NOT NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_SensoresMovimiento_Sensores_Id` FOREIGN KEY (`Id`) REFERENCES `Sensores` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `SensoresTemperatura` (
        `Id` char(36) NOT NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_SensoresTemperatura_Sensores_Id` FOREIGN KEY (`Id`) REFERENCES `Sensores` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `RegistrosMovimiento` (
        `Id` char(36) NOT NULL,
        `Date` datetime(6) NOT NULL,
        `Movimiento` tinyint(1) NOT NULL,
        `SensorMovimientoId` char(36) NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_RegistrosMovimiento_SensoresMovimiento_SensorMovimientoId` FOREIGN KEY (`SensorMovimientoId`) REFERENCES `SensoresMovimiento` (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `RegistrosTemperatura` (
        `Id` char(36) NOT NULL,
        `Date` datetime(6) NOT NULL,
        `Temperatura` float NOT NULL,
        `SensorTemperaturaId` char(36) NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_RegistrosTemperatura_SensoresTemperatura_SensorTemperaturaId` FOREIGN KEY (`SensorTemperaturaId`) REFERENCES `SensoresTemperatura` (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `AccesosHeladera` (
        `Id` char(36) NOT NULL,
        `TarjetaId` char(36) NOT NULL,
        `FechaAcceso` datetime(6) NOT NULL,
        `TipoAcceso` int NOT NULL,
        `HeladeraId` char(36) NOT NULL,
        `AutorizacionId` char(36) NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_AccesosHeladera_Heladeras_HeladeraId` FOREIGN KEY (`HeladeraId`) REFERENCES `Heladeras` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `AdministracionesHeladera` (
        `Id` char(36) NOT NULL,
        `FechaContribucion` datetime(6) NOT NULL,
        `ColaboradorId` char(36) NULL,
        `HeladeraId` char(36) NOT NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_AdministracionesHeladera_Heladeras_HeladeraId` FOREIGN KEY (`HeladeraId`) REFERENCES `Heladeras` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `AutorizacionesManipulacionHeladera` (
        `Id` char(36) NOT NULL,
        `FechaCreacion` datetime(6) NOT NULL,
        `FechaExpiracion` datetime(6) NOT NULL,
        `HeladeraId` char(36) NOT NULL,
        `TarjetaAutorizadaId` char(36) NOT NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_AutorizacionesManipulacionHeladera_Heladeras_HeladeraId` FOREIGN KEY (`HeladeraId`) REFERENCES `Heladeras` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `Colaboradores` (
        `Id` char(36) NOT NULL,
        `PersonaId` char(36) NOT NULL,
        `ContribucionesPreferidas` longtext NOT NULL,
        `Puntos` float NOT NULL,
        `TarjetaColaboracionId` char(36) NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Colaboradores_Personas_PersonaId` FOREIGN KEY (`PersonaId`) REFERENCES `Personas` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `DistribucionesViandas` (
        `Id` char(36) NOT NULL,
        `FechaContribucion` datetime(6) NOT NULL,
        `ColaboradorId` char(36) NULL,
        `HeladeraOrigenId` char(36) NULL,
        `HeladeraDestinoId` char(36) NULL,
        `CantViandas` int NOT NULL,
        `MotivoDistribucion` int NOT NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_DistribucionesViandas_Colaboradores_ColaboradorId` FOREIGN KEY (`ColaboradorId`) REFERENCES `Colaboradores` (`Id`),
        CONSTRAINT `FK_DistribucionesViandas_Heladeras_HeladeraDestinoId` FOREIGN KEY (`HeladeraDestinoId`) REFERENCES `Heladeras` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_DistribucionesViandas_Heladeras_HeladeraOrigenId` FOREIGN KEY (`HeladeraOrigenId`) REFERENCES `Heladeras` (`Id`) ON DELETE RESTRICT
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `DonacionesMonetarias` (
        `Id` char(36) NOT NULL,
        `FechaContribucion` datetime(6) NOT NULL,
        `ColaboradorId` char(36) NULL,
        `Monto` float NOT NULL,
        `FrecuenciaDias` int NOT NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_DonacionesMonetarias_Colaboradores_ColaboradorId` FOREIGN KEY (`ColaboradorId`) REFERENCES `Colaboradores` (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `Incidentes` (
        `Id` char(36) NOT NULL,
        `Fecha` datetime(6) NOT NULL,
        `Resuelto` tinyint(1) NOT NULL,
        `Discriminador` varchar(13) NOT NULL,
        `HeladeraId` char(36) NULL,
        `Tipo` int NULL,
        `ColaboradorId` char(36) NULL,
        `Descripcion` longtext NULL,
        `Foto` longtext NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Incidentes_Colaboradores_ColaboradorId` FOREIGN KEY (`ColaboradorId`) REFERENCES `Colaboradores` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_Incidentes_Heladeras_HeladeraId` FOREIGN KEY (`HeladeraId`) REFERENCES `Heladeras` (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `Premios` (
        `Id` char(36) NOT NULL,
        `Nombre` longtext NOT NULL,
        `PuntosNecesarios` float NOT NULL,
        `Imagen` longtext NOT NULL,
        `ReclamadoPorId` char(36) NULL,
        `FechaReclamo` datetime(6) NOT NULL,
        `Rubro` int NOT NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Premios_Colaboradores_ReclamadoPorId` FOREIGN KEY (`ReclamadoPorId`) REFERENCES `Colaboradores` (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `Suscripciones` (
        `Id` char(36) NOT NULL,
        `HeladeraId` char(36) NOT NULL,
        `ColaboradorId` char(36) NOT NULL,
        `Discriminador` varchar(21) NOT NULL,
        `Maximo` int NULL,
        `Minimo` int NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Suscripciones_Colaboradores_ColaboradorId` FOREIGN KEY (`ColaboradorId`) REFERENCES `Colaboradores` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_Suscripciones_Heladeras_HeladeraId` FOREIGN KEY (`HeladeraId`) REFERENCES `Heladeras` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `Tarjetas` (
        `Id` char(36) NOT NULL,
        `Codigo` longtext NOT NULL,
        `PropietarioId` char(36) NOT NULL,
        `Discriminador` varchar(13) NOT NULL,
        `ResponsableId` char(36) NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Tarjetas_Colaboradores_ResponsableId` FOREIGN KEY (`ResponsableId`) REFERENCES `Colaboradores` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `Viandas` (
        `Id` char(36) NOT NULL,
        `Comida` longtext NOT NULL,
        `FechaDonacion` datetime(6) NOT NULL,
        `FechaCaducidad` datetime(6) NOT NULL,
        `ColaboradorId` char(36) NOT NULL,
        `HeladeraId` char(36) NOT NULL,
        `Calorias` float NOT NULL,
        `Peso` float NOT NULL,
        `Estado` int NOT NULL,
        `UnidadEstandarId` char(36) NOT NULL,
        `AccesoHeladeraId` char(36) NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Viandas_AccesosHeladera_AccesoHeladeraId` FOREIGN KEY (`AccesoHeladeraId`) REFERENCES `AccesosHeladera` (`Id`),
        CONSTRAINT `FK_Viandas_Colaboradores_ColaboradorId` FOREIGN KEY (`ColaboradorId`) REFERENCES `Colaboradores` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_Viandas_Heladeras_HeladeraId` FOREIGN KEY (`HeladeraId`) REFERENCES `Heladeras` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_Viandas_ViandasEstandar_UnidadEstandarId` FOREIGN KEY (`UnidadEstandarId`) REFERENCES `ViandasEstandar` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `VisitasTecnicas` (
        `Id` char(36) NOT NULL,
        `TecnicoId` char(36) NOT NULL,
        `Foto` longtext NULL,
        `Fecha` datetime(6) NOT NULL,
        `Comentario` longtext NULL,
        `IncidenteId` char(36) NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_VisitasTecnicas_Incidentes_IncidenteId` FOREIGN KEY (`IncidenteId`) REFERENCES `Incidentes` (`Id`),
        CONSTRAINT `FK_VisitasTecnicas_Tecnicos_TecnicoId` FOREIGN KEY (`TecnicoId`) REFERENCES `Tecnicos` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `OfertasPremios` (
        `Id` char(36) NOT NULL,
        `FechaContribucion` datetime(6) NOT NULL,
        `ColaboradorId` char(36) NULL,
        `PremioId` char(36) NOT NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_OfertasPremios_Colaboradores_ColaboradorId` FOREIGN KEY (`ColaboradorId`) REFERENCES `Colaboradores` (`Id`),
        CONSTRAINT `FK_OfertasPremios_Premios_PremioId` FOREIGN KEY (`PremioId`) REFERENCES `Premios` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `Notificaciones` (
        `Id` char(36) NOT NULL,
        `Asunto` longtext NOT NULL,
        `Mensaje` longtext NOT NULL,
        `Estado` int NOT NULL,
        `MedioContactoId` char(36) NULL,
        `SuscripcionId` char(36) NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Notificaciones_MediosContacto_MedioContactoId` FOREIGN KEY (`MedioContactoId`) REFERENCES `MediosContacto` (`Id`),
        CONSTRAINT `FK_Notificaciones_Suscripciones_SuscripcionId` FOREIGN KEY (`SuscripcionId`) REFERENCES `Suscripciones` (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `PersonasVulnerables` (
        `Id` char(36) NOT NULL,
        `PersonaId` char(36) NOT NULL,
        `CantidadDeMenores` int NOT NULL,
        `TarjetaId` char(36) NOT NULL,
        `FechaRegistroSistema` datetime(6) NOT NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_PersonasVulnerables_Personas_PersonaId` FOREIGN KEY (`PersonaId`) REFERENCES `Personas` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_PersonasVulnerables_Tarjetas_TarjetaId` FOREIGN KEY (`TarjetaId`) REFERENCES `Tarjetas` (`Id`) ON DELETE RESTRICT
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `RegistrosPersonasVulnerables` (
        `Id` char(36) NOT NULL,
        `FechaContribucion` datetime(6) NOT NULL,
        `ColaboradorId` char(36) NULL,
        `TarjetaId` char(36) NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_RegistrosPersonasVulnerables_Colaboradores_ColaboradorId` FOREIGN KEY (`ColaboradorId`) REFERENCES `Colaboradores` (`Id`),
        CONSTRAINT `FK_RegistrosPersonasVulnerables_Tarjetas_TarjetaId` FOREIGN KEY (`TarjetaId`) REFERENCES `Tarjetas` (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE TABLE `DonacionesViandas` (
        `Id` char(36) NOT NULL,
        `FechaContribucion` datetime(6) NOT NULL,
        `ColaboradorId` char(36) NULL,
        `HeladeraId` char(36) NULL,
        `ViandaId` char(36) NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_DonacionesViandas_Colaboradores_ColaboradorId` FOREIGN KEY (`ColaboradorId`) REFERENCES `Colaboradores` (`Id`),
        CONSTRAINT `FK_DonacionesViandas_Heladeras_HeladeraId` FOREIGN KEY (`HeladeraId`) REFERENCES `Heladeras` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_DonacionesViandas_Viandas_ViandaId` FOREIGN KEY (`ViandaId`) REFERENCES `Viandas` (`Id`) ON DELETE RESTRICT
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_AccesosHeladera_AutorizacionId` ON `AccesosHeladera` (`AutorizacionId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_AccesosHeladera_HeladeraId` ON `AccesosHeladera` (`HeladeraId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_AccesosHeladera_TarjetaId` ON `AccesosHeladera` (`TarjetaId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_AdministracionesHeladera_ColaboradorId` ON `AdministracionesHeladera` (`ColaboradorId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_AdministracionesHeladera_HeladeraId` ON `AdministracionesHeladera` (`HeladeraId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_AutorizacionesManipulacionHeladera_HeladeraId` ON `AutorizacionesManipulacionHeladera` (`HeladeraId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_AutorizacionesManipulacionHeladera_TarjetaAutorizadaId` ON `AutorizacionesManipulacionHeladera` (`TarjetaAutorizadaId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Colaboradores_PersonaId` ON `Colaboradores` (`PersonaId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Colaboradores_TarjetaColaboracionId` ON `Colaboradores` (`TarjetaColaboracionId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_DistribucionesViandas_ColaboradorId` ON `DistribucionesViandas` (`ColaboradorId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_DistribucionesViandas_HeladeraDestinoId` ON `DistribucionesViandas` (`HeladeraDestinoId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_DistribucionesViandas_HeladeraOrigenId` ON `DistribucionesViandas` (`HeladeraOrigenId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_DonacionesMonetarias_ColaboradorId` ON `DonacionesMonetarias` (`ColaboradorId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_DonacionesViandas_ColaboradorId` ON `DonacionesViandas` (`ColaboradorId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_DonacionesViandas_HeladeraId` ON `DonacionesViandas` (`HeladeraId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_DonacionesViandas_ViandaId` ON `DonacionesViandas` (`ViandaId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Heladeras_ModeloId` ON `Heladeras` (`ModeloId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Heladeras_PuntoEstrategicoId` ON `Heladeras` (`PuntoEstrategicoId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Incidentes_ColaboradorId` ON `Incidentes` (`ColaboradorId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Incidentes_HeladeraId` ON `Incidentes` (`HeladeraId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_MediosContacto_PersonaId` ON `MediosContacto` (`PersonaId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Notificaciones_MedioContactoId` ON `Notificaciones` (`MedioContactoId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Notificaciones_SuscripcionId` ON `Notificaciones` (`SuscripcionId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_OfertasPremios_ColaboradorId` ON `OfertasPremios` (`ColaboradorId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_OfertasPremios_PremioId` ON `OfertasPremios` (`PremioId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Personas_DireccionId` ON `Personas` (`DireccionId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Personas_DocumentoIdentidadId` ON `Personas` (`DocumentoIdentidadId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_PersonasVulnerables_PersonaId` ON `PersonasVulnerables` (`PersonaId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_PersonasVulnerables_TarjetaId` ON `PersonasVulnerables` (`TarjetaId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Premios_ReclamadoPorId` ON `Premios` (`ReclamadoPorId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_PuntosEstrategicos_DireccionId` ON `PuntosEstrategicos` (`DireccionId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_RegistrosMovimiento_SensorMovimientoId` ON `RegistrosMovimiento` (`SensorMovimientoId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_RegistrosPersonasVulnerables_ColaboradorId` ON `RegistrosPersonasVulnerables` (`ColaboradorId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_RegistrosPersonasVulnerables_TarjetaId` ON `RegistrosPersonasVulnerables` (`TarjetaId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_RegistrosTemperatura_SensorTemperaturaId` ON `RegistrosTemperatura` (`SensorTemperaturaId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Sensores_HeladeraId` ON `Sensores` (`HeladeraId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Suscripciones_ColaboradorId` ON `Suscripciones` (`ColaboradorId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Suscripciones_HeladeraId` ON `Suscripciones` (`HeladeraId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Tarjetas_PropietarioId` ON `Tarjetas` (`PropietarioId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Tarjetas_ResponsableId` ON `Tarjetas` (`ResponsableId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Tecnicos_AreaCoberturaId` ON `Tecnicos` (`AreaCoberturaId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Tecnicos_PersonaId` ON `Tecnicos` (`PersonaId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_UsuariosSistema_PersonaId` ON `UsuariosSistema` (`PersonaId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Viandas_AccesoHeladeraId` ON `Viandas` (`AccesoHeladeraId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Viandas_ColaboradorId` ON `Viandas` (`ColaboradorId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Viandas_HeladeraId` ON `Viandas` (`HeladeraId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_Viandas_UnidadEstandarId` ON `Viandas` (`UnidadEstandarId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_VisitasTecnicas_IncidenteId` ON `VisitasTecnicas` (`IncidenteId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    CREATE INDEX `IX_VisitasTecnicas_TecnicoId` ON `VisitasTecnicas` (`TecnicoId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    ALTER TABLE `AccesosHeladera` ADD CONSTRAINT `FK_AccesosHeladera_AutorizacionesManipulacionHeladera_Autorizac~` FOREIGN KEY (`AutorizacionId`) REFERENCES `AutorizacionesManipulacionHeladera` (`Id`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    ALTER TABLE `AccesosHeladera` ADD CONSTRAINT `FK_AccesosHeladera_Tarjetas_TarjetaId` FOREIGN KEY (`TarjetaId`) REFERENCES `Tarjetas` (`Id`) ON DELETE CASCADE;
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    ALTER TABLE `AdministracionesHeladera` ADD CONSTRAINT `FK_AdministracionesHeladera_Colaboradores_ColaboradorId` FOREIGN KEY (`ColaboradorId`) REFERENCES `Colaboradores` (`Id`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    ALTER TABLE `AutorizacionesManipulacionHeladera` ADD CONSTRAINT `FK_AutorizacionesManipulacionHeladera_Tarjetas_TarjetaAutorizad~` FOREIGN KEY (`TarjetaAutorizadaId`) REFERENCES `Tarjetas` (`Id`) ON DELETE CASCADE;
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    ALTER TABLE `Colaboradores` ADD CONSTRAINT `FK_Colaboradores_Tarjetas_TarjetaColaboracionId` FOREIGN KEY (`TarjetaColaboracionId`) REFERENCES `Tarjetas` (`Id`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20241121023140_Initial')
BEGIN
    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20241121023140_Initial', '8.0.10');
END;

COMMIT;

