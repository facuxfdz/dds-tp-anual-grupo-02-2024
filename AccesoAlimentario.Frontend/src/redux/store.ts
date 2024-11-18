import {combineReducers, configureStore} from "@reduxjs/toolkit";
import {persistStore, persistReducer} from 'redux-persist';
import storageEngine from './storageEngine';

import themeSlice from "@redux/features/themeSlice";
import customizationSlice from "@redux/features/customizationSlice";
import sessionSlice from "@redux/features/sessionSlice";

import {colaboradoresApi} from "@redux/services/colaboradoresApi";
import {contribucionesApi} from "@redux/services/contribucionesApi";
import {reportesApi} from "@redux/services/reportesApi";
import {serviciosApi} from "@redux/services/serviciosApi";
import {tecnicosApi} from "@redux/services/tecnicosApi";
import {heladerasApi} from "@redux/services/heladerasApi";
import {loginApi} from "@redux/services/loginApi";

const persistConfig = {
    key: 'root',
    storage: storageEngine,
    whitelist: ['customization', 'theme', 'session'],
    timeout: 1000,
};

const rootReducer = combineReducers({
    // Slices
    theme: themeSlice,
    customization: customizationSlice,
    session: sessionSlice,

    // Services
    [colaboradoresApi.reducerPath]: colaboradoresApi.reducer,
    [contribucionesApi.reducerPath]: contribucionesApi.reducer,
    [reportesApi.reducerPath]: reportesApi.reducer,
    [serviciosApi.reducerPath]: serviciosApi.reducer,
    [tecnicosApi.reducerPath]: tecnicosApi.reducer,
    [heladerasApi.reducerPath]: heladerasApi.reducer,
    [loginApi.reducerPath]: loginApi.reducer,
});

const persistedReducer = persistReducer(persistConfig, rootReducer);

export const store = configureStore({
    reducer: persistedReducer,
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware({
                serializableCheck: {
                    ignoredActions: ['persist/PERSIST'],
                    ignoredPaths: ['register'],
                },
            },
        )
            .concat(
                colaboradoresApi.middleware,
                contribucionesApi.middleware,
                reportesApi.middleware,
                serviciosApi.middleware,
                tecnicosApi.middleware,
                heladerasApi.middleware,
                loginApi.middleware,
            ),
});

export const persistor = persistStore(store);

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
