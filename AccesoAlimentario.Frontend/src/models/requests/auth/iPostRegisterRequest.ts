import {TipoDocumento} from "@models/enums/tipoDocumento";
import {ContribucionesTipo} from "@models/enums/contribucionesTipo";
import {IPersonaRequest} from "@models/requests/colaboradores/iAltaColaboradorRequest";

export interface IPostRegisterRequest {
    email: string,
    password: string,
    profilePicture: string,
    registerType: RegisterType,

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

export enum RegisterType {
    Sso,
    Standard
}