import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";
import {IImportarColaboradoresCsvRequest} from "@models/requests/colaboradores/iImportarColaboradoresCsvRequest";

export const colaboradoresApi = createApi({
    reducerPath: "ColaboradoresApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl}),
    tagTypes: ["Colaborador"],
    endpoints: (builder) => ({
        getColaborador: builder.query<
            void,
            { colaboradorId: string }
        >({
            query: ({colaboradorId}) => ({
                url: `colaboradores/${colaboradorId}`,
            }),
            providesTags: ["Colaborador"]
        }),
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
            invalidatesTags: ["Colaborador"],

        })
    }),
});

export const {
    usePostImportarColaboradoresCsvMutation
} = colaboradoresApi;