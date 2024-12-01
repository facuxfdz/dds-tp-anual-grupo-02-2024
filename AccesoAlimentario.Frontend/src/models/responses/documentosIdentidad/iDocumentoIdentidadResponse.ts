import {TipoDocumento} from "@models/enums/tipoDocumento";

export interface IDocumentoIdentidadResponse {
    id: string;
    tipoDocumento: TipoDocumento;
    nroDocumento: number;
    fechaNacimiento?: string;
}