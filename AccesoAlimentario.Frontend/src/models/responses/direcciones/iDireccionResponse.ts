export interface IDireccionResponse {
    id: string;
    calle: string;
    numero: string;
    localidad: string;
    piso?: string;
    departamento?: string;
    codigoPostal: string;
}