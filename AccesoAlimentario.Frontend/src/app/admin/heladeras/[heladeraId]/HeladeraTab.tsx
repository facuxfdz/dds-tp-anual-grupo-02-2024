import {IHeladeraResponse} from "@models/responses/heladeras/iHeladeraResponse";
import Typography from "@mui/material/Typography";
import Grid from "@mui/material/Grid2";
import {LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import {DatePickerElement} from "react-hook-form-mui/date-pickers";
import {Backdrop, Button, Fade, Modal} from "@mui/material";
import {FormContainer, SelectElement, TextFieldElement, useForm} from "react-hook-form-mui";
import IconButton from "@mui/material/IconButton";
import React from "react";
import {useFieldArray} from "react-hook-form";
import {useTheme} from "@mui/material/styles";
import {EstadoHeladera} from "@models/enums/estadoHeladera";
import SensorModal from "@/app/admin/heladeras/[heladeraId]/SensorModal";

export default function HeladeraTab({
                                        heladera
                                    }: {
    heladera: IHeladeraResponse
}) {
    const theme = useTheme();
    const formContext = useForm({
        defaultValues: {
            id: heladera.id,
            puntoEstrategicoNombre: heladera.puntoEstrategico.nombre,
            puntoEstrategicoLongitud: heladera.puntoEstrategico.longitud,
            puntoEstrategicoLatitud: heladera.puntoEstrategico.latitud,
            puntoEstrategicoCalle: heladera.puntoEstrategico.direccion.calle,
            puntoEstrategicoNumero: heladera.puntoEstrategico.direccion.numero,
            puntoEstrategicoLocalidad: heladera.puntoEstrategico.direccion.localidad,
            puntoEstrategicoPiso: heladera.puntoEstrategico.direccion.piso,
            puntoEstrategicoDepartamento: heladera.puntoEstrategico.direccion.departamento,
            puntoEstrategicoCodigoPostal: heladera.puntoEstrategico.direccion.codigoPostal,
            estado: heladera.estado,
            fechaInstalacion: new Date(`${heladera.fechaInstalacion}Z`),
            temperaturaMinimaConfig: heladera.temperaturaMinimaConfig,
            temperaturaMaximaConfig: heladera.temperaturaMaximaConfig,
            sensores: heladera.sensores.map(sensor => {
                return {
                    sensorId: sensor.id,
                    tipo: sensor.tipo
                }
            }),
            modeloCapacidad: heladera.modelo.capacidad,
            modeloTemperaturaMinima: heladera.modelo.temperaturaMinima,
            modeloTemperaturaMaxima: heladera.modelo.temperaturaMaxima
        }
    });
    const [selectedSensorId, setSelectedSensorId] = React.useState<string | null>(null);
    const [openModal, setOpenModal] = React.useState<boolean>(false);

    const {fields: sensores} = useFieldArray({
        name: "sensores",
        control: formContext.control,
    });

    if (!heladera) {
        return <Typography variant="h6">No se encontró la heladera</Typography>
    }

    return (
        <FormContainer
            formContext={formContext}
        >
            <Grid container spacing={3} alignItems="center">
                <Grid size={12} key={"puntoEstrategico"}>
                    <Typography variant="h4" gutterBottom>
                        Punto estratégico
                    </Typography>
                </Grid>
                <Grid size={6} key={"puntoEstrategicoNombre"}>
                    <TextFieldElement
                        name={"puntoEstrategicoNombre"}
                        label={"Nombre del punto estratégico"}
                        placeholder={"Ingrese el nombre del punto estratégico"}
                        required={true}
                        disabled={true}
                        fullWidth
                        rules={
                            {
                                required: "Por favor ingrese el nombre del punto estratégico"
                            }
                        }
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                        }}
                    />
                </Grid>
                <Grid size={3} key={"puntoEstrategicoLongitud"}>
                    <TextFieldElement
                        name={"puntoEstrategicoLongitud"}
                        label={"Longitud"}
                        placeholder={"Ingrese la longitud"}
                        required={true}
                        disabled={true}
                        fullWidth
                        type="number"
                        rules={
                            {
                                required: "Por favor ingrese la longitud"
                            }
                        }
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                        }}
                    />
                </Grid>
                <Grid size={3} key={"puntoEstrategicoLatitud"}>
                    <TextFieldElement
                        name={"puntoEstrategicoLatitud"}
                        label={"Latitud"}
                        placeholder={"Ingrese la latitud"}
                        required={true}
                        disabled={true}
                        fullWidth
                        type="number"
                        rules={
                            {
                                required: "Por favor ingrese la latitud"
                            }
                        }
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                        }}
                    />
                </Grid>
                <Grid size={9} key={"puntoEstrategicoCalle"}>
                    <TextFieldElement
                        name={"puntoEstrategicoCalle"}
                        label={"Calle"}
                        placeholder={"Ingrese la calle"}
                        required={true}
                        disabled={true}
                        fullWidth
                        rules={
                            {
                                required: "Por favor ingrese la calle"
                            }
                        }
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                        }}
                    />
                </Grid>
                <Grid size={3} key={"puntoEstrategicoNumero"}>
                    <TextFieldElement
                        name={"puntoEstrategicoNumero"}
                        label={"Número"}
                        placeholder={"Ingrese el número"}
                        required={true}
                        disabled={true}
                        fullWidth
                        rules={
                            {
                                required: "Por favor ingrese el número"
                            }
                        }
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                        }}
                    />
                </Grid>
                <Grid size={4} key={"puntoEstrategicoLocalidad"}>
                    <TextFieldElement
                        name={"puntoEstrategicoLocalidad"}
                        label={"Localidad"}
                        placeholder={"Ingrese la localidad"}
                        required={true}
                        disabled={true}
                        fullWidth
                        rules={
                            {
                                required: "Por favor ingrese la localidad"
                            }
                        }
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                        }}
                    />
                </Grid>
                <Grid size={3} key={"puntoEstrategicoPiso"}>
                    <TextFieldElement
                        name={"puntoEstrategicoPiso"}
                        label={"Piso"}
                        placeholder={"Ingrese el piso"}
                        required={false}
                        disabled={true}
                        fullWidth
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                        }}
                    />
                </Grid>
                <Grid size={3} key={"puntoEstrategicoDepartamento"}>
                    <TextFieldElement
                        name={"puntoEstrategicoDepartamento"}
                        label={"Departamento"}
                        placeholder={"Ingrese el departamento"}
                        required={false}
                        disabled={true}
                        fullWidth
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                        }}
                    />
                </Grid>
                <Grid size={2} key={"puntoEstrategicoCodigoPostal"}>
                    <TextFieldElement
                        name={"puntoEstrategicoCodigoPostal"}
                        label={"Código Postal"}
                        placeholder={"Ingrese el código postal"}
                        required={true}
                        disabled={true}
                        fullWidth
                        rules={
                            {
                                required: "Por favor ingrese el código postal"
                            }
                        }
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                        }}
                    />
                </Grid>
                <Grid size={12} key={"estadoInstalacion"}>
                    <Typography variant="h4" gutterBottom>
                        Estado e instalación
                    </Typography>
                </Grid>
                <Grid size={6} key={"estado"}>
                    <SelectElement
                        name={"estado"}
                        label={"Estado"}
                        options={[
                            {label: "Activa", id: EstadoHeladera.Activa},
                            {label: "Desperfecto", id: EstadoHeladera.Desperfecto},
                            {label: "Fuera de servicio", id: EstadoHeladera.FueraServicio}
                        ]}
                        required={true}
                        disabled={true}
                        fullWidth
                        rules={
                            {
                                required: "Por favor seleccione un estado"
                            }
                        }
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                        }}
                    />
                </Grid>
                <Grid size={6} key={"fechaInstalacion"}>
                    <LocalizationProvider dateAdapter={AdapterDateFns}>
                        <DatePickerElement
                            label={"Fecha de instalación"}
                            name={"fechaInstalacion"}
                            required={true}
                            disabled={true}
                            rules={
                                {
                                    required: "Por favor ingrese una fecha de instalación"
                                }
                            }
                            sx={{
                                width: '100%',
                                "& .MuiInputBase-input.Mui-disabled": {
                                    WebkitTextFillColor: theme.palette.text.primary,
                                },
                            }}
                        />
                    </LocalizationProvider>
                </Grid>
                <Grid size={6} key={"temperaturaConfiguracion"}>
                    <Typography variant="h4" gutterBottom>
                        Temperatura de configuración
                    </Typography>
                </Grid>
                <Grid size={6} key={"temperaturaActual"}>
                    <Typography variant="h4" gutterBottom>
                        Actual: {heladera.temperaturaActual}°C
                    </Typography>
                </Grid>
                <Grid size={6} key={"temperaturaMinimaConfig"}>
                    <TextFieldElement
                        name={"temperaturaMinimaConfig"}
                        label={"Temperatura mínima de configuración"}
                        placeholder={"Ingrese la temperatura mínima de configuración"}
                        required={true}
                        disabled={true}
                        fullWidth
                        rules={
                            {
                                required: "Por favor ingrese la temperatura mínima de configuración"
                            }
                        }
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                        }}
                    />
                </Grid>
                <Grid size={6} key={"temperaturaMaximaConfig"}>
                    <TextFieldElement
                        name={"temperaturaMaximaConfig"}
                        label={"Temperatura máxima de configuración"}
                        placeholder={"Ingrese la temperatura máxima de configuración"}
                        required={true}
                        disabled={true}
                        fullWidth
                        type="number"
                        rules={
                            {
                                required: "Por favor ingrese la temperatura máxima de configuración"
                            }
                        }
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                        }}
                    />
                </Grid>
                <Grid size={12} key={"sensores"}>
                    <Typography variant="h4" gutterBottom>
                        Sensores
                    </Typography>
                </Grid>
                <Grid size={12} key={"sensoress"}>
                    {
                        sensores.map((sensor, index) => {
                            return (
                                <Grid container spacing={1} alignItems={"center"} mb={2} key={`sensor.${index}`}>
                                    <Grid size={5} key={`sensoresId`}>
                                        <TextFieldElement
                                            name={`sensores.${index}.sensorId`}
                                            label={"ID del sensor"}
                                            placeholder={"Ingrese el ID del sensor (GUID)"}
                                            required={true}
                                            disabled={true}
                                            fullWidth
                                            rules={
                                                {
                                                    required: "Por favor ingrese el ID del sensor"
                                                }
                                            }
                                            sx={{
                                                "& .MuiInputBase-input.Mui-disabled": {
                                                    WebkitTextFillColor: theme.palette.text.primary,
                                                },
                                            }}
                                        />
                                    </Grid>
                                    <Grid size={5} key={"sensoresTipo"}>
                                        <SelectElement
                                            name={`sensores.${index}.tipo`}
                                            label={"Tipo de sensor"}
                                            options={[
                                                {label: "Temperatura", id: "Temperatura"},
                                                {label: "Movimiento", id: "Movimiento"}
                                            ]}
                                            required={true}
                                            disabled={true}
                                            fullWidth
                                            rules={
                                                {
                                                    required: "Por favor seleccione un tipo de sensor"
                                                }
                                            }
                                            sx={{
                                                "& .MuiInputBase-input.Mui-disabled": {
                                                    WebkitTextFillColor: theme.palette.text.primary,
                                                },
                                            }}
                                        />
                                    </Grid>
                                    <Grid size={2} key={"sensoresVer"}>
                                        <Button variant="contained" color="primary" fullWidth size={"large"}
                                                onClick={
                                                    () => {
                                                        setSelectedSensorId(sensor.sensorId);
                                                        setOpenModal(true);
                                                    }
                                                }
                                        >
                                            Ver
                                        </Button>
                                    </Grid>
                                </Grid>
                            )
                        })
                    }
                </Grid>
                <Grid size={12} key={"modelo"}>
                    <Typography variant="h4" gutterBottom>
                        Modelo
                    </Typography>
                </Grid>
                <Grid size={4} key={"modeloCapacidad"}>
                    <TextFieldElement
                        name={"modeloCapacidad"}
                        label={"Capacidad del modelo"}
                        placeholder={"Ingrese la capacidad del modelo"}
                        required={true}
                        disabled={true}
                        fullWidth
                        type="number"
                        rules={
                            {
                                required: "Por favor ingrese la capacidad del modelo"
                            }
                        }
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                        }}
                    />
                </Grid>
                <Grid size={4} key={"modeloTemperaturaMinima"}>
                    <TextFieldElement
                        name={"modeloTemperaturaMinima"}
                        label={"Temperatura mínima del modelo"}
                        placeholder={"Ingrese la temperatura mínima del modelo"}
                        required={true}
                        disabled={true}
                        fullWidth
                        type="number"
                        rules={
                            {
                                required: "Por favor ingrese la temperatura mínima del modelo"
                            }
                        }
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                        }}
                    />
                </Grid>
                <Grid size={4} key={"modeloTemperaturaMaxima"}>
                    <TextFieldElement
                        name={"modeloTemperaturaMaxima"}
                        label={"Temperatura máxima del modelo"}
                        placeholder={"Ingrese la temperatura máxima del modelo"}
                        required={true}
                        disabled={true}
                        fullWidth
                        type="number"
                        rules={
                            {
                                required: "Por favor ingrese la temperatura máxima del modelo"
                            }
                        }
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                        }}
                    />
                </Grid>
            </Grid>
            <SensorModal
                sensorId={selectedSensorId}
                close={() => setOpenModal(false)}
                open={openModal}
            />
        </FormContainer>
    );
}