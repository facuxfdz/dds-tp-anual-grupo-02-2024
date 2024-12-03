"use client";
import {
    Box,
    Button,
    CardActions,
    CircularProgress,
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
import {useGetHeladerasQuery} from "@redux/services/heladerasApi";
import {EstadoHeladera} from "@models/enums/estadoHeladera";
import {heladeraRoute} from "@routes/router";
import NextLink from "next/link";

function getHeladeraEstado(estado: EstadoHeladera){
    switch (estado){
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

    if (isLoading){
        return <CircularProgress />
    }

    if (isError || !data){
        return <Box>Error: Ha ocurrido un error al cargar los datos</Box>
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
                                        </Stack>
                                    </StyledTableCell>
                                </StyledTableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            </CardContent>
        </MainCard>
    );
}