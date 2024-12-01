import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";
import {IDistribucionViandaRequest} from "@models/requests/contribuciones/iDistribucionViandaRequest";
import {IDonacionViandaRequest} from "@models/requests/contribuciones/iDonacionViandaRequest";
import {IDonacionHeladeraRequest} from "@models/requests/contribuciones/iDonacionHeladeraRequest";
import {IDonacionMonetariaRequest} from "@models/requests/contribuciones/iDonacionMonetariaRequest";
import {IRegistroPersonaVulnerableRequest} from "@models/requests/contribuciones/iRegistroPersonaVulnerableRequest";
import {ICanjeDePremioRequest} from "@models/requests/contribuciones/iCanjeDePremioRequest";
import {IOfertaPremioRequest} from "@models/requests/contribuciones/iOfertaPremioRequest";

export const contribucionesApi = createApi({
    reducerPath: "ContribucionesApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl}),
    tagTypes: ["PuntosContribuidor"],
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
            invalidatesTags: ["PuntosContribuidor"]
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
            invalidatesTags: ["PuntosContribuidor"]
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
            invalidatesTags: ["PuntosContribuidor"]
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
            invalidatesTags: ["PuntosContribuidor"]
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
            invalidatesTags: ["PuntosContribuidor"]
        }),
        postRegistroPersonaVulnerable: builder.mutation<
            void,
            IRegistroPersonaVulnerableRequest
        >({
            query: (data) => ({
                url: "contribuciones/registrovulnerable",
                method: "POST",
                body: data
            }),
            invalidatesTags: ["PuntosContribuidor"]
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
            invalidatesTags: ["PuntosContribuidor"]
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
        })
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
    useLazyGetPuntosContribuidorQuery
} = contribucionesApi;