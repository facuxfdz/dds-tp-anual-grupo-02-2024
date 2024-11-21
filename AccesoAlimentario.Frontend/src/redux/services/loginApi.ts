import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { config } from '@config/config';
import { setUser } from '../features/userSlice';

export const loginApi = createApi({
    reducerPath: 'LoginApi',
    baseQuery: fetchBaseQuery({ baseUrl: config.apiUrl }),
    tagTypes: ['Login'],
    endpoints: (builder) => ({
        login: builder.mutation<any, { token: string }>({
            query: ({ token }) => ({
                url: 'auth/login',
                method: 'POST',
                body: { token },
            }),
            // Handle success and update the state here (optional)
            onQueryStarted: async ({ token }, { dispatch, queryFulfilled }) => {
                try {
                    const result = await queryFulfilled;
                    // Handle login success logic here (e.g., dispatch setUser action)
                    dispatch(setUser(result.data.user));
                } catch (error) {
                    console.error('Error during login', error);
                }
            },
        }),
    }),
});

export const { useLoginMutation } = loginApi;
