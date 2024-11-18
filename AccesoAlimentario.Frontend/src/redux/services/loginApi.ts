import {config} from "@config/config";
import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";

export const loginApi = createApi({
    reducerPath: "LoginApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl}),
    tagTypes: ["Login"],
    endpoints: (builder) => ({
        login: builder.query<
            void,
            { loginId: string }
        >({
            query: ({loginId}) => ({
                url: `logins/${loginId}`,
            }),
            providesTags: ["Login"]
        }),
        /*
        rejectProcess: builder.mutation<
            null,
            { processId: string, reason: string }
        >({
            query: ({processId, reason}) => ({
                url: `processes/cancel`,
                method: "POST",
                body: {
                    processId,
                    reason
                }
            })
        }),
        * */
    }),
});

export const {
    useLoginQuery
} = loginApi;