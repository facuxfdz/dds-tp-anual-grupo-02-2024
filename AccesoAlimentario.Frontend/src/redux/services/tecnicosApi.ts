import {config} from "@config/config";
import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {IAltaTecnicoRequest} from "@models/requests/tecnicos/iAltaTecnicoRequest";

export const tecnicosApi = createApi({
    reducerPath: "TecnicosApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl}),
    tagTypes: ["Test"],
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
        }),
    }),
});

export const {
    usePostTecnicoMutation
} = tecnicosApi;