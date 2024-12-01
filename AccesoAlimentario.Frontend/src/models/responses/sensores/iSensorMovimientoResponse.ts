import {ISensorResponse} from "@models/responses/sensores/iSensorResponse";
import {IRegistroMovimientoResponse} from "@models/responses/sensores/iRegistroMovimientoResponse";

export interface ISensorMovimientoResponse extends ISensorResponse {
    registrosMovimiento: IRegistroMovimientoResponse[];
    tipo: 'Movimiento';
}