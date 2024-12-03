import {ContribucionesTipo} from "@models/enums/contribucionesTipo";
import {SexoDocumento} from "@models/enums/sexoDocumento";
import {TipoJuridica} from "@models/enums/tipoJuridica";
import {TipoDocumento} from "@models/enums/tipoDocumento";

export interface IAltaColaboradorRequest {
    persona: IPersonaRequest;
    direccion: {
        calle: string;
        numero: string;
        localidad: string;
        piso: string;
        departamento: string;
        codigoPostal: string;
    };
    documento: {
        tipoDocumento: TipoDocumento;
        nroDocumento: number;
        fechaNacimiento: string;
    };
    contribucionesPreferidas: ContribucionesTipo[];
    tarjeta?: {
        codigo: string;
        tipo: 'Colaboracion';
    };
}

export interface IPersonaRequest {
    nombre: string;
    tipoPersona: 'Humana' | 'Juridica';
}

export interface IPersonaHumanaRequest extends IPersonaRequest {
    apellido: string;
    sexo: SexoDocumento;
    tipoPersona: 'Humana';
}

export interface IPersonaJuridicaRequest extends IPersonaRequest {
    razonSocial: string;
    tipoJuridico: TipoJuridica;
    rubro: string;
    tipoPersona: 'Juridica';
}