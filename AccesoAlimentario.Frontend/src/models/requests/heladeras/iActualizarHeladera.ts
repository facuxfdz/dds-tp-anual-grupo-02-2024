export interface IActualizarHeladera {
    id: string;
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