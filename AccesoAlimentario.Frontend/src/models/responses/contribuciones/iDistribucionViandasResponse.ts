import {IFormaContribucionResponse} from "@models/responses/contribuciones/iFormaContribucionResponse";
import {IHeladeraResponse} from "@models/responses/heladeras/iHeladeraResponse";
import {MotivoDistribucion} from "@models/enums/motivoDistribucion";

export interface IDistribucionViandasResponse extends IFormaContribucionResponse {
    heladeraOrigen?: IHeladeraResponse;
    heladeraDestino?: IHeladeraResponse;
    cantViandas: number;
    motivoDistribucion: MotivoDistribucion;
    tipo: 'DistribucionViandas';
}