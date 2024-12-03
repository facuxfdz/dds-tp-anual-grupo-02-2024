import {IFormaContribucionResponse} from "@models/responses/contribuciones/iFormaContribucionResponse";
import {IHeladeraResponse} from "@models/responses/heladeras/iHeladeraResponse";

export interface IAdministracionHeladeraResponse extends IFormaContribucionResponse {
    heladera: IHeladeraResponse;
    tipo: 'AdministracionHeladera';
}