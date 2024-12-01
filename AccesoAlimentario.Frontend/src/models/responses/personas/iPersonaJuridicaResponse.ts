import {IPersonaResponse} from "@models/responses/personas/iPersonaResponse";
import {TipoJuridica} from "@models/enums/tipoJuridica";

export interface IPersonaJuridicaResponse extends IPersonaResponse {
    razonSocial: string;
    tipo: TipoJuridica;
    rubro: string;
    tipoPersona: 'Juridica';
}