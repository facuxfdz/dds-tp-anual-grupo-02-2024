import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";

export interface Direccion {
    calle: string;
    numero: string;
    localidad: string;
    codigoPostal: string;
    piso?: string;
    departamento?: string;
}

export interface Documento {
    tipoDocumento: "DNI" | "LE" | "LC" | "CUIT" | "CUIL";
    nroDocumento: number;    
    fechaNacimiento?: string;
}

export const authApi = createApi({
    reducerPath: "AuthApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl, credentials: "include"}),
    tagTypes: ["Auth"],
    endpoints: (builder) => ({
        login: builder.query<
            void,
            { email: string, password: string }
        >({
            query: ({email, password}) => ({
                url: `auth/login`,
                method: "POST",
                body: { email, password }
            }),
            providesTags: ["Auth"]
        }),

        register: builder.query<
            void,
            { 
                email: string, 
                password?: string, 
                profile_picture?: string, 
                register_type: "sso" | "standard", 
                user_type?: "Humana" | "Juridica",
                direccion?: Direccion,
                documento?: Documento,
                persona?: any
            }
        >({
            query: ({
                email, 
                password, 
                profile_picture, 
                register_type,
                user_type,
                direccion,
                documento,
                persona
            }) => ({
                url: `auth/register`,
                method: "POST",
                body: { 
                    email, 
                    password, 
                    profile_picture, 
                    register_type, 
                    user_type, 
                    direccion, 
                    documento,
                    persona
                 }
            }),
            providesTags: ["Auth"]
        }),
    }),
});

export const {
    useLoginQuery,
    useRegisterQuery,
    useLazyLoginQuery,
    useLazyRegisterQuery
} = authApi;