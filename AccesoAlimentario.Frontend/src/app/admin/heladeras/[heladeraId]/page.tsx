"use client";
import {
    Backdrop,
    Box,
    Button,
    CardActions,
    CircularProgress, Divider, Fade, MenuItem, Modal, Select,
    Stack, Tab, Tabs,
} from "@mui/material";
import Typography from "@mui/material/Typography";
import CardContent from "@mui/material/CardContent";
import MainCard from "@components/Cards/MainCard";
import React, {useState} from "react";
import {useTheme} from "@mui/material/styles";
import {useGetHeladeraQuery} from "@redux/services/heladerasApi";
import {usePostSuscribirseHeladeraMutation} from "@redux/services/colaboradoresApi";
import {FormContainer, TextFieldElement, useForm} from "react-hook-form-mui";
import {FormFieldValue} from "@components/Forms/Form";
import Grid from "@mui/material/Grid2";
import {TipoSuscripcion} from "@models/enums/tipoSuscripcion";
import {ISuscribirseHeladeraRequest} from "@models/requests/colaboradores/iSuscribirseHeladeraRequest";
import {useAppSelector} from "@redux/hook";
import {useNotification} from "@components/Notifications/NotificationContext";
import ViandasTab from "@/app/admin/heladeras/[heladeraId]/ViandasTab";
import HeladeraTab from "@/app/admin/heladeras/[heladeraId]/HeladeraTab";
import IncidentesTab from "@/app/admin/heladeras/[heladeraId]/IncidentesTab";

function TabPanel({children, value, index}: { children: React.ReactNode, value: number, index: number }) {
    return (
        <div role="tabpanel" hidden={value !== index} id={`simple-tabpanel-${index}`}
             aria-labelledby={`simple-tab-${index}`}>
            {value === index && <Box sx={{pt: 2}}>{children}</Box>}
        </div>
    );
}

export default function HeladeraPage({
                                         params
                                     }: {
    params: {
        heladeraId: string;
    }
}) {
    const theme = useTheme();
    const {
        data: heladera,
        isLoading: isHeladeraLoading,
    } = useGetHeladeraQuery({heladeraId: params.heladeraId});
    const [
        postSuscribirseHeladera,
        {isLoading: isSuscribirseLoading}
    ] = usePostSuscribirseHeladeraMutation();
    const formContext = useForm();
    const [showModal, setShowModal] = React.useState(false);
    const [tipoSuscripcion, setTipoSuscripcion] = React.useState<TipoSuscripcion>(TipoSuscripcion.Faltante);
    const user = useAppSelector(state => state.user);
    const {addNotification} = useNotification();
    const [value, setValue] = useState(0);
    const handleChange = (event: React.SyntheticEvent, newValue: number) => {
        setValue(newValue);
    };

    const handleSuscribirse = async (data: FormFieldValue) => {
        const request: ISuscribirseHeladeraRequest = {
            colaboradorId: user.colaboradorId,
            heladeraId: params.heladeraId,
            minimo: tipoSuscripcion === TipoSuscripcion.Faltante ? Number(data.minimo) : 0,
            maximo: tipoSuscripcion === TipoSuscripcion.Excedente ? Number(data.maximo) : 0,
            tipo: tipoSuscripcion,
        };
        try {
            await postSuscribirseHeladera(request).unwrap();
            addNotification("Suscripción creada correctamente", "success");
            formContext.reset();
            setShowModal(false);
        } catch {
            addNotification("Error al crear la suscripción", "error");
        }
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
                            Detalles de la heladera
                        </Typography>
                        <Typography variant="subtitle1" sx={{flexGrow: 1}}>
                            {heladera?.puntoEstrategico.nombre}
                        </Typography>
                    </Box>
                </Stack>
                <Stack direction="row" spacing={1} sx={{px: 1.5, py: 0.75}}>
                    {
                        user.colaboradorId && (
                            <Button color="primary" variant="contained" onClick={() => setShowModal(true)}
                                    disabled={isHeladeraLoading}>
                                Suscribirse
                            </Button>
                        )
                    }
                </Stack>
            </CardActions>
            <CardContent>
                {
                    isHeladeraLoading ? (
                        <Stack direction="row" spacing={1} justifyContent="center" sx={{py: 2}}>
                            <CircularProgress/>
                        </Stack>
                    ) : (
                        <Box sx={{width: '100%'}}>
                            <Box sx={{borderBottom: 1, borderColor: 'divider'}}>
                                <Tabs value={value} onChange={handleChange} aria-label="basic tabs example" centered>
                                    <Tab
                                        label="Resumen"
                                        icon={<i className="fa-duotone fa-solid fa-list fa-xl"></i>}
                                        iconPosition="start"
                                    />
                                    <Tab label="Viandas"
                                         icon={<i className="fa-duotone fa-regular fa-pot-food fa-xl"></i>}
                                         iconPosition="start"/>
                                    <Tab label="Incidentes"
                                         icon={<i className="fa-duotone fa-solid fa-triangle-exclamation fa-xl"/>}
                                         iconPosition="start"/>
                                </Tabs>
                                <TabPanel value={value} index={0}>
                                    {
                                        heladera ? (
                                            <HeladeraTab heladera={heladera}/>
                                        ) : (
                                            <Typography variant="h6">No se encontró la heladera</Typography>
                                        )
                                    }
                                </TabPanel>
                                <TabPanel value={value} index={1}>
                                    <ViandasTab viandas={heladera?.viandas ?? []}/>
                                </TabPanel>
                                <TabPanel value={value} index={2}>
                                    <IncidentesTab incidentes={heladera?.incidentes ?? []}/>
                                </TabPanel>
                            </Box>
                        </Box>
                    )
                }
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
                        <MainCard modal darkTitle content={false} title={"Crear suscripcion"} sx={{
                            width: {
                                xs: "100%",
                                sm: "80%",
                                md: "60%",
                            }
                        }}>
                            <FormContainer
                                formContext={formContext}
                                onSuccess={handleSuscribirse}
                            >
                                <CardContent>
                                    <Select variant="outlined" value={tipoSuscripcion}
                                            onChange={(e) => {
                                                setTipoSuscripcion(e.target.value as unknown as TipoSuscripcion);
                                                formContext.reset();
                                            }} fullWidth sx={{mb: 2}}>
                                        <MenuItem value={TipoSuscripcion.Faltante}>Faltante de Viandas</MenuItem>
                                        <MenuItem value={TipoSuscripcion.Excedente}>Excedente de Viandas</MenuItem>
                                        <MenuItem value={TipoSuscripcion.Incidente}>Incidente</MenuItem>
                                    </Select>
                                    <Grid container spacing={3} alignItems="center">
                                        {
                                            tipoSuscripcion === TipoSuscripcion.Faltante && (
                                                <Grid size={12} key={"minimo"}>
                                                    <TextFieldElement
                                                        name={"minimo"}
                                                        label={"Mínimo de viandas"}
                                                        placeholder={"Ingrese un mínimo de viandas"}
                                                        required={true}
                                                        fullWidth
                                                        type="number"
                                                        rules={
                                                            {
                                                                required: "Por favor ingrese un mínimo de viandas"
                                                            }
                                                        }
                                                    />
                                                </Grid>
                                            )
                                        }
                                        {
                                            tipoSuscripcion === TipoSuscripcion.Excedente && (
                                                <Grid size={12} key={"maximo"}>
                                                    <TextFieldElement
                                                        name={"maximo"}
                                                        label={"Máximo de viandas"}
                                                        placeholder={"Ingrese un máximo de viandas"}
                                                        required={true}
                                                        fullWidth
                                                        type="number"
                                                        rules={
                                                            {
                                                                required: "Por favor ingrese un máximo de viandas"
                                                            }
                                                        }
                                                    />
                                                </Grid>
                                            )
                                        }
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
                                        disabled={
                                            isSuscribirseLoading
                                        }
                                    >
                                        Suscribirse
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