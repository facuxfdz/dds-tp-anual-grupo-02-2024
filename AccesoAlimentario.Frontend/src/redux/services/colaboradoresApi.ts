import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";
import {IImportarColaboradoresCsvRequest} from "@models/requests/colaboradores/iImportarColaboradoresCsvRequest";
import {IReportarFallaTecnicaRequest} from "@models/requests/colaboradores/iReportarFallaTecnicaRequest";
import {ISuscribirseHeladeraRequest} from "@models/requests/colaboradores/iSuscribirseHeladeraRequest";
import {IAltaColaboradorRequest} from "@models/requests/colaboradores/iAltaColaboradorRequest";
import {ISuscripcionResponse} from "@models/responses/suscripcionesColaboradores/iSuscripcionResponse";

export const colaboradoresApi = createApi({
    reducerPath: "ColaboradoresApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl}),
    tagTypes: ["Colaborador", "Suscripciones"],
    endpoints: (builder) => ({
        postImportarColaboradoresCsv: builder.mutation<
            void,
            IImportarColaboradoresCsvRequest
        >({
            query: (body) => ({
                url: `colaboradores/importar/csv`,
                method: "POST",
                body,
                timeout: 60000
            }),
        }),

        postAltaColaborador: builder.mutation<
            void,
            IAltaColaboradorRequest
        >({
            query: (body) => ({
                url: `colaboradores`,
                method: "POST",
                body,
            }),
        }),

        postReportarFallaTecnica: builder.mutation<
            void,
            IReportarFallaTecnicaRequest
        >({
            query: (body) => ({
                url: `colaboradores/reportar/fallatecnica`,
                method: "POST",
                body
            }),
        }),

        postSuscribirseHeladera: builder.mutation<
            void,
            ISuscribirseHeladeraRequest
        >({
            query: (body) => ({
                url: `colaboradores/suscribirseheladera`,
                method: "POST",
                body,
            }),
            invalidatesTags: ["Suscripciones"]
        }),

        postDesuscribirseHeladera: builder.mutation<
            void,
            string
        >({
            query: (id) => ({
                url: `colaboradores/desuscribirseheladera`,
                method: "POST",
                body: {
                    suscripcionId: id
                },
            }),
            invalidatesTags: ["Suscripciones"]
        }),

        getSuscripciones: builder.query<
            ISuscripcionResponse[],
            string
        >({
            query: (id) => ({
                url: `colaboradores/${id}/suscripciones`,
                method: "GET",
            }),
            providesTags: ["Suscripciones"]
        })
    }),
});

export const {
    usePostImportarColaboradoresCsvMutation,
    usePostReportarFallaTecnicaMutation,
    usePostSuscribirseHeladeraMutation,
    usePostAltaColaboradorMutation,
    usePostDesuscribirseHeladeraMutation,
    useGetSuscripcionesQuery,
} = colaboradoresApi;