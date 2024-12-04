"use client";
import React from "react";
import MainCard from "@components/Cards/MainCard";
import {useTheme} from "@mui/material/styles";
import {Box, Button, CardActions, Divider, Stack} from "@mui/material";
import Typography from "@mui/material/Typography";
import CardContent from "@mui/material/CardContent";
import {FormFieldValue} from "@components/Forms/Form";
import {Controller, FormContainer, SelectElement, TextFieldElement, useForm} from "react-hook-form-mui";
import {useGetHeladerasQuery} from "@redux/services/heladerasApi";
import Grid from "@mui/material/Grid2";
import {MuiFileInput} from "mui-file-input";
import {usePostReportarFallaTecnicaMutation} from "@redux/services/colaboradoresApi";
import {IReportarFallaTecnicaRequest} from "@models/requests/colaboradores/iReportarFallaTecnicaRequest";
import {useAppSelector} from "@redux/hook";
import {useNotification} from "@components/Notifications/NotificationContext";

export default function ReportarIncidenciaPage() {
    const theme = useTheme();
    const user = useAppSelector(state => state.user);
    const formContext = useForm();
    const {data: heladeras} = useGetHeladerasQuery();
    const [
        postReportarFallaTecnica,
        {isLoading: isSaving}
    ] = usePostReportarFallaTecnicaMutation();
    const {addNotification} = useNotification();

    const handleFileChange = (newFile: File | null) => {
        if (!newFile) {
            formContext.setValue("imagen", null);
            return;
        }
        const reader = new FileReader();
        reader.onloadend = () => {
            formContext.setValue("imagen", reader.result);
        };
        reader.readAsDataURL(newFile as Blob);
    };

    const handleSave = async (data: FormFieldValue) => {
        const request: IReportarFallaTecnicaRequest = {
            fecha: new Date().toISOString(),
            reporteroId: user.colaboradorId,
            heladeraId: data.heladera,
            descripcion: data.descripcion,
            foto: data.imagen
        };

        try {
            await postReportarFallaTecnica(request).unwrap();
            addNotification("Incidencia reportada correctamente", "success");
            formContext.reset();
        } catch {
            addNotification("Error al reportar la incidencia", "error");
        }
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
                                Reportar Incidencia
                            </Typography>
                            <Typography variant="subtitle1" sx={{flexGrow: 1}}>
                                Reporta una incidencia en el sistema, completando el formulario
                            </Typography>
                        </Box>
                        <Stack direction="row" spacing={1} justifyContent="center">
                            <Button color="error" variant="contained" onClick={() => {
                                formContext.reset();
                            }}>
                                Reiniciar
                            </Button>
                        </Stack>
                    </Stack>
                </CardActions>
                <CardContent>
                    <Grid container spacing={3} alignItems="center">
                        <Grid size={12} key={"heladera"}>
                            <SelectElement
                                name={"heladera"}
                                label={"Heladera"}
                                options={
                                    (heladeras ?? []).map(heladera => {
                                        return {
                                            label: heladera.puntoEstrategico.nombre,
                                            id: heladera.id
                                        }
                                    })
                                }
                                required={true}
                                fullWidth
                                rules={
                                    {
                                        required: "Por favor seleccione una opción"
                                    }
                                }
                            />
                        </Grid>
                        <Grid size={12} key={"descripcion"}>
                            <TextFieldElement
                                name={"descripcion"}
                                label={"Descripción"}
                                placeholder={"Descripción"}
                                fullWidth
                                rules={
                                    {
                                        required: "Por favor ingrese una descripción"
                                    }
                                }
                            />
                        </Grid>
                        <Grid size={12} key={"imagen"}>
                            <Controller
                                name="imagen"
                                render={({field, fieldState}) => (
                                    <MuiFileInput
                                        {...field}
                                        onChange={(newFile) => {
                                            handleFileChange(newFile);
                                        }}
                                        label="Imagen"
                                        placeholder="Seleccione una imagen"
                                        inputProps={{accept: "image/*"}}
                                        error={!!fieldState.error}
                                        helperText={fieldState.error?.message}
                                        getInputText={(value) => value ? 'Imagen seleccionada' : 'Seleccione una imagen'}
                                        fullWidth
                                        clearIconButtonProps={{
                                            title: "Remove",
                                            children: <i className="fa-sharp fa-solid fa-xmark"/>
                                        }}
                                    />
                                )}
                            />
                        </Grid>
                    </Grid>
                </CardContent>
                <Divider/>
                <CardActions sx={{
                    bgcolor: theme.palette.background.default,
                }}>
                    <Stack direction="row" spacing={1} justifyContent="center"
                           sx={{width: 1, px: 1.5, py: 0.75}}>
                        <Button color="primary" variant="contained" type={"submit"} disabled={isSaving}>
                            Reportar
                        </Button>
                    </Stack>
                </CardActions>
            </FormContainer>
        </MainCard>
    );
}