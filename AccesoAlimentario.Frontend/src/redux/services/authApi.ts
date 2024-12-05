import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";
import {IPostRegisterRequest} from "@models/requests/auth/iPostRegisterRequest";
import {IPersonaResponse} from "@models/responses/personas/iPersonaResponse";
import {IActualizarPerfilRequest} from "@models/requests/auth/iActualizarPerfilRequest";
import {IRolResponseMinimo} from "@models/responses/roles/iRolResponse";

export const authApi = createApi({
    reducerPath: "AuthApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl, credentials: "include"}),
    endpoints: (builder) => ({
        postLoginValidar: builder.mutation<
            { userExists: boolean, token: string },
            string
        >({
            query: (token) => ({
                url: `auth/validar`,
                method: "POST",
                body: {token}
            }),
        }),

        postRegistrar: builder.mutation<
            void,
            IPostRegisterRequest
        >({
            query: (body) => ({
                url: `auth/registrar`,
                method: "POST",
                body
            }),
        }),

        postLogin: builder.mutation<
            { userExists: boolean, token: string },
            { username: string, password: string }
        >({
            query: ({
                        username,
                        password
                    }) => ({
                url: `auth/login`,
                method: "POST",
                body: {
                    username,
                    password
                }
            }),
        }),

        postLogout: builder.mutation<void, void>({
            query: () => ({
                url: `auth/logout`,
                method: "POST",
            }),
        }),

        postValidarPassword: builder.mutation<
            boolean,
            string
        >({
            query: (password) => ({
                url: `auth/password/validar`,
                method: "POST",
                body: {password}
            }),
        }),

        getObtenerPerfil: builder.query<
            {
                persona: IPersonaResponse,
                roles: IRolResponseMinimo[]
            },
            void
        >({
            query: () => ({
                url: `auth/perfil`,
                method: "GET",
            }),
        }),

        actualizarPerfil: builder.mutation<
            void,
            IActualizarPerfilRequest
        >({
            query: (body) => ({
                url: `auth/perfil`,
                method: "PUT",
                body
            }),
        }),
    }),
});

export const {
    usePostLoginValidarMutation,
    usePostRegistrarMutation,
    usePostLoginMutation,
    usePostLogoutMutation,
    usePostValidarPasswordMutation,
    useGetObtenerPerfilQuery,
    useActualizarPerfilMutation
} = authApi;