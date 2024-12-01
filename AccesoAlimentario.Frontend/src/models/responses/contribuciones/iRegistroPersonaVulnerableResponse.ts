import {IFormaContribucionResponse} from "@models/responses/contribuciones/iFormaContribucionResponse";
import {IPersonaVulnerableResponse} from "@models/responses/roles/iPersonaVulnerableResponse";

export interface IRegistroPersonaVulnerableResponse extends IFormaContribucionResponse {
    personaVulnerable: IPersonaVulnerableResponse;
    tipo: 'RegistroPersonaVulnerable';
}