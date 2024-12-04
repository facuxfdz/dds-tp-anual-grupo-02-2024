import {ITecnicoResponse} from "@models/responses/roles/iTecnicoResponse";

export interface IVisitaTecnicaResponse {
    id: string;
    tecnico: ITecnicoResponse;
    foto: string;
    fecha: string;
    comentario: string;
}