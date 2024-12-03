import {jwtDecode} from 'jwt-decode'

export interface DecodedUser {
    aud?: string;

    colaboradorId?: string;
    tecnicoId?: string;

    name?: string;
    profile_picture?: string;

    contribucionesPreferidas?: string[];
    personaTipo?: string;
    // Add other fields as necessary
}

export const parseJwt = (token: string): DecodedUser => {
    try {
        return jwtDecode(token);
    } catch {
        return {} as DecodedUser;
    }
}