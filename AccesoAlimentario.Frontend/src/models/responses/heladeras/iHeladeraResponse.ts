import {IPuntoEstrategicoResponse} from "@models/responses/heladeras/iPuntoEstrategicoResponse";
import {IModeloHeladeraResponse} from "@models/responses/heladeras/iModeloHeladeraResponse";
import {EstadoHeladera} from "@models/enums/estadoHeladera";

export interface IHeladeraResponse {
    id: string,
    puntoEstrategico: IPuntoEstrategicoResponse,
    estado: EstadoHeladera,
    fechaInstalacion: string,
    temperaturaActual: number,
    temperaturaMinimaConfig: number,
    temperaturaMaximaConfig: number,
    modelo: IModeloHeladeraResponse
}