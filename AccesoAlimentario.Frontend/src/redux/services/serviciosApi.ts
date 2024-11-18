import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";

export const serviciosApi = createApi({
    reducerPath: "ServiciosApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl}),
    tagTypes: ["Servicio"],
    endpoints: (builder) => ({
        getServicio: builder.query<
            void,
            { servicioId: string }
        >({
            query: ({servicioId}) => ({
                url: `servicios/${servicioId}`,
            }),
            providesTags: ["Servicio"]
        })
    }),
});

export const {
    useGetServicioQuery
} = serviciosApi;