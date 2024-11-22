import { jwtDecode  } from 'jwt-decode'

export interface DecodedUser {
    aud?: string;
    name?: string;
    email?: string;
    picture?: string;
    // Add other fields as necessary
}

export const parseJwt = (token: string) : DecodedUser => {
    try {
        return jwtDecode(token);
    } catch (e) {
        return {} as DecodedUser;
    }
}