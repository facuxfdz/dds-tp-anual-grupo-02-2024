import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";
import {IImportarColaboradoresCsvRequest} from "@models/requests/colaboradores/iImportarColaboradoresCsvRequest";
import {IReportarFallaTecnicaRequest} from "@models/requests/colaboradores/iReportarFallaTecnicaRequest";
import {ISuscribirseHeladeraRequest} from "@models/requests/colaboradores/iSuscribirseHeladeraRequest";

export const colaboradoresApi = createApi({
    reducerPath: "ColaboradoresApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl}),
    tagTypes: ["Colaborador"],
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
        })
    }),
});

export const {
    usePostImportarColaboradoresCsvMutation
} = colaboradoresApi;