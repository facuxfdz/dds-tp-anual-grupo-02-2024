import {ISensorResponse} from "@models/responses/sensores/iSensorResponse";
import {IRegistroTemperaturaResponse} from "@models/responses/sensores/iRegistroTemperaturaResponse";

export interface ISensorTemperaturaResponse extends ISensorResponse {
    registrosTemperatura: IRegistroTemperaturaResponse[];
    tipo: 'Temperatura';
}