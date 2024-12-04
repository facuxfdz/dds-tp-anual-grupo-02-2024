import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";
import {IHeladeraResponse} from "@models/responses/heladeras/iHeladeraResponse";
import {ISensorResponse} from "@models/responses/sensores/iSensorResponse";

export const heladerasApi = createApi({
    reducerPath: "HeladerasApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl}),
    tagTypes: ["Heladera", "Heladeras", "Sensor"],
    endpoints: (builder) => ({
        getHeladera: builder.query<
            IHeladeraResponse,
            { heladeraId: string }
        >({
            query: ({heladeraId}) => ({
                url: `heladeras/consultar`,
                params: {id: heladeraId}
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
            providesTags: ["Heladeras"]
        }),
        getSensor: builder.query<
            ISensorResponse,
            string
        >({
            query: (sensorId) => ({
                url: `sensores/${sensorId}`,
            }),
            providesTags: ["Sensor"]
        }),
    }),
});

export const {
    useGetHeladeraQuery,
    useGetHeladerasQuery,
    useLazyGetHeladeraQuery,
    useLazyGetHeladerasQuery,
    useGetSensorQuery,
    useLazyGetSensorQuery
} = heladerasApi;