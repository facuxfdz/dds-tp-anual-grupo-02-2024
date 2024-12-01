import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";
import {IHeladeraResponse} from "@models/responses/heladeras/iHeladeraResponse";

export const heladerasApi = createApi({
    reducerPath: "HeladerasApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl}),
    tagTypes: ["Heladera"],
    endpoints: (builder) => ({
        getHeladera: builder.query<
            void,
            { heladeraId: string }
        >({
            query: ({heladeraId}) => ({
                url: `heladeras/${heladeraId}`,
            }),
            providesTags: ["Heladera"]
        }),
        getHeladeras: builder.query<
            IHeladeraResponse[],
            void
        >({
            query: () => ({
                url: `heladeras`,
            }),
            providesTags: ["Heladera"]
        }),
    }),
});

export const {
    useGetHeladeraQuery,
    useGetHeladerasQuery,
    useLazyGetHeladeraQuery,
    useLazyGetHeladerasQuery
} = heladerasApi;