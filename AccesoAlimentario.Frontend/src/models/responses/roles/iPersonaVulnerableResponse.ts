import {ITarjetaConsumoResponse} from "@models/responses/tarjetas/iTarjetaConsumoResponse";
import {IRolResponse} from "@models/responses/roles/iRolResponse";

export interface IPersonaVulnerableResponse extends IRolResponse {
    cantidadDeMenores: number;
    tarjeta: ITarjetaConsumoResponse;
    fechaRegistroSistema: string;
}