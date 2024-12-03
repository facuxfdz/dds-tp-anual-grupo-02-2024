import {IPuntoEstrategicoResponse} from "@models/responses/heladeras/iPuntoEstrategicoResponse";
import {IModeloHeladeraResponse} from "@models/responses/heladeras/iModeloHeladeraResponse";
import {EstadoHeladera} from "@models/enums/estadoHeladera";
import {IViandaResponse} from "@models/responses/heladeras/iViandaResponse";
import {ISensorResponse} from "@models/responses/sensores/iSensorResponse";
import {IIncidenteResponse} from "@models/responses/incidentes/iIncidenteResponse";

export interface IHeladeraResponse {
    id: string,
    puntoEstrategico: IPuntoEstrategicoResponse,
    viandas: IViandaResponse[],
    estado: EstadoHeladera,
    fechaInstalacion: string,
    temperaturaActual: number,
    temperaturaMinimaConfig: number,
    temperaturaMaximaConfig: number,
    sensores: ISensorResponse[],
    incidentes: IIncidenteResponse[],
    modelo: IModeloHeladeraResponse
}