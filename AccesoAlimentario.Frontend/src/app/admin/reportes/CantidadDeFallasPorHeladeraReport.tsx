"use client";
import React from "react";
import {CircularProgress, Stack, Table, TableBody, TableContainer, TableHead, TableRow} from "@mui/material";
import {StyledTableCell} from "@components/Tables/StyledTableCell";
import {StyledTableRow} from "@components/Tables/StyledTableRow";
import {useGetReporteQuery} from "@redux/services/reportesApi";
import {TipoReporte} from "@models/enums/tipoReporte";


interface ICantidadDeFallasPorHeladeraRow {
    Heladera: string,
    CantidadFallas: number
}

export const CantidadDeFallasPorHeladeraReport = () => {
    const {data, isLoading} = useGetReporteQuery({tipoReporte: TipoReporte.CANTIDAD_FALLAS_POR_HELADERA});

    return (
        <>
            {
                isLoading ? (
                    <Stack direction="row" justifyContent="center">
                        <CircularProgress/>
                    </Stack>
                ) : (
                    <TableContainer>
                        <Table sx={{minWidth: 350}} aria-label="simple table">
                            <TableHead>
                                <TableRow>
                                    <StyledTableCell sx={{pl: 3}}>#</StyledTableCell>
                                    <StyledTableCell align="center">Nombre</StyledTableCell>
                                    <StyledTableCell align="center" sx={{pr: 3}}>Cantidad de Fallas</StyledTableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {(JSON.parse(data?.cuerpo || "[]"))
                                    .sort((a: ICantidadDeFallasPorHeladeraRow, b: ICantidadDeFallasPorHeladeraRow) => b.CantidadFallas - a.CantidadFallas)
                                    .map((row: ICantidadDeFallasPorHeladeraRow, index: number) => (
                                    <StyledTableRow hover key={`${index}`}>
                                        <StyledTableCell sx={{pl: 3}} component="th" scope="row">
                                            {index + 1}
                                        </StyledTableCell>
                                        <StyledTableCell align="center">{row.Heladera}</StyledTableCell>
                                        <StyledTableCell align="center" sx={{pr: 3}}>{row.CantidadFallas}</StyledTableCell>
                                    </StyledTableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                )
            }
        </>
    )
}