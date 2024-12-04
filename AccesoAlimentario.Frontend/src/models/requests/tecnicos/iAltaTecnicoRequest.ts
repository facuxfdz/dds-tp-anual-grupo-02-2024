import {IPersonaRequest} from "@models/requests/colaboradores/iAltaColaboradorRequest";
import {TipoDocumento} from "@models/enums/tipoDocumento";

export interface IAltaTecnicoRequest {
    persona: IPersonaRequest;
    documento: {
        tipoDocumento: TipoDocumento;
        nroDocumento: number;
        fechaNacimiento: string;
    };
    mediosDeContacto: {
        preferida: boolean,
        tipo: "Email",
        direccion: string
    }[];
    areaCoberturaLatitud: number;
    areaCoberturaLongitud: number;
    areaCoberturaRadio: number;
}