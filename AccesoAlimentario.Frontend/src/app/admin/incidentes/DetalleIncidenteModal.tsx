import {IIncidenteResponse} from "@models/responses/incidentes/iIncidenteResponse";
import {
    Backdrop,
    Button,
    Divider,
    Fade,
    Modal,
    Table,
    TableBody,
    TableContainer,
    TableHead,
    TableRow
} from "@mui/material";
import React from "react";
import MainCard from "@components/Cards/MainCard";
import CardContent from "@mui/material/CardContent";
import Grid from "@mui/material/Grid2";
import {IFallaTecnicaResponse} from "@models/responses/incidentes/iFallaTecnicaResponse";
import Typography from "@mui/material/Typography";
import {StyledTableCell} from "@components/Tables/StyledTableCell";
import {StyledTableRow} from "@components/Tables/StyledTableRow";
import {formatDate} from "@utils/formatDate";

export default function DetalleIncidenteModal({
                                                  open,
                                                  onClose,
                                                  incidente,
                                              }: {
    open: boolean,
    onClose: () => void,
    incidente: IIncidenteResponse,
}) {
    const [foto, setFoto] = React.useState<string | undefined>(undefined);
    return (
        <Modal
            open={open}
            onClose={onClose}
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
            <Fade in={open}>
                <MainCard modal darkTitle content={false} title={"Detalle Incidente"} sx={{
                    width: {
                        xs: "100%",
                        sm: "80%",
                        md: "60%",
                    }
                }}>
                    <CardContent>
                        <Grid container spacing={3} alignItems="center" direction={"row"}>
                            <Grid
                                size={{
                                    xs: 12,
                                    sm: incidente.tipoIncidente === 'FallaTecnica' ? 9 : 12
                                }}
                                key={"detalle"}
                                sx={{
                                    display: "flex",
                                    flexDirection: "column",
                                    justifyContent: "center",
                                    alignItems: "center",
                                    textAlign: "center"
                                }}
                            >
                                <Grid container spacing={3} alignItems="center">
                                    <Grid size={12} key={"fecha"}>
                                        <Typography variant="h5">
                                            <strong>Fecha:</strong> {incidente.fecha}
                                        </Typography>
                                    </Grid>
                                    <Grid size={12} key={"resuelto"}>
                                        <Typography variant="h5">
                                            <strong>Resuelto:</strong> {incidente.resuelto ? 'Si' : 'No'}
                                        </Typography>
                                    </Grid>
                                    <Grid size={12} key={"tipoIncidente"}>
                                        <Typography variant="h5">
                                            <strong>Tipo de
                                                incidente:</strong> {incidente.tipoIncidente === 'Alerta' ? 'Alerta' : 'Falla Tecnica'}
                                        </Typography>
                                    </Grid>
                                    {
                                        incidente.tipoIncidente === 'FallaTecnica' &&
                                        (
                                            <Grid size={12} key={"descripcion"}>
                                                <Typography variant="h5">
                                                    <strong>Descripcion:</strong> {(incidente as IFallaTecnicaResponse).descripcion}
                                                </Typography>
                                            </Grid>
                                        )
                                    }
                                </Grid>
                            </Grid>
                            {
                                incidente.tipoIncidente === 'FallaTecnica' &&
                                (
                                    <Grid size={{
                                        xs: 12,
                                        sm: 3
                                    }} key={"foto"}>
                                        {
                                            (incidente as IFallaTecnicaResponse).foto === "" || (incidente as IFallaTecnicaResponse).foto == null ? (
                                                <Typography variant="h6">
                                                    No se ha registrado una foto para esta falla
                                                </Typography>
                                            ) : (
                                                <img src={(incidente as IFallaTecnicaResponse).foto} alt="Foto de la falla" style={{width: "100%"}}/>
                                            )
                                        }
                                    </Grid>
                                )
                            }
                            {
                                incidente.visitasTecnicas.length > 0 ?
                                    (
                                        <>
                                            <Grid size={12} key={"divider"}>
                                                <Divider/>
                                            </Grid>
                                            <Grid size={9} key={"visitas"}>
                                                <TableContainer>
                                                    <Table sx={{minWidth: 350}} aria-label="simple table">
                                                        <TableHead>
                                                            <TableRow>
                                                                <StyledTableCell sx={{pl: 3}}>#</StyledTableCell>
                                                                <StyledTableCell align="center">Fecha
                                                                    Visita</StyledTableCell>
                                                                <StyledTableCell
                                                                    align="center">Tecnico</StyledTableCell>
                                                                <StyledTableCell
                                                                    align="center">Comentario</StyledTableCell>
                                                                <StyledTableCell align="center"
                                                                                 sx={{pr: 3}}>Acciones</StyledTableCell>
                                                            </TableRow>
                                                        </TableHead>
                                                        <TableBody>
                                                            {incidente.visitasTecnicas
                                                                .map((row, index) => (
                                                                    <StyledTableRow hover key={`${index}`}>
                                                                        <StyledTableCell sx={{pl: 3}} component="th"
                                                                                         scope="row">
                                                                            {index + 1}
                                                                        </StyledTableCell>
                                                                        <StyledTableCell
                                                                            align="center">{formatDate(row.fecha)}</StyledTableCell>
                                                                        <StyledTableCell
                                                                            align="center">{row.tecnico.persona.nombre}</StyledTableCell>
                                                                        <StyledTableCell
                                                                            align="center">{row.comentario}</StyledTableCell>
                                                                        <StyledTableCell align="center" sx={{pr: 3}}>
                                                                            <Button
                                                                                variant="contained"
                                                                                color="primary"
                                                                                size="small"
                                                                                sx={{minWidth: '30px'}}
                                                                                onClick={() => setFoto(row.foto)}
                                                                                disabled={row.foto === ""}
                                                                            >
                                                                                Ver Foto
                                                                            </Button>
                                                                        </StyledTableCell>
                                                                    </StyledTableRow>
                                                                ))}
                                                        </TableBody>
                                                    </Table>
                                                </TableContainer>
                                            </Grid>
                                            <Grid size={3} key={"acciones"}
                                                  sx={{display: "flex", justifyContent: "center"}}>
                                                {
                                                    foto ?
                                                        (
                                                            foto === "" ? (
                                                                <Typography variant="h6">
                                                                    No se ha registrado una foto para esta visita
                                                                </Typography>
                                                            ) : (
                                                                <img src={foto} alt="Foto de la visita"
                                                                     style={{width: "100%"}}/>
                                                            )
                                                        )
                                                        : (
                                                            <Typography variant="h6">
                                                                Seleccione una visita para ver la foto
                                                            </Typography>
                                                        )
                                                }
                                            </Grid>
                                        </>
                                    ) : (
                                        <Grid size={12} key={"visitas"}>
                                            <Typography variant="h5">
                                                <strong>No se han registrado visitas tecnicas</strong>
                                            </Typography>
                                        </Grid>
                                    )
                            }
                        </Grid>
                    </CardContent>
                </MainCard>
            </Fade>
        </Modal>
    )
}