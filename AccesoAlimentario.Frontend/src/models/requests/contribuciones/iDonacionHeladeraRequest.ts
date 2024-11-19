export interface IDonacionHeladeraRequest{
    colaboradorId: string;
    fechaContribucion: string;
    estado: "Activa" | "Desperfecto" | "FueraServicio";
    fechaInstalacion: string;
    temperaturaMinimaConfig: number;
    temperaturaMaximaConfig: number;
    sensores: {
        tipo: "Temperatura" | "Movimiento";
    }[];
    modelo: {
        capacidad: number;
        temperaturaMinima: number;
        temperaturaMaxima: number;
    }
}