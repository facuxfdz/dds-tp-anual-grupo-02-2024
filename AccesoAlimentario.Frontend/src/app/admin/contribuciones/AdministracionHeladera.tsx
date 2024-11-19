import { Form, FormFieldType, IFormField } from "@components/Forms/Form";
import React, { useEffect, useRef, } from "react";
import { Box, Button, Modal, Stack, TextField } from "@mui/material";
import MainCard from "@components/Cards/MainCard";
import CardContent from "@mui/material/CardContent";
import IconButton from "@mui/material/IconButton";
import {
    useGetRecomendacionesUbicacionHeladeraQuery,
} from "@redux/services/serviciosApi";
import { useNotification } from "@components/Notifications/NotificationContext";
import { useFormContext } from "react-hook-form";
/*import Map from "./Map";*/
import dynamic from 'next/dynamic'

const Map = dynamic(() => import('./Map'), {
    ssr: false,
})

const fields: IFormField[] = [
    {
        id: "nombre",
        label: "Nombre",
        type: FormFieldType.TEXT,
        width: 12,
        value: "",
        placeholder: "Nombre de la heladera",
        isRequired: true,
        regex: "",
        errorMessage: "Ingrese un nombre válido",
        options: [],
    },
    {
        id: "latitud",
        label: "Latitud",
        type: FormFieldType.NUMBER,
        width: 6,
        value: "",
        placeholder: "Ingrese la latitud",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese la latitud",
        options: []
    },
    {
        id: "longitud",
        label: "Longitud",
        type: FormFieldType.NUMBER,
        width: 6,
        value: "",
        placeholder: "Ingrese la longitud",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese la longitud",
        options: []
    },
    {
        id: "calle",
        label: "Calle",
        type: FormFieldType.TEXT,
        width: 6,
        value: "",
        placeholder: "Ingrese su calle",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese su calle",
        options: []
    },
    {
        id: "numero",
        label: "Número",
        type: FormFieldType.NUMBER,
        width: 6,
        value: "",
        placeholder: "Ingrese su número",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese su número",
        options: []
    },
    {
        id: "localidad",
        label: "Localidad",
        type: FormFieldType.TEXT,
        width: 4,
        value: "",
        placeholder: "Ingrese su localidad",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese su localidad",
        options: []
    },
    {
        id: "piso",
        label: "Piso",
        type: FormFieldType.NUMBER,
        width: 2,
        value: "",
        placeholder: "Ingrese su piso",
        isRequired: false,
        regex: "",
        errorMessage: "Por favor ingrese su piso",
        options: []
    },
    {
        id: "departamento",
        label: "Departamento",
        type: FormFieldType.TEXT,
        width: 4,
        value: "",
        placeholder: "Ingrese su departamento",
        isRequired: false,
        regex: "",
        errorMessage: "Por favor ingrese su departamento",
        options: []
    },
    {
        id: "codigoPostal",
        label: "Código Postal",
        type: FormFieldType.NUMBER,
        width: 2,
        value: "",
        placeholder: "Ingrese su código postal",
        isRequired: true,
        regex: "\\d{4,8}",
        errorMessage: "Por favor ingrese su código postal",
        options: []
    },
    {
        id: "fechaInstalacion",
        label: "Fecha de instalación",
        type: FormFieldType.DATE,
        width: 12,
        value: "",
        placeholder: "Ingrese la fecha de instalación",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese la fecha de instalación",
        options: []
    },
    {
        id: "modelo",
        label: "Modelo",
        type: FormFieldType.SELECT,
        width: 12,
        value: "",
        placeholder: "Ingrese el modelo de la heladera",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese el modelo de la heladera",
        options: ["Modelo 1", "Modelo 2", "Modelo 3"]
    },
    {
        id: "temperaturaMinima",
        label: "Temperatura mínima",
        type: FormFieldType.NUMBER,
        width: 6,
        value: "",
        placeholder: "Ingrese la temperatura mínima",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese la temperatura mínima",
        options: []
    },
    {
        id: "temperaturaMaxima",
        label: "Temperatura máxima",
        type: FormFieldType.NUMBER,
        width: 6,
        value: "",
        placeholder: "Ingrese la temperatura máxima",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese la temperatura máxima",
        options: []
    }
];


export const AdministracionHeladera = () => {
    const [open, setOpen] = React.useState(false);
    const [latitud, setLatitud] = React.useState<number>(-34.56);
    const [longitud, setLongitud] = React.useState<number>(-58.45);
    const [radio, setRadio] = React.useState<number>(10);
    const { data, error, isLoading, refetch } = useGetRecomendacionesUbicacionHeladeraQuery({
        latitud: latitud,
        longitud: longitud,
        radio: radio
    });
    const { addNotification } = useNotification();
    const [errorInformed, setErrorInformed] = React.useState(false);
    const mapRef = useRef<L.Map>(null);
    const { setValue } = useFormContext();



    useEffect(() => {
        if (error && !errorInformed) {
            addNotification("Error al obtener las recomendaciones de ubicación", "error");
            setErrorInformed(true);
        }
    }, [addNotification, error, errorInformed]);

    const moveToLocation = (lat: number, lng: number) => {
        if (mapRef.current) {
            mapRef.current.setView([lat, lng], 13);
        }
    };

    return (
        <>
            <Button variant="contained" color="primary" onClick={() => setOpen(true)} sx={{
                width: "100%",
                mb: 2
            }}>
                Solicitar puntos de colocación
            </Button>
            <Form fields={fields} />
            <Modal open={open} onClose={() => setOpen(false)}>
                <MainCard modal darkTitle content={false} title={"Crear contribución"} sx={{ width: "80%" }}>
                    <CardContent>
                        <Stack direction="row" justifyContent={"space-between"} sx={{ py: 2 }}>
                            <TextField label="Latitud" type="number" value={latitud}
                                onChange={(e) => setLatitud(parseFloat(e.target.value))} />
                            <TextField label="Longitud" type="number" value={longitud}
                                onChange={(e) => setLongitud(parseFloat(e.target.value))} />
                            <TextField label="Radio" type="number" value={radio}
                                onChange={(e) => setRadio(parseFloat(e.target.value))} />
                            <IconButton onClick={() => {
                                setErrorInformed(false);
                                refetch();
                                moveToLocation(latitud, longitud);
                            }} disabled={isLoading}>
                                <i className="fa-duotone fa-solid fa-magnifying-glass-location" />
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