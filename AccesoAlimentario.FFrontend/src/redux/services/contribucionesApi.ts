import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";

export const contribucionesApi = createApi({
    reducerPath: "ContribucionesApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl}),
    tagTypes: ["Contribucion"],
    endpoints: (builder) => ({
        getContribucion: builder.query<
            void,
            { contribucionId: string }
        >({
            query: ({contribucionId}) => ({
                url: `contribuciones/${contribucionId}`,
            }),
            providesTags: ["Contribucion"]
        })
    }),
});

export const {
    useGetContribucionQuery
} = contribucionesApi;