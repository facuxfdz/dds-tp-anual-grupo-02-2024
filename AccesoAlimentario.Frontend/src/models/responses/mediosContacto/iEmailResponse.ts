import {IMedioContactoResponse} from "@models/responses/mediosContacto/iMedioContactoResponse";

export interface IEmailResponse extends IMedioContactoResponse {
    direccion: string;
}