import {IPersonaResponse} from "@models/responses/personas/iPersonaResponse";

export interface IRolResponse {
    id: string;
    persona: IPersonaResponse;
    tipo: 'Tecnico' | 'PersonaVulnerable' | 'Colaborador';
}