import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";
import {IDistribucionViandaRequest} from "@models/requests/contribuciones/iDistribucionViandaRequest";
import {IDonacionViandaRequest} from "@models/requests/contribuciones/iDonacionViandaRequest";
import {IDonacionHeladeraRequest} from "@models/requests/contribuciones/iDonacionHeladeraRequest";
import {IDonacionMonetariaRequest} from "@models/requests/contribuciones/iDonacionMonetariaRequest";
import {IRegistroPersonaVulnerableRequest} from "@models/requests/contribuciones/iRegistroPersonaVulnerableRequest";
import {ICanjeDePremioRequest} from "@models/requests/contribuciones/iCanjeDePremioRequest";
import {IOfertaPremioRequest} from "@models/requests/contribuciones/iOfertaPremioRequest";
import {IFormaContribucionResponse} from "@models/responses/contribuciones/iFormaContribucionResponse";
import {TipoRubro} from "@models/enums/tipoRubro";
import {IPremioResponse} from "@models/responses/premios/iPremioResponse";
import {IActualizarHeladera} from "@models/requests/heladeras/iActualizarHeladera";

export const contribucionesApi = createApi({
    reducerPath: "ContribucionesApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl}),
    tagTypes: ["PuntosContribuidor", "Contribuciones", "Premios"],
    endpoints: (builder) => ({
        postDistribucionViandas: builder.mutation<
            void,
            IDistribucionViandaRequest
        >({
            query: (data) => ({
                url: "contribuciones/distribucionviandas",
                method: "POST",
                body: data
            }),
            invalidatesTags: ["PuntosContribuidor", "Contribuciones"]
        }),
        postDonacionHeladera: builder.mutation<
            void,
            IDonacionHeladeraRequest
        >({
            query: (data) => ({
                url: "contribuciones/donacionheladera",
                method: "POST",
                body: data
            }),
            invalidatesTags: ["PuntosContribuidor", "Contribuciones"]
        }),
        postDonacionVianda: builder.mutation<
            void,
            IDonacionViandaRequest
        >({
            query: (data) => ({
                url: "contribuciones/donacionvianda",
                method: "POST",
                body: data
            }),
            invalidatesTags: ["PuntosContribuidor", "Contribuciones"]
        }),
        postDonacionMonetaria: builder.mutation<
            void,
            IDonacionMonetariaRequest
        >({
            query: (data) => ({
                url: "contribuciones/donacionmonetaria",
                method: "POST",
                body: data
            }),
            invalidatesTags: ["PuntosContribuidor", "Contribuciones"]
        }),
        postOfertaPremio: builder.mutation<
            void,
            IOfertaPremioRequest
        >({
            query: (data) => ({
                url: "contribuciones/ofertapremio",
                method: "POST",
                body: data
            }),
            invalidatesTags: ["PuntosContribuidor", "Contribuciones", "Premios"]
        }),
        postRegistroPersonaVulnerable: builder.mutation<
            void,
            IRegistroPersonaVulnerableRequest
        >({
            query: (data) => ({
                url: "contribuciones/registroPersonaVulnerable",
                method: "POST",
                body: data
            }),
            invalidatesTags: ["PuntosContribuidor", "Contribuciones"]
        }),
        postCanjeDePremio: builder.mutation<
            void,
            ICanjeDePremioRequest
        >({
            query: (data) => ({
                url: "contribuciones/canjedepremio",
                method: "POST",
                body: data
            }),
            invalidatesTags: ["PuntosContribuidor", "Contribuciones", "Premios"]
        }),
        getPuntosContribuidor: builder.query<
            number,
            string
        >({
            query: (colaboradorId) => ({
                url: `colaboradores/${colaboradorId}/puntaje`,
                method: "GET"
            }),
            providesTags: ["PuntosContribuidor"]
        }),
        getContribuciones: builder.query<
            IFormaContribucionResponse[],
            string
        >({
            query: (colaboradorId) => ({
                url: "contribuciones/contribucionesColaborador",
                method: "GET",
                params: {
                    colaboradorId
                }
            }),
            providesTags: ["Contribuciones"]
        }),
        getPremios: builder.query<
            IPremioResponse[],
            {nombre?: string, puntosNecesarios?: number, rubro?: TipoRubro}
        >({
            query: (params) => ({
                url: "contribuciones/premios",
                method: "GET",
                params
            }),
            providesTags: ["Premios"]
        }),
        actualizarHeladera: builder.mutation<
            void,
            IActualizarHeladera
        >({
            query: (body) => ({
                url: "heladeras",
                method: "PUT",
                body
            }),
            invalidatesTags: ["Contribuciones"]
        }),
    }),
});

export const {
    usePostCanjeDePremioMutation,
    usePostDistribucionViandasMutation,
    usePostDonacionHeladeraMutation,
    usePostDonacionMonetariaMutation,
    usePostDonacionViandaMutation,
    usePostOfertaPremioMutation,
    usePostRegistroPersonaVulnerableMutation,
    useGetPuntosContribuidorQuery,
    useLazyGetPuntosContribuidorQuery,
    useGetContribucionesQuery,
    useLazyGetContribucionesQuery,
    useGetPremiosQuery,
    useLazyGetPremiosQuery,
    useActualizarHeladeraMutation
} = contribucionesApi;