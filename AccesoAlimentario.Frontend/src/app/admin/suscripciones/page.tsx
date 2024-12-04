"use client";
import {
    Box, Button,
    CardActions, CircularProgress, Modal,
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
import {useTheme} from "@mui/material/styles";
import CardContent from "@mui/material/CardContent";
import {useAppSelector} from "@redux/hook";
import {useGetSuscripcionesQuery, usePostDesuscribirseHeladeraMutation} from "@redux/services/colaboradoresApi";
import {
    ISuscripcionExcedenteViandasResponse
} from "@models/responses/suscripcionesColaboradores/iSuscripcionExcedenteViandasResponse";
import {
    ISuscripcionFaltanteViandasResponse
} from "@models/responses/suscripcionesColaboradores/iSuscripcionFaltanteViandasResponse";
import {useNotification} from "@components/Notifications/NotificationContext";

export default function SuscripcionesPage() {
    const theme = useTheme();
    const user = useAppSelector((state) => state.user);
    const {
        data: suscripciones,
        isLoading: suscripcionesLoading,
    } = useGetSuscripcionesQuery(user.colaboradorId);
    const [
        postDesuscribirseHeladera,
        {isLoading: desuscribirseLoading}
    ] = usePostDesuscribirseHeladeraMutation();
    const {addNotification} = useNotification();
    const [selectedSuscripcionId, setSelectedSuscripcionId] = React.useState<string | null>(null);
    const [openModal, setOpenModal] = React.useState(false);

    const handleDesuscribirse = async () => {
        if (!selectedSuscripcionId) {
            return;
        }
        try {
            await postDesuscribirseHeladera(selectedSuscripcionId).unwrap();
            addNotification("Desuscripción realizada correctamente", "success");
            setOpenModal(false);
            setSelectedSuscripcionId(null);
        } catch {
            addNotification("Error al desuscribirse", "error");
        }
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
                            Suscripciones
                        </Typography>
                        <Typography variant="subtitle1" sx={{flexGrow: 1}}>
                            Listado de suscripciones
                        </Typography>
                    </Box>
                </Stack>
            </CardActions>
            <CardContent>
                {
                    suscripcionesLoading ? (
                        <Stack direction="row" spacing={1} justifyContent="center" sx={{py: 2}}>
                            <CircularProgress/>
                        </Stack>
                    ) : (
                        <TableContainer>
                            <Table sx={{minWidth: 350}} aria-label="simple table">
                                <TableHead>
                                    <TableRow>
                                        <StyledTableCell sx={{pl: 3}}>#</StyledTableCell>
                                        <StyledTableCell align="center">Heladera</StyledTableCell>
                                        <StyledTableCell align="center">Tipo</StyledTableCell>
                                        <StyledTableCell align="center">Minimo | Maximo</StyledTableCell>
                                        <StyledTableCell align="center" sx={{pr: 3}}>Acciones</StyledTableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {(suscripciones || []).map((row, index) => (
                                        <StyledTableRow hover key={`${row.id}`}>
                                            <StyledTableCell sx={{pl: 3}} component="th" scope="row">
                                                {index + 1}
                                            </StyledTableCell>
                                            <StyledTableCell
                                                align="center">{row.heladera.puntoEstrategico.nombre}</StyledTableCell>
                                            <StyledTableCell align="center">{
                                                row.tipo === 'ExcedenteViandas' ? 'Excedente de Viandas'
                                                    : row.tipo === 'FaltanteViandas' ? 'Faltante de Viandas'
                                                        : row.tipo === 'IncidenteHeladera' ? 'Incidente de Heladera'
                                                            : '-'
                                            }</StyledTableCell>
                                            <StyledTableCell align="center">{
                                                row.tipo === 'ExcedenteViandas' ? (row as ISuscripcionExcedenteViandasResponse).maximo
                                                    : row.tipo === 'FaltanteViandas' ? (row as ISuscripcionFaltanteViandasResponse).minimo
                                                        : '-'
                                            }</StyledTableCell>
                                            <StyledTableCell sx={{pr: 3}} align="center">
                                                <Stack direction="row" spacing={1} justifyContent="center">
                                                    <Button
                                                        variant="contained"
                                                        color="error"
                                                        size="small"
                                                        sx={{minWidth: '60px'}}
                                                        onClick={() => {
                                                            setSelectedSuscripcionId(row.id);
                                                            setOpenModal(true);
                                                        }}
                                                    >
                                                        Desuscribirse
                                                    </Button>
                                                </Stack>
                                            </StyledTableCell>
                                        </StyledTableRow>
                                    ))}
                                </TableBody>
                            </Table>
                        </TableContainer>
                    )
                }
            </CardContent>
            <Modal open={openModal} onClose={() => {
                setOpenModal(false);
                setSelectedSuscripcionId(null);
            }}>
                <Box
                    sx={{
                        position: 'absolute',
                        top: '50%',
                        left: '50%',
                        transform: 'translate(-50%, -50%)',
                        width: {
                            xs: '90%',
                            sm: '60%',
                            md: '30%',
                        },
                        bgcolor: 'background.paper',
                        border: '2px solid #000',
                        boxShadow: 24,
                        p: 4,
                    }}
                >
                    <Typography id="modal-modal-title" variant="h6" component="h2">
                        Desuscribirse
                    </Typography>
                    <Typography id="modal-modal-description" sx={{mt: 2}}>
                        ¿Está seguro que desea desuscribirse?
                    </Typography>
                    <Stack direction="row" spacing={1} justifyContent="flex-end" sx={{mt: 2}}>
                        <Button color="error" size="small" onClick={() => {
                            setOpenModal(false);
                            setSelectedSuscripcionId(null);
                        }}>
                            Cancelar
                        </Button>
                        <Button
                            variant="contained"
                            size="small"
                            onClick={handleDesuscribirse}
                            disabled={desuscribirseLoading}
                        >
                            Desuscribirse
                        </Button>
                    </Stack>
                </Box>
            </Modal>
        </MainCard>
    );
}