"use client";
import {
    Backdrop,
    Box,
    Button,
    CardActions, Divider, Fade, MenuItem, Modal, Select,
    Stack,
    Table,
    TableBody,
    TableContainer,
    TableHead,
    TableRow
} from "@mui/material";
import {StyledTableCell} from "@components/Tables/StyledTableCell";
import {StyledTableRow} from "@components/Tables/StyledTableRow";
import React from "react";
import MainCard from "@components/Cards/MainCard";
import Typography from "@mui/material/Typography";
import CardContent from "@mui/material/CardContent";
import {useTheme} from "@mui/material/styles";
import {FormContainer, useForm} from "react-hook-form-mui";
import {FormFieldValue} from "@components/Forms/Form";
import {DonacionMonetariaForm} from "@/app/admin/contribuciones/DonacionMonetariaForm";
import {DonacionViandasForm} from "@/app/admin/contribuciones/DonacionViandasForm";
import {OfertaPremioForm} from "@/app/admin/contribuciones/OfertaPremioForm";
import {AdministracionHeladeraForm} from "@/app/admin/contribuciones/AdministracionHeladeraForm";
import {DistribucionViandasForm} from "@/app/admin/contribuciones/DistribucionViandasForm";
import {
    usePostDistribucionViandasMutation, usePostDonacionHeladeraMutation,
    usePostDonacionMonetariaMutation, usePostDonacionViandaMutation,
    usePostOfertaPremioMutation
} from "@redux/services/contribucionesApi";
import {IDonacionMonetariaRequest} from "@models/requests/contribuciones/iDonacionMonetariaRequest";
import {useNotification} from "@components/Notifications/NotificationContext";
import {IDonacionViandaRequest} from "@models/requests/contribuciones/iDonacionViandaRequest";
import {IOfertaPremioRequest} from "@models/requests/contribuciones/iOfertaPremioRequest";
import {IDonacionHeladeraRequest} from "@models/requests/contribuciones/iDonacionHeladeraRequest";
import {IDistribucionViandaRequest} from "@models/requests/contribuciones/iDistribucionViandaRequest";

function createData(tipo: string, fechaContribucion: string) {
    return {tipo, fechaContribucion};
}

const rows = [
    createData('DonacionMonetaria', '2021-10-10'),
    createData('DonacionVianda', '2021-10-10'),
    createData('OfertaPremio', '2021-10-10'),
    createData('AdministracionHeladera', '2021-10-10'),
    createData('DistribucionViandas', '2021-10-10'),
    createData('DonacionMonetaria', '2021-10-10'),
];

const colaboradorId = "28a57265-a0c4-4813-86a2-38a15d6ebc8a";
export default function ContribucionesPage() {
    const theme = useTheme();
    const [showModal, setShowModal] = React.useState(false);
    const [tipoContribucion, setTipoContribucion] = React.useState<"DonacionMonetaria" | "DonacionVianda" | "OfertaPremio" | "AdministracionHeladera" | "DistribucionViandas">("DonacionMonetaria");
    const formContext = useForm();
    const {addNotification} = useNotification();
    const [
        postDistribucionViandas,
        {isLoading: isLoadingDistribucionViandas}
    ] = usePostDistribucionViandasMutation();
    const [
        postDonacionMonetaria,
        {isLoading: isLoadingDonacionMonetaria}
    ] = usePostDonacionMonetariaMutation();
    const [
        postDonacionViandas,
        {isLoading: isLoadingDonacionViandas}
    ] = usePostDonacionViandaMutation();
    const [
        postOfertaPremio,
        {isLoading: isLoadingOfertaPremio}
    ] = usePostOfertaPremioMutation();
    const [
        postDonacionHeladera,
        {isLoading: isLoadingDonacionHeladera}
    ] = usePostDonacionHeladeraMutation();


    const handleSave = async (data: FormFieldValue) => {
        switch (tipoContribucion) {
            case "DonacionMonetaria":
                const donacionMonetariaData: IDonacionMonetariaRequest = {
                    colaboradorId: colaboradorId,
                    fechaContribucion: data.fechaContribucion,
                    monto: Number(data.monto),
                    frecuenciaDias: Number(data.frecuenciaDias)
                };
                try {
                    await postDonacionMonetaria(donacionMonetariaData).unwrap();
                    addNotification("Donación monetaria creada con éxito", "success");
                    setShowModal(false);
                    formContext.reset();
                } catch {
                    addNotification("Error al crear la donación monetaria", "error");
                }
                break;
            case "DonacionVianda":
                const donacionViandaData: IDonacionViandaRequest = {
                    colaboradorId: colaboradorId,
                    fechaContribucion: data.fechaContribucion,
                    heladeraId: data.heladera,
                    comida: data.comida,
                    fechaCaducidad: data.fechaCaducidad,
                    calorias: Number(data.calorias),
                    peso: Number(data.peso),
                    estadoVianda: "Disponible"
                };
                try {
                    await postDonacionViandas(donacionViandaData).unwrap();
                    addNotification("Donación de vianda creada con éxito", "success");
                    setShowModal(false);
                    formContext.reset();
                } catch {
                    addNotification("Error al crear la donación de vianda", "error");
                }
                break;
            case "OfertaPremio":
                const ofertaPremioData: IOfertaPremioRequest = {
                    colaboradorId: colaboradorId,
                    fechaContribucion: data.fechaContribucion,
                    nombre: data.nombre,
                    puntosNecesarios: Number(data.puntos),
                    imagen: data.imagen,
                    rubro: data.rubro as "Gastronomia" | "Electronica" | "ArticulosHogar" | "Otros"
                };
                try {
                    await postOfertaPremio(ofertaPremioData).unwrap();
                    addNotification("Oferta de premio creada con éxito", "success");
                    setShowModal(false);
                    formContext.reset();
                } catch {
                    addNotification("Error al crear la oferta de premio", "error");
                }
                break;
            case "AdministracionHeladera":
                const sensores = formContext.watch("sensores") as FormFieldValue[];
                const donacionHeladeraData: IDonacionHeladeraRequest = {
                    colaboradorId: colaboradorId,
                    fechaContribucion: data.fechaContribucion,
                    puntoEstrategico: {
                        nombre: data.puntoEstrategicoNombre,
                        longitud: Number(data.puntoEstrategicoLongitud),
                        latitud: Number(data.puntoEstrategicoLatitud),
                        direccion: {
                            calle: data.puntoEstrategicoCalle,
                            numero: data.puntoEstrategicoNumero,
                            localidad: data.puntoEstrategicoLocalidad,
                            piso: data.puntoEstrategicoPiso,
                            departamento: data.puntoEstrategicoDepartamento,
                            codigoPostal: data.puntoEstrategicoCodigoPostal
                        }
                    },
                    estado: data.estado as "Activa" | "Desperfecto" | "FueraServicio",
                    fechaInstalacion: data.fechaInstalacion,
                    temperaturaMinimaConfig: Number(data.temperaturaMinimaConfig),
                    temperaturaMaximaConfig: Number(data.temperaturaMaximaConfig),
                    sensores: sensores.map((sensor: FormFieldValue) => ({
                        id: sensor.id,
                        tipo: sensor.tipo as "Temperatura" | "Movimiento"
                    })),
                    modelo: {
                        capacidad: Number(data.modeloCapacidad),
                        temperaturaMinima: Number(data.modeloTemperaturaMinima),
                        temperaturaMaxima: Number(data.modeloTemperaturaMaxima)
                    }
                };
                try {
                    await postDonacionHeladera(donacionHeladeraData).unwrap();
                    addNotification("Donación de heladera creada con éxito", "success");
                    setShowModal(false);
                    formContext.reset();
                } catch {
                    addNotification("Error al crear la donación de heladera", "error");
                }
                break;
            case "DistribucionViandas":
                const distribucionViandasData: IDistribucionViandaRequest = {
                    colaboradorId: colaboradorId,
                    fechaContribucion: data.fechaContribucion,
                    heladeraOrigenId: data.heladeraOrigenId,
                    heladeraDestinoId: data.heladeraDestinoId,
                    cantidadDeViandas: Number(data.cantidadDeViandas),
                    motivo: data.motivo as "Desperfecto" | "FaltaDeViandas"
                };
                try {
                    await postDistribucionViandas(distribucionViandasData).unwrap();
                    addNotification("Distribución de viandas creada con éxito", "success");
                    setShowModal(false);
                    formContext.reset();
                } catch {
                    addNotification("Error al crear la distribución de viandas", "error");
                }
                break;
            default:
                break;
        }
    };

    return (
        <MainCard content={false} sx={{overflow: 'visible'}}>
            <CardActions
                sx={{
                    position: 'sticky',
                    top: '60px',
                    bgcolor: theme.palette.background.default,
                    zIndex: 1,
                    borderBottom: `1px solid ${theme.palette.divider}`
                }}
            >
                <Stack direction="row" alignItems="center" justifyContent="space-between"
                       sx={{width: 1}}>
                    <Box component="div" sx={{flexGrow: 1, m: 0, pl: 1.5}}>
                        <Typography variant="h5" sx={{flexGrow: 1}}>
                            Contribuciones
                        </Typography>
                        <Typography variant="subtitle1" sx={{flexGrow: 1}}>
                            Puedes ver las contribuciones realizadas por los colaboradores
                        </Typography>
                    </Box>
                </Stack>
                <Stack direction="row" spacing={1} sx={{px: 1.5, py: 0.75}}>
                    <Button color="primary" variant="contained" onClick={() => setShowModal(true)}>
                        Crear
                    </Button>
                </Stack>
            </CardActions>
            <CardContent>
                <TableContainer>
                    <Table sx={{minWidth: 350}} aria-label="simple table">
                        <TableHead>
                            <TableRow>
                                <StyledTableCell sx={{pl: 3}}>#</StyledTableCell>
                                <StyledTableCell align="center">Tipo</StyledTableCell>
                                <StyledTableCell align="center">Fecha de contribución</StyledTableCell>
                                <StyledTableCell sx={{pr: 3}} align="center">Acciones</StyledTableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {rows.map((row, index) => (
                                <StyledTableRow hover key={`${row.tipo}-${index}`}>
                                    <StyledTableCell sx={{pl: 3}} component="th" scope="row">
                                        {index + 1}
                                    </StyledTableCell>
                                    <StyledTableCell align="center">{row.tipo}</StyledTableCell>
                                    <StyledTableCell align="center">{row.fechaContribucion}</StyledTableCell>
                                    <StyledTableCell sx={{pr: 3}} align="center">
                                        <Button color="primary" size="small" variant="contained">
                                            Ver
                                        </Button>
                                    </StyledTableCell>
                                </StyledTableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
                <Modal
                    open={showModal}
                    onClose={() => setShowModal(false)}
                    closeAfterTransition
                    slots={{
                        backdrop: Backdrop
                    }}
                    slotProps={{
                        backdrop: {
                            timeout: 500
                        }
                    }}
                >
                    <Fade in={showModal}>
                        <MainCard modal darkTitle content={false} title={"Crear contribución"} sx={{width: "80%"}}>
                            <FormContainer
                                formContext={formContext}
                                onSuccess={handleSave}
                            >
                                <CardContent>
                                    <Select variant="outlined" value={tipoContribucion}
                                            onChange={(e) => {
                                                setTipoContribucion(e.target.value as "DonacionMonetaria" | "DonacionVianda" | "OfertaPremio" | "AdministracionHeladera" | "DistribucionViandas");
                                                formContext.reset();
                                            }} fullWidth sx={{mb: 2}}>
                                        <MenuItem value="DonacionMonetaria">Donación Monetaria</MenuItem>
                                        <MenuItem value="DonacionVianda">Donación de Viandas</MenuItem>
                                        <MenuItem value="OfertaPremio">Oferta de Premio</MenuItem>
                                        <MenuItem value="AdministracionHeladera">Administración de Heladera</MenuItem>
                                        <MenuItem value="DistribucionViandas">Distribución de Viandas</MenuItem>
                                    </Select>
                                    {
                                        tipoContribucion === "DonacionMonetaria" ? (
                                            <DonacionMonetariaForm/>
                                        ) : tipoContribucion === "DonacionVianda" ? (
                                            <DonacionViandasForm/>
                                        ) : tipoContribucion === "OfertaPremio" ? (
                                            <OfertaPremioForm/>
                                        ) : tipoContribucion === "AdministracionHeladera" ? (
                                            <AdministracionHeladeraForm/>
                                        ) : tipoContribucion === "DistribucionViandas" ? (
                                            <DistribucionViandasForm/>
                                        ) : <></>
                                    }
                                </CardContent>
                                <Divider/>
                                <Stack direction="row" spacing={1} justifyContent="flex-end" sx={{px: 2.5, py: 2}}>
                                    <Button color="error" size="small" onClick={() => setShowModal(false)}>
                                        Cancelar
                                    </Button>
                                    <Button
                                        variant="contained"
                                        size="small"
                                        type="submit"
                                        disabled={
                                            isLoadingDistribucionViandas ||
                                            isLoadingDonacionMonetaria ||
                                            isLoadingDonacionViandas ||
                                            isLoadingOfertaPremio ||
                                            isLoadingDonacionHeladera
                                        }
                                    >
                                        Enviar
                                    </Button>
                                </Stack>
                            </FormContainer>
                        </MainCard>
                    </Fade>
                </Modal>
            </CardContent>
        </MainCard>
    )
        ;
}