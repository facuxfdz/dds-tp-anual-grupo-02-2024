import {IAccesoHeladeraResponse} from "@models/responses/autorizaciones/iAccesoHeladeraResponse";
import {Table, TableBody, TableContainer, TableHead, TableRow} from "@mui/material";
import {StyledTableCell} from "@components/Tables/StyledTableCell";
import {StyledTableRow} from "@components/Tables/StyledTableRow";
import React from "react";
import {formatDate} from "@utils/formatDate";
import {TipoAcceso} from "@models/enums/tipoAcceso";

export const AccesosTab = ({
                               accesos
                           }: {
    accesos: IAccesoHeladeraResponse[]
}) => {
    return (
        <TableContainer>
            <Table sx={{minWidth: 350}} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <StyledTableCell sx={{pl: 3}}>#</StyledTableCell>
                        <StyledTableCell align="center">Fecha Acceso</StyledTableCell>
                        <StyledTableCell align="center">Tipo Acceso</StyledTableCell>
                        <StyledTableCell align="center">Heladera</StyledTableCell>
                        <StyledTableCell align="center" sx={{pr: 3}}>Autorizacion</StyledTableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {accesos
                        .map((row, index) => (
                            <StyledTableRow hover key={`${index}`}>
                                <StyledTableCell sx={{pl: 3}} component="th" scope="row">
                                    {index + 1}
                                </StyledTableCell>
                                <StyledTableCell align="center">{formatDate(row.fechaAcceso)}</StyledTableCell>
                                <StyledTableCell align="center">{
                                    row.tipoAcceso === TipoAcceso.IngresoVianda ? "Ingreso Vianda" : "Retiro Vianda"
                                }</StyledTableCell>
                                <StyledTableCell align="center">{row.heladera.puntoEstrategico.nombre}</StyledTableCell>
                                <StyledTableCell align="center" sx={{pr: 3}}>
                                    {row.autorizacion ? "Autorizado" : "No Autorizado"}
                                </StyledTableCell>
                            </StyledTableRow>
                        ))}
                </TableBody>
            </Table>
        </TableContainer>
    )
}