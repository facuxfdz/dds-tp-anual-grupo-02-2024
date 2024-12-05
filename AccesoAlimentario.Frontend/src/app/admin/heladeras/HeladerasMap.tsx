import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet";
import "leaflet/dist/leaflet.css";
import L from "leaflet";
import { Button, Divider, Stack } from "@mui/material";
import { useAppSelector } from "@/redux/hook";
import {RefObject, useRef} from "react";
import {IHeladeraResponse} from "@models/responses/heladeras/iHeladeraResponse";
import NextLink from "next/link";
import {heladeraRoute} from "@routes/router";
import {EstadoHeladera} from "@models/enums/estadoHeladera";

function getHeladeraEstado(estado: EstadoHeladera) {
    switch (estado) {
        case EstadoHeladera.Activa:
            return 'Activa';
        case EstadoHeladera.Desperfecto:
            return 'Desperfecto';
        case EstadoHeladera.FueraServicio:
            return 'Fuera de Servicio';
        default:
            return 'Desconocido';
    }
}

export const Map = ({
                        data,
                    }:{
    data: IHeladeraResponse[],
}) => {
    const theme = useAppSelector(state => state.theme);
    const tileURL = theme.mode === "dark"
        ? "https://tiles.stadiamaps.com/tiles/alidade_smooth_dark/{z}/{x}/{y}{r}.png"
        : "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png";
    const mapRef = useRef<L.Map>(null);

    const icon = L.divIcon({
        html: `<i class="fa-solid fa-location-dot" style="color: #f00000; font-size: 2rem;"></i>`,
        className: "custom-map-icon",
        iconSize: [25, 41],
        iconAnchor: [12, 41]
    });

    return (
        <MapContainer
            zoom={12}
            style={{ height: "400px", width: "100%" }}
            ref={mapRef}
            center={[-34.603722, -58.381592]}
        >
            <TileLayer url={tileURL} />
            {
               data.map(item => (
                    <Marker position={[item.puntoEstrategico.latitud, item.puntoEstrategico.longitud]} icon={icon} key={item.id}>
                        <Popup closeButton={false}>
                            <Stack spacing={1} justifyContent={"center"} alignItems={"center"}>
                                <span>Nombre: {item.puntoEstrategico.nombre}</span>
                                <Divider />
                                <span>Direccion: {item.puntoEstrategico.direccion.calle} {item.puntoEstrategico.direccion.numero}</span>
                                <span>Localidad {item.puntoEstrategico.direccion.localidad}</span>
                                <span>CP: {item.puntoEstrategico.direccion.codigoPostal}</span>
                                <span>Estado: {getHeladeraEstado(item.estado)}</span>
                                <Divider />
                                <Button color="primary" component={NextLink} href={heladeraRoute(item.id)}>
                                    Ver Detalles
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