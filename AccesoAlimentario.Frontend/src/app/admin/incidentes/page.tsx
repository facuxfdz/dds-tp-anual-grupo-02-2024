"use client";
import {useTheme} from "@mui/material/styles";
import {useGetHeladerasQuery} from "@redux/services/heladerasApi";
import {
    Box, Button,
    CardActions,
    CircularProgress, MenuItem, Select,
    Stack,
    Table,
    TableBody,
    TableContainer,
    TableHead,
    TableRow
} from "@mui/material";
import Typography from "@mui/material/Typography";
import React from "react";
import MainCard from "@components/Cards/MainCard";
import CardContent from "@mui/material/CardContent";
import {StyledTableCell} from "@components/Tables/StyledTableCell";
import {StyledTableRow} from "@components/Tables/StyledTableRow";
import {formatDate} from "@utils/formatDate";
import {IIncidenteResponse} from "@models/responses/incidentes/iIncidenteResponse";
import RegistroVisitaModal from "@/app/admin/incidentes/RegistroVisitaModal";
import DetalleIncidenteModal from "@/app/admin/incidentes/DetalleIncidenteModal";

export default function HeladerasPage() {
    const theme = useTheme();
    const {data, isError, isLoading} = useGetHeladerasQuery();
    const [resulto, setResuelto] = React.useState<"Todos" | "Resulto" | "NoResuelto">("NoResuelto");
    const [openModalDetalle, setOpenModalDetalle] = React.useState(false);
    const [incidente, setIncidente] = React.useState<IIncidenteResponse | undefined>(undefined);
    const [openModalVisita, setOpenModalVisita] = React.useState(false);

    if (isLoading) {
        return (
            <Box sx={{display: 'flex', justifyContent: 'center'}}>
                <CircularProgress/>
            </Box>
        )
    }

    if (isError || !data) {
        return (<Box sx={{display: 'flex', justifyContent: 'center'}}>
            <Typography variant="h5">
                Error al cargar los datos
            </Typography>
        </Box>)
    }

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
                            Incidentes
                        </Typography>
                        <Typography variant="subtitle1" sx={{flexGrow: 1}}>
                            Listado de incidentes reportados por heladeras
                        </Typography>
                    </Box>
                </Stack>
                <Stack direction="row" spacing={1} sx={{px: 1.5, py: 0.75}}>
                    <Select variant="outlined" value={resulto}
                            onChange={(e) => {
                                setResuelto(e.target.value as "Todos" | "Resulto" | "NoResuelto");
                            }}>
                        <MenuItem value="Todos">Todos</MenuItem>
                        <MenuItem value="Resulto">Resuelto</MenuItem>
                        <MenuItem value="NoResuelto">No Resuelto</MenuItem>
                    </Select>
                </Stack>
            </CardActions>
            <CardContent>
                <TableContainer>
                    <Table sx={{minWidth: 350}} aria-label="simple table">
                        <TableHead>
                            <TableRow>
                                <StyledTableCell sx={{pl: 3}}>#</StyledTableCell>
                                <StyledTableCell align="center">Heladera</StyledTableCell>
                                <StyledTableCell align="center">Fecha Reporte</StyledTableCell>
                                <StyledTableCell align="center">Tipo Incidente</StyledTableCell>
                                <StyledTableCell align="center">Visitas</StyledTableCell>
                                <StyledTableCell align="center">Resuelto</StyledTableCell>
                                <StyledTableCell align="center" sx={{pr: 3}}>Acciones</StyledTableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {data.map((row) => {
                                return row.incidentes
                                    .filter(incidente => resulto === 'Todos' || (resulto === 'Resulto' ? incidente.resuelto : !incidente.resuelto))
                                    .map((incidente, index) =>
                                    (
                                        <StyledTableRow hover key={`${row.id}-${index}`}>
                                            <StyledTableCell sx={{pl: 3}} component="th" scope="row">
                                                {index + 1}
                                            </StyledTableCell>
                                            <StyledTableCell
                                                align="center">{row.puntoEstrategico.nombre}</StyledTableCell>
                                            <StyledTableCell
                                                align="center">{formatDate(incidente.fecha)}</StyledTableCell>
                                            <StyledTableCell
                                                align="center">{incidente.tipoIncidente === 'Alerta' ? 'Alerta' : 'Falla Tecnica'}</StyledTableCell>
                                            <StyledTableCell
                                                align="center">{incidente.visitasTecnicas.length}</StyledTableCell>
                                            <StyledTableCell
                                                align="center">{incidente.resuelto ? 'Resuelto' : 'No Resuelto'}</StyledTableCell>
                                            <StyledTableCell align="center" sx={{pr: 3}}>
                                                <Stack direction="row" spacing={1} justifyContent="center">
                                                    <Button
                                                        variant="contained"
                                                        color="primary"
                                                        size="small"
                                                        sx={{minWidth: '30px'}}
                                                        onClick={() => {
                                                            setIncidente(incidente);
                                                            setOpenModalDetalle(true);
                                                        }}
                                                    >
                                                        Ver Detalle
                                                    </Button>
                                                    {
                                                        incidente.resuelto ? null :
                                                            <Button
                                                                variant="contained"
                                                                color="primary"
                                                                size="small"
                                                                sx={{minWidth: '30px'}}
                                                                onClick={() => {
                                                                    setIncidente(incidente);
                                                                    setOpenModalVisita(true);
                                                                }}
                                                            >
                                                                Registrar Visita
                                                            </Button>
                                                    }
                                                </Stack>
                                            </StyledTableCell>
                                        </StyledTableRow>
                                    )
                                )
                            })}
                        </TableBody>
                    </Table>
                </TableContainer>
                {
                    openModalDetalle && incidente &&
                    (
                        <DetalleIncidenteModal
                            incidente={incidente}
                            open={openModalDetalle}
                            onClose={() => {
                                setOpenModalDetalle(false);
                                setIncidente(undefined);
                            }}
                        />
                    )
                }
                {
                    openModalVisita && incidente &&
                    (
                        <RegistroVisitaModal
                            incidenteId={incidente.id}
                            open={openModalVisita}
                            onClose={() => {
                                setOpenModalVisita(false);
                                setIncidente(undefined);
                            }}
                        />
                    )
                }
            </CardContent>
        </MainCard>
    );
}