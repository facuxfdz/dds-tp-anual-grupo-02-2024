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
        sexo: "Masculino" | "Femenino" | "Otro";
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
        tipoDocumento: "DNI" | "LE" | "LC" | "CUIT" | "CUIL";
        nroDocumento: number;
        fechaNacimiento: string;
    }
    cantidadMenores: number;
}