import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";

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
        })
    }),
});

export const {
    useGetHeladeraQuery
} = heladerasApi;