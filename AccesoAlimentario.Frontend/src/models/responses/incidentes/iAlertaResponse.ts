import {IIncidenteResponse} from "@models/responses/incidentes/iIncidenteResponse";
import {TipoAlerta} from "@models/enums/tipoAlerta";

export interface IAlertaResponse extends IIncidenteResponse {
    tipo: TipoAlerta;
    tipoIncidente: 'Alerta';
}