import {IViandaResponse} from "@models/responses/heladeras/iViandaResponse";
import {Table, TableBody, TableContainer, TableHead, TableRow} from "@mui/material";
import {StyledTableCell} from "@components/Tables/StyledTableCell";
import {StyledTableRow} from "@components/Tables/StyledTableRow";
import React from "react";
import {formatDate} from "@utils/formatDate";
import {EstadoVianda} from "@models/enums/estadoVianda";

export default function ViandasTab({
                                       viandas
                                   }: {
    viandas: IViandaResponse[]
}) {
    return (
        <TableContainer>
            <Table sx={{minWidth: 350}} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <StyledTableCell sx={{pl: 3}}>#</StyledTableCell>
                        <StyledTableCell align="center">Comida</StyledTableCell>
                        <StyledTableCell align="center">Fecha Donacion</StyledTableCell>
                        <StyledTableCell align="center">Fecha Vencimiento</StyledTableCell>
                        <StyledTableCell align="center">Calorias</StyledTableCell>
                        <StyledTableCell align="center">Peso</StyledTableCell>
                        <StyledTableCell align="center">Estado</StyledTableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {viandas.map((row, index) => (
                        <StyledTableRow hover key={`${row.id}`}>
                            <StyledTableCell sx={{pl: 3}} component="th" scope="row">
                                {index + 1}
                            </StyledTableCell>
                            <StyledTableCell align="center">{row.comida}</StyledTableCell>
                            <StyledTableCell align="center">{formatDate(row.fechaDonacion)}</StyledTableCell>
                            <StyledTableCell align="center">{formatDate(row.fechaCaducidad)}</StyledTableCell>
                            <StyledTableCell align="center">{row.calorias}</StyledTableCell>
                            <StyledTableCell align="center">{row.peso}</StyledTableCell>
                            <StyledTableCell sx={{pr: 3}} align="center">{
                                row.estado === EstadoVianda.Disponible ? 'Disponible'
                                    : row.estado === EstadoVianda.Consumida ? 'Consumida'
                                        : row.estado === EstadoVianda.Caducada ? 'Caducada'
                                            : '-'
                            }</StyledTableCell>
                        </StyledTableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}