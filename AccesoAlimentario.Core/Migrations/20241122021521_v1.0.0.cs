using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoAlimentario.Core.Migrations
{
    /// <inheritdoc />
    public partial class v100 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AreasCobertura",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Latitud = table.Column<float>(type: "float", nullable: false),
                    Longitud = table.Column<float>(type: "float", nullable: false),
                    Radio = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreasCobertura", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Direcciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Calle = table.Column<string>(type: "longtext", nullable: false),
                    Numero = table.Column<string>(type: "longtext", nullable: false),
                    Localidad = table.Column<string>(type: "longtext", nullable: false),
                    Piso = table.Column<string>(type: "longtext", nullable: true),
                    Departamento = table.Column<string>(type: "longtext", nullable: true),
                    CodigoPostal = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direcciones", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DocumentosIdentidad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    TipoDocumento = table.Column<int>(type: "int", nullable: false),
                    NroDocumento = table.Column<int>(type: "int", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentosIdentidad", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ModelosHeladera",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Capacidad = table.Column<int>(type: "int", nullable: false),
                    TemperaturaMinima = table.Column<int>(type: "int", nullable: false),
                    TemperaturaMaxima = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelosHeladera", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reportes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Cuerpo = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reportes", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ViandasEstandar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Largo = table.Column<float>(type: "float", nullable: false),
                    Ancho = table.Column<float>(type: "float", nullable: false),
                    Profundidad = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViandasEstandar", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PuntosEstrategicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    Longitud = table.Column<float>(type: "float", nullable: false),
                    Latitud = table.Column<float>(type: "float", nullable: false),
                    DireccionId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuntosEstrategicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PuntosEstrategicos_Direcciones_DireccionId",
                        column: x => x.DireccionId,
                        principalTable: "Direcciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    DireccionId = table.Column<Guid>(type: "char(36)", nullable: true),
                    DocumentoIdentidadId = table.Column<Guid>(type: "char(36)", nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Discriminador = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    Apellido = table.Column<string>(type: "longtext", nullable: true),
                    Sexo = table.Column<int>(type: "int", nullable: true),
                    RazonSocial = table.Column<string>(type: "longtext", nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: true),
                    Rubro = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personas_Direcciones_DireccionId",
                        column: x => x.DireccionId,
                        principalTable: "Direcciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Personas_DocumentosIdentidad_DocumentoIdentidadId",
                        column: x => x.DocumentoIdentidadId,
                        principalTable: "DocumentosIdentidad",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Heladeras",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    PuntoEstrategicoId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    FechaInstalacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TemperaturaActual = table.Column<float>(type: "float", nullable: false),
                    TemperaturaMinimaConfig = table.Column<float>(type: "float", nullable: false),
                    TemperaturaMaximaConfig = table.Column<float>(type: "float", nullable: false),
                    ModeloId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heladeras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Heladeras_ModelosHeladera_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "ModelosHeladera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Heladeras_PuntosEstrategicos_PuntoEstrategicoId",
                        column: x => x.PuntoEstrategicoId,
                        principalTable: "PuntosEstrategicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MediosContacto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Preferida = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Discriminador = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    PersonaId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Direccion = table.Column<string>(type: "longtext", nullable: true),
                    Numero = table.Column<string>(type: "longtext", nullable: true),
                    Whatsapp_Numero = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediosContacto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediosContacto_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tecnicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    PersonaId = table.Column<Guid>(type: "char(36)", nullable: false),
                    AreaCoberturaId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tecnicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tecnicos_AreasCobertura_AreaCoberturaId",
                        column: x => x.AreaCoberturaId,
                        principalTable: "AreasCobertura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tecnicos_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UsuariosSistema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    PersonaId = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserName = table.Column<string>(type: "longtext", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosSistema", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuariosSistema_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sensores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    HeladeraId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensores_Heladeras_HeladeraId",
                        column: x => x.HeladeraId,
                        principalTable: "Heladeras",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SensoresMovimiento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensoresMovimiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensoresMovimiento_Sensores_Id",
                        column: x => x.Id,
                        principalTable: "Sensores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SensoresTemperatura",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensoresTemperatura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensoresTemperatura_Sensores_Id",
                        column: x => x.Id,
                        principalTable: "Sensores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RegistrosMovimiento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Movimiento = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SensorMovimientoId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosMovimiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosMovimiento_SensoresMovimiento_SensorMovimientoId",
                        column: x => x.SensorMovimientoId,
                        principalTable: "SensoresMovimiento",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RegistrosTemperatura",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Temperatura = table.Column<float>(type: "float", nullable: false),
                    SensorTemperaturaId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosTemperatura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosTemperatura_SensoresTemperatura_SensorTemperaturaId",
                        column: x => x.SensorTemperaturaId,
                        principalTable: "SensoresTemperatura",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AccesosHeladera",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    TarjetaId = table.Column<Guid>(type: "char(36)", nullable: false),
                    FechaAcceso = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TipoAcceso = table.Column<int>(type: "int", nullable: false),
                    HeladeraId = table.Column<Guid>(type: "char(36)", nullable: false),
                    AutorizacionId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccesosHeladera", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccesosHeladera_Heladeras_HeladeraId",
                        column: x => x.HeladeraId,
                        principalTable: "Heladeras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AdministracionesHeladera",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    FechaContribucion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ColaboradorId = table.Column<Guid>(type: "char(36)", nullable: true),
                    HeladeraId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministracionesHeladera", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdministracionesHeladera_Heladeras_HeladeraId",
                        column: x => x.HeladeraId,
                        principalTable: "Heladeras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AutorizacionesManipulacionHeladera",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HeladeraId = table.Column<Guid>(type: "char(36)", nullable: false),
                    TarjetaAutorizadaId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorizacionesManipulacionHeladera", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutorizacionesManipulacionHeladera_Heladeras_HeladeraId",
                        column: x => x.HeladeraId,
                        principalTable: "Heladeras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Colaboradores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    PersonaId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ContribucionesPreferidas = table.Column<string>(type: "longtext", nullable: false),
                    Puntos = table.Column<float>(type: "float", nullable: false),
                    TarjetaColaboracionId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colaboradores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colaboradores_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DistribucionesViandas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    FechaContribucion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ColaboradorId = table.Column<Guid>(type: "char(36)", nullable: true),
                    HeladeraOrigenId = table.Column<Guid>(type: "char(36)", nullable: true),
                    HeladeraDestinoId = table.Column<Guid>(type: "char(36)", nullable: true),
                    CantViandas = table.Column<int>(type: "int", nullable: false),
                    MotivoDistribucion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistribucionesViandas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistribucionesViandas_Colaboradores_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaboradores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DistribucionesViandas_Heladeras_HeladeraDestinoId",
                        column: x => x.HeladeraDestinoId,
                        principalTable: "Heladeras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistribucionesViandas_Heladeras_HeladeraOrigenId",
                        column: x => x.HeladeraOrigenId,
                        principalTable: "Heladeras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DonacionesMonetarias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    FechaContribucion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ColaboradorId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Monto = table.Column<float>(type: "float", nullable: false),
                    FrecuenciaDias = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonacionesMonetarias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonacionesMonetarias_Colaboradores_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaboradores",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Incidentes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Resuelto = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Discriminador = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    HeladeraId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: true),
                    ColaboradorId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true),
                    Foto = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incidentes_Colaboradores_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaboradores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidentes_Heladeras_HeladeraId",
                        column: x => x.HeladeraId,
                        principalTable: "Heladeras",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Premios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    PuntosNecesarios = table.Column<float>(type: "float", nullable: false),
                    Imagen = table.Column<string>(type: "longtext", nullable: false),
                    ReclamadoPorId = table.Column<Guid>(type: "char(36)", nullable: true),
                    FechaReclamo = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Rubro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Premios_Colaboradores_ReclamadoPorId",
                        column: x => x.ReclamadoPorId,
                        principalTable: "Colaboradores",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Suscripciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    HeladeraId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ColaboradorId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Discriminador = table.Column<string>(type: "varchar(21)", maxLength: 21, nullable: false),
                    Maximo = table.Column<int>(type: "int", nullable: true),
                    Minimo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suscripciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suscripciones_Colaboradores_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaboradores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Suscripciones_Heladeras_HeladeraId",
                        column: x => x.HeladeraId,
                        principalTable: "Heladeras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tarjetas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Codigo = table.Column<string>(type: "longtext", nullable: false),
                    PropietarioId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Discriminador = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    ResponsableId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarjetas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tarjetas_Colaboradores_ResponsableId",
                        column: x => x.ResponsableId,
                        principalTable: "Colaboradores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Viandas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Comida = table.Column<string>(type: "longtext", nullable: false),
                    FechaDonacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaCaducidad = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ColaboradorId = table.Column<Guid>(type: "char(36)", nullable: false),
                    HeladeraId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Calorias = table.Column<float>(type: "float", nullable: false),
                    Peso = table.Column<float>(type: "float", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    UnidadEstandarId = table.Column<Guid>(type: "char(36)", nullable: false),
                    AccesoHeladeraId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viandas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Viandas_AccesosHeladera_AccesoHeladeraId",
                        column: x => x.AccesoHeladeraId,
                        principalTable: "AccesosHeladera",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Viandas_Colaboradores_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaboradores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Viandas_Heladeras_HeladeraId",
                        column: x => x.HeladeraId,
                        principalTable: "Heladeras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Viandas_ViandasEstandar_UnidadEstandarId",
                        column: x => x.UnidadEstandarId,
                        principalTable: "ViandasEstandar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VisitasTecnicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    TecnicoId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Foto = table.Column<string>(type: "longtext", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Comentario = table.Column<string>(type: "longtext", nullable: true),
                    IncidenteId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitasTecnicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitasTecnicas_Incidentes_IncidenteId",
                        column: x => x.IncidenteId,
                        principalTable: "Incidentes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VisitasTecnicas_Tecnicos_TecnicoId",
                        column: x => x.TecnicoId,
                        principalTable: "Tecnicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OfertasPremios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    FechaContribucion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ColaboradorId = table.Column<Guid>(type: "char(36)", nullable: true),
                    PremioId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfertasPremios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfertasPremios_Colaboradores_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaboradores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OfertasPremios_Premios_PremioId",
                        column: x => x.PremioId,
                        principalTable: "Premios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Asunto = table.Column<string>(type: "longtext", nullable: false),
                    Mensaje = table.Column<string>(type: "longtext", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    MedioContactoId = table.Column<Guid>(type: "char(36)", nullable: true),
                    SuscripcionId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificaciones_MediosContacto_MedioContactoId",
                        column: x => x.MedioContactoId,
                        principalTable: "MediosContacto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notificaciones_Suscripciones_SuscripcionId",
                        column: x => x.SuscripcionId,
                        principalTable: "Suscripciones",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PersonasVulnerables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    PersonaId = table.Column<Guid>(type: "char(36)", nullable: false),
                    CantidadDeMenores = table.Column<int>(type: "int", nullable: false),
                    TarjetaId = table.Column<Guid>(type: "char(36)", nullable: false),
                    FechaRegistroSistema = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonasVulnerables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonasVulnerables_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonasVulnerables_Tarjetas_TarjetaId",
                        column: x => x.TarjetaId,
                        principalTable: "Tarjetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RegistrosPersonasVulnerables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    FechaContribucion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ColaboradorId = table.Column<Guid>(type: "char(36)", nullable: true),
                    TarjetaId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosPersonasVulnerables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosPersonasVulnerables_Colaboradores_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaboradores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegistrosPersonasVulnerables_Tarjetas_TarjetaId",
                        column: x => x.TarjetaId,
                        principalTable: "Tarjetas",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DonacionesViandas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    FechaContribucion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ColaboradorId = table.Column<Guid>(type: "char(36)", nullable: true),
                    HeladeraId = table.Column<Guid>(type: "char(36)", nullable: true),
                    ViandaId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonacionesViandas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonacionesViandas_Colaboradores_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaboradores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DonacionesViandas_Heladeras_HeladeraId",
                        column: x => x.HeladeraId,
                        principalTable: "Heladeras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonacionesViandas_Viandas_ViandaId",
                        column: x => x.ViandaId,
                        principalTable: "Viandas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AccesosHeladera_AutorizacionId",
                table: "AccesosHeladera",
                column: "AutorizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_AccesosHeladera_HeladeraId",
                table: "AccesosHeladera",
                column: "HeladeraId");

            migrationBuilder.CreateIndex(
                name: "IX_AccesosHeladera_TarjetaId",
                table: "AccesosHeladera",
                column: "TarjetaId");

            migrationBuilder.CreateIndex(
                name: "IX_AdministracionesHeladera_ColaboradorId",
                table: "AdministracionesHeladera",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_AdministracionesHeladera_HeladeraId",
                table: "AdministracionesHeladera",
                column: "HeladeraId");

            migrationBuilder.CreateIndex(
                name: "IX_AutorizacionesManipulacionHeladera_HeladeraId",
                table: "AutorizacionesManipulacionHeladera",
                column: "HeladeraId");

            migrationBuilder.CreateIndex(
                name: "IX_AutorizacionesManipulacionHeladera_TarjetaAutorizadaId",
                table: "AutorizacionesManipulacionHeladera",
                column: "TarjetaAutorizadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_PersonaId",
                table: "Colaboradores",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_TarjetaColaboracionId",
                table: "Colaboradores",
                column: "TarjetaColaboracionId");

            migrationBuilder.CreateIndex(
                name: "IX_DistribucionesViandas_ColaboradorId",
                table: "DistribucionesViandas",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_DistribucionesViandas_HeladeraDestinoId",
                table: "DistribucionesViandas",
                column: "HeladeraDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_DistribucionesViandas_HeladeraOrigenId",
                table: "DistribucionesViandas",
                column: "HeladeraOrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_DonacionesMonetarias_ColaboradorId",
                table: "DonacionesMonetarias",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_DonacionesViandas_ColaboradorId",
                table: "DonacionesViandas",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_DonacionesViandas_HeladeraId",
                table: "DonacionesViandas",
                column: "HeladeraId");

            migrationBuilder.CreateIndex(
                name: "IX_DonacionesViandas_ViandaId",
                table: "DonacionesViandas",
                column: "ViandaId");

            migrationBuilder.CreateIndex(
                name: "IX_Heladeras_ModeloId",
                table: "Heladeras",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Heladeras_PuntoEstrategicoId",
                table: "Heladeras",
                column: "PuntoEstrategicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidentes_ColaboradorId",
                table: "Incidentes",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidentes_HeladeraId",
                table: "Incidentes",
                column: "HeladeraId");

            migrationBuilder.CreateIndex(
                name: "IX_MediosContacto_PersonaId",
                table: "MediosContacto",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_MedioContactoId",
                table: "Notificaciones",
                column: "MedioContactoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_SuscripcionId",
                table: "Notificaciones",
                column: "SuscripcionId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasPremios_ColaboradorId",
                table: "OfertasPremios",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasPremios_PremioId",
                table: "OfertasPremios",
                column: "PremioId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_DireccionId",
                table: "Personas",
                column: "DireccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_DocumentoIdentidadId",
                table: "Personas",
                column: "DocumentoIdentidadId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonasVulnerables_PersonaId",
                table: "PersonasVulnerables",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonasVulnerables_TarjetaId",
                table: "PersonasVulnerables",
                column: "TarjetaId");

            migrationBuilder.CreateIndex(
                name: "IX_Premios_ReclamadoPorId",
                table: "Premios",
                column: "ReclamadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_PuntosEstrategicos_DireccionId",
                table: "PuntosEstrategicos",
                column: "DireccionId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosMovimiento_SensorMovimientoId",
                table: "RegistrosMovimiento",
                column: "SensorMovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosPersonasVulnerables_ColaboradorId",
                table: "RegistrosPersonasVulnerables",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosPersonasVulnerables_TarjetaId",
                table: "RegistrosPersonasVulnerables",
                column: "TarjetaId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosTemperatura_SensorTemperaturaId",
                table: "RegistrosTemperatura",
                column: "SensorTemperaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensores_HeladeraId",
                table: "Sensores",
                column: "HeladeraId");

            migrationBuilder.CreateIndex(
                name: "IX_Suscripciones_ColaboradorId",
                table: "Suscripciones",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Suscripciones_HeladeraId",
                table: "Suscripciones",
                column: "HeladeraId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarjetas_PropietarioId",
                table: "Tarjetas",
                column: "PropietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarjetas_ResponsableId",
                table: "Tarjetas",
                column: "ResponsableId");

            migrationBuilder.CreateIndex(
                name: "IX_Tecnicos_AreaCoberturaId",
                table: "Tecnicos",
                column: "AreaCoberturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tecnicos_PersonaId",
                table: "Tecnicos",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosSistema_PersonaId",
                table: "UsuariosSistema",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Viandas_AccesoHeladeraId",
                table: "Viandas",
                column: "AccesoHeladeraId");

            migrationBuilder.CreateIndex(
                name: "IX_Viandas_ColaboradorId",
                table: "Viandas",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Viandas_HeladeraId",
                table: "Viandas",
                column: "HeladeraId");

            migrationBuilder.CreateIndex(
                name: "IX_Viandas_UnidadEstandarId",
                table: "Viandas",
                column: "UnidadEstandarId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitasTecnicas_IncidenteId",
                table: "VisitasTecnicas",
                column: "IncidenteId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitasTecnicas_TecnicoId",
                table: "VisitasTecnicas",
                column: "TecnicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccesosHeladera_AutorizacionesManipulacionHeladera_Autorizac~",
                table: "AccesosHeladera",
                column: "AutorizacionId",
                principalTable: "AutorizacionesManipulacionHeladera",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccesosHeladera_Tarjetas_TarjetaId",
                table: "AccesosHeladera",
                column: "TarjetaId",
                principalTable: "Tarjetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdministracionesHeladera_Colaboradores_ColaboradorId",
                table: "AdministracionesHeladera",
                column: "ColaboradorId",
                principalTable: "Colaboradores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AutorizacionesManipulacionHeladera_Tarjetas_TarjetaAutorizad~",
                table: "AutorizacionesManipulacionHeladera",
                column: "TarjetaAutorizadaId",
                principalTable: "Tarjetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Colaboradores_Tarjetas_TarjetaColaboracionId",
                table: "Colaboradores",
                column: "TarjetaColaboracionId",
                principalTable: "Tarjetas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colaboradores_Tarjetas_TarjetaColaboracionId",
                table: "Colaboradores");

            migrationBuilder.DropTable(
                name: "AdministracionesHeladera");

            migrationBuilder.DropTable(
                name: "DistribucionesViandas");

            migrationBuilder.DropTable(
                name: "DonacionesMonetarias");

            migrationBuilder.DropTable(
                name: "DonacionesViandas");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "OfertasPremios");

            migrationBuilder.DropTable(
                name: "PersonasVulnerables");

            migrationBuilder.DropTable(
                name: "RegistrosMovimiento");

            migrationBuilder.DropTable(
                name: "RegistrosPersonasVulnerables");

            migrationBuilder.DropTable(
                name: "RegistrosTemperatura");

            migrationBuilder.DropTable(
                name: "Reportes");

            migrationBuilder.DropTable(
                name: "UsuariosSistema");

            migrationBuilder.DropTable(
                name: "VisitasTecnicas");

            migrationBuilder.DropTable(
                name: "Viandas");

            migrationBuilder.DropTable(
                name: "MediosContacto");

            migrationBuilder.DropTable(
                name: "Suscripciones");

            migrationBuilder.DropTable(
                name: "Premios");

            migrationBuilder.DropTable(
                name: "SensoresMovimiento");

            migrationBuilder.DropTable(
                name: "SensoresTemperatura");

            migrationBuilder.DropTable(
                name: "Incidentes");

            migrationBuilder.DropTable(
                name: "Tecnicos");

            migrationBuilder.DropTable(
                name: "AccesosHeladera");

            migrationBuilder.DropTable(
                name: "ViandasEstandar");

            migrationBuilder.DropTable(
                name: "Sensores");

            migrationBuilder.DropTable(
                name: "AreasCobertura");

            migrationBuilder.DropTable(
                name: "AutorizacionesManipulacionHeladera");

            migrationBuilder.DropTable(
                name: "Heladeras");

            migrationBuilder.DropTable(
                name: "ModelosHeladera");

            migrationBuilder.DropTable(
                name: "PuntosEstrategicos");

            migrationBuilder.DropTable(
                name: "Tarjetas");

            migrationBuilder.DropTable(
                name: "Colaboradores");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "Direcciones");

            migrationBuilder.DropTable(
                name: "DocumentosIdentidad");
        }
    }
}
