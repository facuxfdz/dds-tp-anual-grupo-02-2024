import {IFormaContribucionResponse} from "@models/responses/contribuciones/iFormaContribucionResponse";
import {IHeladeraResponse} from "@models/responses/heladeras/iHeladeraResponse";
import {IViandaResponse} from "@models/responses/heladeras/iViandaResponse";

export interface IDonacionViandasResponse extends IFormaContribucionResponse {
    heladera: IHeladeraResponse;
    vianda: IViandaResponse;
    tipo: 'DonacionVianda';
}