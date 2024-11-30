import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";

export const authApi = createApi({
    reducerPath: "AuthApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl}),
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
            { email: string, password?: string, profile_picture?: string, register_type: "sso" | "standard" }
        >({
            query: ({email, password, profile_picture, register_type}) => ({
                url: `auth/register`,
                method: "POST",
                body: { email, password, profile_picture, register_type }
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