import {IPremioResponse} from "@models/responses/premios/iPremioResponse";
import {IFormaContribucionResponse} from "@models/responses/contribuciones/iFormaContribucionResponse";

export interface IOfertaPremioResponse extends IFormaContribucionResponse {
    premio: IPremioResponse;
    tipo: 'OfertaPremio';
}