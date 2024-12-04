"use client";
import {CircularProgress, Stack, Table, TableBody, TableContainer, TableHead, TableRow} from "@mui/material";
import React from "react";
import {StyledTableCell} from "@components/Tables/StyledTableCell";
import {StyledTableRow} from "@components/Tables/StyledTableRow";
import {useGetReporteQuery} from "@redux/services/reportesApi";
import {TipoReporte} from "@models/enums/tipoReporte";

interface ICantidadDeColaboracionesPorColaboradorRow {
    Colaborador: string,
    CantidadViandas: number
}

export const CantidadDeColaboracionesPorColaboradorReport = () => {
    const {data, isLoading} = useGetReporteQuery({tipoReporte: TipoReporte.CANTIDAD_VIANDAS_POR_COLABORADOR});

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
                                    <StyledTableCell align="center">Colaborador</StyledTableCell>
                                    <StyledTableCell align="center" sx={{pr: 3}}>Viandas Donadas</StyledTableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {(JSON.parse(data?.cuerpo || "[]"))
                                    .sort((a: ICantidadDeColaboracionesPorColaboradorRow, b: ICantidadDeColaboracionesPorColaboradorRow) => b.CantidadViandas - a.CantidadViandas)
                                    .map((row: ICantidadDeColaboracionesPorColaboradorRow, index: number) => (
                                        <StyledTableRow hover key={`${index}`}>
                                            <StyledTableCell sx={{pl: 3}} component="th" scope="row">
                                                {index + 1}
                                            </StyledTableCell>
                                            <StyledTableCell align="center">{row.Colaborador}</StyledTableCell>
                                            <StyledTableCell align="center" sx={{pr: 3}}>
                                                {row.CantidadViandas}
                                            </StyledTableCell>
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