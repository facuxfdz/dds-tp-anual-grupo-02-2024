import {config} from "@config/config";
import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";

export const reportesApi = createApi({
    reducerPath: "ReportesApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl}),
    tagTypes: ["Reporte"],
    endpoints: (builder) => ({
        getReporte: builder.query<
            void,
            { reporteId: string }
        >({
            query: ({reporteId}) => ({
                url: `reportes/${reporteId}`,
            }),
            providesTags: ["Reporte"]
        })
    }),
});

export const {
    useGetReporteQuery
} = reportesApi;