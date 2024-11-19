export interface IDonacionViandaRequest {
    colaboradorId: string;
    fechaContribucion: string;
    heladeraId: string;
    comida: string;
    fechaCaducidad: string;
    calorias: number;
    peso: number;
    estadoVianda: "Disponible";
}