import {IPersonaResponse} from "@models/responses/personas/iPersonaResponse";
import {SexoDocumento} from "@models/enums/sexoDocumento";

export interface IPersonaHumanaReponse extends IPersonaResponse {
    apellido: string;
    sexo: SexoDocumento;
    tipoPersona: 'Humana';
}