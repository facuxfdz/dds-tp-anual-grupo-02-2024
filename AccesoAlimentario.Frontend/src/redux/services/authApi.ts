import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";
import {IPostRegisterRequest} from "@models/requests/auth/iPostRegisterRequest";

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
    }),
});

export const {
    usePostLoginValidateMutation,
    usePostRegisterMutation,
    usePostLoginMutation,
    usePostLogoutMutation
} = authApi;