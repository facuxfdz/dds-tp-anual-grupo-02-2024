export interface ISuscribirseHeladeraRequest {
    colaboradorId: string;
    heladeraId: string;
    minimo: number;
    maximo: number;
    tipo: "Faltante" | "Excendente" | "Incidente";
}