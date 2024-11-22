import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet";
import "leaflet/dist/leaflet.css";
import L from "leaflet";
import { Button, CircularProgress, Divider, Stack } from "@mui/material";
import { useAppSelector } from "@/redux/hook";
import { IRecomendacionUbicacionHeladeraResponse } from "@/models/responses/servicios/iRecomendacionUbicacionHeladeraResponse";
import { RefObject } from "react";


export const Map = ({
    latitud,
    longitud,
    data,
    isLoading,
    mapRef,
    setOpen,
    setValue
}:{
    latitud: number,
    longitud: number,
    data: IRecomendacionUbicacionHeladeraResponse[],
    isLoading: boolean,
    mapRef: RefObject<L.Map>,
    setOpen: (value: boolean) => void,
    setValue: (field: string, value: string | number) => void
}) => {
    const theme = useAppSelector(state => state.theme);
    const tileURL = theme.mode === "dark"
        ? "https://tiles.stadiamaps.com/tiles/alidade_smooth_dark/{z}/{x}/{y}{r}.png"
        : "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png";

    const icon = L.divIcon({
        html: `<i class="fa-solid fa-location-dot" style="color: #f00000; font-size: 2rem;"></i>`,
        className: "custom-map-icon",
        iconSize: [25, 41],
        iconAnchor: [12, 41]
    });

    return (
        <MapContainer
            center={[latitud, longitud]}
            zoom={12}
            style={{ height: "400px", width: "100%" }}
            ref={mapRef}
        >
            <TileLayer url={tileURL} />
            {
                isLoading ? <CircularProgress /> : data?.map(item => (
                    <Marker position={[item.latitud, item.longitud]} icon={icon} key={item.id}>
                        <Popup closeButton={false}>
                            <Stack spacing={1} justifyContent={"center"} alignItems={"center"}>
                                <strong>Nombre: {item.nombre}</strong>
                                <Divider />
                                <span>Direccion: {item.direccion.calle} {item.direccion.numero}</span>
                                <span>Localidad {item.direccion.localidad}</span>
                                <span>CP: {item.direccion.codigoPostal}</span>
                                <Divider />
                                <Button variant="contained" color="primary" onClick={() => {
                                    setOpen(false);
                                    setValue("puntoEstrategicoNombre", item.nombre);
                                    setValue("puntoEstrategicoLongitud", item.longitud);
                                    setValue("puntoEstrategicoLatitud", item.latitud);
                                    setValue("puntoEstrategicoCalle", item.direccion.calle);
                                    setValue("puntoEstrategicoNumero", item.direccion.numero);
                                    setValue("puntoEstrategicoLocalidad", item.direccion.localidad);
                                    setValue("puntoEstrategicoPiso", item.direccion.piso);
                                    setValue("puntoEstrategicoDepartamento", item.direccion.departamento);
                                    setValue("puntoEstrategicoCodigoPostal", item.direccion.codigoPostal);
                                }}>
                                    Seleccionar
                                </Button>
                            </Stack>
                        </Popup>
                    </Marker>
                ))
            }
        </MapContainer>
    )
};

export default Map;