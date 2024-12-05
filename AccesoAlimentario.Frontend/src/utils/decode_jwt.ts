import {jwtDecode} from 'jwt-decode'

export interface DecodedUser {
    aud?: string;

    colaboradorId?: string;
    tecnicoId?: string;

    name?: string;
    profile_picture?: string;

    contribucionesPreferidas?: string;
    tarjetaColaboracionId?: string;
    personaTipo?: string;
    isAdmin?: string;
}

export interface DecodedUserGoogle {
    aud?: string;
    email?: string;
    name?: string;
    picture?: string;
}

export const parseJwt = (token: string): DecodedUser => {
    try {
        return jwtDecode(token);
    } catch {
        return {} as DecodedUser;
    }
}

export const parseJwtGoogle = (token: string): DecodedUserGoogle => {
    try {
        return jwtDecode(token);
    } catch {
        return {} as DecodedUserGoogle;
    }
}