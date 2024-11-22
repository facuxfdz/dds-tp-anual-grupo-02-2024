"use client";
import React from "react";
import {Table, TableBody, TableContainer, TableHead, TableRow} from "@mui/material";
import {StyledTableCell} from "@components/Tables/StyledTableCell";
import {StyledTableRow} from "@components/Tables/StyledTableRow";

function createData(nombre: string, estado: string, cantidad: number) {
    return {nombre, estado, cantidad};
}

const rows = [
    createData('Heladera 1', 'Activa', 2),
    createData('Heladera 2', 'Desperfecto', 87),
    createData('Heladera 3', 'Fuera de Servicio', 31),
    createData('Heladera 4', 'Activa', 45),
    createData('Heladera 5', 'Activa', 0),
];

export const CantidadDeFallasPorHeladeraReport = () => {
    return (
        <TableContainer>
            <Table sx={{minWidth: 350}} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <StyledTableCell sx={{pl: 3}}>#</StyledTableCell>
                        <StyledTableCell align="center">Nombre</StyledTableCell>
                        <StyledTableCell align="center">Estado</StyledTableCell>
                        <StyledTableCell align="center" sx={{pr: 3}}>Cantidad de Fallas</StyledTableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows.map((row, index) => (
                        <StyledTableRow hover key={`${row.nombre}-${index}`}>
                            <StyledTableCell sx={{pl: 3}} component="th" scope="row">
                                {index + 1}
                            </StyledTableCell>
                            <StyledTableCell align="center">{row.nombre}</StyledTableCell>
                            <StyledTableCell align="center">{row.estado}</StyledTableCell>
                            <StyledTableCell align="center" sx={{pr: 3}}>
                                {row.cantidad}
                            </StyledTableCell>
                        </StyledTableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    )
}