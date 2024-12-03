import {IHeladeraResponse} from "@models/responses/heladeras/iHeladeraResponse";

export interface IAutorizacionManipulacionHeladeraResponse
{
    id: string;
    fechaCreacion: string;
    fechaExpiracion: string;
    heladera: IHeladeraResponse;
}