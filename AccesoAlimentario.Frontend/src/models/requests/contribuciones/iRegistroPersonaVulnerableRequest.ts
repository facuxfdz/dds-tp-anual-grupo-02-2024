import {TipoDocumento} from "@models/enums/tipoDocumento";
import {SexoDocumento} from "@models/enums/sexoDocumento";

export interface IRegistroPersonaVulnerableRequest {
    colaboradorId: string;
    tarjeta: {
        codigo: string;
        tipo: string;
        responsableId: string;
    }
    persona: {
        nombre: string;
        tipo: "Humana",
        apellido: string;
        sexo: SexoDocumento;
    }
    direccion: {
        calle: string;
        numero: string;
        localidad: string;
        piso: string;
        departamento: string;
        codigoPostal: string;
    }
    documento: {
        tipoDocumento: TipoDocumento;
        nroDocumento: number;
        fechaNacimiento: string;
    }
    cantidadMenores: number;
}