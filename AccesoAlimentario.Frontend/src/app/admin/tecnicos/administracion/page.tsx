"use client";
import {useTheme} from "@mui/material/styles";
import {useDeleteTecnicoMutation, useGetTecnicosQuery} from "@redux/services/tecnicosApi";
import {useNotification} from "@components/Notifications/NotificationContext";
import {
    Backdrop,
    Box, Button,
    CardActions,
    CircularProgress, Divider, Fade, Modal,
    Stack,
    Table,
    TableBody,
    TableContainer,
    TableHead,
    TableRow
} from "@mui/material";
import React from "react";
import MainCard from "@components/Cards/MainCard";
import Typography from "@mui/material/Typography";
import CardContent from "@mui/material/CardContent";
import {StyledTableCell} from "@components/Tables/StyledTableCell";
import {StyledTableRow} from "@components/Tables/StyledTableRow";
import Grid from "@mui/material/Grid2";
import {useAppSelector} from "@redux/hook";
import {formatDate} from "@utils/formatDate";

export default function AdministracionTecnicosPage() {
    const theme = useTheme();
    const {data, isLoading} = useGetTecnicosQuery();
    const [
        deleteTecnico,
        {isLoading: isDeleting}
    ] = useDeleteTecnicoMutation();
    const [tecnicoSeleccionado, setTecnicoSeleccionado] = React.useState<string | null>(null);
    const [showModal, setShowModal] = React.useState(false);
    const {addNotification} = useNotification();
    const user = useAppSelector(state => state.user);

    const handleDelete = (tecnicoId: string) => {
        setTecnicoSeleccionado(tecnicoId);
        setShowModal(true);
    }

    const handleSubmit = async () => {
        if (tecnicoSeleccionado) {
            try {
                await deleteTecnico(tecnicoSeleccionado).unwrap();
                addNotification("Técnico eliminado correctamente", "success");
                setShowModal(false);
                setTecnicoSeleccionado(null);
            } catch {
                addNotification("Error al eliminar técnico", "error");
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
                            Tecnicos
                        </Typography>
                        <Typography variant="subtitle1" sx={{flexGrow: 1}}>
                            Listado de tecnicos
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
                                <StyledTableCell align="center">Fecha Alta</StyledTableCell>
                                <StyledTableCell align="center">Tipo Persona</StyledTableCell>
                                <StyledTableCell align="center" sx={{pr: 3}}>Acciones</StyledTableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {(data || []).map((row, index) => (
                                <StyledTableRow hover key={`${row.id}-${index}`}>
                                    <StyledTableCell sx={{pl: 3}} component="th" scope="row">
                                        {index + 1}
                                    </StyledTableCell>
                                    <StyledTableCell align="center">{row.persona.nombre}</StyledTableCell>
                                    <StyledTableCell align="center">{formatDate(row.persona.fechaAlta)}</StyledTableCell>
                                    <StyledTableCell align="center">{row.persona.tipoPersona}</StyledTableCell>
                                    <StyledTableCell align="center" sx={{pr: 3}}>
                                        <Stack direction="row" spacing={1} justifyContent="center">
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
                    <MainCard modal darkTitle content={false} title={"Eliminar tecnico"} sx={{
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
                                        ¿Está seguro que desea eliminar el técnico?
                                    </Typography>
                                </Grid>
                            </Grid>
                        </CardContent>
                        <Divider/>
                        <Stack direction="row" spacing={1} justifyContent="flex-end" sx={{px: 2.5, py: 2}}>
                            <Button color="secondary" size="small" onClick={() => {
                                setShowModal(false);
                                setTecnicoSeleccionado(null);
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