import {EstadoVianda} from "@models/enums/estadoVianda";

export interface IViandaResponse {
    id: string;
    comida: string;
    fechaDonacion: string;
    fechaCaducidad: string;
    calorias: number;
    peso: number;
    estado: EstadoVianda;
}