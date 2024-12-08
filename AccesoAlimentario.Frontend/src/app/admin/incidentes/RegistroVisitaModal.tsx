import {Backdrop, Button, CardActions, Divider, Fade, Modal, Stack} from "@mui/material";
import React from "react";
import MainCard from "@components/Cards/MainCard";
import CardContent from "@mui/material/CardContent";
import Grid from "@mui/material/Grid2";
import {CheckboxButtonGroup, Controller, FormContainer, TextFieldElement, useForm} from "react-hook-form-mui";
import {usePostVisitaHeladeraMutation} from "@redux/services/heladerasApi";
import {useNotification} from "@components/Notifications/NotificationContext";
import {FormFieldValue} from "@components/Forms/Form";
import {IRegistroVisitaHeladeraRequest} from "@models/requests/tecnicos/iRegistroVisitaHeladeraRequest";
import {MuiFileInput} from "mui-file-input";
import {useTheme} from "@mui/material/styles";
import {LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import {DatePickerElement} from "react-hook-form-mui/date-pickers";
import {useAppSelector} from "@redux/hook";

export default function RegistroVisitaModal({
                                                open,
                                                onClose,
                                                incidenteId,
                                            }: {
    open: boolean,
    onClose: () => void,
    incidenteId: string,
}) {
    const formContext = useForm();
    const [
        postVisitaHeladera,
        {isLoading: isPostLoading}
    ] = usePostVisitaHeladeraMutation();
    const {addNotification} = useNotification();
    const theme = useTheme();
    const user = useAppSelector(state => state.user);

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
        const request: IRegistroVisitaHeladeraRequest = {
            incidenteId: incidenteId,
            tecnicoId: user.tecnicoId,
            foto: data.imagen,
            fecha: data.fecha,
            comentario: data.comentario,
            resuelto: (data.resuelto as unknown as string[]).includes("Resuelto")
        };
        try {
            await postVisitaHeladera(request).unwrap();
            addNotification("Visita registrada correctamente", "success");
            formContext.reset();
            onClose();
        } catch {
            addNotification("Error al registrar la visita", "error");
        }
    }

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
                <MainCard modal darkTitle content={false} title={"Registrar visita"}
                          sx={{
                              width: {
                                  xs: "100%",
                                  sm: "80%",
                                  md: "60%",
                              }
                          }}>
                    <FormContainer
                        formContext={formContext}
                        onSuccess={handleSave}
                    >
                        <CardContent>
                            <Grid container spacing={3} alignItems="center">
                                <Grid size={12} key={"fecha"}>
                                    <LocalizationProvider dateAdapter={AdapterDateFns}>
                                        <DatePickerElement
                                            label={"Fecha de la visita"}
                                            name={"fecha"}
                                            required={true}
                                            rules={
                                                {
                                                    required: "Por favor ingrese una fecha"
                                                }
                                            }
                                            sx={{width: '100%'}}
                                        />
                                    </LocalizationProvider>
                                </Grid>
                                <Grid size={12} key={"comentario"}>
                                    <TextFieldElement
                                        name={"comentario"}
                                        label={"Comentario"}
                                        placeholder={"Comentario"}
                                        fullWidth
                                        rules={
                                            {
                                                required: "Por favor ingrese un comentario"
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
                                <Grid size={12} key={"resuelto"}>
                                    <CheckboxButtonGroup
                                        name={"resuelto"}
                                        label={"Resuelto"}
                                        options={[
                                            {label: "Resuelto", id: "Resuelto"},
                                        ]}
                                        row
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
                                <Button color="primary" variant="contained" type={"submit"} disabled={isPostLoading}>
                                    Registrar
                                </Button>
                            </Stack>
                        </CardActions>
                    </FormContainer>
                </MainCard>
            </Fade>
        </Modal>
    )
}