import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";
import {IPostRegisterRequest} from "@models/requests/auth/iPostRegisterRequest";
import {IPersonaResponse} from "@models/responses/personas/iPersonaResponse";

export const authApi = createApi({
    reducerPath: "AuthApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl, credentials: "include"}),
    endpoints: (builder) => ({
        postLoginValidate: builder.mutation<
            { userExists: boolean, token: string },
            string
        >({
            query: (token) => ({
                url: `auth/validate`,
                method: "POST",
                body: {token}
            }),
        }),

        postRegister: builder.mutation<
            void,
            IPostRegisterRequest
        >({
            query: (body) => ({
                url: `auth/register`,
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
                url: `auth/password/validate`,
                method: "POST",
                body: {password}
            }),
        }),

        getObtenerPerfil: builder.query<
            {
                persona: IPersonaResponse,
                roles: object[]
            },
            void
        >({
            query: () => ({
                url: `auth/perfil`,
                method: "GET",
            }),
        })
    }),
});

export const {
    usePostLoginValidateMutation,
    usePostRegisterMutation,
    usePostLoginMutation,
    usePostLogoutMutation,
    usePostValidarPasswordMutation,
    useGetObtenerPerfilQuery
} = authApi;