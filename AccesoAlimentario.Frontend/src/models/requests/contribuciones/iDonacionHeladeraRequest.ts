export interface IDonacionHeladeraRequest {
    colaboradorId: string;
    fechaContribucion: string;
    puntoEstrategico: {
        nombre: string;
        longitud: number;
        latitud: number;
        direccion: {
            calle: string;
            numero: string;
            localidad: string;
            piso: string;
            departamento: string;
            codigoPostal: string;
        }
    }
    estado: "Activa" | "Desperfecto" | "FueraServicio";
    fechaInstalacion: string;
    temperaturaMinimaConfig: number;
    temperaturaMaximaConfig: number;
    sensores: {
        id: string;
        tipo: "Temperatura" | "Movimiento";
    }[];
    modelo: {
        capacidad: number;
        temperaturaMinima: number;
        temperaturaMaxima: number;
    }
}