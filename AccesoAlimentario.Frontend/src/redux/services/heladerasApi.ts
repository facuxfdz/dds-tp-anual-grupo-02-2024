import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";
import {IHeladeraResponse} from "@models/responses/heladeras/iHeladeraResponse";
import {ISensorResponse} from "@models/responses/sensores/iSensorResponse";
import {IRegistroVisitaHeladeraRequest} from "@models/requests/tecnicos/iRegistroVisitaHeladeraRequest";

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

        postVisitaHeladera: builder.mutation<
            void,
            IRegistroVisitaHeladeraRequest
        >({
            query: (body) => ({
                url: `tecnicos/registrar/visita`,
                method: "POST",
                body
            }),
            invalidatesTags: ["Heladeras"]
        }),

        deleteHeladera: builder.mutation<
            void,
            string
        >({
            query: (heladeraId) => ({
                url: `heladeras`,
                method: "DELETE",
                body: {
                    id: heladeraId
                }
            }),
            invalidatesTags: ["Heladeras"]
        })
    }),
});

export const {
    useGetHeladeraQuery,
    useGetHeladerasQuery,
    useLazyGetHeladeraQuery,
    useLazyGetHeladerasQuery,
    useGetSensorQuery,
    useLazyGetSensorQuery,
    usePostVisitaHeladeraMutation,
    useDeleteHeladeraMutation
} = heladerasApi;