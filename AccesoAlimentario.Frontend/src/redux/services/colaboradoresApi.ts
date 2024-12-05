import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";
import {IImportarColaboradoresCsvRequest} from "@models/requests/colaboradores/iImportarColaboradoresCsvRequest";
import {IReportarFallaTecnicaRequest} from "@models/requests/colaboradores/iReportarFallaTecnicaRequest";
import {ISuscribirseHeladeraRequest} from "@models/requests/colaboradores/iSuscribirseHeladeraRequest";
import {IAltaColaboradorRequest} from "@models/requests/colaboradores/iAltaColaboradorRequest";
import {ISuscripcionResponse} from "@models/responses/suscripcionesColaboradores/iSuscripcionResponse";
import {IAccesoHeladeraResponse} from "@models/responses/autorizaciones/iAccesoHeladeraResponse";
import {
    IAutorizacionManipulacionHeladeraResponse
} from "@models/responses/autorizaciones/iAutorizacionesManipulacionHeladeraResponse";
import {
    ISolicitarAutorizacionAperturaDeHeladeraRequest
} from "@models/requests/colaboradores/iSolicitarAutorizacionAperturaDeHeladeraRequest";
import {IPremioResponse} from "@models/responses/premios/iPremioResponse";
import {TipoRubro} from "@models/enums/tipoRubro";
import {IColaboradorResponse} from "@models/responses/roles/iColaboradorResponse";

export const colaboradoresApi = createApi({
    reducerPath: "ColaboradoresApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl}),
    tagTypes: ["Colaborador", "Suscripciones", "Accesos", "Colaboradores"],
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
        }),

        getAccesos: builder.query<
            { accesos: IAccesoHeladeraResponse[], autorizaciones: IAutorizacionManipulacionHeladeraResponse[] },
            string
        >({
            query: (id) => ({
                url: `colaboradores/${id}/accesos`,
                method: "GET",
            }),
            providesTags: ["Accesos"]
        }),

        postSolicitarAccesoHeladera: builder.mutation<
            void,
            ISolicitarAutorizacionAperturaDeHeladeraRequest
        >({
            query: (body) => ({
                url: `Heladeras/SolicitarAutorizacionAperturaDeHeladera`,
                method: "POST",
                body,
            }),
            invalidatesTags: ["Accesos"]
        }),

        getPremiosReclamados: builder.query<
            IPremioResponse[],
            { colaboradorId: string, nombre?: string, puntosNecesarios?: number, rubro?: TipoRubro }
        >({
            query: ({
                        colaboradorId: id,
                        nombre,
                        puntosNecesarios,
                        rubro
                    }) => ({
                url: `colaboradores/${id}/premiosCanjeados`,
                method: "GET",
                params: {
                    nombre, puntosNecesarios, rubro
                }
            }),
        }),

        getColaboradores: builder.query<
            IColaboradorResponse[],
            void
        >({
            query: () => {
                return {
                    url: `colaboradores`,
                    method: "GET"
                }
            },
            providesTags: ["Colaboradores"]
        }),

        deleteColaborador: builder.mutation<void, string>({
            query: (id) => ({
                url: `colaboradores/${id}`,
                method: "DELETE",
            }),
            invalidatesTags: ["Colaborador", "Colaboradores"]
        }),
    }),
});

export const {
    usePostImportarColaboradoresCsvMutation,
    usePostReportarFallaTecnicaMutation,
    usePostSuscribirseHeladeraMutation,
    usePostAltaColaboradorMutation,
    usePostDesuscribirseHeladeraMutation,
    useGetSuscripcionesQuery,
    useGetAccesosQuery,
    usePostSolicitarAccesoHeladeraMutation,
    useLazyGetPremiosReclamadosQuery,
    useGetColaboradoresQuery,
    useDeleteColaboradorMutation
} = colaboradoresApi;