import {IVisitaTecnicaResponse} from "@models/responses/incidentes/iVisitaTecnicaResponse";

export interface IIncidenteResponse {
    id: string;
    visitasTecnicas: IVisitaTecnicaResponse[];
    fecha: string;
    resuelto: boolean;
    tipoIncidente: 'Alerta' | 'FallaTecnica';
}