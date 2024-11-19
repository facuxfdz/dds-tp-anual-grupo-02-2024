import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";
import {IDistribucionViandaRequest} from "@models/requests/contribuciones/iDistribucionViandaRequest";
import {IDonacionViandaRequest} from "@models/requests/contribuciones/iDonacionViandaRequest";
import {IDonacionHeladeraRequest} from "@models/requests/contribuciones/iDonacionHeladeraRequest";
import {IDonacionMonetariaRequest} from "@models/requests/contribuciones/iDonacionMonetariaRequest";
import {IRegistroPersonaVulnerableRequest} from "@models/requests/contribuciones/iRegistroPersonaVulnerableRequest";
import {ICanjeDePremioRequest} from "@models/requests/contribuciones/iCanjeDePremioRequest";

export const contribucionesApi = createApi({
    reducerPath: "ContribucionesApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl}),
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
        }),
        postOfertaPremio: builder.mutation<
            void,
            IDonacionMonetariaRequest
        >({
            query: (data) => ({
                url: "contribuciones/ofertapremio",
                method: "POST",
                body: data
            }),
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
    usePostRegistroPersonaVulnerableMutation
} = contribucionesApi;