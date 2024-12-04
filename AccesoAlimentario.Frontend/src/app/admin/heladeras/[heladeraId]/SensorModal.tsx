import MainCard from "@components/Cards/MainCard";
import {useLazyGetSensorQuery} from "@redux/services/heladerasApi";
import {
    Backdrop,
    Button,
    CircularProgress,
    Divider, Modal,
    Stack,
    Table,
    TableBody,
    TableContainer,
    TableHead,
    TableRow
} from "@mui/material";
import React, {useEffect} from "react";
import CardContent from "@mui/material/CardContent";
import Grid from "@mui/material/Grid2"
import {StyledTableCell} from "@components/Tables/StyledTableCell";
import {StyledTableRow} from "@components/Tables/StyledTableRow";
import {formatDate} from "@utils/formatDate";
import {ISensorTemperaturaResponse} from "@models/responses/sensores/iSensorTemperaturaResponse";
import {ISensorMovimientoResponse} from "@models/responses/sensores/iSensorMovimientoResponse";

export default function SensorModal({
                                        sensorId,
                                        close,
                                        open
                                    }: {
    sensorId: string | null,
    close: () => void,
    open: boolean
}) {
    const [
        getSensor,
        {data: sensor, isLoading: sensorLoading}
    ] = useLazyGetSensorQuery();

    useEffect(() => {
        if (sensorId) {
            getSensor(sensorId);
        }
    }, [sensorId, getSensor]);

    if (!sensorId) {
        return <></>;
    }

    return (
        <Modal
            open={open}
            onClose={close}
            closeAfterTransition
            slots={{
                backdrop: Backdrop
            }}
            slotProps={{
                backdrop: {
                    timeout: 500
                }
            }}
        >
            <MainCard modal darkTitle content={false} title={"Registros Sensor"} sx={{width: "80%"}}>
                <CardContent>
                    {
                        sensorLoading ? (
                            <Grid container justifyContent="center">
                                <CircularProgress/>
                            </Grid>
                        ) : (
                            <TableContainer>
                                <Table sx={{minWidth: 350}} aria-label="simple table">
                                    <TableHead>
                                        <TableRow>
                                            <StyledTableCell sx={{pl: 3}}>#</StyledTableCell>
                                            <StyledTableCell align="center">Fecha</StyledTableCell>
                                            <StyledTableCell align="center">Valor</StyledTableCell>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {
                                            sensor?.tipo === 'Temperatura' ?
                                                (
                                                    (sensor! as ISensorTemperaturaResponse).registrosTemperatura.map((row, index) => (
                                                        <StyledTableRow hover key={`${row.id}`}>
                                                            <StyledTableCell sx={{pl: 3}} component="th" scope="row">
                                                                {index + 1}
                                                            </StyledTableCell>
                                                            <StyledTableCell
                                                                align="center">{formatDate(row.date)}</StyledTableCell>
                                                            <StyledTableCell align="center">
                                                                {row.temperatura}
                                                            </StyledTableCell>
                                                        </StyledTableRow>
                                                    )))
                                                :
                                                sensor?.tipo === 'Movimiento' ?
                                                    (
                                                        (sensor! as ISensorMovimientoResponse).registrosMovimiento.map((row, index) => (
                                                            <StyledTableRow hover key={`${row.id}`}>
                                                                <StyledTableCell sx={{pl: 3}} component="th"
                                                                                 scope="row">
                                                                    {index + 1}
                                                                </StyledTableCell>
                                                                <StyledTableCell
                                                                    align="center">{formatDate(row.date)}</StyledTableCell>
                                                                <StyledTableCell align="center">
                                                                    {row.movimiento ? 'Movimiento' : 'Sin movimiento'}
                                                                </StyledTableCell>
                                                            </StyledTableRow>
                                                        ))
                                                    ) : (<></>)
                                        }
                                    </TableBody>
                                </Table>
                            </TableContainer>
                        )
                    }
                </CardContent>
                <Divider/>
                <Stack direction="row" spacing={1} justifyContent="flex-end" sx={{px: 2.5, py: 2}}>
                    <Button color="error" size="small" onClick={close} variant={"contained"}>
                        Cerrar
                    </Button>
                </Stack>
            </MainCard>
        </Modal>

    );
}