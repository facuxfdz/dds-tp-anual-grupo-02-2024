import {EstadoVianda} from "@models/enums/estadoVianda";

export interface IViandaResponse {
    id: string;
    comida: string;
    fechaDonacion: string;
    fechaVencimiento: string;
    calorias: number;
    peso: number;
    estado: EstadoVianda;
}