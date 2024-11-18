import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";
import {
    IRecomendacionUbicacionHeladeraResponse
} from "@models/responses/servicios/iRecomendacionUbicacionHeladeraResponse";

export const serviciosApi = createApi({
    reducerPath: "ServiciosApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl}),
    tagTypes: ["Servicio"],
    endpoints: (builder) => ({
        getRecomendacionesUbicacionHeladera: builder.query<
            IRecomendacionUbicacionHeladeraResponse[],
            {
                latitud: number,
                longitud: number,
                radio: number
            }
        >({
            query: ({latitud, longitud, radio}) => ({
                url: `servicios/ObtenerRecomendacionUbicacionHeladera`,
                params: {
                    latitud,
                    longitud,
                    radio
                }
            }),
            providesTags: ["Servicio"]
        })
    }),
});

export const {
    useLazyGetRecomendacionesUbicacionHeladeraQuery,
    useGetRecomendacionesUbicacionHeladeraQuery
} = serviciosApi;