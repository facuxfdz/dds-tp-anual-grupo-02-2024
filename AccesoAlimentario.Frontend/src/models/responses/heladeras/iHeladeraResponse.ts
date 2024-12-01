export interface IHeladeraResponse {
    id: string,
    puntoEstrategico: {
        id: string,
        nombre: string,
        longitud: number,
        latitud: number,
        direccion: {
            id: string,
            calle: string,
            numero: string,
            localidad: string,
            piso: string,
            departamento: string,
            codigoPostal: string
        }
    },
    estado: EstadoHeladera,
    fechaInstalacion: string,
    temperaturaActual: number,
    temperaturaMinimaConfig: number,
    temperaturaMaximaConfig: number,
    modelo: {
        id: string,
        capacidad: number,
        temperaturaMinima: number,
        temperaturaMaxima: number
    }
}

export enum EstadoHeladera
{
    Activa,
    Desperfecto,
    FueraServicio,
}