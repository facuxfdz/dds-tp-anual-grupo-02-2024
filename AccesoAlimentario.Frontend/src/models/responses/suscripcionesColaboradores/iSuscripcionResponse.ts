import {IHeladeraResponse} from "@models/responses/heladeras/iHeladeraResponse";

export interface ISuscripcionResponse {
    id: string;
    heladera: IHeladeraResponse;
    tipo: 'ExcedenteViandas' | 'FaltanteViandas' | 'IncidenteHeladera';
}