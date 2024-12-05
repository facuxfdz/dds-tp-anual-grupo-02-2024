import {IPersonaRequest} from "@models/requests/colaboradores/iAltaColaboradorRequest";
import {TipoDocumento} from "@models/enums/tipoDocumento";
import {ContribucionesTipo} from "@models/enums/contribucionesTipo";

export interface IActualizarPerfilRequest {
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
    contribucionesPreferidas?: ContribucionesTipo[];
    tarjeta?: {
        codigo: string;
        tipo: 'Colaboracion';
    };

    areaCoberturaLatitud?: number;
    areaCoberturaLongitud?: number;
    areaCoberturaRadio?: number;
}