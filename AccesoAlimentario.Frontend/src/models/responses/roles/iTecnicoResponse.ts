import {IRolResponse} from "@models/responses/roles/iRolResponse";
import {IAreaCoberturaResponse} from "@models/responses/roles/iAreaCoberturaResponse";

export interface ITecnicoResponse extends IRolResponse {
    areaCobertura: IAreaCoberturaResponse;
}