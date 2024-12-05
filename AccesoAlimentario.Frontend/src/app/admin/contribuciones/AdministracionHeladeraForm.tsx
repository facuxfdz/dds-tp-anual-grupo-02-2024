import React, {useEffect, useRef,} from "react";
import {Box, Button, Modal, Stack, TextField} from "@mui/material";
import MainCard from "@components/Cards/MainCard";
import CardContent from "@mui/material/CardContent";
import IconButton from "@mui/material/IconButton";
import {
    useGetRecomendacionesUbicacionHeladeraQuery,
} from "@redux/services/serviciosApi";
import {useNotification} from "@components/Notifications/NotificationContext";
import {useFieldArray, useFormContext} from "react-hook-form";
import dynamic from 'next/dynamic'
import Grid from "@mui/material/Grid2";
import {LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import {DatePickerElement} from "react-hook-form-mui/date-pickers";
import {SelectElement, TextFieldElement} from "react-hook-form-mui";
import Typography from "@mui/material/Typography";
import {v4 as uuidv4} from 'uuid';
import {useTheme} from "@mui/material/styles";
import {EstadoHeladera} from "@models/enums/estadoHeladera";

const Map = dynamic(() => import('./Map'), {
    ssr: false,
})

export const AdministracionHeladeraForm = ({
                                               onlyView = false,
                                               edit = false
                                           }: {
    onlyView?: boolean,
    edit?: boolean
}) => {
    const theme = useTheme();
    const [open, setOpen] = React.useState(false);
    const [latitud, setLatitud] = React.useState<number>(-34.56);
    const [longitud, setLongitud] = React.useState<number>(-58.45);
    const [radio, setRadio] = React.useState<number>(10);
    const {data, error, isLoading, refetch} = useGetRecomendacionesUbicacionHeladeraQuery({
        latitud: latitud,
        longitud: longitud,
        radio: radio
    });
    const {addNotification} = useNotification();
    const [errorInformed, setErrorInformed] = React.useState(false);
    const mapRef = useRef<L.Map>(null);
    const {setValue} = useFormContext();
    const {fields: sensores, append, remove} = useFieldArray({
        name: "sensores",
    });

    useEffect(() => {
        if (error && !errorInformed) {
            addNotification("Error al obtener las recomendaciones de ubicación", "error");
            setErrorInformed(true);
        }
    }, [addNotification, error, errorInformed]);

    const moveToLocation = (lat: number, lng: number) => {
        if (mapRef.current) {
            mapRef.current.flyTo([lat, lng], 13);
        }
    };

    return (
        <>
            <Grid container spacing={3} alignItems="center">
                <Grid size={12} key={"fechaContribucion"}>
                    <LocalizationProvider dateAdapter={AdapterDateFns}>
                        <DatePickerElement
                            label={"Fecha de la donación"}
                            name={"fechaContribucion"}
                            required={true}
                            rules={
                                {
                                    required: "Por favor ingrese una fecha"
                                }
                            }
                            disabled={onlyView}
                            sx={{
                                width: '100%',
                                "& .MuiInputBase-input.Mui-disabled": {
                                    WebkitTextFillColor: theme.palette.text.primary,
                                },
                                "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                                    color: theme.palette.text.secondary,
                                },
                            }}
                        />
                    </LocalizationProvider>
                </Grid>
                <Grid size={9} key={"puntoEstrategico"}>
                    <Typography variant="h4" gutterBottom>
                        Punto estratégico
                    </Typography>
                </Grid>
                {
                    !onlyView && (
                        <Grid size={3} key={"puntoEstrategicoBuscar"}>
                            <Button variant="contained" color="primary" onClick={() => setOpen(true)} sx={{
                                width: "100%",
                                mb: 2
                            }}>
                                Solicitar puntos de colocación
                            </Button>
                        </Grid>
                    )
                }
                <Grid size={6} key={"puntoEstrategicoNombre"}>
                    <TextFieldElement
                        name={"puntoEstrategicoNombre"}
                        label={"Nombre del punto estratégico"}
                        placeholder={"Ingrese el nombre del punto estratégico"}
                        required={true}
                        fullWidth
                        rules={
                            {
                                required: "Por favor ingrese el nombre del punto estratégico"
                            }
                        }
                        disabled={onlyView && !edit}
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                            "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                                color: theme.palette.text.secondary,
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
                        fullWidth
                        type="number"
                        rules={
                            {
                                required: "Por favor ingrese la longitud"
                            }
                        }
                        disabled={onlyView && !edit}
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                            "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                                color: theme.palette.text.secondary,
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
                        fullWidth
                        type="number"
                        rules={
                            {
                                required: "Por favor ingrese la latitud"
                            }
                        }
                        disabled={onlyView && !edit}
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                            "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                                color: theme.palette.text.secondary,
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
                        fullWidth
                        rules={
                            {
                                required: "Por favor ingrese la calle"
                            }
                        }
                        disabled={onlyView && !edit}
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                            "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                                color: theme.palette.text.secondary,
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
                        fullWidth
                        rules={
                            {
                                required: "Por favor ingrese el número"
                            }
                        }
                        disabled={onlyView && !edit}
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                            "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                                color: theme.palette.text.secondary,
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
                        fullWidth
                        rules={
                            {
                                required: "Por favor ingrese la localidad"
                            }
                        }
                        disabled={onlyView && !edit}
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                            "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                                color: theme.palette.text.secondary,
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
                        fullWidth
                        disabled={onlyView && !edit}
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                            "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                                color: theme.palette.text.secondary,
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
                        fullWidth
                        disabled={onlyView && !edit}
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                            "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                                color: theme.palette.text.secondary,
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
                        fullWidth
                        rules={
                            {
                                required: "Por favor ingrese el código postal"
                            }
                        }
                        disabled={onlyView && !edit}
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                            "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                                color: theme.palette.text.secondary,
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
                        fullWidth
                        rules={
                            {
                                required: "Por favor seleccione un estado"
                            }
                        }
                        disabled={onlyView && !edit}
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                            "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                                color: theme.palette.text.secondary,
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
                            rules={
                                {
                                    required: "Por favor ingrese una fecha de instalación"
                                }
                            }
                            disabled={onlyView}
                            sx={{
                                width: '100%',
                                "& .MuiInputBase-input.Mui-disabled": {
                                    WebkitTextFillColor: theme.palette.text.primary,
                                },
                                "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                                    color: theme.palette.text.secondary,
                                },
                            }}
                        />
                    </LocalizationProvider>
                </Grid>
                <Grid size={12} key={"temperaturaConfiguracion"}>
                    <Typography variant="h4" gutterBottom>
                        Temperatura de configuración
                    </Typography>
                </Grid>
                <Grid size={6} key={"temperaturaMinimaConfig"}>
                    <TextFieldElement
                        name={"temperaturaMinimaConfig"}
                        label={"Temperatura mínima de configuración"}
                        placeholder={"Ingrese la temperatura mínima de configuración"}
                        required={true}
                        fullWidth
                        rules={
                            {
                                required: "Por favor ingrese la temperatura mínima de configuración"
                            }
                        }
                        disabled={onlyView && !edit}
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                            "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                                color: theme.palette.text.secondary,
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
                        fullWidth
                        type="number"
                        rules={
                            {
                                required: "Por favor ingrese la temperatura máxima de configuración"
                            }
                        }
                        disabled={onlyView && !edit}
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                            "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                                color: theme.palette.text.secondary,
                            },
                        }}
                    />
                </Grid>
                <Grid size={9} key={"sensores"}>
                    <Typography variant="h4" gutterBottom>
                        Sensores
                    </Typography>
                </Grid>
                {
                    (!onlyView || edit) && (
                        <Grid size={3} key={"sensoresAgregar"}>
                            <Button
                                variant="contained"
                                color="primary"
                                onClick={() => append({id: uuidv4(), tipo: "Temperatura"})}
                                sx={{width: "100%", mb: 2}}
                            >
                                Agregar sensor
                            </Button>
                        </Grid>
                    )
                }
                <Grid size={12} key={"sensoress"}>
                    {
                        sensores.map((sensor, index) => {
                            return (
                                <Grid container spacing={1} alignItems={"center"} mb={2} key={`sensor.${index}`}>
                                    <Grid size={6} key={`sensoresId${index}`}>
                                        <TextFieldElement
                                            name={`sensores[${index}].id`}
                                            label={"ID del sensor"}
                                            placeholder={"Ingrese el ID del sensor (GUID)"}
                                            required={true}
                                            fullWidth
                                            rules={
                                                {
                                                    required: "Por favor ingrese el ID del sensor"
                                                }
                                            }
                                            disabled={onlyView && !edit}
                                            sx={{
                                                "& .MuiInputBase-input.Mui-disabled": {
                                                    WebkitTextFillColor: theme.palette.text.primary,
                                                },
                                                "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                                                    color: theme.palette.text.secondary,
                                                },
                                            }}
                                        />
                                    </Grid>
                                    <Grid size={5} key={"sensoresTipo"}>
                                        <SelectElement
                                            name={`sensores[${index}].tipo`}
                                            label={"Tipo de sensor"}
                                            options={[
                                                {label: "Temperatura", id: "Temperatura"},
                                                {label: "Movimiento", id: "Movimiento"}
                                            ]}
                                            required={true}
                                            fullWidth
                                            rules={
                                                {
                                                    required: "Por favor seleccione un tipo de sensor"
                                                }
                                            }
                                            disabled={onlyView && !edit}
                                            sx={{
                                                "& .MuiInputBase-input.Mui-disabled": {
                                                    WebkitTextFillColor: theme.palette.text.primary,
                                                },
                                                "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                                                    color: theme.palette.text.secondary,
                                                },
                                            }}
                                        />
                                    </Grid>
                                    {
                                        (!onlyView || edit) && (
                                            <Grid size={1} key={"sensoresEliminar"} textAlign={"center"}>
                                                <IconButton onClick={() => remove(index)}>
                                                    <i className="fa-duotone fa-solid fa-trash"/>
                                                </IconButton>
                                            </Grid>
                                        )
                                    }
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
                        fullWidth
                        type="number"
                        rules={
                            {
                                required: "Por favor ingrese la capacidad del modelo"
                            }
                        }
                        disabled={onlyView && !edit}
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                            "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                                color: theme.palette.text.secondary,
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
                        fullWidth
                        type="number"
                        rules={
                            {
                                required: "Por favor ingrese la temperatura mínima del modelo"
                            }
                        }
                        disabled={onlyView && !edit}
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                            "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                                color: theme.palette.text.secondary,
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
                        fullWidth
                        type="number"
                        rules={
                            {
                                required: "Por favor ingrese la temperatura máxima del modelo"
                            }
                        }
                        disabled={onlyView && !edit}
                        sx={{
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                            "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                                color: theme.palette.text.secondary,
                            },
                        }}
                    />
                </Grid>
            </Grid>


            <Modal open={open} onClose={() => setOpen(false)}>
                <MainCard modal darkTitle content={false} title={"Crear contribución"} sx={{width: "80%"}}>
                    <CardContent>
                        <Stack direction="row" justifyContent={"space-between"} sx={{py: 2}}>
                            <TextField label="Latitud" type="number" value={latitud}
                                       onChange={(e) => setLatitud(parseFloat(e.target.value))}/>
                            <TextField label="Longitud" type="number" value={longitud}
                                       onChange={(e) => setLongitud(parseFloat(e.target.value))}/>
                            <TextField label="Radio" type="number" value={radio}
                                       onChange={(e) => setRadio(parseFloat(e.target.value))}/>
                            <IconButton onClick={() => {
                                setErrorInformed(false);
                                refetch();
                                moveToLocation(latitud, longitud);
                            }} disabled={isLoading}>
                                <i className="fa-duotone fa-solid fa-magnifying-glass-location"/>
                            </IconButton>
                        </Stack>
                        <Box>
                            <Map
                                latitud={latitud}
                                longitud={longitud}
                                data={data ?? []}
                                isLoading={isLoading}
                                mapRef={mapRef}
                                setOpen={setOpen}
                                setValue={setValue}
                            />
                        </Box>
                    </CardContent>
                </MainCard>
            </Modal>
        </>
    );
}