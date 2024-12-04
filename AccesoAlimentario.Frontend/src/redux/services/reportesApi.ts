import {config} from "@config/config";
import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {IReporteResponse} from "@models/responses/reportes/iReporteResponse";
import {TipoReporte} from "@models/enums/tipoReporte";

export const reportesApi = createApi({
    reducerPath: "ReportesApi",
    baseQuery: fetchBaseQuery({baseUrl: config.apiUrl}),
    tagTypes: ["Reporte"],
    endpoints: (builder) => ({
        getReporte: builder.query<
            IReporteResponse,
            { tipoReporte: TipoReporte }
        >({
            query: ({tipoReporte}) => ({
                url: `reportes/${tipoReporte}`,
            }),
            providesTags: ["Reporte"]
        })
    }),
});

export const {
    useGetReporteQuery
} = reportesApi;