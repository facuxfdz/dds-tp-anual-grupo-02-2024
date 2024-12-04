"use client";
import {CircularProgress, Stack, Table, TableBody, TableContainer, TableHead, TableRow} from "@mui/material";
import React from "react";
import {StyledTableCell} from "@components/Tables/StyledTableCell";
import {StyledTableRow} from "@components/Tables/StyledTableRow";
import {useGetReporteQuery} from "@redux/services/reportesApi";
import {TipoReporte} from "@models/enums/tipoReporte";

interface ICantidadDeViandasRetiradasColocadasPorHeladeraRow {
    Heladera: string,
    CantidadViandasRetiradas: number,
    CantidadViandasColocadas: number
}

export const CantidadDeViandasRetiradasColocadasPorHeladeraReport = () => {
    const {data, isLoading} = useGetReporteQuery({tipoReporte: TipoReporte.CANTIDAD_VIANDAS_RET_COL_POR_HELADERA});

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
                                    <StyledTableCell align="center">Heladera</StyledTableCell>
                                    <StyledTableCell align="center">Viandas Retiradas</StyledTableCell>
                                    <StyledTableCell align="center" sx={{pr: 3}}>Viandas Colocadas</StyledTableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {(JSON.parse(data?.cuerpo || "[]"))
                                    .sort((a: ICantidadDeViandasRetiradasColocadasPorHeladeraRow, b: ICantidadDeViandasRetiradasColocadasPorHeladeraRow) => b.CantidadViandasRetiradas - a.CantidadViandasRetiradas)
                                    .map((row: ICantidadDeViandasRetiradasColocadasPorHeladeraRow, index: number) => (
                                        <StyledTableRow hover key={`${index}`}>
                                            <StyledTableCell sx={{pl: 3}} component="th" scope="row">
                                                {index + 1}
                                            </StyledTableCell>
                                            <StyledTableCell align="center">{row.Heladera}</StyledTableCell>
                                            <StyledTableCell
                                                align="center">{row.CantidadViandasRetiradas}</StyledTableCell>
                                            <StyledTableCell align="center" sx={{pr: 3}}>
                                                {row.CantidadViandasColocadas}
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