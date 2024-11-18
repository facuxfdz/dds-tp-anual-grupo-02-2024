import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {config} from "@config/config";

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
        })
    }),
});

export const {
    useGetColaboradorQuery
} = colaboradoresApi;