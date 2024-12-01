import {IIncidenteResponse} from "@models/responses/incidentes/iIncidenteResponse";

export interface IFallaTecnicaResponse extends IIncidenteResponse {
    descripcion?: string;
    foto?: string;
    tipoIncidente: 'FallaTecnica';
}