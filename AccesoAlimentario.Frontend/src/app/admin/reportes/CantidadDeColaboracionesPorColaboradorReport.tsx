"use client";
import {Table, TableBody, TableContainer, TableHead, TableRow} from "@mui/material";
import React from "react";
import {StyledTableCell} from "@components/Tables/StyledTableCell";
import {StyledTableRow} from "@components/Tables/StyledTableRow";

function createData(colaborador: string, viandasDonadas: number) {
    return {colaborador, viandasDonadas};
}

const rows = [
    createData('Juan', 2),
    createData('Pedro', 87),
    createData('Maria', 31),
    createData('Ana', 45),
    createData('Jose', 0),
];

export const CantidadDeColaboracionesPorColaboradorReport = () => {
    return (
        <TableContainer>
            <Table sx={{minWidth: 350}} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <StyledTableCell sx={{pl: 3}}>#</StyledTableCell>
                        <StyledTableCell align="center">Colaborador</StyledTableCell>
                        <StyledTableCell align="center" sx={{pr: 3}}>Viandas Donadas</StyledTableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows.map((row, index) => (
                        <StyledTableRow hover key={`${row.colaborador}-${index}`}>
                            <StyledTableCell sx={{pl: 3}} component="th" scope="row">
                                {index + 1}
                            </StyledTableCell>
                            <StyledTableCell align="center">{row.colaborador}</StyledTableCell>
                            <StyledTableCell align="center" sx={{pr: 3}}>
                                {row.viandasDonadas}
                            </StyledTableCell>
                        </StyledTableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    )
}