import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";
import {IObtenerPremioResponse} from "@models/responses/premios/iObtenerPremioResponse";

export const premiosApi = createApi({
    reducerPath: "PremiosApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl}),
    tagTypes: ["Premio"],
    endpoints: (builder) => ({
        getPremio: builder.query<
            void,
            { premioId: string }
        >({
            query: ({premioId}) => ({
                url: `premios/${premioId}`,
            }),
            providesTags: ["Premio"]
        }),
        getPremios: builder.query<
            IObtenerPremioResponse[],
            void
        >({
            query: () => ({
                url: `premios`,
            }),
            providesTags: ["Premio"]
        }),
    }),
});

export const {
    useGetPremioQuery,
    useGetPremiosQuery,
    useLazyGetPremioQuery,
    useLazyGetPremiosQuery
} = premiosApi;