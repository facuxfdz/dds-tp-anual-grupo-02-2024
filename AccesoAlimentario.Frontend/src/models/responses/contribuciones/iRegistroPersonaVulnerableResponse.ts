import {IFormaContribucionResponse} from "@models/responses/contribuciones/iFormaContribucionResponse";
import {IPersonaVulnerableResponse} from "@models/responses/roles/iPersonaVulnerableResponse";

export interface IRegistroPersonaVulnerableResponse extends IFormaContribucionResponse {
    propietario: IPersonaVulnerableResponse;
    tipo: 'RegistroPersonaVulnerable';
}