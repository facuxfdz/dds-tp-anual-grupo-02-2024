import {config} from "@config/config";
import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";

export const tecnicosApi = createApi({
    reducerPath: "TecnicosApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl}),
    tagTypes: ["Test"],
    endpoints: (builder) => ({
        getTecnicos: builder.query<
            void,
            { testId: string }
        >({
            query: ({testId}) => ({
                url: `tests/${testId}`,
            }),
            providesTags: ["Test"]
        })
    }),
});

export const {
    useGetTecnicosQuery
} = tecnicosApi;