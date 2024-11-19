export interface IDistribucionViandaRequest {
    colaboradorId: string;
    fechaContribucion: string;
    heladeraOrigenId: string;
    heladeraDestinoId: string;
    cantidadDeViandas: number;
    motivo: "Desperfecto" | "FaltaDeViandas";
}