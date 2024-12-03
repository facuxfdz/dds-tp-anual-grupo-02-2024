import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {ContribucionesTipo} from "@models/enums/contribucionesTipo";

export interface User {
    // Si es colaborador, el id es el del colaborador
    // Si es tecnico, el id es el del tecnico
    colaboradorId: string;
    tecnicoId: string;

    name: string;
    profile_picture: string;

    contribucionesPreferidas: ContribucionesTipo[];
    personaTipo: 'Humana' | 'Juridica';
}

const initialState: User = {
    colaboradorId: '1a5f2333-f5e4-4b77-a727-ebf880de41de',
    tecnicoId: '1a5f2333-f5e4-4b77-a727-ebf880de41de',

    name: 'Marcos Pedaci',
    profile_picture: '',

    contribucionesPreferidas: [],
    personaTipo: 'Humana',
};

const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        setUser: (state, action: PayloadAction<User>) => {
            state.colaboradorId = action.payload.colaboradorId;
            state.tecnicoId = action.payload.tecnicoId;

            state.name = action.payload.name;
            state.profile_picture = action.payload.profile_picture;

            state.contribucionesPreferidas = action.payload.contribucionesPreferidas;
            state.personaTipo = action.payload.personaTipo;
        },
        clearUser: (state) => {
            state.colaboradorId = '';
            state.tecnicoId = '';

            state.name = '';
            state.profile_picture = '';

            state.contribucionesPreferidas = [];
            state.personaTipo = 'Humana';
        },
    },
});

export const {setUser, clearUser} = userSlice.actions;
export default userSlice.reducer;
