"use client";
import {
    Box, Button,
    CardActions,
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

function createData(nombre: string) {
    return {nombre};
}

const rows = [
    createData('Heladera 1'),
    createData('Heladera 2'),
    createData('Heladera 3'),
    createData('Heladera 4'),
    createData('Heladera 5'),
];

export default function SuscripcionesPage() {
    const theme = useTheme();
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
                <TableContainer>
                    <Table sx={{minWidth: 350}} aria-label="simple table">
                        <TableHead>
                            <TableRow>
                                <StyledTableCell sx={{pl: 3}}>#</StyledTableCell>
                                <StyledTableCell align="center">Nombre</StyledTableCell>
                                <StyledTableCell align="center" sx={{pr: 3}}>Acciones</StyledTableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {rows.map((row, index) => (
                                <StyledTableRow hover key={`${row.nombre}-${index}`}>
                                    <StyledTableCell sx={{pl: 3}} component="th" scope="row">
                                        {index}
                                    </StyledTableCell>
                                    <StyledTableCell align="center">{row.nombre}</StyledTableCell>
                                    <StyledTableCell sx={{pr: 3}} align="center">
                                        <Stack direction="row" spacing={1} justifyContent="center">
                                            <Button
                                                variant="contained"
                                                color="primary"
                                                sx={{minWidth: '30px'}}
                                                size="small"
                                            >
                                                Ver Detalles
                                            </Button>
                                            <Button
                                                variant="contained"
                                                color="error"
                                                size="small"
                                                sx={{minWidth: '30px'}}
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
            </CardContent>
        </MainCard>
    );
}