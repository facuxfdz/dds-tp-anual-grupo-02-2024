import {config} from "@config/config";
import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {IAltaTecnicoRequest} from "@models/requests/tecnicos/iAltaTecnicoRequest";
import {ITecnicoResponse} from "@models/responses/roles/iTecnicoResponse";

export const tecnicosApi = createApi({
    reducerPath: "TecnicosApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl}),
    tagTypes: ["Tecnicos"],
    endpoints: (builder) => ({
        postTecnico: builder.mutation<
            void,
            IAltaTecnicoRequest
        >({
            query: (body) => ({
                url: `tecnicos`,
                method: "POST",
                body,
            }),
            invalidatesTags: ["Tecnicos"],
        }),

        getTecnicos: builder.query<ITecnicoResponse[], void>({
            query: () => ({
                url: `tecnicos`,
                method: "GET",
            }),
            providesTags: ["Tecnicos"],
        }),

        deleteTecnico: builder.mutation<void, string>({
            query: (id) => ({
                url: `tecnicos/${id}`,
                method: "DELETE",
            }),
            invalidatesTags: ["Tecnicos"],
        })
    }),
});

export const {
    usePostTecnicoMutation,
    useGetTecnicosQuery,
    useDeleteTecnicoMutation
} = tecnicosApi;