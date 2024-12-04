"use client";

import {useGetAccesosQuery, usePostSolicitarAccesoHeladeraMutation} from "@redux/services/colaboradoresApi";
import {useAppSelector} from "@redux/hook";
import {
    Backdrop,
    Box,
    Button,
    CardActions,
    CircularProgress, Divider,
    Fade,
    MenuItem, Modal,
    Select,
    Stack,
    Tab,
    Tabs
} from "@mui/material";
import Typography from "@mui/material/Typography";
import React, {useState} from "react";
import MainCard from "@components/Cards/MainCard";
import {useTheme} from "@mui/material/styles";
import CardContent from "@mui/material/CardContent";
import {AccesosTab} from "@/app/admin/accesos/AccesosTab";
import AutorizacionesTab from "@/app/admin/accesos/AutorizacionesTab";
import {FormContainer, SelectElement, useForm} from "react-hook-form-mui";
import {DonacionMonetariaForm} from "@/app/admin/contribuciones/DonacionMonetariaForm";
import {DonacionViandasForm} from "@/app/admin/contribuciones/DonacionViandasForm";
import {OfertaPremioForm} from "@/app/admin/contribuciones/OfertaPremioForm";
import {AdministracionHeladeraForm} from "@/app/admin/contribuciones/AdministracionHeladeraForm";
import {DistribucionViandasForm} from "@/app/admin/contribuciones/DistribucionViandasForm";
import {FormFieldValue, IFormField} from "@components/Forms/Form";
import {
    ISolicitarAutorizacionAperturaDeHeladeraRequest
} from "@models/requests/colaboradores/iSolicitarAutorizacionAperturaDeHeladeraRequest";
import {useNotification} from "@components/Notifications/NotificationContext";
import {useGetHeladerasQuery} from "@redux/services/heladerasApi";
import Grid from "@mui/material/Grid2";
import {ContribucionesTipo} from "@models/enums/contribucionesTipo";

function TabPanel({children, value, index}: { children: React.ReactNode, value: number, index: number }) {
    return (
        <div role="tabpanel" hidden={value !== index} id={`simple-tabpanel-${index}`}
             aria-labelledby={`simple-tab-${index}`}>
            {value === index && <Box sx={{pt: 2}}>{children}</Box>}
        </div>
    );
}

export default function AccesosPage() {
    const theme = useTheme();
    const user = useAppSelector(state => state.user);
    const [value, setValue] = useState(0);
    const handleChange = (event: React.SyntheticEvent, newValue: number) => {
        setValue(newValue);
    };
    const {data, isLoading} = useGetAccesosQuery(user.colaboradorId);
    const [showModal, setShowModal] = React.useState(false);
    const [
        solicitarAccesoHeladera,
        {isLoading: isLoadingAccesoHeladera}
    ] = usePostSolicitarAccesoHeladeraMutation();
    const {addNotification} = useNotification();
    const formContext = useForm();
    const {data: heladeras} = useGetHeladerasQuery();

    const handleSave = async (data: FormFieldValue) => {
        const request: ISolicitarAutorizacionAperturaDeHeladeraRequest = {
            heladeraId: data.heladeraId,
            tarjetaId: user.tarjetaColaboracionId,
        };

        try {
            await solicitarAccesoHeladera(request).unwrap();
            addNotification("Se ha enviado la solicitud de acceso a la heladera", "success");
            formContext.reset();
            setShowModal(false);
        } catch {
            addNotification("No se ha podido enviar la solicitud de acceso a la heladera", "error");
        }
    }

    if (isLoading) {
        return (
            <Box sx={{display: "flex", justifyContent: "center"}}>
                <CircularProgress/>
            </Box>
        )
    }

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
                            Accesos y Autorizaciones
                        </Typography>
                    </Box>
                </Stack>
                <Stack direction="row" spacing={1} sx={{px: 1.5, py: 0.75}}>
                    <Button color="primary" variant="contained" onClick={() => setShowModal(true)} size={"small"}>
                        Solicitar Autorización
                    </Button>
                </Stack>
            </CardActions>
            <CardContent>
                <Box sx={{borderBottom: 1, borderColor: 'divider'}}>
                    <Tabs value={value} onChange={handleChange} aria-label="basic tabs example" centered>
                        <Tab
                            label="Accesos"
                            icon={<i className="fa-sharp-duotone fa-light fa-door-open fa-xl"></i>}
                            iconPosition="start"
                        />
                        <Tab label="Autorizaciones"
                             icon={<i className="fa-sharp-duotone fa-regular fa-key fa-xl"></i>} iconPosition="start"/>
                    </Tabs>
                </Box>
                <TabPanel value={value} index={0}>
                    <AccesosTab accesos={data?.accesos || []}/>
                </TabPanel>
                <TabPanel value={value} index={1}>
                    <AutorizacionesTab autorizaciones={data?.autorizaciones || []}/>
                </TabPanel>
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
                        <MainCard modal darkTitle content={false} title={"Solicitar autorizacion"} sx={{width: "80%"}}>
                            <FormContainer
                                formContext={formContext}
                                onSuccess={handleSave}
                            >
                                <CardContent>
                                    <Grid container spacing={3} alignItems="center">
                                        <Grid size={12} key={"heladeraId"}>
                                            <SelectElement
                                                name={"heladeraId"}
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
                                    </Grid>
                                </CardContent>
                                <Divider/>
                                <Stack direction="row" spacing={1} justifyContent="flex-end" sx={{px: 2.5, py: 2}}>
                                    <Button color="error" size="small" onClick={() => setShowModal(false)}>
                                        Cancelar
                                    </Button>
                                    <Button
                                        variant="contained"
                                        size="small"
                                        type="submit"
                                        disabled={isLoadingAccesoHeladera}
                                    >
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