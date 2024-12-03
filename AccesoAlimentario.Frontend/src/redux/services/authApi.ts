import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";
import {IPostRegisterRequest} from "@models/requests/auth/iPostRegisterRequest";

export const authApi = createApi({
    reducerPath: "AuthApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl, credentials: "include"}),
    endpoints: (builder) => ({
        postLoginValidate: builder.mutation<
            {userExists: boolean},
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
    }),
});

export const {
    usePostLoginValidateMutation,
    usePostRegisterMutation
} = authApi;