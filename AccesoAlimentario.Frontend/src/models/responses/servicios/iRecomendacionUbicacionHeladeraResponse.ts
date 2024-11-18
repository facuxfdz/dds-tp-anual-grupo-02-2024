export interface IRecomendacionUbicacionHeladeraResponse {
    id: string;
    nombre: string;
    longitud: number;
    latitud: number;
    direccion: {
        id: string;
        calle: string;
        numero: number;
        localidad: string;
        piso: string;
        departamento: string;
        codigoPostal: string;
    };
}