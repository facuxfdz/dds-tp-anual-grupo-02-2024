"use client";
import {
    Backdrop,
    Box,
    Button,
    CardActions,
    CircularProgress, Divider, Fade, Modal,
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
import Typography from "@mui/material/Typography";
import CardContent from "@mui/material/CardContent";
import MainCard from "@components/Cards/MainCard";
import {useTheme} from "@mui/material/styles";
import {useDeleteHeladeraMutation, useGetHeladerasQuery} from "@redux/services/heladerasApi";
import {EstadoHeladera} from "@models/enums/estadoHeladera";
import {heladeraRoute} from "@routes/router";
import NextLink from "next/link";
import Grid from "@mui/material/Grid2";
import {useNotification} from "@components/Notifications/NotificationContext";
import {useAppSelector} from "@redux/hook";

function getHeladeraEstado(estado: EstadoHeladera) {
    switch (estado) {
        case EstadoHeladera.Activa:
            return 'Activa';
        case EstadoHeladera.Desperfecto:
            return 'Desperfecto';
        case EstadoHeladera.FueraServicio:
            return 'Fuera de Servicio';
        default:
            return 'Desconocido';
    }
}

export default function HeladerasPage() {
    const theme = useTheme();
    const {data, isError, isLoading} = useGetHeladerasQuery();
    const [
        deleteHeladera,
        {isLoading: isDeleting}
    ] = useDeleteHeladeraMutation();
    const [heladeraSeleccionada, setHeladeraSeleccionada] = React.useState<string | null>(null);
    const [showModal, setShowModal] = React.useState(false);
    const {addNotification} = useNotification();
    const user = useAppSelector((state) => state.user);

    const handleDelete = (heladeraId: string) => {
        setHeladeraSeleccionada(heladeraId);
        setShowModal(true);
    }

    const handleSubmit = async () => {
        if (heladeraSeleccionada) {
            try {
                await deleteHeladera(heladeraSeleccionada).unwrap();
                addNotification("Heladera eliminada correctamente", "success");
                setShowModal(false);
            } catch {
                addNotification("Error al eliminar la heladera", "error");
            }
        }
    }

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
                            Heladeras
                        </Typography>
                        <Typography variant="subtitle1" sx={{flexGrow: 1}}>
                            Listado de heladeras
                        </Typography>
                    </Box>
                </Stack>
            </CardActions>
            <CardContent>
                <TableContainer>
                    <Table sx={{minWidth: 350}} aria-label="simple table">
                        <TableHead>
                            <TableRow>
                                <StyledTableCell sx={{pl: 3}}>#</StyledTableCell>
                                <StyledTableCell align="center">Nombre</StyledTableCell>
                                <StyledTableCell align="center">Estado</StyledTableCell>
                                <StyledTableCell align="center" sx={{pr: 3}}>Acciones</StyledTableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {data.map((row, index) => (
                                <StyledTableRow hover key={`${row.id}-${index}`}>
                                    <StyledTableCell sx={{pl: 3}} component="th" scope="row">
                                        {index + 1}
                                    </StyledTableCell>
                                    <StyledTableCell align="center">{row.puntoEstrategico.nombre}</StyledTableCell>
                                    <StyledTableCell align="center">{getHeladeraEstado(row.estado)}</StyledTableCell>
                                    <StyledTableCell align="center" sx={{pr: 3}}>
                                        <Stack direction="row" spacing={1} justifyContent="center">
                                            <Button
                                                variant="contained"
                                                color="primary"
                                                size="small"
                                                sx={{minWidth: '30px'}}
                                                component={NextLink}
                                                href={heladeraRoute(row.id)}
                                            >
                                                Ver Detalles
                                            </Button>
                                            {
                                                user.isAdmin && (
                                                    <Button
                                                        variant="contained"
                                                        color="error"
                                                        size="small"
                                                        sx={{minWidth: '30px'}}
                                                        onClick={() => handleDelete(row.id)}
                                                    >
                                                        Eliminar
                                                    </Button>
                                                )
                                            }
                                        </Stack>
                                    </StyledTableCell>
                                </StyledTableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            </CardContent>

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
                    <MainCard modal darkTitle content={false} title={"Eliminar heladera"} sx={{
                        width: {
                            xs: "100%",
                            sm: "80%",
                            md: "60%",
                        }
                    }}>
                            <CardContent>
                                <Grid container spacing={3} alignItems="center">
                                    <Grid size={12}>
                                        <Typography variant="body1">
                                            ¿Está seguro que desea eliminar la heladera?
                                        </Typography>
                                    </Grid>
                                </Grid>
                            </CardContent>
                            <Divider/>
                            <Stack direction="row" spacing={1} justifyContent="flex-end" sx={{px: 2.5, py: 2}}>
                                <Button color="secondary" size="small" onClick={() => {
                                    setShowModal(false);
                                    setHeladeraSeleccionada(null);
                                }}>
                                    Cancelar
                                </Button>
                                <Button
                                    variant="contained"
                                    size="small"
                                    disabled={
                                        isDeleting
                                    }
                                    color="error"
                                    onClick={handleSubmit}
                                >
                                    Eliminar
                                </Button>
                            </Stack>
                    </MainCard>
                </Fade>
            </Modal>
        </MainCard>
    );
}