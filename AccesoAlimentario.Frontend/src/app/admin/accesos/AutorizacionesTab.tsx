import {
    IAutorizacionManipulacionHeladeraResponse
} from "@models/responses/autorizaciones/iAutorizacionesManipulacionHeladeraResponse";
import {Table, TableBody, TableContainer, TableHead, TableRow} from "@mui/material";
import {StyledTableCell} from "@components/Tables/StyledTableCell";
import {StyledTableRow} from "@components/Tables/StyledTableRow";
import {formatDate} from "@utils/formatDate";
import React from "react";

export default function AutorizacionesTab({
                                              autorizaciones
                                          }: {
    autorizaciones: IAutorizacionManipulacionHeladeraResponse[]
}) {
    return (
        <TableContainer>
            <Table sx={{minWidth: 350}} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <StyledTableCell sx={{pl: 3}}>#</StyledTableCell>
                        <StyledTableCell align="center">Fecha Creacion</StyledTableCell>
                        <StyledTableCell align="center">Fecha Expiracion</StyledTableCell>
                        <StyledTableCell align="center" sx={{pr: 3}}>Heladera</StyledTableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {autorizaciones
                        .map((row, index) => (
                            <StyledTableRow hover key={`${index}`}>
                                <StyledTableCell sx={{pl: 3}} component="th" scope="row">
                                    {index + 1}
                                </StyledTableCell>
                                <StyledTableCell align="center">{formatDate(row.fechaCreacion, "yyyy-MM-dd HH:mm")}</StyledTableCell>
                                <StyledTableCell align="center">{formatDate(row.fechaExpiracion, "yyyy-MM-dd HH:mm")}</StyledTableCell>
                                <StyledTableCell align="center" sx={{pr: 3}}>
                                    {row.heladera.puntoEstrategico.nombre}
                                </StyledTableCell>
                            </StyledTableRow>
                        ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}