import {IDireccionResponse} from "@models/responses/direcciones/iDireccionResponse";
import {IDocumentoIdentidadResponse} from "@models/responses/documentosIdentidad/iDocumentoIdentidadResponse";
import {IMedioContactoResponse} from "@models/responses/mediosContacto/iMedioContactoResponse";

export interface IPersonaResponse {
    id: string;
    nombre: string;
    direccion?: IDireccionResponse;
    documentoIdentidad?: IDocumentoIdentidadResponse;
    mediosDeContacto: IMedioContactoResponse[];
    fechaAlta: string;
    tipoPersona: 'Humana' | 'Juridica';
}