import {IIncidenteResponse} from "@models/responses/incidentes/iIncidenteResponse";
import {Table, TableBody, TableContainer, TableHead, TableRow} from "@mui/material";
import {StyledTableCell} from "@components/Tables/StyledTableCell";
import {StyledTableRow} from "@components/Tables/StyledTableRow";
import {formatDate} from "@utils/formatDate";
import React from "react";

export default function IncidentesTab({
                                             incidentes
                                      }:{
    incidentes: IIncidenteResponse[]
}){
    return (
        <TableContainer>
            <Table sx={{minWidth: 350}} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <StyledTableCell sx={{pl: 3}}>#</StyledTableCell>
                        <StyledTableCell align="center">Fecha</StyledTableCell>
                        <StyledTableCell align="center">Tipo</StyledTableCell>
                        <StyledTableCell align="center">Visitas Tecnicas</StyledTableCell>
                        <StyledTableCell align="center">Resuelto</StyledTableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {incidentes.map((row, index) => (
                        <StyledTableRow hover key={`${row.id}`}>
                            <StyledTableCell sx={{pl: 3}} component="th" scope="row">
                                {index + 1}
                            </StyledTableCell>
                            <StyledTableCell align="center">{formatDate(row.fecha)}</StyledTableCell>
                            <StyledTableCell align="center">{
                                row.tipoIncidente === 'Alerta' ? 'Alerta' : 'Falla Tecnica'
                            }</StyledTableCell>
                            <StyledTableCell align="center">{row.visitasTecnicas.length}</StyledTableCell>
                            <StyledTableCell sx={{pr: 3}} align="center">{
                                row.resuelto ? 'Resuelto' : 'No Resuelto'
                            }</StyledTableCell>
                        </StyledTableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    )
}