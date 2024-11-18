"use client";
import {
    Backdrop,
    Box,
    Button,
    CardActions, Divider, Fade, MenuItem, Modal, Select,
    Stack,
    Table,
    TableBody,
    TableContainer,
    TableHead,
    TableRow
} from "@mui/material";
import {StyledTableCell} from "@components/Tables/StyledTableCell";
import {StyledTableRow} from "@components/Tables/StyledTableRow";
import React from "react";
import MainCard from "@components/Cards/MainCard";
import Typography from "@mui/material/Typography";
import CardContent from "@mui/material/CardContent";
import {useTheme} from "@mui/material/styles";
import {FormContainer, useForm} from "react-hook-form-mui";
import {FormFieldValue} from "@components/Forms/Form";
import {DonacionMonetariaForm} from "@/app/admin/contribuciones/DonacionMonetariaForm";
import {DonacionViandasForm} from "@/app/admin/contribuciones/DonacionViandasForm";
import {OfertaPremioForm} from "@/app/admin/contribuciones/OfertaPremioForm";
import {AdministracionHeladera} from "@/app/admin/contribuciones/AdministracionHeladera";
import {DistribucionViandas} from "@/app/admin/contribuciones/DistribucionViandas";

function createData(name: string, calories: number, fat: number, carbs: number, protein: number) {
    return {name, calories, fat, carbs, protein};
}

const rows = [
    createData('Frozen yoghurt', 159, 6.0, 24, 4.0),
    createData('Ice cream sandwich', 237, 9.0, 37, 4.3),
    createData('Eclair', 262, 16.0, 24, 6.0),
    createData('Cupcake', 305, 3.7, 67, 4.3),
    createData('Gingerbread', 356, 16.0, 49, 3.9)
];

export default function ContribucionesPage() {
    const theme = useTheme();
    const [showModal, setShowModal] = React.useState(false);
    const [tipoContribucion, setTipoContribucion] = React.useState<"DonacionMonetaria" | "DonacionVianda" | "OfertaPremio" | "AdministracionHeladera" | "DistribucionViandas">("DonacionMonetaria");
    const formContext = useForm();
    const handleSave = async (data: FormFieldValue) => {
        console.log(data);
    };

    return (
        <MainCard content={false} sx={{overflow: 'visible'}}>
            <CardActions
                sx={{
                    position: 'sticky',
                    top: '60px',
                    bgcolor: theme.palette.background.default,
                    zIndex: 1,
                    borderBottom: `1px solid ${theme.palette.divider}`
                }}
            >
                <Stack direction="row" alignItems="center" justifyContent="space-between"
                       sx={{width: 1}}>
                    <Box component="div" sx={{flexGrow: 1, m: 0, pl: 1.5}}>
                        <Typography variant="h5" sx={{flexGrow: 1}}>
                            Contribuciones
                        </Typography>
                        <Typography variant="subtitle1" sx={{flexGrow: 1}}>
                            Puedes ver las contribuciones realizadas por los colaboradores
                        </Typography>
                    </Box>
                </Stack>
                <Stack direction="row" spacing={1} sx={{px: 1.5, py: 0.75}}>
                    <Button color="primary" variant="contained" onClick={() => setShowModal(true)}>
                        Crear
                    </Button>
                </Stack>
            </CardActions>
            <CardContent>
                <TableContainer>
                    <Table sx={{minWidth: 350}} aria-label="simple table">
                        <TableHead>
                            <TableRow>
                                <StyledTableCell sx={{pl: 3}}>Dessert (100g serving)</StyledTableCell>
                                <StyledTableCell align="right">Calories</StyledTableCell>
                                <StyledTableCell align="right">Fat&nbsp;(g)</StyledTableCell>
                                <StyledTableCell align="right">Carbs&nbsp;(g)</StyledTableCell>
                                <StyledTableCell align="right">Protein&nbsp;(g)</StyledTableCell>
                                <StyledTableCell align="right">Protein&nbsp;(g)</StyledTableCell>
                                <StyledTableCell align="right">Protein&nbsp;(g)</StyledTableCell>
                                <StyledTableCell align="right" sx={{pr: 3}}>
                                    Protein&nbsp;(g)
                                </StyledTableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {rows.map((row) => (
                                <StyledTableRow hover key={row.name}>
                                    <StyledTableCell sx={{pl: 3}} component="th" scope="row">
                                        {row.name}
                                    </StyledTableCell>
                                    <StyledTableCell align="right">{row.calories}</StyledTableCell>
                                    <StyledTableCell align="right">{row.fat}</StyledTableCell>
                                    <StyledTableCell align="right">{row.carbs}</StyledTableCell>
                                    <StyledTableCell align="right">{row.protein}</StyledTableCell>
                                    <StyledTableCell align="right">{row.protein}</StyledTableCell>
                                    <StyledTableCell align="right">{row.protein}</StyledTableCell>
                                    <StyledTableCell sx={{pr: 3}} align="right">
                                        {row.protein}
                                    </StyledTableCell>
                                </StyledTableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
                <Modal
                    open={showModal}
                    onClose={() => setShowModal(false)}
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
                    <Fade in={showModal}>
                        <MainCard modal darkTitle content={false} title={"Crear contribución"} sx={{width: "80%"}}>
                            <FormContainer
                                formContext={formContext}
                                onSuccess={handleSave}
                            >
                                <CardContent>
                                    <Select variant="outlined" value={tipoContribucion}
                                            onChange={(e) => {
                                                setTipoContribucion(e.target.value as "DonacionMonetaria" | "DonacionVianda" | "OfertaPremio" | "AdministracionHeladera" | "DistribucionViandas");
                                                formContext.reset();
                                            }} fullWidth sx={{mb: 2}}>
                                        <MenuItem value="DonacionMonetaria">Donación Monetaria</MenuItem>
                                        <MenuItem value="DonacionVianda">Donación de Viandas</MenuItem>
                                        <MenuItem value="OfertaPremio">Oferta de Premio</MenuItem>
                                        <MenuItem value="AdministracionHeladera">Administración de Heladera</MenuItem>
                                        <MenuItem value="DistribucionViandas">Distribución de Viandas</MenuItem>
                                    </Select>
                                    {
                                        tipoContribucion === "DonacionMonetaria" ? (
                                            <DonacionMonetariaForm/>
                                        ) : tipoContribucion === "DonacionVianda" ? (
                                            <DonacionViandasForm/>
                                        ) : tipoContribucion === "OfertaPremio" ? (
                                            <OfertaPremioForm/>
                                        ) : tipoContribucion === "AdministracionHeladera" ? (
                                            <AdministracionHeladera/>
                                        ) : tipoContribucion === "DistribucionViandas" ? (
                                            <DistribucionViandas/>
                                        ) : <></>
                                    }
                                </CardContent>
                                <Divider/>
                                <Stack direction="row" spacing={1} justifyContent="flex-end" sx={{px: 2.5, py: 2}}>
                                    <Button color="error" size="small" onClick={() => setShowModal(false)}>
                                        Cancelar
                                    </Button>
                                    <Button variant="contained" size="small">
                                        Enviar
                                    </Button>
                                </Stack>
                            </FormContainer>
                        </MainCard>
                    </Fade>
                </Modal>
            </CardContent>
        </MainCard>
    );
}