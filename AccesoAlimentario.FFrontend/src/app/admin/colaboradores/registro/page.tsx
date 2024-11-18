"use client";
import React from "react";
import {Box, Button, CardActions, Divider, MenuItem, Select, Stack} from "@mui/material";
import {FormContainer, useForm} from "react-hook-form-mui";
import Typography from "@mui/material/Typography";
import CardContent from "@mui/material/CardContent";
import {Form, FormFieldValue} from "@components/Forms/Form";
import MainCard from "@components/Cards/MainCard";
import {useTheme} from "@mui/material/styles";
import {RegistroPersonaFisica} from "@/app/admin/colaboradores/registro/RegistroPersonaFisica";
import {RegistroPersonaJuridica} from "@/app/admin/colaboradores/registro/RegistroPersonaJuridica";

export default function ColaboradoresRegistroPage() {
    const theme = useTheme();
    const [tipoColaborador, setTipoColaborador] = React.useState<"fisica" | "juridico">("fisica");
    const formContext = useForm();
    const handleSave = async (data: FormFieldValue) => {
        console.log(data);
    };

    return (
        <MainCard content={false} sx={{overflow: 'visible'}}>
            <FormContainer
                formContext={formContext}
                onSuccess={handleSave}
            >
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
                                Registro de Colaboradores
                            </Typography>
                            <Typography variant="subtitle1" sx={{flexGrow: 1}}>
                                Registrar un colaborador en el sistema, completando el formulario
                            </Typography>
                        </Box>
                    </Stack>
                    <Stack direction="row" spacing={1} sx={{px: 1.5, py: 0.75}}>
                        <Select variant="outlined" value={tipoColaborador}
                                onChange={(e) => {
                                    setTipoColaborador(e.target.value as "fisica" | "juridico");
                                    formContext.reset();
                                }}>
                            <MenuItem value="fisica">Persona Física</MenuItem>
                            <MenuItem value="juridico">Persona Jurídica</MenuItem>
                        </Select>
                    </Stack>
                </CardActions>
                <CardContent>
                    {
                        tipoColaborador === "fisica" ? (
                            <RegistroPersonaFisica/>
                        ) : (
                            <RegistroPersonaJuridica/>
                        )
                    }
                </CardContent>
                <Divider/>
                <CardActions sx={{
                    bgcolor: theme.palette.background.default,
                }}>
                    <Stack direction="row" spacing={1} justifyContent="center"
                           sx={{width: 1, px: 1.5, py: 0.75}}>
                        <Button color="primary" variant="contained" type={"submit"} disabled={false}>
                            Registrar
                        </Button>
                    </Stack>
                </CardActions>
            </FormContainer>
        </MainCard>
    );
}