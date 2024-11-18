"use client";
import MainCard from "@/components/Cards/MainCard";
import {Box, Button, CardActions, Stack, Table, TableBody, TableContainer, TableHead, TableRow} from "@mui/material";
import {useTheme} from "@mui/material/styles";
import React from "react";
import Typography from "@mui/material/Typography";
import CardContent from "@mui/material/CardContent";
import {StyledTableCell} from "@components/Tables/StyledTableCell";
import {StyledTableRow} from "@components/Tables/StyledTableRow";

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

export default function IncidentesPage() {
    const theme = useTheme();

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
                    <Button color="primary" variant="contained" type={"submit"} disabled={false}>
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
            </CardContent>
        </MainCard>
    );
}