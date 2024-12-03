import {TipoReporte} from "@models/enums/tipoReporte";

export interface IReporteResponse {
    id: string;
    tipo: TipoReporte;
    fechaCreacion: string;
    fechaExpiracion: string;
    cuerpo: string;
}