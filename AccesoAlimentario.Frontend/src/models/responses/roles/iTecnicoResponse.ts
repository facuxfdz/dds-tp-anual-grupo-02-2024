import {IRolResponse, IRolResponseMinimo} from "@models/responses/roles/iRolResponse";
import {IAreaCoberturaResponse} from "@models/responses/roles/iAreaCoberturaResponse";

export interface ITecnicoResponse extends IRolResponse {
    areaCobertura: IAreaCoberturaResponse;
}

export interface ITecnicoResponseMinimo extends IRolResponseMinimo {
    areaCobertura: IAreaCoberturaResponse;
}