import {IFormaContribucionResponse} from "@models/responses/contribuciones/iFormaContribucionResponse";

export interface IDonacionMonetariaResponse extends IFormaContribucionResponse {
    monto: number;
    frecuenciaDias: number;
    tipo: 'DonacionMonetaria';
}