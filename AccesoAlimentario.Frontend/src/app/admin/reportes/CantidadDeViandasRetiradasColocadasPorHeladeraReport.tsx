"use client";
import {Table, TableBody, TableContainer, TableHead, TableRow} from "@mui/material";
import React from "react";
import {StyledTableCell} from "@components/Tables/StyledTableCell";
import {StyledTableRow} from "@components/Tables/StyledTableRow";

function createData(heladera: string, viandasRetiradas: number, viandasColocadas: number) {
    return {heladera, viandasRetiradas, viandasColocadas};
}

const rows = [
    createData('Heladera 1', 10, 2),
    createData('Heladera 2', 4, 87),
    createData('Heladera 3', 34, 31),
    createData('Heladera 4', 7, 45),
    createData('Heladera 5', 15, 0),
];

export const CantidadDeViandasRetiradasColocadasPorHeladeraReport = () => {
    return (
        <TableContainer>
            <Table sx={{minWidth: 350}} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <StyledTableCell sx={{pl: 3}}>#</StyledTableCell>
                        <StyledTableCell align="center">Heladera</StyledTableCell>
                        <StyledTableCell align="center">Viandas Retiradas</StyledTableCell>
                        <StyledTableCell align="center" sx={{pr: 3}}>Viandas Colocadas</StyledTableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows.map((row, index) => (
                        <StyledTableRow hover key={`${row.heladera}-${index}`}>
                            <StyledTableCell sx={{pl: 3}} component="th" scope="row">
                                {index + 1}
                            </StyledTableCell>
                            <StyledTableCell align="center">{row.heladera}</StyledTableCell>
                            <StyledTableCell align="center">{row.viandasRetiradas}</StyledTableCell>
                            <StyledTableCell align="center" sx={{pr: 3}}>
                                {row.viandasColocadas}
                            </StyledTableCell>
                        </StyledTableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    )
}